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
        public static readonly string _url = AppSettings.ApiReceiptServiceUrl;
        public systblApp_CoreAPI_Caf CreateCaf(string cafcontent, string documentType, int folioStartNumber, int folioEndNumber, int recAgent, bool disable = false)
        {
            systblApp_CoreAPI_Caf obj = null;
            using (var client = new HttpClient())
            {
                var caf = new ServiceTaxCreateCAFRequestViewModel();
                caf.DocumentType  = documentType;
                caf.CAFContent = HttpUtility.HtmlEncode(cafcontent);
                caf.FolioStartNumber = folioStartNumber;
                caf.FolioEndNumber = folioEndNumber;
                caf.RecAgent = recAgent;

                var jsonstring = JsonConvert.SerializeObject(caf);

                StringContent content = GetConfiguredContentCall(client, jsonstring);

                var taskPost = client.PostAsync("receipt/tax/caf", content);
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
                        obj = new systblApp_CoreAPI_Caf
                        {
                            Id = responseObject.CAF.Id,
                            CompanyLegalName = responseObject.CAF.CompanyLegalName,
                            CompanyRUT = responseObject.CAF.CompanyLegalName,
                            DocumentType = responseObject.CAF.DocumentType,
                            FolioStartNumber = responseObject.CAF.FolioStartNumber,
                            FolioEndNumber = responseObject.CAF.FolioEndNumber,
                            FolioCurrentNumber = responseObject.CAF.FolioCurrentNumber,
                            AuthorizationDate = responseObject.CAF.AuthorizationDate,
                            FileContent = responseObject.CAF.FileContent,
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

        public systblApp_CoreAPI_Caf UpdateCaf(int id, string cafcontent, string documentType, int folioStartNumber, int folioEndNumber, int recAgent, bool disabled = false)
        {
            systblApp_CoreAPI_Caf obj = null;
            using (var client = new HttpClient())
            {
                var caf = new ServiceTaxUpdateCAFRequestViewModel();
                caf.Id = id;
                caf.DocumentType = documentType;
                caf.CAFContent = HttpUtility.HtmlEncode(cafcontent);
                caf.FolioStartNumber = folioStartNumber;
                caf.FolioEndNumber = folioEndNumber;
                caf.RecAgent = recAgent;
                caf.Disabled = disabled;

                var jsonstring = JsonConvert.SerializeObject(caf);

                StringContent content = GetConfiguredContentCall(client, jsonstring);

                var taskPost = client.PutAsync("receipt/tax/caf", content);
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
                        obj = new systblApp_CoreAPI_Caf
                        {
                            Id = responseObject.CAF.Id,
                            CompanyLegalName = responseObject.CAF.CompanyLegalName,
                            CompanyRUT = responseObject.CAF.CompanyLegalName,
                            DocumentType = responseObject.CAF.DocumentType,
                            FolioStartNumber = responseObject.CAF.FolioStartNumber,
                            FolioEndNumber = responseObject.CAF.FolioEndNumber,
                            FolioCurrentNumber = responseObject.CAF.FolioCurrentNumber,
                            AuthorizationDate = responseObject.CAF.AuthorizationDate,
                            FileContent = responseObject.CAF.FileContent,
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

                var taskPost = client.PostAsync("receipt/tax/caf/delete", content);
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
                    }
                    else
                    {
                        throw new Exception($"{responseObject.ReturnInfo.ErrorMessage}");
                    }

                }
            }
        }
        public List<systblApp_CoreAPI_Caf> SearchCafs(int id, string documentType, int folioStartNumber, int folioEndNumber, int folioCurrentNumber, int recAgent)
        {
            List<systblApp_CoreAPI_Caf> objList = null;
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

                var taskPost = client.PostAsync("/receipt/tax/caf/search", content);
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
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("ApplicationId", "1");
            client.DefaultRequestHeaders.Add("ces-appObjectId", "1");
            client.DefaultRequestHeaders.Add("ces-userId", "1");
            client.DefaultRequestHeaders.Add("ces-requestTime", DateTime.Now.ToShortDateString());
            return content;
        }
    }
}
