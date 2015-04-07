using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Providers
{
    public class WebApiCallInformationProvider : IWebApiCallInformationProvider
    {
        private const string CompositeValueItemDelimiter = "\r\n";
        private const string CompositeValueItemNameValueDelimiter = ":";
        private const string CompositeValueValueDelimiter = ";";

        public WebApiCallInformationProvider()
        {
            
        }

        public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, ExceptionLoggerContext context)
        {
            if (context == null || exceptionLogDataContainer == null) return;

            //AddRequestProperties("HTTP HEADERS", () => HttpContext.Current.Request.Headers, exceptionLogDataContainer);

            //AddRequestProperties(exceptionLogDataContainer, context);
            //AddRouteDetails(exceptionLogDataContainer, context);
            //AddRequestHeaders(exceptionLogDataContainer, context);

            //AddGroup("Request Headers", () => context.RequestContext.Url.Request.Headers, exceptionLogDataContainer);
            //AddGroup("HTTP HEADERS", () => HttpContext.Current.Request.Headers, exceptionLogDataContainer);
        }

        private static void AddRouteDetails(ExceptionLogDataContainer exceptionLogDataContainer, ExceptionLoggerContext context)
        {
            var group = exceptionLogDataContainer.GetGroupByTitle("Route");
            group.AddItem("Route Template", context.RequestContext.RouteData.Route.RouteTemplate);
            foreach (var key in context.RequestContext.RouteData.Values.Keys)
            {
                group.AddItem(key, context.RequestContext.RouteData.Values[key]);
            }
        }

        private static void AddRequestProperties(ExceptionLogDataContainer exceptionLogDataContainer, ExceptionLoggerContext context)
        {
            var group = exceptionLogDataContainer.GetGroupByTitle("Request Properties");
            //group.AddItem("Request URI", context.Request.RequestUri.AbsoluteUri);
            //group.AddItem("Host", context.Request.RequestUri.Host);
            //group.AddItem("Port", context.Request.RequestUri.Port);
            //group.AddItem("Method", context.Request.Method);
            //group.AddItem("Version", context.Request.Version);
            //group.AddItem("Scheme", context.Request.RequestUri.Scheme);
            //group.AddItem("Query", context.Request.RequestUri.Query);

            foreach (var item in context.RequestContext.Url.Request.Properties)
            {
                group.AddItem(item.Key, item.Value);  
            }
        }

        private static void AddRequestHeaders(ExceptionLogDataContainer exceptionLogDataContainer, ExceptionLoggerContext context)
        {
            var group = exceptionLogDataContainer.GetGroupByTitle("Request Headers");
         
            foreach (var item in context.RequestContext.Url.Request.Headers)
            {
                group.AddItem(item.Key, item.Value);
                //if (IsCompositeValue(item.Value))
                //{
                //    group.AddItem(item.Key, string.Join(item.Value, CompositeValueValueDelimiter));
                //}
                //else
                //{
                //    group.AddItem(item.Key, item.Value);
                //}
            }
        }

        ///-----

        ///// <summary>
        ///// Gets web application context parameters
        ///// </summary>
        ///// <param name="groupTitle">Title of the group to add</param>
        ///// <param name="contextItemListProvider">Function which provide list of context items</param>
        ///// <param name="exceptionLogDataContainer">Exception log data container</param>
        //private static void AddGroup(string groupTitle, Func<IEnumerable<KeyValuePair<String, IEnumerable<String>>>> contextItemListProvider,
        //                             ExceptionLogDataContainer exceptionLogDataContainer)
        //{
        //    //In case of RESTful service some context collections are not available with strange exception,
        //    //this function call prevent logic to get failed
        //    var contextItemList = GetContextCollection(contextItemListProvider);
        //    if (contextItemList == null || !contextItemList.Any()) return;

        //    //Add new group
        //    var group = exceptionLogDataContainer.GetGroupByTitle(groupTitle);

        //    foreach (var name in contextItemList)
        //    {
        //        var currentName = name;
        //        //Get item value safely
        //        var itemValue = GetContextItemValue(() => contextItemList[currentName]);
        //        if (IsCompositeValue(itemValue))
        //        {
        //            ParseCompositeValue(itemValue, group);
        //        }
        //        else
        //        {
        //            group.AddItem(name, itemValue);
        //        }
        //    }
        //}
        ///// <summary>
        ///// Gets context collection using safe way
        ///// In case of RESTful services this functionality failing some time with strange exception
        ///// </summary>
        ///// <param name="contextItemListProvider">Function which provide context collection</param>
        ///// <returns></returns>
        //private static IEnumerable<KeyValuePair<String, IEnumerable<String>>> GetContextCollection(Func<IEnumerable<KeyValuePair<String, IEnumerable<String>>>> contextItemListProvider)
        //{
        //    try
        //    {
        //        var list = contextItemListProvider();
        //        return !list.Any() ? null : list;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// Gets value from context collection using safe way
        ///// In case of RESTful services this functionality failing some time with strange exception
        ///// </summary>
        ///// <param name="contextItemValueProvider">Function which provide value</param>
        ///// <returns></returns>
        //private static string GetContextItemValue(Func<string> contextItemValueProvider)
        //{
        //    try
        //    {
        //        return contextItemValueProvider();
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// Checks if value is a composite (contains multiple items delimited by \r\n)
        /// </summary>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        private static bool IsCompositeValue(object itemValue)
        {
            return itemValue is Array;
        }

        ///// <summary>
        ///// Parses composite item value to collection of the log items
        ///// </summary>
        ///// <param name="compositeValue">Composite item value</param>
        ///// <param name="group">Log item group </param>
        ///// <returns></returns>
        //private static void ParseCompositeValue(object[] compositeValue, ExceptionLogItemGroup group)
        //{
        //    foreach (var itemParts in compositeValue
        //        .Where(p => p.Length > 0)
        //        .Select(item => item.Split(CompositeValueItemNameValueDelimiter.ToCharArray()))
        //        .Where(itemParts => itemParts.Length == 2))
        //    {
        //        group.AddItem(itemParts[0], itemParts[1].Trim());
        //    }
        //}
    }
}
