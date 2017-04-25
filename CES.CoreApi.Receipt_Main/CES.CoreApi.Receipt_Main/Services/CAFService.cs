using CES.CoreApi.Receipt_Main.CAFUtilities;
using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Repositories;
using CES.CoreApi.Receipt_Main.Validators;
using System;
using System.IO;
using System.Linq;
using System.Web;
using CES.CoreApi.Receipt_Main.Model.Services;


namespace CES.CoreApi.Receipt_Main.Services
{
    public class CAFService
    {
        private ICafService _cafdomain;
        private CAFRepository _repository = null;
        private CAFValidator _validator = null;
        private CAFParser _parser = null;
        public bool Successful = false;
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public CAFService(ICafService cafdomain)
        {
            _repository = new CAFRepository();
            _validator = new CAFValidator();
            _parser = new CAFParser();
            _cafdomain = cafdomain;
        }
        public TaxCreateCAFResponse CreateCAF(TaxCreateCAFRequest request)
        {
            var response = new TaxCreateCAFResponse();
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
                var objDbCaf = CreateCAFModel(Guid.NewGuid(), xml, objCAF);

                _cafdomain.CreateCaf(objDbCaf);

                response.CAF = objDbCaf;
                response.ProcessResult = true;
                Successful = true;
            }
            catch (Exception ex)
            {
                Logging.Log.Error($"ERROR: Inserting CAF {ex}");
                ErrorCode = "56";
                ErrorMessage = ex.Message;
                response.ReturnInfo = new ReturnInfo() {ErrorCode = 56,  ErrorMessage = ErrorMessage, ResultProcess = false };                
            }

            return response;
        }

        internal TaxSearchCAFByTypeResponse SearchCaf(TaxSearchCAFByTypeRequest request)
        {
            var response = new TaxSearchCAFByTypeResponse();
            var results =_repository.Get(request.Id, request.DocumentType, request.FolioCurrentNumber, request.FolioStartNumber, request.FolioEndNumber);

            if (results != null)
            {
                //response.Results = results.ToList();
                response.ProcessResult = true;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 1, ErrorMessage = null, ResultProcess = true };
            }
            
            return response;
        }

        internal object UpdateCAF(TaxUpdateCAFRequest request)
        {
            var response = new TaxUpdateCAFResponse();
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
                var objDbCaf = CreateCAFModel(Guid.Parse(request.Id), xml, objCAF);

                var result = _repository.Update(objDbCaf);

                response.ReturnInfo = new ReturnInfo() { ErrorCode = 56, ErrorMessage = ErrorMessage, ResultProcess = result };
                response.CAF = objDbCaf;
                response.ProcessResult = result;
                Successful = result;
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
            var response = new TaxUpdateCAFResponse();
            try
            {               
                Logging.Log.Info("Deleting CAF in DB");
                var result = _repository.Delete(request.Id);
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 56, ErrorMessage = ErrorMessage, ResultProcess = result };               
                response.ProcessResult = result;
                Successful = result;
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
                var result = _repository.Get(request.Id, request.DocumentType, request.FolioCurrentNumber, null, null);

                if (result == null && result.Count() == 0)
                {
                    Logging.Log.Error($"ERROR: Not results found to data entry");
                    response.ReturnInfo = new ReturnInfo() { ErrorCode = 59, ErrorMessage = "Not results found to data entry", ResultProcess = false };
                    Successful = false;
                    return response;
                }

                //Obtener el Ultimo Folio con el criterios ingresado
                var objDbCAF = result.OrderByDescending(m=> m.DateAuthorization).FirstOrDefault();
                var objXmlCAF = _parser.GetCAFObjectFromString(objDbCAF.FileContent);

                response.NextFolioNumber = objDbCAF.FolioCurrentNumber.Value + 1;
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

                var results = _repository.Get(request.Id, request.DocumentType, folioCurrentNumber, null, null);

                if (results == null && results.Count() == 0)
                {
                    Logging.Log.Error($"ERROR: Cannot update current folio number");
                    response.ReturnInfo = new ReturnInfo() { ErrorCode = 59, ErrorMessage = "Cannot update current folio number: Not results found to data entry", ResultProcess = false };
                    Successful = false;
                    return response;
                }

                var objDbCaf = results.OrderByDescending(m => m.DateAuthorization).FirstOrDefault();
                var objXmlCAF = _parser.GetCAFObjectFromString(objDbCaf.FileContent);               

                objDbCaf.FolioCurrentNumber = request.NextFolioNumber.Value;

                var result = _repository.Update(objDbCaf);              
                Successful = result;
                response.ReturnInfo = new ReturnInfo() { ErrorCode = 1, ErrorMessage = "Process done", ResultProcess = result };
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
        
        private Caf CreateCAFModel(Guid? id, string xml, AUTORIZACION objCAF)
        {
            return new Caf
            {
                Id = id,
                CompanyTaxId = objCAF.CAF.DA.RE,
                CompanyLegalName = objCAF.CAF.DA.RS,
                DateAuthorization = objCAF.CAF.DA.FA.ToShortDateString(),
                DocumentType = objCAF.CAF.DA.TD,
                FolioStartNumber = objCAF.CAF.DA.RNG.D,
                FolioEndNumber = objCAF.CAF.DA.RNG.H,
                FileContent = xml
            };
        }

    }
}