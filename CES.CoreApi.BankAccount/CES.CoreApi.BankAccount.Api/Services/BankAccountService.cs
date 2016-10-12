using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Models.DTOs;
using CES.CoreApi.BankAccount.Api.Repositories;
using CES.CoreApi.BankAccount.Api.Utilities;
using CES.CoreApi.BankAccount.Service.Business.Logic.Processors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CES.CoreApi.BankAccount.Api.Services
{
    public class BankAccountService
    {
        private BankAccountValidationProcessor _processor;
        public BankAccountService()
        {
            _processor = new BankAccountValidationProcessor();
        }

        internal ValidateBankAccountInfo ValidateBankDeposit(ValidateBankDepositRequest bankDepositRequest)
        {
            return _processor.ValidateBankDeposit(bankDepositRequest);
        }
    }
}