using System.Net;
using System.Xml.Linq;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest.Helpers
{
    public class MelissaResponseParserHelper
    {
        public const string AutoCompleteRawResponse = "<ResponseGlobal xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Version>2.0.01</Version><ResultCode>XS01</ResultCode><ErrorString i:nil=\"true\"/><Results><ResultGlobal><Address><Address>1445 Brett Pl, SAN PEDRO CA 90732</Address><Address1>1445 Brett Pl</Address1><Address2>SAN PEDRO CA 90732</Address2><Address3 i:nil=\"true\"/><Address4 i:nil=\"true\"/><Address5 i:nil=\"true\"/><Address6 i:nil=\"true\"/><Address7 i:nil=\"true\"/><Address8 i:nil=\"true\"/><Address9 i:nil=\"true\"/><Address10 i:nil=\"true\"/><Address11 i:nil=\"true\"/><Address12 i:nil=\"true\"/><DeliveryAddress>1445 Brett Pl</DeliveryAddress><DeliveryAddress1>1445 Brett Pl</DeliveryAddress1><DeliveryAddress2 i:nil=\"true\"/><DeliveryAddress3 i:nil=\"true\"/><DeliveryAddress4 i:nil=\"true\"/><DeliveryAddress5 i:nil=\"true\"/><DeliveryAddress6 i:nil=\"true\"/><DeliveryAddress7 i:nil=\"true\"/><DeliveryAddress8 i:nil=\"true\"/><DeliveryAddress9 i:nil=\"true\"/><DeliveryAddress10 i:nil=\"true\"/><DeliveryAddress11 i:nil=\"true\"/><DeliveryAddress12 i:nil=\"true\"/><CountryName>United States</CountryName><ISO3166_2 i:nil=\"true\"/><ISO3166_3 i:nil=\"true\"/><ISO3166_N i:nil=\"true\"/><SuperAdministrativeArea i:nil=\"true\"/><AdministrativeArea>CA</AdministrativeArea><SubAdministrativeArea i:nil=\"true\"/><Locality>SAN PEDRO</Locality><DependentLocality i:nil=\"true\"/><DoubleDependentLocality i:nil=\"true\"/><Thoroughfare i:nil=\"true\"/><DependentThoroughfare i:nil=\"true\"/><Building i:nil=\"true\"/><Premise i:nil=\"true\"/><SubBuilding>Unit 101,Unit 102,Unit 103,Unit 104,Unit 105,Unit 106,Unit 107,Unit 108,Unit 109,Unit 110,Unit 111,Unit 112,Unit 113,Unit 114,Unit 115,Unit 116,Unit 117,Unit 118,Unit 119,Unit 120,Unit 121,Unit 122,Unit 301,Unit 302,Unit 303,Unit 304,Unit 305,Unit 306,Unit 307,Unit 308,Unit 309,Unit 310,Unit 311,Unit 312,Unit 313,Unit 314,Unit 315,Unit 316,Unit 317,Unit 318,Unit 319,Unit 320,Unit 321,Unit 322</SubBuilding><PostalCode>90732</PostalCode><PostalCodePrimary>90732</PostalCodePrimary><PostalCodeSecondary>5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111,5111</PostalCodeSecondary><Organization i:nil=\"true\"/><PostBox i:nil=\"true\"/><Unmatched i:nil=\"true\"/><GeneralDelivery i:nil=\"true\"/><DeliveryInstallation i:nil=\"true\"/><Route i:nil=\"true\"/><AdditionalContent i:nil=\"true\"/></Address></ResultGlobal></Results></ResponseGlobal>";
        public const string AutoCompleteRawResponseNoAddress = "<ResponseGlobal xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Version>2.0.01</Version><ResultCode>XS01</ResultCode><ErrorString i:nil=\"true\"/><Results></Results></ResponseGlobal>";
        public const string AutoCompleteRawResponseNoResults = "<ResponseGlobal xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Version>2.0.01</Version><ResultCode>XS01</ResultCode><ErrorString i:nil=\"true\"/></ResponseGlobal>";

        public const string ValidateAddressRawResponse = "<Response xmlns=\"urn:mdGlobalAddress\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Version>3.0.1.90</Version><TransmissionReference/><TransmissionResults/><TotalRecords>1</TotalRecords><Records><ResponseRecord><RecordID>1</RecordID><Results>AC02,AC13,AC16,AV25,GS05</Results><FormattedAddress>1445 BRETT PL UNIT 108;SAN PEDRO CA  90732-5111</FormattedAddress><Organization/><AddressLine1>1445 Brett Pl Unit 108</AddressLine1><AddressLine2>San Pedro CA 90732-5111</AddressLine2><AddressLine3/><AddressLine4/><AddressLine5/><AddressLine6/><AddressLine7/><AddressLine8/><SubPremises>Unit 108</SubPremises><DoubleDependentLocality/><DependentLocality/><Locality>San Pedro</Locality><SubAdministrativeArea>Los Angeles</SubAdministrativeArea><AdministrativeArea>CA</AdministrativeArea><PostalCode>90732-5111</PostalCode><AddressType>H</AddressType><AddressKey>90732511133</AddressKey><SubNationalArea/><CountryName>United States of America</CountryName><CountryISO3166_1_Alpha2>US</CountryISO3166_1_Alpha2><CountryISO3166_1_Alpha3>USA</CountryISO3166_1_Alpha3><CountryISO3166_1_Numeric>840</CountryISO3166_1_Numeric><Thoroughfare> Brett Pl </Thoroughfare><ThoroughfarePreDirection/><ThoroughfareLeadingType/><ThoroughfareName>Brett</ThoroughfareName><ThoroughfareTrailingType>Pl</ThoroughfareTrailingType><ThoroughfarePostDirection/><DependentThoroughfare/><DependentThoroughfarePreDirection/><DependentThoroughfareLeadingType/><DependentThoroughfareName/><DependentThoroughfareTrailingType/><DependentThoroughfarePostDirection/><Building/><PremisesType/><PremisesNumber>1445</PremisesNumber><SubPremisesType>Unit</SubPremisesType><SubPremisesNumber>108</SubPremisesNumber><PostBox/><Latitude>33.755800</Latitude><Longitude>-118.308570</Longitude></ResponseRecord></Records></Response>";
        public const string ValidateAddressRawResponseNoRecords = "<Response xmlns=\"urn:mdGlobalAddress\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Version>3.0.1.90</Version><TransmissionReference/><TransmissionResults/><TotalRecords>1</TotalRecords></Response>";

        public const string GeocodeAddressRawResponse = "<Response xmlns=\"urn:mdGlobalAddress\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Version>3.0.1.90</Version><TransmissionReference/><TransmissionResults/><TotalRecords>1</TotalRecords><Records><ResponseRecord><RecordID>1</RecordID><Results>AC02,AC16,AV25,GS05</Results><FormattedAddress>1445 BRETT PL UNIT 112;SAN PEDRO CA  90732-5111</FormattedAddress><Organization/><AddressLine1>1445 Brett Pl Unit 112</AddressLine1><AddressLine2>San Pedro CA 90732-5111</AddressLine2><AddressLine3/><AddressLine4/><AddressLine5/><AddressLine6/><AddressLine7/><AddressLine8/><SubPremises>Unit 112</SubPremises><DoubleDependentLocality/><DependentLocality/><Locality>San Pedro</Locality><SubAdministrativeArea>Los Angeles</SubAdministrativeArea><AdministrativeArea>CA</AdministrativeArea><PostalCode>90732-5111</PostalCode><AddressType>H</AddressType><AddressKey>90732511137</AddressKey><SubNationalArea/><CountryName>United States of America</CountryName><CountryISO3166_1_Alpha2>US</CountryISO3166_1_Alpha2><CountryISO3166_1_Alpha3>USA</CountryISO3166_1_Alpha3><CountryISO3166_1_Numeric>840</CountryISO3166_1_Numeric><Thoroughfare> Brett Pl </Thoroughfare><ThoroughfarePreDirection/><ThoroughfareLeadingType/><ThoroughfareName>Brett</ThoroughfareName><ThoroughfareTrailingType>Pl</ThoroughfareTrailingType><ThoroughfarePostDirection/><DependentThoroughfare/><DependentThoroughfarePreDirection/><DependentThoroughfareLeadingType/><DependentThoroughfareName/><DependentThoroughfareTrailingType/><DependentThoroughfarePostDirection/><Building/><PremisesType/><PremisesNumber>1445</PremisesNumber><SubPremisesType>Unit</SubPremisesType><SubPremisesNumber>112</SubPremisesNumber><PostBox/><Latitude>33.755800</Latitude><Longitude>-118.308570</Longitude></ResponseRecord></Records></Response>";
        public const string GeocodeAddressRawResponseNoRecords = "<Response xmlns=\"urn:mdGlobalAddress\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Version>3.0.1.90</Version><TransmissionReference/><TransmissionResults/><TotalRecords>1</TotalRecords></Response>";

        public static XNamespace XNamespace = "urn:mdGlobalAddress"; 

        public static DataResponse GetAutomcompleteDataResponse(bool isInvalid = false)
        {
            return new DataResponse(AutoCompleteRawResponse, HttpStatusCode.OK, !isInvalid);
        }

        public static DataResponse GetAutomcompleteDataResponseNoAddress()
        {
            return new DataResponse(AutoCompleteRawResponseNoAddress, HttpStatusCode.OK, true);
        }

        public static DataResponse GetAutomcompleteDataResponseNoResults()
        {
            return new DataResponse(AutoCompleteRawResponseNoResults, HttpStatusCode.OK, true);
        }

        public static DataResponse GetValidateAddressDataResponse(bool isInvalid = false)
        {
            return new DataResponse(ValidateAddressRawResponse, HttpStatusCode.OK, !isInvalid);
        }

        public static DataResponse GetValidateAddressDataResponseNoAddress()
        {
            return new DataResponse(ValidateAddressRawResponseNoRecords, HttpStatusCode.OK, true);
        }

        public static DataResponse GetGeocodeAddressDataResponse(bool isInvalid = false)
        {
            return new DataResponse(GeocodeAddressRawResponse, HttpStatusCode.OK, !isInvalid);
        }

        public static DataResponse GetGeocodeAddressDataResponseNoAddress()
        {
            return new DataResponse(GeocodeAddressRawResponseNoRecords, HttpStatusCode.OK, true);
        }

        public static AddressModel GetAddressModel()
        {
            return new AddressModel
            {
                Address1 = "1445 Brett Pl",
                Address2 = string.Empty,
                AdministrativeArea = "CA",
                City = "San Pedro",
                Country = "US",
                PostalCode = "90732",
                FormattedAddress = "1445 Brett Pl, San Pedro, CA 90732",
                UnitOrApartment = string.Empty,
                UnitsOrApartments = null
            };
        }

        public static LocationModel GetLocationModel()
        {
            return new LocationModel
            {
                Latitude = (decimal)33.7558,
                Longitude = (decimal)-118.308570
            };
        }
    }
}
