using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
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
