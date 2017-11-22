using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.UI.WPF.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Helpers
{
    public class CafApiService
    {
        private const string application_type = "application/json";
        private const string application_id = "1";
        private const string application_object_id = "1";
        private const string application_user_id = "1";
        private const string _url_search = "/receipt/tax/caf/search";
        private const string _url_caf_new_update = "receipt/tax/caf";
        private const string _url_caf_delete = "receipt/tax/caf/delete";
        public static readonly string _url = AppSettings.ApiReceiptServiceUrl;
        public actblTaxDocument_AuthCode CreateCaf(string cafcontent, string documentType, int folioStartNumber, int folioEndNumber, int folioCurrentNumber, int recAgent, bool disable = false)
        {
            actblTaxDocument_AuthCode obj = null;
            using (var client = new HttpClient())
            {
                var caf = new ServiceTaxCreateCAFRequestViewModel();
                caf.DocumentType  = documentType;
                caf.CAFContent = HttpUtility.HtmlEncode(cafcontent);
                caf.FolioStartNumber = folioStartNumber;
                caf.FolioEndNumber = folioEndNumber;
                caf.FolioCurrentNumber = folioCurrentNumber;
                caf.RecAgent = recAgent;

                var jsonstring = JsonConvert.SerializeObject(caf);

                StringContent content = GetConfiguredContentCall(client, jsonstring);

                var taskPost = client.PostAsync(_url_caf_new_update, content);
                taskPost.Wait();
                var response = taskPost.Result;
                if (response.IsSuccessStatusCode)
                {
                    var taskresponse = response.Content.ReadAsStringAsync();
                    taskresponse.Wait();
                    var data = taskresponse.Result;

                    var responseObject = JsonConvert.DeserializeObject<ServiceTaxCreateCAFResponseViewModel>(data);

                    if (responseObject.ProcessResult)
                    {
                        obj = new actblTaxDocument_AuthCode
                        {
                            fAuthCodeID = responseObject.CAF.fAuthCodeID,
                            fCompanyLegalName = responseObject.CAF.fCompanyLegalName,
                            fCompanyTaxID = responseObject.CAF.fCompanyLegalName,
                            fDocumentTypeID = responseObject.CAF.fDocumentTypeID,
                            fStartNumber = responseObject.CAF.fStartNumber,
                            fEndNumber = responseObject.CAF.fEndNumber,
                            fCurrentNumber = responseObject.CAF.fCurrentNumber,
                            fAuthorizationDate = responseObject.CAF.fAuthorizationDate,
                            fFileContent = responseObject.CAF.fFileContent,
                            fChanged = responseObject.CAF.fChanged,
                            fDelete = responseObject.CAF.fDelete,
                            fDisabled = responseObject.CAF.fDisabled,
                            fModified = responseObject.CAF.fModified,
                            fModifiedID = responseObject.CAF.fModifiedID,
                            fTime = responseObject.CAF.fTime,
                        };
                    }
                    else
                    {
                        throw new Exception($"{responseObject.ReturnInfo.ErrorMessage}");
                    }

                }
            }

            return obj;
        }

        public actblTaxDocument_AuthCode UpdateCaf(int id, string cafcontent, string documentType, int folioStartNumber, int folioEndNumber, int folioCurrentNumber, int recAgent, bool disabled = false)
        {
            actblTaxDocument_AuthCode obj = null;
            using (var client = new HttpClient())
            {
                var caf = new ServiceTaxUpdateCAFRequestViewModel();
                caf.Id = id;
                caf.DocumentType = documentType;
                caf.CAFContent = HttpUtility.HtmlEncode(cafcontent);
                caf.FolioStartNumber = folioStartNumber;
                caf.FolioEndNumber = folioEndNumber;
                caf.FolioCurrentNumber = folioCurrentNumber;
                caf.RecAgent = recAgent;
                caf.Disabled = disabled;

                var jsonstring = JsonConvert.SerializeObject(caf);

                StringContent content = GetConfiguredContentCall(client, jsonstring);

                var taskPost = client.PutAsync(_url_caf_new_update, content);
                taskPost.Wait();
                var response = taskPost.Result;

                if (response.IsSuccessStatusCode)
                {
                    var taskresponse = response.Content.ReadAsStringAsync();
                    taskresponse.Wait();
                    var data = taskresponse.Result;

                    var responseObject = JsonConvert.DeserializeObject<ServiceTaxUpdateCAFResponseViewModel>(data);

                    if (responseObject.ProcessResult)
                    {
                        obj = new actblTaxDocument_AuthCode
                        {
                            fAuthCodeID = responseObject.CAF.fAuthCodeID,
                            fCompanyLegalName = responseObject.CAF.fCompanyLegalName,
                            fCompanyTaxID = responseObject.CAF.fCompanyLegalName,
                            fDocumentTypeID = responseObject.CAF.fDocumentTypeID,
                            fStartNumber = responseObject.CAF.fStartNumber,
                            fEndNumber = responseObject.CAF.fEndNumber,
                            fCurrentNumber = responseObject.CAF.fCurrentNumber,
                            fAuthorizationDate = responseObject.CAF.fAuthorizationDate,
                            fFileContent = responseObject.CAF.fFileContent,
                            fChanged = responseObject.CAF.fChanged,
                            fDelete = responseObject.CAF.fDelete,
                            fDisabled = responseObject.CAF.fDisabled,
                            fModified = responseObject.CAF.fModified,
                            fModifiedID = responseObject.CAF.fModifiedID,
                            fTime = responseObject.CAF.fTime,
                        };
                    }
                    else
                    {
                        throw new Exception($"{responseObject.ReturnInfo.ErrorMessage}");
                    }

                }
            }

            return obj;
        }
        public void DeleteCaf(int id)
        {
            using (var client = new HttpClient())
            {
                var caf = new ServiceTaxDeleteCAFRequestViewModel();
                caf.Id = id;
              
                var jsonstring = JsonConvert.SerializeObject(caf);

                StringContent content = GetConfiguredContentCall(client, jsonstring);

                var taskPost = client.PostAsync(_url_caf_delete, content);
                taskPost.Wait();
                var response = taskPost.Result;

                if (response.IsSuccessStatusCode)
                {
                    var taskresponse = response.Content.ReadAsStringAsync();
                    taskresponse.Wait();
                    var data = taskresponse.Result;

                    var responseObject = JsonConvert.DeserializeObject<ServiceTaxDeleteCAFResponseViewModel>(data);

                    if (!responseObject.ProcessResult)
                    {                    
                        throw new Exception($"{responseObject.ReturnInfo.ErrorMessage}");
                    }

                }
            }
        }
        public List<actblTaxDocument_AuthCode> SearchCafs(int id, string documentType, int folioStartNumber, int folioEndNumber, int folioCurrentNumber, int recAgent)
        {
            List<actblTaxDocument_AuthCode> objList = null;
            using (var client = new HttpClient())
            {
                var cafSearchRequest = new ServiceTaxSearchCAFByTypeRequestViewModel();
                cafSearchRequest.Id = id;
                cafSearchRequest.DocumentType = documentType;               
                cafSearchRequest.FolioStartNumber = folioStartNumber;
                cafSearchRequest.FolioEndNumber = folioEndNumber;
                cafSearchRequest.FolioCurrentNumber = folioCurrentNumber;
                cafSearchRequest.RecAgent = recAgent;

                var jsonstring = JsonConvert.SerializeObject(cafSearchRequest);

                StringContent content = GetConfiguredContentCall(client, jsonstring);

                var taskPost = client.PostAsync(_url_search, content);
                taskPost.Wait();
                var response = taskPost.Result;

                if (response.IsSuccessStatusCode)
                {
                    var taskresponse = response.Content.ReadAsStringAsync();
                    taskresponse.Wait();
                    var data = taskresponse.Result;

                    var responseObject = JsonConvert.DeserializeObject<ServiceTaxSearchCAFByTypeResponseViewModel>(data);

                    if (responseObject.ProcessResult)
                    {
                        objList = responseObject.Results;                       
                    }
                    else
                    {
                        throw new Exception(responseObject.ReturnInfo.ErrorMessage);
                    }                 
                }
            }

            return objList;
        }
        private StringContent GetConfiguredContentCall(HttpClient client, string jsonstring)
        {
            StringContent content = new StringContent(jsonstring, Encoding.UTF8, application_type);
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(application_type));            
            client.DefaultRequestHeaders.Add("ApplicationId", application_id);
            client.DefaultRequestHeaders.Add("ces-appObjectId", application_object_id);
            client.DefaultRequestHeaders.Add("ces-userId", application_user_id);
            client.DefaultRequestHeaders.Add("ces-requestTime", DateTime.Now.ToShortDateString());
            return content;
        }
    }
}
