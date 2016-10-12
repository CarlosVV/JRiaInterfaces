using System;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CES.CoreApi.CallLog.Tools
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

        #region Visual Basic emulation methods 

        //Emulates the built-in Left funcion of Visual Basic
        public static string vbLeft(string strIn, int length)
        {
            if (length < 0) throw new Exception("Length cannot be negative");
            if (strIn == null || strIn.Length == 0) return "";
            if (length == 0) return "";
            if (length >= strIn.Length) return strIn;
            return strIn.Substring(0, length);
        }

        //@@2007-08-24 SCR# 443711 Added support routine
        //Emulates the built-in Rigth funcion of Visual Basic
        public static string vbRight(string strIn, int length)
        {
            if (length < 0) throw new Exception("Length cannot be negative");
            if (strIn == null || strIn.Length == 0) return "";
            if (length == 0) return "";
            if (length >= strIn.Length) return strIn;
            return strIn.Substring(strIn.Length - length);
        }

        //Emulates the built-in Mid funcion of Visual Basic with start and length arguments
        public static string vbMid(string strIn, int start, int length)
        {
            if (start < 1) throw new Exception("Start index cannot be negative");
            if (length < 0) throw new Exception("Length cannot be negative");
            if (strIn == null || strIn.Length == 0) return "";
            if (length == 0) return "";
            if (start > strIn.Length) return "";
            if (start + length - 1 > strIn.Length)
                return strIn.Substring(start - 1);
            else
                return strIn.Substring(start - 1, length);
        }

        //Emulates the built-in Mid funcion of Visual Basic with start argument
        public static string vbMid(string strIn, int start)
        {
            if (start < 1) throw new Exception("Start index cannot be negative");
            if (strIn == null || strIn.Length == 0) return "";
            if (start > strIn.Length)
                return "";
            else
                return strIn.Substring(start - 1);
        }

        //This function emulates the Asc function like VB.
        //It cannot be casted simply to int, because, although visual basic for applications
        //uses unicode string, this function returns the byte value for current code-page
        //encoding
        public static int vbAsc(char c)
        {
            byte[] curCodePageBytes = new byte[1];
            Encoder curCodePageEnc = Encoding.Default.GetEncoder();

            curCodePageEnc.GetBytes(new char[] { c }, 0, 1, curCodePageBytes, 0, true);

            return (int)curCodePageBytes[0];
        }

        //Returns the encoding code-page for the first character of the argument
        //function fails, as in visual basic for applications if the argument supplied
        //is null or it is an empty string
        public static int vbAsc(string str)
        {
            return vbAsc(str[0]);
        }

        /// <summary>
        /// Returns the unicode char character corresponding to c (mapped in de operating system code page
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char vbChr(int c)
        {
            byte[] curCodePageBytes = new byte[2];
            char[] chars = null;

            curCodePageBytes[0] = (byte)(c & 0x00FF);
            curCodePageBytes[1] = (byte)(((uint)c & 0xFF00) >> 8);

            Decoder curCodePageDec = Encoding.Default.GetDecoder();

            chars = new char[curCodePageDec.GetCharCount(curCodePageBytes, 0, 2)];

            curCodePageDec.GetChars(curCodePageBytes, 0, 2, chars, 0, true);

            return chars[0];
        }

        #endregion
    }
}