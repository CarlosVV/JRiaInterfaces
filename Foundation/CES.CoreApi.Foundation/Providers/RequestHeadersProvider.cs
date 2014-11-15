﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Foundation.Providers
{
    public class RequestHeadersProvider : IRequestHeadersProvider
    {
        #region Public methods
        
        public Dictionary<string, object> GetHeaders(string bindingName)
        {
            var isRestCall = bindingName.IndexOf("WEBHTTPBINDING", StringComparison.OrdinalIgnoreCase) >= 0;
            return isRestCall
                ? GetRestHeaders()
                : GetSoapHeaders();
        }

        public Dictionary<string, object> GetHeaders()
        {
            return GetSoapHeaders();
        }

        #endregion

        #region Private methods

        private static Dictionary<string, object> GetSoapHeaders()
        {
            return OperationContext.Current == null
                ? new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
                : OperationContext.Current.IncomingMessageHeaders
                    .ToList()
                    .ToDictionary(p => p.Name,
                        p => GetHeader(p.Name, p.Namespace), StringComparer.InvariantCultureIgnoreCase);
        }

        private static Dictionary<string, object> GetRestHeaders()
        {
            var headers = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

            if (WebOperationContext.Current == null)
                return headers;

            for (var i = 0; i < WebOperationContext.Current.IncomingRequest.Headers.Count; i++)
            {
                var key = WebOperationContext.Current.IncomingRequest.Headers.GetKey(i);
                var values = WebOperationContext.Current.IncomingRequest.Headers.GetValues(key);
                headers.Add(key, values);
            }

            return headers;
        }

        private static object GetHeader(string name, MessageHeaders headers, string ns)
        {
            if (string.IsNullOrEmpty(ns))
                ns = string.Empty;
            
            var index = headers.FindHeader(name, ns);
            if (index < 0) return null;

            var serializer = new DataContractSerializer(typeof (string), name, ns, null, Int32.MaxValue, false, false, null);
            object headerValue;
            //This should be fixed to get serializer work correctly
            try
            {
                headerValue = headers.GetHeader<object>(index, serializer);
            }
            catch (Exception)
            {
                headerValue = headers[index];
            }
            return headerValue;
        }


        private static object GetHeader(string name, string ns)
        {
            return GetHeader(name, OperationContext.Current.IncomingMessageHeaders, ns);
        }

        #endregion
    }
}
