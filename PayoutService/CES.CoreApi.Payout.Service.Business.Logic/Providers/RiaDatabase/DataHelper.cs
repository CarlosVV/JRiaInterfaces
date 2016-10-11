using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Payout.Service.Business.Logic.Exceptions;
using CES.CoreApi.Payout.Service.Business.Logic.Utilities;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.RiaDatabase
{
    public class DataHelper : IDataHelper
    {
        #region core
        private const int STANDARD_ISO_CURRENCY_LENGTH = 3;
        private readonly IRiaRepository _repository;
        public DataHelper(IRiaRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region public methods
        public CurrencyCodeModel GetCurrencyCode(int isoCodeNum)
        {
            //Check for proper code value:
            if (isoCodeNum < 1)
            {
                throw new InvalidMoneyException(
                    Messages.S_GetMessage("ErrorMoneyCurrencyCode"));
            }

            return GetCurrencyCodeData(isoCodeNum, "");
        }
        public CurrencyCodeModel GetCurrencyCode(string isoCodeText)
        {
            //Check for proper code length:
            if (isoCodeText == null)
            {
                throw new InvalidMoneyException(
                    Messages.S_GetMessage("ErrorMoneyNullCurrency"));
            }

            if (isoCodeText.Length != STANDARD_ISO_CURRENCY_LENGTH)
            {
                throw new InvalidMoneyException(
                    Messages.S_GetMessage("ErrorMoneyCurrencyLength"));
            }
            return GetCurrencyCodeData(0, isoCodeText);
        }
        public CountryCodeModel GetCountryCode(int lookupType, string countryCode)
        {
            return GetCountryCodeData(lookupType, countryCode);
        }
        #endregion

        #region Private methods
        private CurrencyCodeModel GetCurrencyCodeData(int isoCodeNum, string isoCodeText)
        {
            var reponses = _repository.GetCurrencyCodeData(isoCodeNum, isoCodeText);

            return reponses;
        }
        private  CountryCodeModel GetCountryCodeData(int lookupType, string countryCode)
        {
            var reponses = _repository.GetCountryCodeData(lookupType, countryCode);

            return reponses;
        }
        #endregion
    }
}
