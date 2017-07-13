using CES.Caching;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public class StoreService : IStoreService
    {
        private IStoreRepository repo;
        private string _StorageFilename = $"{AppSettings.ApplicationBinPath}\\LocalStorage\\Stores.json";
        public StoreService(IStoreRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_TaxReceipt_Store> GetAllStores()
        {
            var key = $"GetAllStores{AppSettings.AppId}";
            Logging.Log.Info($"Get All Stores from Cache. Key: {key}");
            IEnumerable<systblApp_TaxReceipt_Store> data = null;

            if (AppSettings.IsWindowsDesktopApplication)
            {
                data = repo.find(c => !c.fDisabled.Value && !c.fDelete.Value);
                return data.ToList();
            }

            data = Cache.Get<IEnumerable<systblApp_TaxReceipt_Store>>(key, null);

            if (!data?.Any() == null)
            {
                Logging.Log.Info("Get All Stores from Cache Database.");
                if (!AppSettings.UseLocalStorageForStores)
                {
                    data = repo.find(c => !c.fDisabled.Value && !c.fDelete.Value);
                }
                else
                {

                    using (var reader = new StreamReader(_StorageFilename))
                    {
                        var storageContent = reader.ReadToEnd();
                        if (storageContent != null)
                        {
                            data = JsonConvert.DeserializeObject<List<systblApp_TaxReceipt_Store>>(storageContent);
                        }
                    }

                }

                Cache.Add(key, data, new TimeSpan(1, 0, 0));

                return data.ToList();
            }

            return data.ToList();
        }

        public void CreateStore(systblApp_TaxReceipt_Store objectEntry)
        {
            this.repo.CreateStore(objectEntry);

        }
        public void UpdateStore(systblApp_TaxReceipt_Store objectEntry)
        {
            this.repo.UpdateStore(objectEntry);

        }
        public void RemoveStore(systblApp_TaxReceipt_Store objectEntry)
        {
            this.repo.RemoveStore(objectEntry);

        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
