using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace CES.CoreApi.CallLog.Tools
{
    /// <summary>
    /// Added for extension methods for strings
    /// </summary>
    public static class ExtensionMethods_String
    {
        /// <summary>
        /// Returns the leftmost of a sustring
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string arg, int length)
        {
            return StringTools.vbLeft(arg, length);
        }

        /// <summary>
        /// Returns the rightmost of a sustring
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string arg, int length)
        {
            //@@2010-03-22 lb SRC# 730911 Created
            return StringTools.vbRight(arg, length); //@@2014-04-15 lb SCR# 1975411 Fixed bug
        }

        /// <summary>
        /// Emulates the built-in Mid funcion of Visual Basic with start and length arguments
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Mid(this string arg, int start, int length)
        {
            //@@2010-03-22 lb SRC#  2011211 Created
            return StringTools.vbMid(arg, start, length);
        }

        /// <summary>
        /// Emulates the built-in Mid funcion of Visual Basic with start argument
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string Mid(this string arg, int start)
        {
            //@@2010-03-22 lb SRC#  2011211 Created
            return StringTools.vbMid(arg, start);
        }

        /// <summary>
        /// Reverses a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Reverse(this string value)
        {
            if (value == "") return "";

            char[] chars = value.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// Removes invalid characters in a path and replaces it with the specifed character.
        /// </summary>
        /// <param name="value">the string where the characters will be removed/replaced</param>
        /// <param name="replaceWith">The character to replace with</param>
        /// <returns></returns>
        /// <remarks>This method is not guaranteed to contain the complete set of characters that are invalid in file and directory names.
        /// The full set of invalid characters can vary by file system. For example, on Windows-based desktop platforms, invalid path characters
        /// might include ASCII/Unicode characters 1 through 31, as well as quote ("), less than (&lt;), greater than (&gt;), pipe (|),
        /// backspace (\b), null (\0) and tab (\t).</remarks>
        public static string RemoveInvalidCharsInFileName(this string value, char replaceWith)
        {
            //@@2011-03-02 lb SCR# 1000911 Created
            char[] invalidChars = Path.GetInvalidFileNameChars();
            StringBuilder sb = new StringBuilder(value);

            foreach (char invalidChar in invalidChars)
                sb.Replace(invalidChar, replaceWith);

            return sb.ToString();
        }

        /// <summary>
        /// Removes invalid characters in a path and replaces it with a space character.
        /// </summary>
        /// <param name="value">the string where the characters will be replaced</param>
        /// <returns></returns>
        /// <remarks>This method is not guaranteed to contain the complete set of characters that are invalid in file and directory names.
        /// The full set of invalid characters can vary by file system. For example, on Windows-based desktop platforms, invalid path characters
        /// might include ASCII/Unicode characters 1 through 31, as well as quote ("), less than (&lt;), greater than (&gt;), pipe (|),
        /// backspace (\b), null (\0) and tab (\t).</remarks>
        public static string RemoveInvalidCharsInFileName(this string value)
        {
            //@@2011-03-02 lb SCR# 1000911 Created
            return RemoveInvalidCharsInFileName(value, ' ');
        }

        /// <summary>
        /// Indicates if the string matches the regular expression indicated in the argument
        /// </summary>
        /// <param name="arg">The source string which is under analisys</param>
        /// <param name="regexExp">The regular expression applied to the source string </param>
        /// <param name="options">Options applied to the regular expression</param>
        /// <returns></returns>
        public static bool Matches(this string arg, string regexExp, RegexOptions options)
        {
            //@@2011-10-11 lb SCR SCR# 1215911 Created
            if (arg == "") return false;

            Regex regex = new Regex(regexExp, options);
            return regex.IsMatch(arg);
        }

        /// <summary>
        /// Indicates if the string matches the regular expression indicated in the argument
        /// </summary>
        /// <param name="arg">The source string which is under analisys</param>
        /// <param name="regexExp">The regular expression applied to the source string </param>
        /// <returns></returns>
        public static bool Matches(this string arg, string regexExp)
        {
            //@@2011-10-11 lb SCR SCR# 1215911 Created
            if (arg == "") return false;

            Regex regex = new Regex(regexExp);
            return regex.IsMatch(arg);
        }

        /// <summary>
        /// Returns a DBNull object if the value is an empty string
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static object DBNullIfEmpty(this string arg)
        {
            //@@2013-11-26 lb SCR# 1933411 Created

            if (arg == "")
                return DBNull.Value;
            else
                return arg;
        }

        /// <summary>
        /// Converts an expression from pascal/camel convention to sentence case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSentenceCase(this string arg)
        {
            //@@2014-03-18 lb SCR# 1961411 Created
            return Regex.Replace(arg, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        /// <summary>
        /// Deserializes the string into an object of T type, Which must be decoreated with DataContract and Member Atributtes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static T DeserializeFromJson<T>(this string arg)
        {
            //@@2015-07-24 lb SCR# 2367511 Added to provide a utility function
            var jsonSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));

            byte[] streamBytes = Encoding.UTF8.GetBytes(arg);
            T obj;

            using (MemoryStream ms = new MemoryStream(streamBytes))
            {
                obj = (T)jsonSerializer.ReadObject(ms);
            }

            return obj;
        }
    }
}
