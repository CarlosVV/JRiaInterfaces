using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Models.DTOs;
using CES.CoreApi.Receipt_Main.Repositories;
using System.Collections.Generic;

namespace CES.CoreApi.Receipt_Main.Services
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