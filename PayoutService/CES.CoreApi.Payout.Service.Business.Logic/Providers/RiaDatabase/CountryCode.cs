using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Payout.Service.Business.Logic.Exceptions;
using CES.CoreApi.Payout.Service.Business.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.RiaDatabase
{
    

    /// <summary>
    /// Holds a Country Code values.
    /// Allow translation from 2 char to 3 char codes.
    /// 
    /// Author  : David Go
    /// </summary>
    public class CountryCode
    {
        public const string US_CODE = "US";
        public const string USA_CODE = "USA";
        public const string PR_CODE = "PR"; //Puerto Rico
        public const string PRI_CODE = "PRI"; //Puerto Rico
        public const string CA_CODE = "CA"; //Canada
        public const string MX_CODE = "MX"; //Mexico

        const int SP_TYPE_LOOKUP_ABBREV = 0; //2 char code
        const int SP_TYPE_LOOKUP_ISO = 1; //3 char code

        const int CHARCODE_LENGTH2 = 2;
        const int CHARCODE_LENGTH3 = 3;
        string m_2char_CountryCode;
        string m_3charISO_CountryCode;
        int m_ISOCodeNumber;
        string m_countryDescription;

        private readonly IDataHelper m_dataHelper;


        /// <summary>
        /// PUBLIC CONSTRUCTOR:
        /// </summary>
        /// <param name="dataHelper"></param>
        public CountryCode(IDataHelper dataHelper)
        {
            m_dataHelper = dataHelper;
        }

        /// <summary>
        /// PRIVATE CONSTRUCTOR:
        /// In some cases the user has not filled in country information
        /// for the specific record.  For Example customer tax country could be empty.
        /// To allow for this create and empty countryCode 
        /// </summary>
        /// <returns></returns>
        private CountryCodeModel CreateEmptyCountryCode()
        {
            return new CountryCodeModel()
            {
                Char2CountryCode = string.Empty,
                Char3ISOCountryCode = string.Empty,
                ISONumericCode = 0,
                CountryDescription = string.Empty,
                ResultSetHasRows = false
            };
        }


        /// <summary>
        /// Create a new CountryCode from a 2 character
        /// country abbreviation.
        /// </summary>
        /// <param name="countrycode2"></param>
        /// <returns></returns>
        public CountryCodeModel CreateFrom2CharCode(string countrycode2, bool allowEmptyCode)
        {
            try
            {
                //If an empty code is sent in and it's OK to be empty, create an empty strings version:
                try
                {
                    //First trim any white space.  Sometimes a blank gets saved with " ".
                    countrycode2 = countrycode2.Trim();

                    if (countrycode2.Length < 1 && allowEmptyCode)
                    {
                        return CreateEmptyCountryCode();
                    }
                }
                catch (NullReferenceException)
                {
                    if (allowEmptyCode)
                    {
                        return CreateEmptyCountryCode();
                    }
                    else
                    {
                        countrycode2 = ""; //And let the process continue.  Will result in proper error below
                    }
                }

                //Make sure code is proper length and all letters.
                if (countrycode2.Length != CHARCODE_LENGTH2)
                {
                    throw new CountryCodeException(
                        Messages.S_GetMessage("ErrorCountryCodeLength2"));
                }
                for (int i = 0; i < countrycode2.Length; i++)
                {
                    if (!Char.IsLetter(countrycode2[i]))
                    {
                        throw new CountryCodeException(
                            Messages.S_GetMessage("ErrorCountryCodeLetters"));
                    }
                }
                //Create a Country Code object:
                //return new CountryCode(SP_TYPE_LOOKUP_ABBREV, countrycode2);

                return m_dataHelper.GetCountryCode(SP_TYPE_LOOKUP_ABBREV, countrycode2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Create a new CountryCode from a 3 character ISO
        /// country code.
        /// </summary>
        /// <param name="countrycode3"></param>
        /// <returns></returns>
        public CountryCodeModel CreateFrom3CharCode(string countrycode3, bool allowEmptyCode)
        {
            try
            {
                //If an empty code is sent in and it's OK to be empty, create an empty strings version:
                try
                {
                    //First trim any white space.  Sometimes a blank gets saved with " ".
                    countrycode3 = countrycode3.Trim();

                    if (countrycode3.Length < 1 && allowEmptyCode)
                    {
                        return CreateEmptyCountryCode();
                    }
                }
                catch (NullReferenceException)
                {
                    if (allowEmptyCode)
                    {
                        return CreateEmptyCountryCode();
                    }
                    else
                    {
                        countrycode3 = ""; //And let the process continue.  Will result in proper error below
                    }
                }

                //Make sure code is proper length and all letters.
                if (countrycode3.Length != CHARCODE_LENGTH3)
                {
                    throw new CountryCodeException(
                        Messages.S_GetMessage("ErrorCountryCodeLength3"));
                }
                for (int i = 0; i < countrycode3.Length; i++)
                {
                    if (!Char.IsLetter(countrycode3[i]))
                    {
                        throw new CountryCodeException(
                            Messages.S_GetMessage("ErrorCountryCodeLetters"));
                    }
                }
                //Create a Country Code object:
                //return new CountryCode(SP_TYPE_LOOKUP_ISO, countrycode3);
                return m_dataHelper.GetCountryCode(SP_TYPE_LOOKUP_ISO, countrycode3);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        


        public string Get2CharCountryCode()
        {
            return m_2char_CountryCode;
        }

        public string Get3CharISOCountryCode()
        {
            return m_3charISO_CountryCode;
        }

        public int GetISONumericCode()
        {
            return m_ISOCodeNumber;
        }

        public string GetCountryDescription()
        {
            return m_countryDescription;
        }

    }
}
