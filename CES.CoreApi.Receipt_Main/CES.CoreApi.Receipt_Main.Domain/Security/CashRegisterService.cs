using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Security;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class CashRegisterService : ICashRegisterService
    {
        private ICashRegisterRepository repo;
        public CashRegisterService(ICashRegisterRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_TaxReceipt_CashRegister> GetAllCashRegisters()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateCashRegister(systblApp_TaxReceipt_CashRegister objectEntry)
        {
            this.repo.CreateCashRegister(objectEntry);
       
        }
        public void UpdateCashRegister(systblApp_TaxReceipt_CashRegister objectEntry)
        {
            this.repo.UpdateCashRegister(objectEntry);

        }
        public void RemoveCashRegister(systblApp_TaxReceipt_CashRegister objectEntry)
        {
            this.repo.RemoveCashRegister(objectEntry);

        }
        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
