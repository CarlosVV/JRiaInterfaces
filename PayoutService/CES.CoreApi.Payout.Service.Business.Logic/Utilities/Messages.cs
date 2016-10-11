using CES.CoreApi.Payout.Service.Business.Logic.Properties;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;


namespace CES.CoreApi.Payout.Service.Business.Logic.Utilities
{

    /// <summary>
    /// Resource manager to allow all messages to be stored in resource.resx files
    /// These files can then be added and modified to return all messages in 
    /// different languages.
    /// 
    /// Author  : David Go
    /// </summary>
    class Messages
    {
        //TODO: CoreAPI: Is this something that should reside elsewhere in the coreAPI archetecture?

        private static readonly Log4NetProxy _logger = new Log4NetProxy();

        //TODO: Test multi-langauge with this set:
        //private static ResourceManager ms_languageResource = new ResourceManager(
        //     "CES.CoreApi.Payout.Service.Business.Logic", Assembly.GetExecutingAssembly());
        private static ResourceManager ms_languageResource = Resources.ResourceManager;

        private static string ms_defaultLanguage = "en-US";



        /// <summary>
        /// Get the messsage string in the language of the specified locale.
        /// </summary>
        /// <param name="msgStringName"></param>
        /// <param name="locale"></param>
        /// <returns></returns>
        public static string S_GetMessage(string msgStringName, string locale)
        {
            SetLanguage(locale);
            return ms_languageResource.GetString(msgStringName);
        }

        /// <summary>
        /// Get the messsage string in the default language. 
        /// </summary>
        /// <param name="msgStringName"></param>
        /// <param name="locale"></param>
        /// <returns></returns>
        public static string S_GetMessage(string msgStringName)
        {
            SetLanguage(ms_defaultLanguage);
            return ms_languageResource.GetString(msgStringName);
        }


        /// <summary>
        /// These language settings work with the resouce.resx and 
        /// resource.lang-COUNTRY.resx files.
        /// The locale should be specified like this: 
        ///     "en-US" is United States English.
        /// If the specified locale is not found then the default 
        /// value in resource.resx is used.
        /// </summary>
        /// <param name="locale"></param>
        private static void SetLanguage(string locale)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(locale);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(locale);
            //The default resource file does not seem to be working, so explicitly set the default language to en-US
            try
            {
                //If currently set language matches the requested one, all is OK.
                string currentLang = ms_languageResource.GetString("LanguageInUse");//This will cause an exception if lang not supported.
                if (currentLang.Equals(locale))
                {
                    //For debug:
                    //ms_log.Warn("Language set to : " + currentLang);
                }
                else
                {
                    //If the languages do not match, then use the default.                    
                    throw new Exception(); //forces the default setting in the catch block.
                }
            }
            catch (Exception)
            {
                _logger.PublishWarning("Language not supported [" + locale + "].  Set to default: " + ms_defaultLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(ms_defaultLanguage);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ms_defaultLanguage);
            }
        }


    }
}
