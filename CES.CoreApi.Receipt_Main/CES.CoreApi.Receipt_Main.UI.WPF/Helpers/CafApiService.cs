using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.UI.WPF.Model.CafApiService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Helpers
{
    public class CafApiService
    {
        private const string URL = "http://localhost:48712/";

        public async Task<systblApp_CoreAPI_Caf> CreateCaf(string cafcontent)
        {
            systblApp_CoreAPI_Caf obj = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var caf = new ServiceTaxCreateCAFRequestViewModel();
                caf.CAFContent = cafcontent;
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

        public async Task<systblApp_CoreAPI_Caf> UpdateCaf(systblApp_CoreAPI_Caf obj)
        {
            //systblApp_CoreAPI_Caf obj = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
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
