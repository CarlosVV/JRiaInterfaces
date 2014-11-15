using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class GooglePushPinParameterProvider : IGooglePushPinParameterProvider
    {
        #region Core

        private const string PushPinTemplate = "&markers={0}{1}{2},{3}";
        private const string ColorTemplate = "color:{0}{1}";
        private const string LabelTemplate = "label:{0}{1}";
        private const string Delimiter = "%7C";
        private const int LabelMaxLength = 1;

        #endregion

        #region Public methods

        public string GetPushPinParameter(ICollection<PushPinModel> pushPins)
        {
            if (pushPins == null || pushPins.Count == 0)
                return string.Empty;

            var pins = (from pin in pushPins
                let marker = GetPushpinFormatted(pin)
                select marker)
                .ToArray();

            return string.Join(string.Empty, pins);
        }

        #endregion

        private static string GetPushpinFormatted(PushPinModel pin)
        {
            CorrectPinParameters(pin);

            return string.Format(CultureInfo.InvariantCulture,
                PushPinTemplate,
                GetColorFormatted(pin.PinColor),
                GetLabelFormatted(pin.Label),
                pin.Location.Latitude,
                pin.Location.Longitude);
        }
        
        private static string GetColorFormatted(Color pinColor)
        {
            return pinColor == Color.Undefined
                ? string.Empty
                : string.Format(CultureInfo.InvariantCulture,
                    ColorTemplate,
                    pinColor.ToString().ToLowerInvariant(),
                    Delimiter);
        }

        private static string GetLabelFormatted(string label)
        {
            return string.IsNullOrEmpty(label)
                ? string.Empty
                : string.Format(CultureInfo.InvariantCulture,
                    LabelTemplate,
                    label,
                    Delimiter);
        }

        private static void CorrectPinParameters(PushPinModel pin)
        {
            if (string.IsNullOrEmpty(pin.Label))
                pin.Label = string.Empty;
            if (pin.Label.Length > LabelMaxLength)
                pin.Label = pin.Label.Substring(0, LabelMaxLength);
            if (!pin.Label.All(char.IsLetterOrDigit))
                pin.Label = string.Empty;
            pin.Label = pin.Label.ToUpperInvariant();
        }
    }
}
