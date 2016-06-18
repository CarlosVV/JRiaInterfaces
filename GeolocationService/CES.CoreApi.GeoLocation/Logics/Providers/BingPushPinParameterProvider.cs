using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class BingPushPinParameterProvider : IBingPushPinParameterProvider
    {
        #region Core

        private const string PushPinTemplate = "&pp={0},{1};{2};{3}";
        private const int LabelMaxLength = 3;
        private const int IconStyleMinValue = 0;
        private const int IconStyleMaxValue = 112;
        private const int IconStyleDefaultValue = 1;

        #endregion

        #region Public methods

        public string GetPushPinParameter(ICollection<PushPinModel> pushPins)
        {
            if (pushPins == null || pushPins.Count == 0)
                return string.Empty;

            var pins = (from pin in pushPins
                select GetPushpinFormatted(pin))
                .ToArray();

            return string.Join(string.Empty, pins);
        }

        #endregion

        private static string GetPushpinFormatted(PushPinModel pin)
        {
            CorrectPinParameters(pin);

            return string.Format(CultureInfo.InvariantCulture,
                PushPinTemplate,
                pin.Location.Latitude,
                pin.Location.Longitude,
                pin.IconStyle,
                pin.Label);
        }

        private static void CorrectPinParameters(PushPinModel pin)
        {
            if (string.IsNullOrEmpty(pin.Label))
                pin.Label = string.Empty;
            if (pin.Label.Length > LabelMaxLength)
                pin.Label = pin.Label.Substring(0, LabelMaxLength);

            if (!pin.IconStyle.HasValue)
                pin.IconStyle = IconStyleDefaultValue;
            if (pin.IconStyle < IconStyleMinValue)
                pin.IconStyle = IconStyleDefaultValue;
            if (pin.IconStyle > IconStyleMaxValue)
                pin.IconStyle = IconStyleDefaultValue;
        }
    }
}

