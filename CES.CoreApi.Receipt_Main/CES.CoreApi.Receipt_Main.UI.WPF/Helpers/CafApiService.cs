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
        public async Task<systblApp_CoreAPI_Caf> CreateCaf(string cafcontent)
        {
            systblApp_CoreAPI_Caf obj = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var caf = new ServiceTaxCreateCAFRequestViewModel();
                caf.CAFContent = HttpUtility.HtmlEncode(cafcontent);
                var jsonstring = JsonConvert.SerializeObject(caf);
                StringContent content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("ApplicationId", "1");
                client.DefaultRequestHeaders.Add("ces-appObjectId", "1");
                client.DefaultRequestHeaders.Add("ces-userId", "1");
                client.DefaultRequestHeaders.Add("ces-requestTime", DateTime.Now.ToShortDateString());
         
                HttpResponseMessage response = await client.PostAsync("receipt/tax/caf", content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ServiceTaxCreateCAFResponseViewModel>(data);
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
            }

            return obj;
        }

        public async Task<systblApp_CoreAPI_Caf> UpdateCaf(systblApp_CoreAPI_Caf obj)
        {
            //systblApp_CoreAPI_Caf obj = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var caf = new ServiceTaxUpdateCAFRequestViewModel();

                //TODO: Specify other fields like FolioStart and FolioEnd
                caf.Id =  $"{obj.Id}";
                caf.CAFContent = obj.FileContent;

                StringContent content = new StringContent(JsonConvert.SerializeObject(caf));
                HttpResponseMessage response = await client.PostAsync("receipt/tax/caf", content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ServiceTaxCreateCAFResponseViewModel>(data);
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
            }

            return obj;
        }
    }
}
