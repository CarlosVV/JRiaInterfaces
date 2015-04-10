update systblApp_CoreAPI_Settings 
set fValue = '
{
	"Endpoints": [
	{
		"EndpointName": "httpAddressService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IAddressService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "Transport"
	},
	{
		"EndpointName": "httpGeocodeService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IGeocodeService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "Transport"
	},
	{
		"EndpointName": "httpMapService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IMapService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "Transport"
	},
	{
		"EndpointName": "httpHealthMonitoringService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IHealthMonitoringService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "Transport"
	},
	{
		"EndpointName": "httpClientSideSupportService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IClientSideSupportService",
		"Binding": "basicHttpBinding",
		"BindingConfigurationName": "basicHttpBindingConfiguration",
		"SecurityMode": "Transport"
	},
	{
		"EndpointName": "tcpAddressService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IAddressService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
		"SecurityMode": "Transport"
	},
	{
		"EndpointName": "tcpGeocodeService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IGeocodeService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
		"SecurityMode": "Transport"
	},
	{
		"EndpointName": "tcpMapService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IMapService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
		"SecurityMode": "Transport"
	},
	{
		"EndpointName": "tcpHealthMonitoringService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IHealthMonitoringService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
		"SecurityMode": "Transport"
	},
	{
		"EndpointName": "tcpClientSideSupportService",
		"Contract": "CES.CoreApi.GeoLocation.Service.Contract.Interfaces.IClientSideSupportService",
		"Binding": "netTcpBinding",
		"BindingConfigurationName": "netTcpBindingConfiguration",
		"SecurityMode": "Transport"
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
		"Security": "Transport",
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
		"Security": "Transport",
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
'
where fName = 'ServiceConfiguration'