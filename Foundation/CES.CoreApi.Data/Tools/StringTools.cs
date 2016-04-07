using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CES.CoreApi.Data.Tools
{
    public static class StringTools
    {
        private const string SecurityCredentialReplacementTemplate = "{0}=********;";
        private const string SecurityCredentialRegex = "\\s*=([^;]*)(?:$|;)";

        public static string RemoveSecurityCredentials(this string input)
        {
            var securityQualifiers = new[] {"password"}; //possible values are { "user", "uid", "password", "pwd", "user id" } 

            return securityQualifiers.Aggregate(input,
                (current, qualifier) =>
                    Regex.Replace(current, qualifier + SecurityCredentialRegex, 
                    string.Format(CultureInfo.InvariantCulture, SecurityCredentialReplacementTemplate, qualifier),
                        RegexOptions.IgnoreCase));
        }
    }
}