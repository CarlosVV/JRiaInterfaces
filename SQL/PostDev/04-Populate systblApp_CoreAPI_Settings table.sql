USE [fxDB6]
go

print 'BEGIN'

declare @fAppID as int, @fModifiedID as int, @currentTime as datetime
declare @temp TABLE (fCoreAPISettingsID int, fAppID int, fName varchar(100), fValue varchar(max), fDescription varchar(250), fModified datetime, fModifiedID int, fTime datetime)
select @fAppID = 8000, @fModifiedID = 34177711, @currentTime = GETDATE()

--Populate @temp table

--Bing related configuration
insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(1, @fAppID, 'BingAddressGeocodeAndVerificationUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations?CountryRegion={0}&adminDistrict={1}&locality={2}&postalCode={3}&addressLine={4}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={5}', 
	'Bing address geocode and verification service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(2, @fAppID, 'BingFormattedAddressGeocodeAndVerificationUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations?q={0}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={1}', 
	'Bing formatted address geocode and verification service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(3, @fAppID, 'BingAddressAutocompleteUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations/{0}?o=xml&userIp=127.0.0.1&maxResults={1}&include=ciso2&key={2}',
	'Bing address autocomplete service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(4, @fAppID, 'BingReverseGeocodePointUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={2}', 
	'Bing reverse geocode point service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(5, @fAppID, 'BingMappingUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Imagery/Map/{0}/{1},{2}/{3}?mapSize={4},{5}&format={6}{7}&key={8}', 
	'Bing map service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(6, @fAppID, 'BingLicenseKey', 'AkuAfUPJx-izRLvlNf5GXBWPybHdcFwh34U5krgE2RsGiQs9xwLpUvpvPo8yceiI', 
	'Bing license key', @currentTime, @fModifiedID, @currentTime)


--MelissaData related configuration
insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(7, @fAppID, 'MelissaDataAddressGeocodeAndVerificationUrlTemplate', 'http://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&a2={2}&loc={3}&admarea{4}&postal={5}&ctry={6}', 
	'Melissa Data address geocoding and verification service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(8, @fAppID, 'MelissaDataAddressAutocompleteUrlTemplate', 'http://expressentry.melissadata.net/web/GlobalExpressAddress?id={0}&format=xml&address1={1}&administrativearea={2}&Country={3}&maxrecords={4}', 
	'Melissa Data address autocomplete service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(9, @fAppID, 'MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate', 'http://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&maxrecords=1', 
	'Melissa Data formatted address geocoding and verification service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(10, @fAppID, 'MelissaDataLicenseKey', '109099452', 'Melissa Data license key', @currentTime, @fModifiedID, @currentTime)

--Google related configuraiton
insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(11, @fAppID, 'GoogleAddressGeocodeAndVerificationUrlTemplate', 'http://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}', 
	'Google address geocode and verification service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(12, @fAppID, 'GoogleAddressAutocompleteUrlTemplate', 'http://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}', 
	'Google address autocomplete service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(13, @fAppID, 'GoogleReverseGeocodePointUrlTemplate', 'http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}{2}', 
	'Google address reverse geocode service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(14, @fAppID, 'GoogleMappingUrlTemplate', 'http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&size={3}x{4}&format={5}&maptype={6}{7}{8}', 
	'Google mapping service URL template', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(15, @fAppID, 'GoogleLicenseKey', '', 'Google license key', @currentTime, @fModifiedID, @currentTime)

--Geolocation service configuraiton
insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(16, @fAppID, 'NumberOfProvidersToProcessResult', '2', 
	'Maximum number of data providers of the same service type used to process request.', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(17, @fAppID, 'AddressAutompleteMaximumNumberOfHints', '25', 
	'Maximum number of address autocomplete hints returned by one request.', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(18, @fAppID, 'AddressAutompleteDefaultNumberOfHints', '15', 
	'Default number of autocomplete hints returned by one request.', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(19, @fAppID, 'DataProviderServiceConfiguration', '
{
	"CountryConfigurations": [{
		"CountryCode": "Default",
		"DataProviders": [{
			"DataProviderServiceType": "AddressVerification",
			"DataProviderType": "Bing",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "AddressVerification",
			"DataProviderType": "MelissaData",
			"Priority": 2
		},
		{
			"DataProviderServiceType": "AddressAutoComplete",
			"DataProviderType": "MelissaData",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Geocoding",
			"DataProviderType": "Bing",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Mapping",
			"DataProviderType": "Google",
			"Priority": 1
		}]
	},
	{
		"CountryCode": "US",
		"DataProviders": [{
			"DataProviderServiceType": "AddressVerification",
			"DataProviderType": "MelissaData",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "AddressVerification",
			"DataProviderType": "google",
			"Priority": 2
		},
		{
			"DataProviderServiceType": "AddressAutoComplete",
			"DataProviderType": "google",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Geocoding",
			"DataProviderType": "google",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Geocoding",
			"DataProviderType": "Bing",
			"Priority": 2
		},
		{
			"DataProviderServiceType": "Mapping",
			"DataProviderType": "google",
			"Priority": 1
		}]
	},
	{
		"CountryCode": "CA",
		"DataProviders": [{
			"DataProviderServiceType": "AddressVerification",
			"DataProviderType": "MelissaData",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "AddressAutoComplete",
			"DataProviderType": "MelissaData",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Geocoding",
			"DataProviderType": "Bing",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Mapping",
			"DataProviderType": "Bing",
			"Priority": 1
		}]
	}]
}
	', 'Data providers service configuration by country.', @currentTime, @fModifiedID, @currentTime)

insert @temp (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	values(20, @fAppID, 'ServiceConfiguration', '
{
	"Endpoints": [
	{
		"EndpointName": "httpAddressService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IAddressService",
		"Binding": "basicHttpBinding",
		"BindingfName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "httpGeocodeService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IGeocodeService",
		"Binding": "basicHttpBinding",
		"BindingfName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "httpMapService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IMapService",
		"Binding": "basicHttpBinding",
		"BindingfName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "httpHealthMonitoringService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IHealthMonitoringService",
		"Binding": "basicHttpBinding",
		"BindingfName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "httpClientSideSupportService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IClientSideSupportService",
		"Binding": "basicHttpBinding",
		"BindingfName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpAddressService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IAddressService",
		"Binding": "netTcpBinding",
		"BindingfName": "netTcpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpGeocodeService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IGeocodeService",
		"Binding": "netTcpBinding",
		"BindingfName": "netTcpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpMapService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IMapService",
		"Binding": "netTcpBinding",
		"BindingfName": "netTcpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpHealthMonitoringService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IHealthMonitoringService",
		"Binding": "netTcpBinding",
		"BindingfName": "netTcpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpClientSideSupportService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IClientSideSupportService",
		"Binding": "netTcpBinding",
		"BindingfName": "netTcpBindingConfiguration",
		"SecurityMode": "None"
	}],
	"Bindings": [
	{
		"Binding": "basicHttpBinding",
		"Name": "basicHttpBindingConfiguration",
		"MaxBufferPoolSize": "2147483647",
		"MaxBufferSize": "2147483647",
		"MaxReceivedMessageSize": "2147483647",
		"ReaderQuotas": {
			"MaxArrayLength": "2147483647",
			"MaxBytesPerRead": "2147483647",
			"MaxDepth": "2147483647",
			"MaxNameTableCharCount": "2147483647",
			"MaxStringContentLength": "2147483647"
		},
		"Security": "None",
		"ReliableSession": {
			"Enabled": "false"
		}
	},
	{
		"Binding": "netTcpBinding",
		"Name": "netTcpBindingConfiguration",
		"MaxBufferSize": "147483647",
		"MaxBufferPoolSize": "147483647",
		"MaxReceivedMessageSize": "147483647",
		"ListenBacklog": "147483647",
		"TransactionFlow": "false",
		"MaxConnections": "147483647",
		"ReaderQuotas": {
			"MaxArrayLength": "147483647",
			"MaxBytesPerRead": "147483647",
			"MaxDepth": "147483647",
			"MaxNameTableCharCount": "147483647",
			"MaxStringContentLength": "147483647"
		},
		"Security": "None",
		"ReliableSession": {
			"Enabled": "false"
		}
	}],
	"Behavior": {
		"IsHttpsEnabled": "false",
		"IsDebugEnabled": "true",
		"IsHelpEnabled": "false",
		"IsWsdlEnabled": "true",
		"IsJsonRequestEnabled": "false",
		"IsJsonResponseEnabled": "false",
		"IsAutomaticFormatSelectionEnabled": "false"
	}
}
', 'Service configuration.', @currentTime, @fModifiedID, @currentTime)

--Get correct ID number
Declare @lAppID      Int, 
@lAppObjectID        Int,
@lAutoNumberID       int, 
@lNumbersToGet       int, 
@lRetVal             int, 
@lNextNumberToUse    BigInt , 
@lSuffixAddOn        int

Select	@lAppID = 8000, 
		@lAppObjectID = 0, 
		@lAutoNumberID = 9983, 
		@lNumbersToGet = 20

exec [dbo].[an_sp_GetNextAutoID] 
	@lAppID              = @lAppID, 
	@lAppObjectID        = @lAppObjectID,
	@lAutoNumberID       = @lAutoNumberID, 
	@lNumbersToGet       = @lNumbersToGet, 
	@lRetVal             = @lRetVal           output, 
	@lNextNumberToUse    = @lNextNumberToUse  output, 
	@lSuffixAddOn        = @lSuffixAddOn      output

declare @fCoreAPISettingsID int,
		@currentID			varchar(20),
		@counter			int

set @counter = @lNextNumberToUse

declare id_cursor CURSOR FOR 
SELECT fCoreAPISettingsID
FROM @temp
ORDER BY fCoreAPISettingsID

OPEN id_cursor
FETCH NEXT FROM id_cursor 
INTO @fCoreAPISettingsID

WHILE @@FETCH_STATUS = 0
BEGIN

	set @currentID = cast(@counter as varchar(10)) + cast(@lSuffixAddOn as varchar(10))

	update @temp 
	set fCoreAPISettingsID = @currentID
	where fCoreAPISettingsID = @fCoreAPISettingsID
	
	set @counter = @counter + 1

	FETCH NEXT FROM id_cursor 
    INTO @fCoreAPISettingsID
END 
CLOSE id_cursor;
DEALLOCATE id_cursor;

insert into systblApp_CoreAPI_Settings (fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime)
	select fCoreAPISettingsID, fAppID, fName, fValue, fDescription, fModified, fModifiedID, fTime from @temp

print 'END'
