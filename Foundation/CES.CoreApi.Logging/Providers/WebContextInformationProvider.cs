using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Providers
{
    public class WebContextInformationProvider 
    {
        private const string CompositeValueItemDelimiter = "\r\n";
        private const string CompositeValueItemNameValueDelimiter = ":";

        #region Public methods

        /// <summary>
        /// Adds web context information
        /// </summary>
        public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer)
        {
            if (HttpContext.Current != null)
            {
                AddGroup("HTTP HEADERS", () => HttpContext.Current.Request.Headers, exceptionLogDataContainer);
                AddGroup("SERVER VARIABLES", () => HttpContext.Current.Request.ServerVariables, exceptionLogDataContainer);
                AddRequestGroup("REQUEST", exceptionLogDataContainer);
                AddGroup("REQUEST QUERY STRING", () => HttpContext.Current.Request.QueryString, exceptionLogDataContainer);
                AddGroup("REQUEST FORM", () => HttpContext.Current.Request.Form, exceptionLogDataContainer);
            }
        }

        #endregion //Public methods

        #region Private methods

        /// <summary>
        /// Gets web application context parameters
        /// </summary>
        /// <param name="groupTitle">Title of the group to add</param>
        /// <param name="contextItemListProvider">Function which provide list of context items</param>
        /// <param name="exceptionLogDataContainer">Exception log data container</param>
        private static void AddGroup(string groupTitle, Func<NameValueCollection> contextItemListProvider,
                                     ExceptionLogDataContainer exceptionLogDataContainer)
        {
            //In case of RESTful service some context collections are not available with strange exception,
            //this function call prevent logic to get failed
            var contextItemList = GetContextCollection(contextItemListProvider);
            if (contextItemList == null || contextItemList.Count == 0) return;

            //Add new group
            var group = exceptionLogDataContainer.GetGroupByTitle(groupTitle);

            foreach (string name in contextItemList)
            {
                var currentName = name;
                //Get item value safely
                var itemValue = GetContextItemValue(() => contextItemList[currentName]);
                if (IsCompositeValue(itemValue))
                {
                    ParseCompositeValue(itemValue, group);
                }
                else
                {
                    group.AddItem(name, itemValue);
                }
            }
        }

        /// <summary>
        /// Gets context collection using safe way
        /// In case of RESTful services this functionality failing some time with strange exception
        /// </summary>
        /// <param name="contextItemListProvider">Function which provide context collection</param>
        /// <returns></returns>
        private static NameValueCollection GetContextCollection(Func<NameValueCollection> contextItemListProvider)
        {
            try
            {
                var list = contextItemListProvider();
                return list.Count == 0 ? null : list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets value from context collection using safe way
        /// In case of RESTful services this functionality failing some time with strange exception
        /// </summary>
        /// <param name="contextItemValueProvider">Function which provide value</param>
        /// <returns></returns>
        private static string GetContextItemValue(Func<string> contextItemValueProvider)
        {
            try
            {
                return contextItemValueProvider();
            }
            catch 
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Adds Request details group to the group list
        /// </summary>
        /// <param name="groupTitle">Group title</param>
        /// <param name="exceptionLogDataContainer">Exception log data container</param>
        private static void AddRequestGroup(string groupTitle, ExceptionLogDataContainer exceptionLogDataContainer)
        {
            var group = exceptionLogDataContainer.GetGroupByTitle(groupTitle);

            group.AddItem("IS_SECURE_CONNECTION", () => HttpContext.Current.Request.IsSecureConnection);
            group.AddItem("HTTP_METHOD", () => HttpContext.Current.Request.HttpMethod);
            group.AddItem("URL", () => HttpContext.Current.Request.Url);
            group.AddItem("URL_REFERRER", () => HttpContext.Current.Request.UrlReferrer);
            group.AddItem("IS_AUTHENTICATED", () => HttpContext.Current.Request.IsAuthenticated);
            group.AddItem("LOGON_USER_IDENTITY", () => HttpContext.Current.Request.LogonUserIdentity);
            group.AddItem("TOTAL_BYTES", () => HttpContext.Current.Request.TotalBytes);
        }

        /// <summary>
        /// Checks if value is a composite (contains multiple items delimited by \r\n)
        /// </summary>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        private static bool IsCompositeValue(string itemValue)
        {
            return itemValue.Contains(CompositeValueItemDelimiter);
        }

        /// <summary>
        /// Parses composite item value to collection of the log items
        /// </summary>
        /// <param name="compositeValue">Composite item value</param>
        /// <param name="group">Log item group </param>
        /// <returns></returns>
        private static void ParseCompositeValue(string compositeValue, ExceptionLogItemGroup group)
        {
            foreach (var itemParts in compositeValue.Split(CompositeValueItemDelimiter.ToCharArray())
                .Where(p => p.Length > 0)
                .Select(item => item.Split(CompositeValueItemNameValueDelimiter.ToCharArray()))
                .Where(itemParts => itemParts.Length == 2))
            {
                group.AddItem(itemParts[0], itemParts[1].Trim());
            }
        }

        #endregion //Private methods
    }
}