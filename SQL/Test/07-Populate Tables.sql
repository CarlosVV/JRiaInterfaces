

truncate table ApplicationConfiguration
truncate table Application_ServiceOperation
truncate table Application_ApplicationServer
delete from [dbo].[ServiceOperation]
delete from ApplicationServer
delete from systblapp where fAppID in (8000, 8010, 220)

--Populate systblapp table
insert into systblapp ([fAppID],[fType],[fName],[fDescription],[fDisabled],[fModified],[fModifiedID],[fDelete],[fChanged],[fTime])
	values (8000, 10, 'Geolocation Service Test', 'Geolocation Core API service test application', 0, '2014-11-14 10:00:00.000',101,0,1,'2014-11-14 10:00:00.000')
insert into systblapp ([fAppID],[fType],[fName],[fDescription],[fDisabled],[fModified],[fModifiedID],[fDelete],[fChanged],[fTime])
	values (8010, 10, 'Geolocation Service', 'Geolocation Core API service', 0, '2014-11-14 10:00:00.000',101,0,1,'2014-11-14 10:00:00.000')

--For testing only	
insert into systblapp ([fAppID],[fType],[fName],[fDescription],[fDisabled],[fModified],[fModifiedID],[fDelete],[fChanged],[fTime])
	values (220, 10, 'RIA Digital', 'RIA Digital', 0, '2014-11-14 10:00:00.000',101,0,1,'2014-11-14 10:00:00.000')

--Populate server table
insert into ApplicationServer ([ApplicationServerID], Name, [Description]) values (1, 'CoreAPISrv1', 'Core API services server #1')

--Populate Application_ApplicationServer table
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (8000, 1, 1)
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (8010, 1, 1)

--Populate ServiceOperation table

--Address Verification
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1001, 8010, 'ValidateAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1002, 8010, 'ValidateFormattedAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1003, 8010, 'GetAutocompleteList', 1)

--Geocode
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1006, 8010, 'GeocodeAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1007, 8010, 'GeocodeFormattedAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1008, 8010, 'ReverseGeocodePoint', 1)

--Mapping
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1009, 8010, 'GetMap', 1)

--Health check
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1004, 8010, 'ClearCache', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1005, 8010, 'Ping', 1)

--Client side support
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1010, 8010, 'GetProviderKey', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1011, 8010, 'LogEvent', 1)

--Populate Application_ServiceOperation

--8000 - Test application
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1001, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1002, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1003, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1006, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1007, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1008, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1009, 1)
--health monitoring operations
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1004, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1005, 1)
--Client side support operations
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1010, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1011, 1)

--220 - RIA Digital
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (220, 1002, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (220, 1007, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (220, 1009, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (220, 1010, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (220, 1011, 1)

--Bing related configuration
insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'BingAddressGeocodeAndVerificationUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations?CountryRegion={0}&adminDistrict={1}&locality={2}&postalCode={3}&addressLine={4}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={5}', 'Bing address geocode and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'BingFormattedAddressGeocodeAndVerificationUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations?q={0}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={1}', 'Bing formatted address geocode and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'BingAddressAutocompleteUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations/{0}?o=xml&userIp=127.0.0.1&maxResults={1}&include=ciso2&key={2}', 'Bing address autocomplete service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'BingReverseGeocodePointUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={2}', 'Bing reverse geocode point service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'BingMappingUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Imagery/Map/{0}/{1},{2}/{3}?mapSize={4},{5}&format={6}{7}&key={8}', 'Bing map service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'BingLicenseKey', 'AkuAfUPJx-izRLvlNf5GXBWPybHdcFwh34U5krgE2RsGiQs9xwLpUvpvPo8yceiI', 'Bing license key')


--MelissaData related configursation
insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'MelissaDataAddressGeocodeAndVerificationUrlTemplate', 'http://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&a2={2}&loc={3}&admarea{4}&postal={5}&ctry={6}', 'Melissa Data address geocoding and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'MelissaDataAddressAutocompleteUrlTemplate', 'http://expressentry.melissadata.net/web/GlobalExpressAddress?id={0}&format=xml&address1={1}&administrativearea={2}&Country={3}&maxrecords={4}', 'Melissa Data address autocomplete service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate', 'http://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&maxrecords=1', 'Melissa Data formatted address geocoding and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'MelissaDataLicenseKey', '109099452', 'Melissa Data license key')

--Google related configuraiton
insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'GoogleAddressGeocodeAndVerificationUrlTemplate', 'http://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}', 'Google address geocode and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'GoogleAddressAutocompleteUrlTemplate', 'http://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}', 'Google address autocomplete service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'GoogleReverseGeocodePointUrlTemplate', 'http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}{2}', 'Google address reverse geocode service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'GoogleMappingUrlTemplate', 'http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&size={3}x{4}&format={5}&maptype={6}{7}{8}', 'Google mapping service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'GoogleLicenseKey', '', 'Google license key')

--Geolocation service configuraiton
insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'NumberOfProvidersToProcessResult', '2', 'Maximum number of data providers of the same service type used to process request.')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'AddressAutompleteMaximumNumberOfHints', '25', 'Maximum number of address autocomplete hints returned by one request..')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'AddressAutompleteDefaultNumberOfHints', '15', 'Default number of autocomplete hints returned by one request.')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'DataProviderServiceConfiguration', '
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
	', 'Data providers service configuration by country.')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(8010, 'ServiceConfiguration', '
{
	"Endpoints": [
	{
		"EndpointName": "httpAddressService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IAddressService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "httpGeocodeService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IGeocodeService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "httpMapService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IMapService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "httpHealthMonitoringService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IHealthMonitoringService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "httpClientSideSupportService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IClientSideSupportService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpAddressService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IAddressService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpGeocodeService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IGeocodeService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpMapService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IMapService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpHealthMonitoringService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IHealthMonitoringService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
		"SecurityMode": "None"
	},
	{
		"EndpointName": "tcpClientSideSupportService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IClientSideSupportService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
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
		"IsHttpsEnabled": "true",
		"IsDebugEnabled": "true",
		"IsHelpEnabled": "false",
		"IsWsdlEnabled": "true",
		"IsJsonRequestEnabled": "false",
		"IsJsonResponseEnabled": "false",
		"IsAutomaticFormatSelectionEnabled": "false"
	}
}
', 'Service configuration.')
