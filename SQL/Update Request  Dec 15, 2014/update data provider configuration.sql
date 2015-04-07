update systblApp_CoreAPI_Settings 
set fValue = '
{
	"CountryConfigurations": [{
		"CountryCode": "Default",
		"DataProviders": [{
			"DataProviderServiceType": "AddressVerification",
			"DataProviderType": "bing",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "AddressVerification",
			"DataProviderType": "google",
			"Priority": 2
		},
		{
			"DataProviderServiceType": "AddressAutoComplete",
			"DataProviderType": "bing",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Geocoding",
			"DataProviderType": "bing",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Mapping",
			"DataProviderType": "bing",
			"Priority": 1
		}]
	},
	{
		"CountryCode": "US",
		"DataProviders": [{
			"DataProviderServiceType": "AddressVerification",
			"DataProviderType": "google",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "AddressVerification",
			"DataProviderType": "bing",
			"Priority": 2
		},
		{
			"DataProviderServiceType": "AddressAutoComplete",
			"DataProviderType": "bing",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Geocoding",
			"DataProviderType": "google",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "Geocoding",
			"DataProviderType": "google",
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
			"DataProviderType": "bing",
			"Priority": 1
		},
		{
			"DataProviderServiceType": "AddressAutoComplete",
			"DataProviderType": "bing",
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
'
where fName = 'DataProviderServiceConfiguration'