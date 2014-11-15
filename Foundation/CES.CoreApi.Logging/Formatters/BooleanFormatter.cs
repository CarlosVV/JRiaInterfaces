using System;
using System.Globalization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
    public class BooleanFormatter : IBooleanFormatter
    {
        #region Public methods

        /// <summary>
        /// Formats input string as Yes/No
        /// </summary>
        public string Format(string inputValue)
        {
            if (string.IsNullOrEmpty(inputValue)) 
                return string.Empty;

            if (inputValue.Equals(CommonConstants.StringBoolean.CaptionY, StringComparison.OrdinalIgnoreCase) ||
                inputValue.Equals(CommonConstants.StringBoolean.CaptionYes, StringComparison.OrdinalIgnoreCase) ||
                inputValue.Equals(CommonConstants.StringBoolean.StringTrue, StringComparison.OrdinalIgnoreCase) ||
                inputValue.Equals(CommonConstants.StringBoolean.StringOne, StringComparison.OrdinalIgnoreCase))
                return CommonConstants.StringBoolean.CaptionYes;

            if (inputValue.Equals(CommonConstants.StringBoolean.CaptionN, StringComparison.OrdinalIgnoreCase) ||
                inputValue.Equals(CommonConstants.StringBoolean.CaptionNo, StringComparison.OrdinalIgnoreCase) ||
                inputValue.Equals(CommonConstants.StringBoolean.StringFalse, StringComparison.OrdinalIgnoreCase) ||
                inputValue.Equals(CommonConstants.StringBoolean.StringZero, StringComparison.OrdinalIgnoreCase))
                return CommonConstants.StringBoolean.CaptionNo;

            throw new ApplicationException(string.Format(CultureInfo.InvariantCulture,
                                                         "Value '{0}' was not recognized as boolean.", 
                                                         inputValue));
        }

        /// <summary>
        /// Formats input string as Yes/No
        /// </summary>
        public string Format(int inputValue)
        {
            if (inputValue == 1)
                return CommonConstants.StringBoolean.CaptionYes;

            if (inputValue == 0)
                return CommonConstants.StringBoolean.CaptionNo;

            throw new ApplicationException(string.Format(CultureInfo.InvariantCulture,
                                                         "Value '{0}' was not recognized as boolean.",
                                                         inputValue));
        }

        /// <summary>
        /// Formats input string as Yes/No string
        /// </summary>
        public string Format(bool inputValue)
        {
            return inputValue
                       ? CommonConstants.StringBoolean.CaptionYes
                       : CommonConstants.StringBoolean.CaptionNo;
        }

        #endregion //Public methods
    }
}
