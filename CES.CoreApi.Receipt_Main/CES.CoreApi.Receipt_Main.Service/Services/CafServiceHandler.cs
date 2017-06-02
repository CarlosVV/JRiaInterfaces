using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Service.Repositories;
using CES.CoreApi.Receipt_Main.Service.Validators;
using System;
using System.IO;
using System.Linq;
using System.Web;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.CAFUtilities;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;

namespace CES.CoreApi.Receipt_Main.Service.Services
{
    public class CafServiceHandler
    {
        private ICafService _cafdomain;
        private CafValidator _validator = null;
        private CafParser _parser = null;
        public bool Successful = false;
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public CafServiceHandler(ICafService cafdomain)
        {
            _validator = new CafValidator();
            _parser = new CafParser();
            _cafdomain = cafdomain;
        }
        public TaxCreateCafFResponse CreateCAF(TaxCreateCafRequest request)
        {
            var response = new TaxCreateCafFResponse();
            try
            {
                var xmlEncoded = request.CAFContent;
                var xmlWriter = new StringWriter();
                var xml = string.Empty;

                HttpUtility.HtmlDecode(xmlEncoded, xmlWriter);
                xml = xmlWriter.ToString();

                Logging.Log.Info("Validating XML CAF  ");

                if (!_validator.Validate(xml))
                {
                    ErrorCode = "55";
                    ErrorMessage = $"CAF is no valid: {_validator.Error}";
                    response.ReturnInfo = new ReturnInfo() { ErrorCode = 56, ErrorMessage = ErrorMessage, ResultProcess = false };
                    return response;
                }

                Logging.Log.Info("Getting Object CAF ");
                var objCAF = _parser.GetCAFObjectFromString(xml);


                Logging.Log.Info("Insert into DB");
                var objDbCaf = CreateCAFModel(request.FolioStartNumber, request.FolioEndNumber, 0, request.RecAgent, xml, objCAF);

                if (ValidateCaf(objDbCaf))
                {
                    Logging.Log.Info($"Insertion Begin CAF");

                    _cafdomain.CreateCaf(objDbCaf);
                    _cafdomain.SaveChanges();

                    response.CAF = objDbCaf;
                    response.ProcessResult = true;
                    Successful = true;
                    Logging.Log.Info($"Insertion End CAF");
                }
                else
                {
                    Logging.Log.Error($"ERROR: Duplicated CAF");
                    ErrorCode = "55";
                    ErrorMessage = "Duplicated CAF";
                    response.ProcessResult = false;
                    Successful = false;
                    response.ReturnInfo = new ReturnInfo() { ErrorCode = 55, ErrorMessage = ErrorMessage, ResultProcess = false };
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Error($"ERROR: Inserting CAF {ex}");
                ErrorCode = "56";
                ErrorMessage = ex.Message;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 56, ErrorMessage = ErrorMessage, ResultProcess = false };
            }

            return response;
        }

        internal TaxSearchCAFByTypeResponse SearchCaf(TaxSearchCAFByTypeRequest request)
        {
            var response = new TaxSearchCAFByTypeResponse();
            var results = from item in  _cafdomain.GetAllCafs() where
                          (request.Id == 0 || request.Id == item.Id) &&
                          (string.IsNullOrWhiteSpace(request.DocumentType) || request.DocumentType == "0" || request.DocumentType == item.DocumentType) &&
                          (!request.FolioStartNumber.HasValue  || request.FolioStartNumber.Value == 0 || request.FolioStartNumber.Value == item.FolioStartNumber) &&
                          (!request.FolioEndNumber.HasValue || request.FolioEndNumber.Value == 0 || request.FolioEndNumber.Value == item.FolioEndNumber) &&
                          (!request.FolioCurrentNumber.HasValue || request.FolioCurrentNumber.Value == 0 || request.FolioCurrentNumber.Value == item.FolioCurrentNumber) &&
                           (!request.RecAgent.HasValue || request.RecAgent.Value == 0 || request.RecAgent.Value == item.RecAgent) 
                          select item;

            if (results != null)
            {
                response.Results = results.ToList();
                response.ProcessResult = true;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 1, ErrorMessage = null, ResultProcess = true };
            }

            return response;
        }

        internal object UpdateCAF(TaxUpdateCafRequest request)
        {
            var response = new TaxUpdateCafResponse();
            try
            {
                var xmlEncoded = request.CAFContent;
                var xmlWriter = new StringWriter();
                var xml = string.Empty;

                HttpUtility.HtmlDecode(xmlEncoded, xmlWriter);
                xml = xmlWriter.ToString();

                Logging.Log.Info("Validating XML CAF  ");

                if (!_validator.Validate(xml))
                {
                    ErrorCode = "55";
                    ErrorMessage = $"CAF is no valid: {_validator.Error}";
                    response.ReturnInfo = new ReturnInfo() { ErrorCode = 56, ErrorMessage = ErrorMessage, ResultProcess = false };
                    return response;
                }

                Logging.Log.Info("Getting Object CAF ");
                var objCAF = _parser.GetCAFObjectFromString(xml);

                Logging.Log.Info("Insert into DB");
                var objDbCaf = CreateCAFModel(request.FolioStartNumber, request.FolioEndNumber, request.FolioCurrentNumber, request.RecAgent, xml, objCAF, request.Id);

                _cafdomain.UpdateCaf(objDbCaf);
                _cafdomain.SaveChanges();

                response.ReturnInfo = new ReturnInfo() { ErrorCode = 56, ErrorMessage = ErrorMessage, ResultProcess = true };
                response.CAF = objDbCaf;
                response.ProcessResult = true;
                Successful = true;
            }
            catch (Exception ex)
            {
                Logging.Log.Error($"ERROR: Updating CAF {ex}");
                ErrorCode = "56";
                ErrorMessage = ex.Message;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 56, ErrorMessage = ErrorMessage, ResultProcess = false };
            }

            return response;
        }

        internal object DeleteCAF(TaxDeleteCAFRequest request)
        {
            var response = new TaxUpdateCafResponse();
            try
            {
                Logging.Log.Info("Deleting CAF in DB");
                var caf = _cafdomain.GetAllCafs().Where(m => m.Id == request.Id).First();
                _cafdomain.RemoveCaf(caf);
                _cafdomain.SaveChanges();
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 56, ErrorMessage = ErrorMessage, ResultProcess = true };
                response.ProcessResult = true;
                Successful = true;
            }
            catch (Exception ex)
            {
                Logging.Log.Error($"ERROR: Deleting CAF {ex}");
                ErrorCode = "56";
                ErrorMessage = ex.Message;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 56, ErrorMessage = ErrorMessage, ResultProcess = false };
            }

            return response;
        }

        internal object GetFolio(TaxGetFolioRequest request)
        {
            var response = new TaxGetFolioResponse();
            try
            {
                Logging.Log.Info("Getting Folio in DB");

                var result = _cafdomain.GetAllCafs().Where(m =>
                       request.Id == m.Id &&
                       request.DocumentType == m.DocumentType &&
                       request.FolioCurrentNumber == m.FolioCurrentNumber
                );

                if (result == null && result.Count() == 0)
                {
                    Logging.Log.Error($"ERROR: Not results found to data entry");
                    response.ReturnInfo = new ReturnInfo() { ErrorCode = 59, ErrorMessage = "Not results found to data entry", ResultProcess = false };
                    Successful = false;
                    return response;
                }

                //Obtener el Ultimo Folio con el criterios ingresado
                var objDbCAF = result.OrderByDescending(m => m.AuthorizationDate).FirstOrDefault();
                var objXmlCAF = _parser.GetCAFObjectFromString(objDbCAF.FileContent);

                response.NextFolioNumber = objDbCAF.FolioCurrentNumber + 1;
                response.CAF = objDbCAF;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 1, ErrorMessage = "Process done", ResultProcess = true };
                response.ResponseTime = DateTime.Today;
                response.ResponseTime = DateTime.UtcNow;
                response.TransferDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logging.Log.Error($"ERROR: Getting Folio CAF {ex}");
                ErrorCode = "56";
                ErrorMessage = ex.Message;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 60, ErrorMessage = ErrorMessage, ResultProcess = false };
            }

            return response;
        }

        internal object UpdateFolio(TaxUpdateFolioRequest request)
        {
            var response = new TaxUpdateFolioResponse();
            try
            {
                Logging.Log.Info("Getting Folio in DB");
                var folioCurrentNumber = request.NextFolioNumber - 1;

                if (folioCurrentNumber < 0)
                {
                    Logging.Log.Error($"ERROR: Next Folio Number is not valid: {request.NextFolioNumber}");
                    response.ReturnInfo = new ReturnInfo() { ErrorCode = 59, ErrorMessage = $"Next Folio Number is not valid: {request.NextFolioNumber}", ResultProcess = false };
                    Successful = false;
                    return response;
                }

                var results = _cafdomain.GetAllCafs().Where(m => m.Id == request.Id && m.DocumentType == request.DocumentType && m.FolioCurrentNumber == folioCurrentNumber); //m. request.DocumentType, folioCurrentNumber, null, null);

                if (results == null && results.Count() == 0)
                {
                    Logging.Log.Error($"ERROR: Cannot update current folio number");
                    response.ReturnInfo = new ReturnInfo() { ErrorCode = 59, ErrorMessage = "Cannot update current folio number: Not results found to data entry", ResultProcess = false };
                    Successful = false;
                    return response;
                }

                var objDbCaf = results.OrderByDescending(m => m.AuthorizationDate).FirstOrDefault();
                var objXmlCAF = _parser.GetCAFObjectFromString(objDbCaf.FileContent);

                objDbCaf.FolioCurrentNumber = request.NextFolioNumber.Value;

                _cafdomain.UpdateCaf(objDbCaf);
                Successful = true;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 1, ErrorMessage = "Process done", ResultProcess = true };
                response.ResponseTime = DateTime.Today;
                response.ResponseTime = DateTime.UtcNow;
                response.TransferDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logging.Log.Error($"ERROR: Updating Folio CAF {ex}");
                ErrorCode = "56";
                ErrorMessage = ex.Message;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 61, ErrorMessage = ErrorMessage, ResultProcess = false };
            }

            return response;
        }
        private bool ValidateCaf(systblApp_CoreAPI_Caf objCaf)
        {
            var query = _cafdomain.GetAllCafs();
            if (query != null && query.Count() > 0)
            {
                var search = query.Where(m =>
                m.FolioStartNumber == objCaf.FolioStartNumber &&
                m.FolioEndNumber == objCaf.FolioEndNumber &&
                m.DocumentType == objCaf.DocumentType &&
                m.RecAgent == objCaf.RecAgent).FirstOrDefault();

                if (search != null)
                {
                    return false;
                }
            }
            return true;
        }
        private systblApp_CoreAPI_Caf CreateCAFModel(int? foliostartnumber, int? folioendnumber, int? foliocurrentnumber, int? recAgent, string xml, AUTORIZACION objCAF, int id = 0)
        {
            return new systblApp_CoreAPI_Caf
            {
                Id = id,
                CompanyRUT = objCAF.CAF.DA.RE,
                CompanyLegalName = objCAF.CAF.DA.RS,
                AuthorizationDate = objCAF.CAF.DA.FA,
                DocumentType = objCAF.CAF.DA.TD.ToString(),
                FolioStartNumber = foliostartnumber == null ? objCAF.CAF.DA.RNG.D : foliostartnumber.Value,
                FolioEndNumber = folioendnumber == null ? objCAF.CAF.DA.RNG.H : folioendnumber.Value,
                FolioCurrentNumber = foliocurrentnumber == null ? 0 : foliocurrentnumber.Value,
                RecAgent = recAgent == null ? 0 : recAgent.Value,
                FileContent = xml
            };
        }

    }
}