using System.Net;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest.Helpers
{
    internal class GoogleResponseParserHelper
    {
        #region Constants

        public const string ValidateAddressRawResponse = @"
<GeocodeResponse>
 <status>OK</status>
 <result>
  <type>subpremise</type>
  <formatted_address>1445 Brett Place #108, San Pedro, CA 90732, USA</formatted_address>
  <address_component>
   <long_name>108</long_name>
   <short_name>108</short_name>
   <type>subpremise</type>
  </address_component>
  <address_component>
   <long_name>1445</long_name>
   <short_name>1445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Brett Place</long_name>
   <short_name>Brett Pl</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Harbor</long_name>
   <short_name>Harbor</short_name>
   <type>neighborhood</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>San Pedro</long_name>
   <short_name>San Pedro</short_name>
   <type>sublocality_level_1</type>
   <type>sublocality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Los Angeles</long_name>
   <short_name>Los Angeles</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Los Angeles County</long_name>
   <short_name>Los Angeles County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>90732</long_name>
   <short_name>90732</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>33.7558010</lat>
    <lng>-118.3085720</lng>
   </location>
   <location_type>ROOFTOP</location_type>
   <viewport>
    <southwest>
     <lat>33.7544520</lat>
     <lng>-118.3099210</lng>
    </southwest>
    <northeast>
     <lat>33.7571500</lat>
     <lng>-118.3072230</lng>
    </northeast>
   </viewport>
  </geometry>
  <partial_match>true</partial_match>
 </result>
</GeocodeResponse>";

        public const string ValidateAddressRawResponseNoAddress = @"
<GeocodeResponse>
 <status>OK</status>
 <result>
<type>subpremise</type>
  <formatted_address></formatted_address>
 <geometry>
   <location>
    <lat>33.7558010</lat>
    <lng>-118.3085720</lng>
   </location>
   <location_type>ROOFTOP</location_type>
   <viewport>
    <southwest>
     <lat>33.7544520</lat>
     <lng>-118.3099210</lng>
    </southwest>
    <northeast>
     <lat>33.7571500</lat>
     <lng>-118.3072230</lng>
    </northeast>
   </viewport>
  </geometry>
  <partial_match>true</partial_match>
 </result>
</GeocodeResponse>";
        
        public const string AutoCompleteRawResponse = @"
<GeocodeResponse>
 <status>OK</status>
 <result>
  <type>street_address</type>
  <formatted_address>1445 Brett Avenue, Oakdale, CA 95361, USA</formatted_address>
  <address_component>
   <long_name>1445</long_name>
   <short_name>1445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Brett Avenue</long_name>
   <short_name>Brett Ave</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Oakdale</long_name>
   <short_name>Oakdale</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Stanislaus County</long_name>
   <short_name>Stanislaus County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>95361</long_name>
   <short_name>95361</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>37.7737576</lat>
    <lng>-120.8340137</lng>
   </location>
   <location_type>RANGE_INTERPOLATED</location_type>
   <viewport>
    <southwest>
     <lat>37.7724151</lat>
     <lng>-120.8353669</lng>
    </southwest>
    <northeast>
     <lat>37.7751130</lat>
     <lng>-120.8326690</lng>
    </northeast>
   </viewport>
   <bounds>
    <southwest>
     <lat>37.7737576</lat>
     <lng>-120.8340222</lng>
    </southwest>
    <northeast>
     <lat>37.7737705</lat>
     <lng>-120.8340137</lng>
    </northeast>
   </bounds>
  </geometry>
  <partial_match>true</partial_match>
 </result>
 <result>
  <type>street_address</type>
  <formatted_address>1445 Brett Place, San Pedro, CA 90732, USA</formatted_address>
  <address_component>
   <long_name>1445</long_name>
   <short_name>1445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Brett Place</long_name>
   <short_name>Brett Pl</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Harbor</long_name>
   <short_name>Harbor</short_name>
   <type>neighborhood</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>San Pedro</long_name>
   <short_name>San Pedro</short_name>
   <type>sublocality_level_1</type>
   <type>sublocality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Los Angeles</long_name>
   <short_name>Los Angeles</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Los Angeles County</long_name>
   <short_name>Los Angeles County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>90732</long_name>
   <short_name>90732</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>33.7558010</lat>
    <lng>-118.3085720</lng>
   </location>
   <location_type>ROOFTOP</location_type>
   <viewport>
    <southwest>
     <lat>33.7544520</lat>
     <lng>-118.3099210</lng>
    </southwest>
    <northeast>
     <lat>33.7571500</lat>
     <lng>-118.3072230</lng>
    </northeast>
   </viewport>
  </geometry>
  <partial_match>true</partial_match>
 </result>
 <result>
  <type>street_address</type>
  <formatted_address>1445 Brett 1000 Court, Pinole, CA 94564, USA</formatted_address>
  <address_component>
   <long_name>1445</long_name>
   <short_name>1445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Brett Court</long_name>
   <short_name>Brett Ct</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Pinole</long_name>
   <short_name>Pinole</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Contra Costa County</long_name>
   <short_name>Contra Costa County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>94564</long_name>
   <short_name>94564</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>38.0083602</lat>
    <lng>-122.2983138</lng>
   </location>
   <location_type>RANGE_INTERPOLATED</location_type>
   <viewport>
    <southwest>
     <lat>38.0070185</lat>
     <lng>-122.2996628</lng>
    </southwest>
    <northeast>
     <lat>38.0097164</lat>
     <lng>-122.2969648</lng>
    </northeast>
   </viewport>
   <bounds>
    <southwest>
     <lat>38.0083602</lat>
     <lng>-122.2983138</lng>
    </southwest>
    <northeast>
     <lat>38.0083747</lat>
     <lng>-122.2983138</lng>
    </northeast>
   </bounds>
  </geometry>
  <partial_match>true</partial_match>
 </result>
 <result>
  <type>street_address</type>
  <formatted_address>1445 Bret Hill Court, San Jose, CA 95120, USA</formatted_address>
  <address_component>
   <long_name>1445</long_name>
   <short_name>1445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Bret Hill Court</long_name>
   <short_name>Bret Hill Ct</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Shadow Brook II</long_name>
   <short_name>Shadow Brook II</short_name>
   <type>neighborhood</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>San Jose</long_name>
   <short_name>San Jose</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Santa Clara County</long_name>
   <short_name>Santa Clara County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>95120</long_name>
   <short_name>95120</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>37.2116050</lat>
    <lng>-121.8462361</lng>
   </location>
   <location_type>RANGE_INTERPOLATED</location_type>
   <viewport>
    <southwest>
     <lat>37.2102579</lat>
     <lng>-121.8475940</lng>
    </southwest>
    <northeast>
     <lat>37.2129558</lat>
     <lng>-121.8448960</lng>
    </northeast>
   </viewport>
   <bounds>
    <southwest>
     <lat>37.2116050</lat>
     <lng>-121.8462539</lng>
    </southwest>
    <northeast>
     <lat>37.2116087</lat>
     <lng>-121.8462361</lng>
    </northeast>
   </bounds>
  </geometry>
  <partial_match>true</partial_match>
 </result>
 <result>
  <type>street_address</type>
  <formatted_address>1445 Bret Cove Court, San Jose, CA 95120, USA</formatted_address>
  <address_component>
   <long_name>1445</long_name>
   <short_name>11000 445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Bret Cove Court</long_name>
   <short_name>Bret Cove Ct</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Graystone</long_name>
   <short_name>Graystone</short_name>
   <type>neighborhood</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>San Jose</long_name>
   <short_name>San Jose</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Santa Clara County</long_name>
   <short_name>Santa Clara County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>95120</long_name>
   <short_name>95120</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>37.2113224</lat>
    <lng>-121.8456100</lng>
   </location>
   <location_type>RANGE_INTERPOLATED</location_type>
   <viewport>
    <southwest>
     <lat>37.2099691</lat>
     <lng>-121.8469515</lng>
    </southwest>
    <northeast>
     <lat>37.2126671</lat>
     <lng>-121.8442536</lng>
    </northeast>
   </viewport>
   <bounds>
    <southwest>
     <lat>37.2113138</lat>
     <lng>-121.8456100</lng>
    </southwest>
    <northeast>
     <lat>37.2113224</lat>
     <lng>-121.8455951</lng>
    </northeast>
   </bounds>
  </geometry>
  <partial_match>true</partial_match>
 </result>
 <result>
  <type>street_address</type>
  <formatted_address>1445 Brett Lane, Modesto, CA 95358, USA</formatted_address>
  <address_component>
   <long_name>1445</long_name>
   <short_name>1445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Brett Lane</long_name>
   <short_name>Brett Ln</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Modesto</long_name>
   <short_name>Modesto</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Stanislaus County</long_name>
   <short_name>Stanislaus County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>95358</long_name>
   <short_name>95358</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>37.6519329</lat>
    <lng>-121.0325718</lng>
   </location>
   <location_type>RANGE_INTERPOLATED</location_type>
   <viewport>
    <southwest>
     <lat>37.6505773</lat>
     <lng>-121.0339248</lng>
    </southwest>
    <northeast>
     <lat>37.6532753</lat>
     <lng>-121.0312268</lng>
    </northeast>
   </viewport>
   <bounds>
    <southwest>
     <lat>37.6519197</lat>
     <lng>-121.0325798</lng>
    </southwest>
    <northeast>
     <lat>37.6519329</lat>
     <lng>-121.0325718</lng>
    </northeast>
   </bounds>
  </geometry>
  <partial_match>true</partial_match>
 </result>
 <result>
  <type>street_address</type>
  <formatted_address>1445 Brett Avenue, Rohnert Park, CA 94928, USA</formatted_address>
  <address_component>
   <long_name>1445</long_name>
   <short_name>1445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Brett Avenue</long_name>
   <short_name>Brett Ave</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Rohnert Park</long_name>
   <short_name>Rohnert Park</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Sonoma County</long_name>
   <short_name>Sonoma County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>94928</long_name>
   <short_name>94928</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>38.3331913</lat>
    <lng>-122.6930416</lng>
   </location>
   <location_type>RANGE_INTERPOLATED</location_type>
   <viewport>
    <southwest>
     <lat>38.3318455</lat>
     <lng>-122.6943823</lng>
    </southwest>
    <northeast>
     <lat>38.3345435</lat>
     <lng>-122.6916843</lng>
    </northeast>
   </viewport>
   <bounds>
    <southwest>
     <lat>38.3331913</lat>
     <lng>-122.6930416</lng>
    </southwest>
    <northeast>
     <lat>38.3331977</lat>
     <lng>-122.6930250</lng>
    </northeast>
   </bounds>
  </geometry>
  <partial_match>true</partial_match>
 </result>
 <result>
  <type>street_address</type>
  <formatted_address>1445 Brett Court, Modesto, CA 95358, USA</formatted_address>
  <address_component>
   <long_name>1445</long_name>
   <short_name>1445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Brett Court</long_name>
   <short_name>Brett Ct</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Modesto</long_name>
   <short_name>Modesto</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Stanislaus County</long_name>
   <short_name>Stanislaus County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>95358</long_name>
   <short_name>95358</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>37.6518625</lat>
    <lng>-121.0355699</lng>
   </location>
   <location_type>RANGE_INTERPOLATED</location_type>
   <viewport>
    <southwest>
     <lat>37.6505062</lat>
     <lng>-121.0369190</lng>
    </southwest>
    <northeast>
     <lat>37.6532042</lat>
     <lng>-121.0342210</lng>
    </northeast>
   </viewport>
   <bounds>
    <southwest>
     <lat>37.6518479</lat>
     <lng>-121.0355701</lng>
    </southwest>
    <northeast>
     <lat>37.6518625</lat>
     <lng>-121.0355699</lng>
    </northeast>
   </bounds>
  </geometry>
  <partial_match>true</partial_match>
 </result>
 <result>
  <type>route</type>
  <formatted_address>Bret Avenue, Cupertino, CA 95014, USA</formatted_address>
  <address_component>
   <long_name>Bret Avenue</long_name>
   <short_name>Bret Ave</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Cupertino</long_name>
   <short_name>Cupertino</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Santa Clara County</long_name>
   <short_name>Santa Clara County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>95014</long_name>
   <short_name>95014</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>37.3194986</lat>
    <lng>-122.0043415</lng>
   </location>
   <location_type>GEOMETRIC_CENTER</location_type>
   <viewport>
    <southwest>
     <lat>37.3164519</lat>
     <lng>-122.0056900</lng>
    </southwest>
    <northeast>
     <lat>37.3229600</lat>
     <lng>-122.0029920</lng>
    </northeast>
   </viewport>
   <bounds>
    <southwest>
     <lat>37.3164519</lat>
     <lng>-122.0046880</lng>
    </southwest>
    <northeast>
     <lat>37.3229600</lat>
     <lng>-122.0039940</lng>
    </northeast>
   </bounds>
  </geometry>
  <partial_match>true</partial_match>
 </result>
 <result>
  <type>route</type>
  <formatted_address>Bret Avenue, San Rafael, CA 94901, USA</formatted_address>
  <address_component>
   <long_name>Bret Avenue</long_name>
   <short_name>Bret Ave</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>San Rafael</long_name>
   <short_name>San Rafael</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Marin County</long_name>
   <short_name>Marin County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>94901</long_name>
   <short_name>94901</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>37.9622169</lat>
    <lng>-122.5188474</lng>
   </location>
   <location_type>GEOMETRIC_CENTER</location_type>
   <viewport>
    <southwest>
     <lat>37.9609010</lat>
     <lng>-122.5207120</lng>
    </southwest>
    <northeast>
     <lat>37.9635990</lat>
     <lng>-122.5175601</lng>
    </northeast>
   </viewport>
   <bounds>
    <southwest>
     <lat>37.9618930</lat>
     <lng>-122.5207120</lng>
    </southwest>
    <northeast>
     <lat>37.9626070</lat>
     <lng>-122.5175601</lng>
    </northeast>
   </bounds>
  </geometry>
  <partial_match>true</partial_match>
 </result>
</GeocodeResponse>";

        public const string AutoCompleteRawResponseNoAddress = @"
<GeocodeResponse>
 <status>OK</status>
 </GeocodeResponse>";

        public const string GeocodeAddressRawResponse = @"
<GeocodeResponse>
 <status>OK</status>
 <result>
  <type>subpremise</type>
  <formatted_address>1445 Brett Place #112, San Pedro, CA 90732, USA</formatted_address>
  <address_component>
   <long_name>112</long_name>
   <short_name>112</short_name>
   <type>subpremise</type>
  </address_component>
  <address_component>
   <long_name>1445</long_name>
   <short_name>1445</short_name>
   <type>street_number</type>
  </address_component>
  <address_component>
   <long_name>Brett Place</long_name>
   <short_name>Brett Pl</short_name>
   <type>route</type>
  </address_component>
  <address_component>
   <long_name>Harbor</long_name>
   <short_name>Harbor</short_name>
   <type>neighborhood</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>San Pedro</long_name>
   <short_name>San Pedro</short_name>
   <type>sublocality_level_1</type>
   <type>sublocality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Los Angeles</long_name>
   <short_name>Los Angeles</short_name>
   <type>locality</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>Los Angeles County</long_name>
   <short_name>Los Angeles County</short_name>
   <type>administrative_area_level_2</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>California</long_name>
   <short_name>CA</short_name>
   <type>administrative_area_level_1</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>United States</long_name>
   <short_name>US</short_name>
   <type>country</type>
   <type>political</type>
  </address_component>
  <address_component>
   <long_name>90732</long_name>
   <short_name>90732</short_name>
   <type>postal_code</type>
  </address_component>
  <geometry>
   <location>
    <lat>33.7558010</lat>
    <lng>-118.3085720</lng>
   </location>
   <location_type>ROOFTOP</location_type>
   <viewport>
    <southwest>
     <lat>33.7544520</lat>
     <lng>-118.3099210</lng>
    </southwest>
    <northeast>
     <lat>33.7571500</lat>
     <lng>-118.3072230</lng>
    </northeast>
   </viewport>
  </geometry>
  <partial_match>true</partial_match>
 </result>
</GeocodeResponse>";

        public const string GeocodeAddressRawResponseNoAddress = @"
<GeocodeResponse>
 <status>OK</status>
 <result>
  <type>subpremise</type>
  <formatted_address></formatted_address>
  <geometry>
   <location>
    <lat>33.7558010</lat>
    <lng>-118.3085720</lng>
   </location>
   <location_type>ROOFTOP</location_type>
   <viewport>
    <southwest>
     <lat>33.7544520</lat>
     <lng>-118.3099210</lng>
    </southwest>
    <northeast>
     <lat>33.7571500</lat>
     <lng>-118.3072230</lng>
    </northeast>
   </viewport>
  </geometry>
  <partial_match>true</partial_match>
 </result>
</GeocodeResponse>";

        #endregion

        public static DataResponse GetAutomcompleteDataResponse(bool isInvalid = false)
        {
            return new DataResponse(AutoCompleteRawResponse, HttpStatusCode.OK, !isInvalid);
        }

        public static DataResponse GetAutomcompleteDataResponseNoAddress()
        {
            return new DataResponse(AutoCompleteRawResponseNoAddress, HttpStatusCode.OK, true);
        }

        public static DataResponse GetValidateAddressDataResponse(bool isInvalid = false)
        {
            return new DataResponse(ValidateAddressRawResponse, HttpStatusCode.OK, !isInvalid);
        }

        public static DataResponse GetValidateAddressDataResponseNoAddress()
        {
            return new DataResponse(ValidateAddressRawResponseNoAddress, HttpStatusCode.OK, true);
        }

        public static DataResponse GetGeocodeAddressDataResponse(bool isInvalid = false)
        {
            return new DataResponse(GeocodeAddressRawResponse, HttpStatusCode.OK, !isInvalid);
        }

        public static DataResponse GetGeocodeAddressDataResponseNoAddress()
        {
            return new DataResponse(GeocodeAddressRawResponseNoAddress, HttpStatusCode.OK, true);
        }

        public static BinaryDataResponse GetMapDataResponse(bool isInvalid = false)
        {
            return new BinaryDataResponse(new byte[1024], HttpStatusCode.OK, !isInvalid);
        }

        public static AutocompleteSuggestionModel GetAutocompleteSuggestionModel()
        {
            return new AutocompleteSuggestionModel
            {
                Address = GetAddressModel(),
                Location = GetAutocompleteLocationModel()
            };
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
                FormattedAddress = "1445 Brett Place, San Pedro, CA 90732, USA",
                UnitOrApartment = string.Empty,
                UnitsOrApartments = null
            };
        }

        public static LocationModel GetLocationModel()
        {
            return new LocationModel
            {
                Latitude = 33.7558010,
                Longitude = -118.3085720
            };
        }

        public static LocationModel GetAutocompleteLocationModel()
        {
            return new LocationModel
            {
                Latitude = 37.7737576,
                Longitude = -120.8340137
            };
        }
    }
}
