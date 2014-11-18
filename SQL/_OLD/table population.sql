

truncate table ApplicationConfiguration
truncate table Application_ServiceOperation
truncate table Application_ApplicationServer
delete from [dbo].[ServiceOperation]
delete from Application
delete from ApplicationServer


--Populate server table
insert into ApplicationServer ([ApplicationServerID], Name, [Description]) values (1, 'CoreAPISrv1', 'Core API services server #1')
insert into ApplicationServer ([ApplicationServerID], Name, [Description]) values (2, 'CoreAPISrv2', 'Core API services server #2')
insert into ApplicationServer ([ApplicationServerID], Name, [Description]) values (3, 'CoreAPISrv3', 'Core API services server #3')
insert into ApplicationServer ([ApplicationServerID], Name, [Description]) values (4, 'CoreAPISrv4', 'Core API services server #4')

--Populate Application table
insert into [Application] (ApplicationID, IsActive, Name)
	values (1, 1, 'Parent application for every Core API service')
insert into [Application] (ApplicationID, ParentApplicationID, IsActive, Name)
	values (1000, 1, 1, 'Geolocation Core API service')
insert into [Application] (ApplicationID, ParentApplicationID, IsActive, Name)
	values (2000, 1, 1, 'Test web application')
insert into [Application] (ApplicationID, ParentApplicationID, IsActive, Name)
	values (3000, 1, 0, 'Test web application #2')

--Populate Application_ApplicationServer table
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (1, 1, 1)
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (1, 2, 1)
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (1, 3, 1)
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (1, 4, 1)
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (1000, 1, 1)
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (1000, 2, 0)

--Populate ServiceOperation table

--Address Verification
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1001, 1000, 'ValidateAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1002, 1000, 'ValidateFormattedAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1003, 1000, 'GetAutocompleteList', 1)

--Geocode
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1006, 1000, 'GeocodeAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1007, 1000, 'GeocodeFormattedAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1008, 1000, 'ReverseGeocodePoint', 1)

--Mapping
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1009, 1000, 'GetMap', 1)

--Health check
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1004, 1000, 'ClearCache', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1005, 1000, 'Ping', 1)

--Populate Application_ServiceOperation
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (2000, 1001, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (2000, 1002, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (2000, 1003, 1)

insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (2000, 1006, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (2000, 1007, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (2000, 1008, 1)

insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (2000, 1009, 1)

insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (3000, 1002, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (3000, 1001, 0)

--Assign CoreAPI to health monitoring operations
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (1, 1004, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (1, 1005, 1)

--Populate ApplicationConfiguration
--Foundation configuration
--insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
--	values(1, 'CacheName', 'CoreAPI', 'Name of Core API region in appFabric cache')
--insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
--	values(1, 'CacheLifetime', '0.01:00:00', 'Core API cache lifetime in days.hours:minutes:seconds')

--Geolocation configuration
insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'BingAddressGeocodeAndVerificationUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations?CountryRegion={0}&adminDistrict={1}&locality={2}&postalCode={3}&addressLine={4}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={5}', 'Bing address geocode and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'BingFormattedAddressGeocodeAndVerificationUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations?q={0}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={1}', 'Bing formatted address geocode and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'BingAddressAutocompleteUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations/{0}?o=xml&userIp=127.0.0.1&maxResults={1}&include=ciso2&key={2}', 'Bing address autocomplete service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'BingReverseGeocodePointUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={2}', 'Bing reverse geocode point service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'BingMappingUrlTemplate', 'http://dev.virtualearth.net/REST/v1/Imagery/Map/{0}/{1},{2}/{3}?mapSize={4},{5}&format={6}{7}&key={8}', 'Bing map service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'BingLicenseKey', 'AkuAfUPJx-izRLvlNf5GXBWPybHdcFwh34U5krgE2RsGiQs9xwLpUvpvPo8yceiI', 'Bing license key')



insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'MelissaDataAddressGeocodeAndVerificationUrlTemplate', 'http://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&a2={2}&loc={3}&admarea{4}&postal={5}&ctry={6}', 'Melissa Data address geocoding and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'MelissaDataAddressAutocompleteUrlTemplate', 'http://expressentry.melissadata.net/web/GlobalExpressAddress?id={0}&format=xml&address1={1}&administrativearea={2}&Country={3}&maxrecords={4}', 'Melissa Data address autocomplete service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate', 'http://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&maxrecords=1', 'Melissa Data formatted address geocoding and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'MelissaDataLicenseKey', '109099452', 'Melissa Data license key')


insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'GoogleAddressGeocodeAndVerificationUrlTemplate', 'http://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}', 'Google address geocode and verification service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'GoogleAddressAutocompleteUrlTemplate', 'http://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}', 'Google address autocomplete service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'GoogleReverseGeocodePointUrlTemplate', 'http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}{2}', 'Google address reverse geocode service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'GoogleMappingUrlTemplate', 'http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&size={3}x{4}&format={5}&maptype={6}{7}{8}', 'Google mapping service URL template')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'GoogleLicenseKey', '', 'Google license key')


insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'NumberOfProvidersToProcessResult', '2', 'Maximum number of data providers of the same service type used to process request.')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'AddressAutompleteMaximumNumberOfHints', '25', 'Maximum number of address autocomplete hints returned by one request..')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'AddressAutompleteDefaultNumberOfHints', '15', 'Default number of autocomplete hints returned by one request.')

insert ApplicationConfiguration (ApplicationID, ConfigurationName, ConfigurationValue, [Description])
	values(1000, 'DataProviderServiceConfiguration', '
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
	values(1000, 'ServiceConfiguration', '
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
', 'Service configuration.')
