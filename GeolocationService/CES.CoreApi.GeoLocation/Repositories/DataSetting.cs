using System.Collections.Generic;
using System.Text;

namespace CES.CoreApi.GeoLocation.Repositories
{
	public class CountryCode
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class DataSetting
	{
		public static string GetCode(string countryName)
		{
			if (string.IsNullOrEmpty(countryName))
				return string.Empty;

			var collection = GetCountries();
			foreach (var item in collection)
			{
				if (item.name.Equals(countryName, System.StringComparison.OrdinalIgnoreCase))
					return item.code;
			}

			return countryName;
		}
		
		public static List<CountryCode> GetCountries()
		{

			var countries= 	Newtonsoft.Json.JsonConvert.DeserializeObject<List<CountryCode>>(GetCountryCodes());
			return countries;
		}
		private static string GetCountryCodes() {
			StringBuilder sb = new StringBuilder();
			sb.Append("[\"name\": \"Afghanistan\", \"code\": \"AF\"},");
			sb.Append("\name\": \"Åland Islands\", \"code\": \"AX\"},");
			sb.Append("\name\": \"Albania\", \"code\": \"AL\"},");
			sb.Append("\name\": \"Algeria\", \"code\": \"DZ\"},");
			sb.Append("\name\": \"American Samoa\", \"code\": \"AS\"},");
			sb.Append("\name\": \"AndorrA\", \"code\": \"AD\"},");
			sb.Append("\name\": \"Angola\", \"code\": \"AO\"},");
			sb.Append("\name\": \"Anguilla\", \"code\": \"AI\"},");
			sb.Append("\name\": \"Antarctica\", \"code\": \"AQ\"},");
			sb.Append("\name\": \"Antigua and Barbuda\", \"code\": \"AG\"},");
			sb.Append("\name\": \"Argentina\", \"code\": \"AR\"},");
			sb.Append("\name\": \"Armenia\", \"code\": \"AM\"},");
			sb.Append("\name\": \"Aruba\", \"code\": \"AW\"},");
			sb.Append("\name\": \"Australia\", \"code\": \"AU\"},");
			sb.Append("\name\": \"Austria\", \"code\": \"AT\"},");
			sb.Append("\name\": \"Azerbaijan\", \"code\": \"AZ\"},");
			sb.Append("\name\": \"Bahamas\", \"code\": \"BS\"},");
			sb.Append("\name\": \"Bahrain\", \"code\": \"BH\"},");
			sb.Append("\name\": \"Bangladesh\", \"code\": \"BD\"},");
			sb.Append("\name\": \"Barbados\", \"code\": \"BB\"},");
			sb.Append("\name\": \"Belarus\", \"code\": \"BY\"},");
			sb.Append("\name\": \"Belgium\", \"code\": \"BE\"},");
			sb.Append("\name\": \"Belize\", \"code\": \"BZ\"},");
			sb.Append("\name\": \"Benin\", \"code\": \"BJ\"},");
			sb.Append("\name\": \"Bermuda\", \"code\": \"BM\"},");
			sb.Append("\name\": \"Bhutan\", \"code\": \"BT\"},");
			sb.Append("\name\": \"Bolivia\", \"code\": \"BO\"},");
			sb.Append("\name\": \"Bosnia and Herzegovina\", \"code\": \"BA\"},");
			sb.Append("\name\": \"Botswana\", \"code\": \"BW\"},");
			sb.Append("\name\": \"Bouvet Island\", \"code\": \"BV\"},");
			sb.Append("\name\": \"Brazil\", \"code\": \"BR\"},");
			sb.Append("\name\": \"British Indian Ocean Territory\", \"code\": \"IO\"},");
			sb.Append("\name\": \"Brunei Darussalam\", \"code\": \"BN\"},");
			sb.Append("\name\": \"Bulgaria\", \"code\": \"BG\"},");
			sb.Append("\name\": \"Burkina Faso\", \"code\": \"BF\"},");
			sb.Append("\name\": \"Burundi\", \"code\": \"BI\"},");
			sb.Append("\name\": \"Cambodia\", \"code\": \"KH\"},");
			sb.Append("\name\": \"Cameroon\", \"code\": \"CM\"},");
			sb.Append("\name\": \"Canada\", \"code\": \"CA\"},");
			sb.Append("\name\": \"Cape Verde\", \"code\": \"CV\"},");
			sb.Append("\name\": \"Cayman Islands\", \"code\": \"KY\"},");
			sb.Append("\name\": \"Central African Republic\", \"code\": \"CF\"},");
			sb.Append("\name\": \"Chad\", \"code\": \"TD\"},");
			sb.Append("\name\": \"Chile\", \"code\": \"CL\"},");
			sb.Append("\name\": \"China\", \"code\": \"CN\"},");
			sb.Append("\name\": \"Christmas Island\", \"code\": \"CX\"},");
			sb.Append("\name\": \"Cocos (Keeling) Islands\", \"code\": \"CC\"},");
			sb.Append("\name\": \"Colombia\", \"code\": \"CO\"},");
			sb.Append("\name\": \"Comoros\", \"code\": \"KM\"},");
			sb.Append("\name\": \"Congo\", \"code\": \"CG\"},");
			sb.Append("\name\": \"Congo, The Democratic Republic of the\", \"code\": \"CD\"},");
			sb.Append("\name\": \"Cook Islands\", \"code\": \"CK\"},");
			sb.Append("\name\": \"Costa Rica\", \"code\": \"CR\"},");
			sb.Append("\name\": \"Cote D\"Ivoire\", \"code\": \"CI\"},");
			sb.Append("\name\": \"Croatia\", \"code\": \"HR\"},");
			sb.Append("\name\": \"Cuba\", \"code\": \"CU\"},");
			sb.Append("\name\": \"Cyprus\", \"code\": \"CY\"},");
			sb.Append("\name\": \"Czech Republic\", \"code\": \"CZ\"},");
			sb.Append("\name\": \"Denmark\", \"code\": \"DK\"},");
			sb.Append("\name\": \"Djibouti\", \"code\": \"DJ\"},");
			sb.Append("\name\": \"Dominica\", \"code\": \"DM\"},");
			sb.Append("\name\": \"Dominican Republic\", \"code\": \"DO\"},");
			sb.Append("\name\": \"Ecuador\", \"code\": \"EC\"},");
			sb.Append("\name\": \"Egypt\", \"code\": \"EG\"},");
			sb.Append("\name\": \"El Salvador\", \"code\": \"SV\"},");
			sb.Append("\name\": \"Equatorial Guinea\", \"code\": \"GQ\"},");
			sb.Append("\name\": \"Eritrea\", \"code\": \"ER\"},");
			sb.Append("\name\": \"Estonia\", \"code\": \"EE\"},");
			sb.Append("\name\": \"Ethiopia\", \"code\": \"ET\"},");
			sb.Append("\name\": \"Falkland Islands (Malvinas)\", \"code\": \"FK\"},");
			sb.Append("\name\": \"Faroe Islands\", \"code\": \"FO\"},");
			sb.Append("\name\": \"Fiji\", \"code\": \"FJ\"},");
			sb.Append("\name\": \"Finland\", \"code\": \"FI\"},");
			sb.Append("\name\": \"France\", \"code\": \"FR\"},");
			sb.Append("\name\": \"French Guiana\", \"code\": \"GF\"},");
			sb.Append("\name\": \"French Polynesia\", \"code\": \"PF\"},");
			sb.Append("\name\": \"French Southern Territories\", \"code\": \"TF\"},");
			sb.Append("\name\": \"Gabon\", \"code\": \"GA\"},");
			sb.Append("\name\": \"Gambia\", \"code\": \"GM\"},");
			sb.Append("\name\": \"Georgia\", \"code\": \"GE\"},");
			sb.Append("\name\": \"Germany\", \"code\": \"DE\"},");
			sb.Append("\name\": \"Ghana\", \"code\": \"GH\"},");
			sb.Append("\name\": \"Gibraltar\", \"code\": \"GI\"},");
			sb.Append("\name\": \"Greece\", \"code\": \"GR\"},");
			sb.Append("\name\": \"Greenland\", \"code\": \"GL\"},");
			sb.Append("\name\": \"Grenada\", \"code\": \"GD\"},");
			sb.Append("\name\": \"Guadeloupe\", \"code\": \"GP\"},");
			sb.Append("\name\": \"Guam\", \"code\": \"GU\"},");
			sb.Append("\name\": \"Guatemala\", \"code\": \"GT\"},");
			sb.Append("\name\": \"Guernsey\", \"code\": \"GG\"},");
			sb.Append("\name\": \"Guinea\", \"code\": \"GN\"},");
			sb.Append("\name\": \"Guinea-Bissau\", \"code\": \"GW\"},");
			sb.Append("\name\": \"Guyana\", \"code\": \"GY\"},");
			sb.Append("\name\": \"Haiti\", \"code\": \"HT\"},");
			sb.Append("\name\": \"Heard Island and Mcdonald Islands\", \"code\": \"HM\"},");
			sb.Append("\name\": \"Holy See (Vatican City State)\", \"code\": \"VA\"},");
			sb.Append("\name\": \"Honduras\", \"code\": \"HN\"},");
			sb.Append("\name\": \"Hong Kong\", \"code\": \"HK\"},");
			sb.Append("\name\": \"Hungary\", \"code\": \"HU\"},");
			sb.Append("\name\": \"Iceland\", \"code\": \"IS\"},");
			sb.Append("\name\": \"India\", \"code\": \"IN\"},");
			sb.Append("\name\": \"Indonesia\", \"code\": \"ID\"},");
			sb.Append("\name\": \"Iran, Islamic Republic Of\", \"code\": \"IR\"},");
			sb.Append("\name\": \"Iraq\", \"code\": \"IQ\"},");
			sb.Append("\name\": \"Ireland\", \"code\": \"IE\"},");
			sb.Append("\name\": \"Isle of Man\", \"code\": \"IM\"},");
			sb.Append("\name\": \"Israel\", \"code\": \"IL\"},");
			sb.Append("\name\": \"Italy\", \"code\": \"IT\"},");
			sb.Append("\name\": \"Jamaica\", \"code\": \"JM\"},");
			sb.Append("\name\": \"Japan\", \"code\": \"JP\"},");
			sb.Append("\name\": \"Jersey\", \"code\": \"JE\"},");
			sb.Append("\name\": \"Jordan\", \"code\": \"JO\"},");
			sb.Append("\name\": \"Kazakhstan\", \"code\": \"KZ\"},");
			sb.Append("\name\": \"Kenya\", \"code\": \"KE\"},");
			sb.Append("\name\": \"Kiribati\", \"code\": \"KI\"},");
			sb.Append("\name\": \"Korea, Democratic People\"S Republic of\", \"code\": \"KP\"},");
			sb.Append("\name\": \"Korea, Republic of\", \"code\": \"KR\"},");
			sb.Append("\name\": \"Kuwait\", \"code\": \"KW\"},");
			sb.Append("\name\": \"Kyrgyzstan\", \"code\": \"KG\"},");
			sb.Append("\name\": \"Lao People\"S Democratic Republic\", \"code\": \"LA\"},");
			sb.Append("\name\": \"Latvia\", \"code\": \"LV\"},");
			sb.Append("\name\": \"Lebanon\", \"code\": \"LB\"},");
			sb.Append("\name\": \"Lesotho\", \"code\": \"LS\"},");
			sb.Append("\name\": \"Liberia\", \"code\": \"LR\"},");
			sb.Append("\name\": \"Libyan Arab Jamahiriya\", \"code\": \"LY\"},");
			sb.Append("\name\": \"Liechtenstein\", \"code\": \"LI\"},");
			sb.Append("\name\": \"Lithuania\", \"code\": \"LT\"},");
			sb.Append("\name\": \"Luxembourg\", \"code\": \"LU\"},");
			sb.Append("\name\": \"Macao\", \"code\": \"MO\"},");
			sb.Append("\name\": \"Macedonia, The Former Yugoslav Republic of\", \"code\": \"MK\"},");
			sb.Append("\name\": \"Madagascar\", \"code\": \"MG\"},");
			sb.Append("\name\": \"Malawi\", \"code\": \"MW\"},");
			sb.Append("\name\": \"Malaysia\", \"code\": \"MY\"},");
			sb.Append("\name\": \"Maldives\", \"code\": \"MV\"},");
			sb.Append("\name\": \"Mali\", \"code\": \"ML\"},");
			sb.Append("\name\": \"Malta\", \"code\": \"MT\"},");
			sb.Append("\name\": \"Marshall Islands\", \"code\": \"MH\"},");
			sb.Append("\name\": \"Martinique\", \"code\": \"MQ\"},");
			sb.Append("\name\": \"Mauritania\", \"code\": \"MR\"},");
			sb.Append("\name\": \"Mauritius\", \"code\": \"MU\"},");
			sb.Append("\name\": \"Mayotte\", \"code\": \"YT\"},");
			sb.Append("\name\": \"Mexico\", \"code\": \"MX\"},");
			sb.Append("\name\": \"Micronesia, Federated States of\", \"code\": \"FM\"},");
			sb.Append("\name\": \"Moldova, Republic of\", \"code\": \"MD\"},");
			sb.Append("\name\": \"Monaco\", \"code\": \"MC\"},");
			sb.Append("\name\": \"Mongolia\", \"code\": \"MN\"},");
			sb.Append("\name\": \"Montserrat\", \"code\": \"MS\"},");
			sb.Append("\name\": \"Morocco\", \"code\": \"MA\"},");
			sb.Append("\name\": \"Mozambique\", \"code\": \"MZ\"},");
			sb.Append("\name\": \"Myanmar\", \"code\": \"MM\"},");
			sb.Append("\name\": \"Namibia\", \"code\": \"NA\"},");
			sb.Append("\name\": \"Nauru\", \"code\": \"NR\"},");
			sb.Append("\name\": \"Nepal\", \"code\": \"NP\"},");
			sb.Append("\name\": \"Netherlands\", \"code\": \"NL\"},");
			sb.Append("\name\": \"Netherlands Antilles\", \"code\": \"AN\"},");
			sb.Append("\name\": \"New Caledonia\", \"code\": \"NC\"},");
			sb.Append("\name\": \"New Zealand\", \"code\": \"NZ\"},");
			sb.Append("\name\": \"Nicaragua\", \"code\": \"NI\"},");
			sb.Append("\name\": \"Niger\", \"code\": \"NE\"},");
			sb.Append("\name\": \"Nigeria\", \"code\": \"NG\"},");
			sb.Append("\name\": \"Niue\", \"code\": \"NU\"},");
			sb.Append("\name\": \"Norfolk Island\", \"code\": \"NF\"},");
			sb.Append("\name\": \"Northern Mariana Islands\", \"code\": \"MP\"},");
			sb.Append("\name\": \"Norway\", \"code\": \"NO\"},");
			sb.Append("\name\": \"Oman\", \"code\": \"OM\"},");
			sb.Append("\name\": \"Pakistan\", \"code\": \"PK\"},");
			sb.Append("\name\": \"Palau\", \"code\": \"PW\"},");
			sb.Append("\name\": \"Palestinian Territory, Occupied\", \"code\": \"PS\"},");
			sb.Append("\name\": \"Panama\", \"code\": \"PA\"},");
			sb.Append("\name\": \"Papua New Guinea\", \"code\": \"PG\"},");
			sb.Append("\name\": \"Paraguay\", \"code\": \"PY\"},");
			sb.Append("\name\": \"Peru\", \"code\": \"PE\"},");
			sb.Append("\name\": \"Philippines\", \"code\": \"PH\"},");
			sb.Append("\name\": \"Pitcairn\", \"code\": \"PN\"},");
			sb.Append("\name\": \"Poland\", \"code\": \"PL\"},");
			sb.Append("\name\": \"Portugal\", \"code\": \"PT\"},");
			sb.Append("\name\": \"Puerto Rico\", \"code\": \"PR\"},");
			sb.Append("\name\": \"Qatar\", \"code\": \"QA\"},");
			sb.Append("\name\": \"Reunion\", \"code\": \"RE\"},");
			sb.Append("\name\": \"Romania\", \"code\": \"RO\"},");
			sb.Append("\name\": \"Russian Federation\", \"code\": \"RU\"},");
			sb.Append("\name\": \"RWANDA\", \"code\": \"RW\"},");
			sb.Append("\name\": \"Saint Helena\", \"code\": \"SH\"},");
			sb.Append("\name\": \"Saint Kitts and Nevis\", \"code\": \"KN\"},");
			sb.Append("\name\": \"Saint Lucia\", \"code\": \"LC\"},");
			sb.Append("\name\": \"Saint Pierre and Miquelon\", \"code\": \"PM\"},");
			sb.Append("\name\": \"Saint Vincent and the Grenadines\", \"code\": \"VC\"},");
			sb.Append("\name\": \"Samoa\", \"code\": \"WS\"},");
			sb.Append("\name\": \"San Marino\", \"code\": \"SM\"},");
			sb.Append("\name\": \"Sao Tome and Principe\", \"code\": \"ST\"},");
			sb.Append("\name\": \"Saudi Arabia\", \"code\": \"SA\"},");
			sb.Append("\name\": \"Senegal\", \"code\": \"SN\"},");
			sb.Append("\name\": \"Serbia and Montenegro\", \"code\": \"CS\"},");
			sb.Append("\name\": \"Seychelles\", \"code\": \"SC\"},");
			sb.Append("\name\": \"Sierra Leone\", \"code\": \"SL\"},");
			sb.Append("\name\": \"Singapore\", \"code\": \"SG\"},");
			sb.Append("\name\": \"Slovakia\", \"code\": \"SK\"},");
			sb.Append("\name\": \"Slovenia\", \"code\": \"SI\"},");
			sb.Append("\name\": \"Solomon Islands\", \"code\": \"SB\"},");
			sb.Append("\name\": \"Somalia\", \"code\": \"SO\"},");
			sb.Append("\name\": \"South Africa\", \"code\": \"ZA\"},");
			sb.Append("\name\": \"South Georgia and the South Sandwich Islands\", \"code\": \"GS\"},");
			sb.Append("\name\": \"Spain\", \"code\": \"ES\"},");
			sb.Append("\name\": \"Sri Lanka\", \"code\": \"LK\"},");
			sb.Append("\name\": \"Sudan\", \"code\": \"SD\"},");
			sb.Append("\name\": \"Suriname\", \"code\": \"SR\"},");
			sb.Append("\name\": \"Svalbard and Jan Mayen\", \"code\": \"SJ\"},");
			sb.Append("\name\": \"Swaziland\", \"code\": \"SZ\"},");
			sb.Append("\name\": \"Sweden\", \"code\": \"SE\"},");
			sb.Append("\name\": \"Switzerland\", \"code\": \"CH\"},");
			sb.Append("\name\": \"Syrian Arab Republic\", \"code\": \"SY\"},");
			sb.Append("\name\": \"Taiwan, Province of China\", \"code\": \"TW\"},");
			sb.Append("\name\": \"Tajikistan\", \"code\": \"TJ\"},");
			sb.Append("\name\": \"Tanzania, United Republic of\", \"code\": \"TZ\"},");
			sb.Append("\name\": \"Thailand\", \"code\": \"TH\"},");
			sb.Append("\name\": \"Timor-Leste\", \"code\": \"TL\"},");
			sb.Append("\name\": \"Togo\", \"code\": \"TG\"},");
			sb.Append("\name\": \"Tokelau\", \"code\": \"TK\"},");
			sb.Append("\name\": \"Tonga\", \"code\": \"TO\"},");
			sb.Append("\name\": \"Trinidad and Tobago\", \"code\": \"TT\"},");
			sb.Append("\name\": \"Tunisia\", \"code\": \"TN\"},");
			sb.Append("\name\": \"Turkey\", \"code\": \"TR\"},");
			sb.Append("\name\": \"Turkmenistan\", \"code\": \"TM\"},");
			sb.Append("\name\": \"Turks and Caicos Islands\", \"code\": \"TC\"},");
			sb.Append("\name\": \"Tuvalu\", \"code\": \"TV\"},");
			sb.Append("\name\": \"Uganda\", \"code\": \"UG\"},");
			sb.Append("\name\": \"Ukraine\", \"code\": \"UA\"},");
			sb.Append("\name\": \"United Arab Emirates\", \"code\": \"AE\"},");
			sb.Append("\name\": \"United Kingdom\", \"code\": \"GB\"},");
			sb.Append("\name\": \"United States\", \"code\": \"US\"},");
			sb.Append("\name\": \"United States Minor Outlying Islands\", \"code\": \"UM\"},");
			sb.Append("\name\": \"Uruguay\", \"code\": \"UY\"},");
			sb.Append("\name\": \"Uzbekistan\", \"code\": \"UZ\"},");
			sb.Append("\name\": \"Vanuatu\", \"code\": \"VU\"},");
			sb.Append("\name\": \"Venezuela\", \"code\": \"VE\"},");
			sb.Append("\name\": \"Viet Nam\", \"code\": \"VN\"},");
			sb.Append("\name\": \"Virgin Islands, British\", \"code\": \"VG\"},");
			sb.Append("\name\": \"Virgin Islands, U.S.\", \"code\": \"VI\"},");
			sb.Append("\name\": \"Wallis and Futuna\", \"code\": \"WF\"},");
			sb.Append("\name\": \"Western Sahara\", \"code\": \"EH\"},");
			sb.Append("\name\": \"Yemen\", \"code\": \"YE\"},");
			sb.Append("\name\": \"Zambia\", \"code\": \"ZM\"},");
			sb.Append("\name\": \"Zimbabwe\", \"code\": \"ZW\"}");
			sb.Append("]");

			return sb.ToString();
		}
	}
}
