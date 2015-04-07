
update systblApp_CoreAPI_Settings
set fValue = 'https://dev.virtualearth.net/REST/v1/Locations?CountryRegion={0}&adminDistrict={1}&locality={2}&postalCode={3}&addressLine={4}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={5}'
where fName = 'BingAddressGeocodeAndVerificationUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://dev.virtualearth.net/REST/v1/Locations?q={0}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={1}'
where fName = 'BingFormattedAddressGeocodeAndVerificationUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://dev.virtualearth.net/REST/v1/Locations/{0}?o=xml&userIp=127.0.0.1&maxResults={1}&include=ciso2&key={2}'
where fName = 'BingAddressAutocompleteUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={2}'
where fName = 'BingReverseGeocodePointUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://dev.virtualearth.net/REST/v1/Imagery/Map/{0}/{1},{2}/{3}?mapSize={4},{5}&format={6}{7}&key={8}'
where fName = 'BingMappingUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&a2={2}&loc={3}&admarea{4}&postal={5}&ctry={6}'
where fName = 'MelissaDataAddressAutocompleteUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://expressentry.melissadata.net/web/GlobalExpressAddress?id={0}&format=xml&address1={1}&administrativearea={2}&Country={3}&maxrecords={4}'
where fName = 'MelissaDataAddressGeocodeAndVerificationUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&maxrecords=1'
where fName = 'MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}'
where fName = 'GoogleAddressGeocodeAndVerificationUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}'
where fName = 'GoogleAddressAutocompleteUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}{2}'
where fName = 'GoogleReverseGeocodePointUrlTemplate'

update systblApp_CoreAPI_Settings
set fValue = 'https://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&size={3}x{4}&format={5}&maptype={6}{7}{8}'
where fName = 'GoogleMappingUrlTemplate'
