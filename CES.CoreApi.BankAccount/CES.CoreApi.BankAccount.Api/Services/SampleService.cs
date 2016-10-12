using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Models.DTOs;
using CES.CoreApi.BankAccount.Api.Repositories;
using System.Collections.Generic;

namespace CES.CoreApi.BankAccount.Api.Services
{
    /// <summary>
    /// Sample service class please replace it or remove it.
    ///	Use Service class to implement:
    ///  1)	Business logic

    /// </summary>
    public class SampleService
    {
        private SampleRepositoryCached _repository;

        public SampleService()
        {
            _repository = new SampleRepositoryCached();
        }

        internal IEnumerable<CurrencyCountry> GetServiceOfferdCurrencies(CountryCurrencyRequest countryCurrencyRequest)
        {
            return _repository.GetServiceOfferdCurrencies(countryCurrencyRequest);
        }
    }
}