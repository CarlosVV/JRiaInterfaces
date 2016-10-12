using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;

using CES.CoreApi.CallLog.Shared;
using CES.CoreApi.CallLog.Db;

namespace CES.CoreApi.CallLog.Http
{
    /// <summary>
    /// Helps to query web services sending http requests
    /// </summary>
    public class HttpLoggedRequest
    {
        //@@2015-02-06 lb SCR# 2235011 Created
        //@@2015-02-17 lb SCR# 2245111 Specified missing parameter (This SCR is used only to propagate the fix on this module)
        //@@215-05-14 lb Modified the module to issue HTTP requests to read the response streams with the encoding specified in the response headers, or optionally detecting it from the very response stream.
        //@@2015-11-05 lb SCR# 2455111 Added overload to post binary content

        private DBWsCallLogOps _dBWsCallLogOps = new DBWsCallLogOps();
        public struct Result
        {
            /// <summary>
            /// The content text after transforming the BinaryContent property applying the HttpLoggedRequest.ExpectedContentEncoding
            /// </summary>
            public string TextContent { get; set; }

            public HttpStatusCode StatusCode { get; set; }

            public string[] Headers { get; set; }

            public string ContentType { get; set; }

            /// <summary>
            /// Always established the value of this property when performing the call
            /// </summary>
            public byte[] BinaryContent { get; set; }

            /// <summary>
            /// Gets the content's encoding
            /// </summary>
            public Encoding ContentEncoding { get; set; }
        }

        public HttpLoggedRequest()
        {
            Headers = new Dictionary<string,string>();
            UrlParams = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
            Timeout = TimeSpan.Zero;
            UseEncodingFromStream = false;
        }

        #region properties

        /// <summary>
        /// The remote host
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// The content type to send
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The encoding and character set accepted by the server
        /// </summary>
        public string Accept { get; set; }

        /// <summary>
        /// The credentials, if any
        /// </summary>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// Exposes the collection of headers to add to the requests
        /// </summary>
        public Dictionary<string, string> Headers { get; private set; }

        /// <summary>
        /// Exposes the collection of parameters in the URL
        /// </summary>            
        public Dictionary<string, string> UrlParams { get; private set; }

        /// <summary>
        /// Gets or sets the timeout for the GetResponse operation
        /// </summary>
        /// <remarks>If default is taken (TimeSpan.Zero) it is applied the default to the call (100 secs)</remarks>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// Gets or sets a collection of x509 certificates for the client to authenticate
        /// </summary>
        public X509CertificateCollection ClientCertificates { get; set; }

        /// <summary>
        /// Gets or  sets the user agent for the agent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Get or set the timeout for writing in the request stream or reading from the response stream
        /// </summary>
        /// <remarks>If default is taken (TimeSpan.Zero) it is applied the default</remarks>
        public TimeSpan ReadWriteTimeout { get; set; }

        /// <summary>
        /// Tries to detect the encoding from the stream instead of using the character set header in the response
        /// </summary>
        public bool UseEncodingFromStream { get; set; }

        /// <summary>
        /// The value of date standard header
        /// </summary>
        public DateTime? Date { get; set; }

        #endregion

        /// <summary>
        /// Perform an arbitrary action using the internal data to build querystring
        /// </summary>
        /// <param name="method">the method like POST, GET, PUT ...</param>
        /// <param name="content">The content to send (valid for certain methods only, like POST or PUT)</param>
        /// <param name="resource">Any additional relative address to add to the base address</param>
        /// <param name="contextCall">A description of the call, for logging purposes</param>
        /// <param name="contextTransactionID">A transaction's identifier, if we are in the context of a call for a transaction</param>
        /// <returns>Return always a Result unless there is an exception</returns>
        public Result DoMethod(string method, string content = "", string resource = "", string contextCall = "", int contextTransactionID = 0, int contextServiceID = 0)
        {
            WebResponse webResponse = null;
            Result qbResult = new Result();
            qbResult.StatusCode = HttpStatusCode.Conflict;
            qbResult.TextContent = "";

            try
            {
                StringBuilder completeURL = new StringBuilder();//full URL. Include the query string and optional resources

                if (UrlParams.Count != 0)
                {
                    foreach (KeyValuePair<string, string> pair in UrlParams)
                    {
                        if (completeURL.Length != 0) completeURL.Append('&');

                        completeURL.AppendFormat("{0}={1}", HttpUtility.UrlEncode(pair.Key), HttpUtility.UrlEncode(pair.Value));
                    }

                    completeURL.Insert(0, '?');
                }

                string hostAndResource = BaseAddress ?? "";

                if (!string.IsNullOrWhiteSpace(resource))
                {
                    resource = HttpUtility.UrlEncode(resource);

                    const string SEPARATOR = "/";

                    if (!resource.StartsWith(SEPARATOR) && !hostAndResource.EndsWith(SEPARATOR))
                        hostAndResource += (SEPARATOR + resource);
                    else
                        hostAndResource += resource;
                }

                completeURL.Insert(0, hostAndResource);

                string completeUrlStr = completeURL.ToString();

                Uri address = new Uri(completeUrlStr);
                WebRequest request = WebRequest.Create(address);
                HttpWebResponse httpResp = null;

                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);


                bool isXmlTxt = false;
                Encoding contentEncoding = null;

                //Try to parse the MIME and request encoding 
                if (!string.IsNullOrWhiteSpace(ContentType))
                {
                    try
                    {
                        System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(ContentType);
                        if (ct.MediaType.IndexOf("text/xml", StringComparison.CurrentCultureIgnoreCase) != -1)
                            isXmlTxt = true;

                        //Try to get the encoding
                        if (string.IsNullOrWhiteSpace(ct.CharSet))
                            contentEncoding = Encoding.GetEncoding(ct.CharSet);
                    }
                    catch { }
                }

                if (contentEncoding == null)
                    contentEncoding = Encoding.UTF8; //default encoding

                //The content
                byte[] byteArray = null;
                if (!string.IsNullOrEmpty(content))
                {
                    byteArray = contentEncoding.GetBytes(content);
                }

                request.Method = method;
                
                HttpWebRequest httpReq = (HttpWebRequest)request;

                //Check for any headers
                if (Headers != null && Headers.Count != 0)
                    foreach (KeyValuePair<string, string> headerPair in Headers)
                    {
                        try
                        { //request.Headers.Add(headerPair.Key, headerPair.Value);
                            request.Headers[headerPair.Key] = headerPair.Value;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error when attempting to add header '{0}:{1}': {2}", headerPair.Key, headerPair.Value, ex.Message));
                        }
                    }

                if (this.Date != null)
                    httpReq.Date = this.Date.Value;

                if (!string.IsNullOrWhiteSpace(Accept))
                    httpReq.Accept = Accept;

                // Set the ContentType property of the WebRequest.
                if (!string.IsNullOrWhiteSpace(ContentType))
                    request.ContentType = ContentType;
                
                if(!string.IsNullOrWhiteSpace(UserAgent))
                    httpReq.UserAgent = UserAgent;

                if(ClientCertificates != null && ClientCertificates.Count != 0)
                    httpReq.ClientCertificates = ClientCertificates;
                
                if (Credentials != null)
                    request.Credentials = Credentials;

                if (Timeout != TimeSpan.Zero)
                    request.Timeout = (int)Timeout.TotalMilliseconds;

                if (ReadWriteTimeout != TimeSpan.Zero)
                    httpReq.ReadWriteTimeout =  (int)ReadWriteTimeout.TotalMilliseconds;

                Guid contextGuid = Guid.NewGuid();

                StringBuilder reqHeaders = new StringBuilder();                

                if (request.Headers != null && request.Headers.Count != 0)
                {
                    for (int x = 0; x < request.Headers.Count; x++)
                    {
                        if (reqHeaders.Length != 0)
                        {
                            reqHeaders.Append('\r'); reqHeaders.Append('\n');
                        }
                        
                        reqHeaders.AppendFormat("{0}:{1}", request.Headers.GetKey(x), request.Headers[x]);
                    }
                }

                try
                {
                    //Save request in logs first
                    if (!string.IsNullOrWhiteSpace(contextCall))
                    {
                        if (!string.IsNullOrEmpty(request.ContentType) && isXmlTxt)
                            _dBWsCallLogOps.WriteToLogAsXML(DataFlowDirection.Request, contextTransactionID, contextServiceID, contextCall, content, contextGuid, null, completeUrlStr, reqHeaders.ToString());
                        else
                            _dBWsCallLogOps.WriteToLogAsText(DataFlowDirection.Request, contextTransactionID, contextServiceID, contextCall, content, contextGuid, completeUrlStr, reqHeaders.ToString());
                    }

                    if (byteArray != null && byteArray.Length != 0)
                    {
                        request.ContentLength = byteArray.Length;

                        using (Stream st = request.GetRequestStream())
                        {
                            st.Write(byteArray, 0, byteArray.Length);
                            st.Flush();
                        }

                        webResponse = request.GetResponse();
                    }
                    else
                    {
                        webResponse = request.GetResponse();
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response != null && ex.Response is HttpWebResponse)
                        webResponse = httpResp = (HttpWebResponse)ex.Response;
                    else
                        throw new Exception("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }

                httpResp = (HttpWebResponse)webResponse;

                qbResult.StatusCode = httpResp.StatusCode;

                string respHeaders = "";
                List<string> respHeadersList = new List<string>();

                if (httpResp.Headers != null && httpResp.Headers.Count != 0)
                {
                    for (int x = 0; x < httpResp.Headers.Count; x++)
                    {
                        respHeadersList.Add(string.Format("{0}:{1}", httpResp.Headers.GetKey(x), httpResp.Headers[x]));
                    }
                }

                if (respHeadersList.Count != 0)
                {
                    qbResult.Headers = respHeadersList.ToArray();

                    respHeaders = string.Join("\r\n", qbResult.Headers);
                }

                using (Stream responseStream = httpResp.GetResponseStream())
                {

                    string characterSet = httpResp.CharacterSet;
                    Encoding encoding = null;

                    if (!UseEncodingFromStream)
                    {
                        if (!string.IsNullOrEmpty(characterSet))
                            try { encoding = Encoding.GetEncoding(characterSet); }
                            catch { encoding = null; }
                    }

                    byte[] buffer = new byte[1024];

                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        qbResult.BinaryContent = ms.ToArray();
                    }

                    using (MemoryStream ms = new MemoryStream(qbResult.BinaryContent))
                    {
                        using (StreamReader sr = (encoding != null) ? new StreamReader(ms, encoding) :
                                    new StreamReader(ms, true))
                        {
                            qbResult.TextContent = sr.ReadToEnd();
                            qbResult.ContentEncoding = sr.CurrentEncoding;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(contextCall))
                {
                    if (!string.IsNullOrEmpty(httpResp.ContentType) && httpResp.ContentType.IndexOf("text/xml", StringComparison.CurrentCultureIgnoreCase) != -1)
                        _dBWsCallLogOps.WriteToLogAsXML(DataFlowDirection.Response, contextTransactionID, contextServiceID, contextCall, qbResult.TextContent, contextGuid, null, completeUrlStr, respHeaders, (int)qbResult.StatusCode);
                    else
                        _dBWsCallLogOps.WriteToLogAsText(DataFlowDirection.Response, contextTransactionID, contextServiceID, contextCall, qbResult.TextContent, contextGuid, completeUrlStr, respHeaders, (int)qbResult.StatusCode);
                }

                return qbResult;
            }
            finally
            {
                if (webResponse != null)
                    try { webResponse.Close(); }
                    catch { }
            }
        }

        /// <summary>
        /// Perform an arbitrary action using the internal data to build querystring
        /// </summary>
        /// <param name="method">the method like POST, GET, PUT ...</param>
        /// <param name="binContent">The content to send (valid for certain methods only, like POST or PUT)</param>
        /// <param name="resource">Any additional relative address to add to the base address</param>
        /// <param name="contextCall">A description of the call, for logging purposes</param>
        /// <param name="contextTransactionID">A transaction's identifier, if we are in the context of a call for a transaction</param>
        /// <param name="contextServiceID">The ID of the service within which contextTransactionID is defined</param>
        /// <param name="contentEncoding">Overrides the content-type, to determine the content encoding if it is of text type, for logging purposes only</param>
        /// <returns>Return always a Result unless there is an exception</returns>
        public Result DoMethod(string method, byte[] binContent = null, string resource = "", string contextCall = "", int contextTransactionID = 0, int contextServiceID = 0, Encoding contentEncoding = null)
        {
            WebResponse webResponse = null;
            Result qbResult = new Result();
            qbResult.StatusCode = HttpStatusCode.Conflict;
            qbResult.TextContent = "";

            try
            {
                StringBuilder completeURL = new StringBuilder();//full URL. Include the query string and optional resources

                if (UrlParams.Count != 0)
                {
                    foreach (KeyValuePair<string, string> pair in UrlParams)
                    {
                        if (completeURL.Length != 0) completeURL.Append('&');

                        completeURL.AppendFormat("{0}={1}", HttpUtility.UrlEncode(pair.Key), HttpUtility.UrlEncode(pair.Value));
                    }

                    completeURL.Insert(0, '?');
                }

                string hostAndResource = BaseAddress ?? "";

                if (!string.IsNullOrWhiteSpace(resource))
                {
                    resource = HttpUtility.UrlEncode(resource);

                    const string SEPARATOR = "/";

                    if (!resource.StartsWith(SEPARATOR) && !hostAndResource.EndsWith(SEPARATOR))
                        hostAndResource += (SEPARATOR + resource);
                    else
                        hostAndResource += resource;
                }

                completeURL.Insert(0, hostAndResource);

                string completeUrlStr = completeURL.ToString();

                Uri address = new Uri(completeUrlStr);
                WebRequest request = WebRequest.Create(address);
                HttpWebResponse httpResp = null;

                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

                request.Method = method;

                HttpWebRequest httpReq = (HttpWebRequest)request;

                //Check for any headers
                if (Headers != null && Headers.Count != 0)
                    foreach (KeyValuePair<string, string> headerPair in Headers)
                    {
                        try { request.Headers[headerPair.Key] = headerPair.Value; }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error when attempting to add header '{0}:{1}': {2}", headerPair.Key, headerPair.Value, ex.Message));
                        }
                    }

                if (this.Date != null)
                    httpReq.Date = this.Date.Value;

                if (!string.IsNullOrWhiteSpace(Accept))
                    httpReq.Accept = Accept;

                // Set the ContentType property of the WebRequest.
                if (!string.IsNullOrWhiteSpace(ContentType))
                    request.ContentType = ContentType;

                if (!string.IsNullOrWhiteSpace(UserAgent))
                    httpReq.UserAgent = UserAgent;

                if (ClientCertificates != null && ClientCertificates.Count != 0)
                    httpReq.ClientCertificates = ClientCertificates;

                if (Credentials != null)
                    request.Credentials = Credentials;

                if (Timeout != TimeSpan.Zero)
                    request.Timeout = (int)Timeout.TotalMilliseconds;

                if (ReadWriteTimeout != TimeSpan.Zero)
                    httpReq.ReadWriteTimeout = (int)ReadWriteTimeout.TotalMilliseconds;

                Guid contextGuid = Guid.NewGuid();

                StringBuilder reqHeaders = new StringBuilder();

                if (request.Headers != null && request.Headers.Count != 0)
                {
                    for (int x = 0; x < request.Headers.Count; x++)
                    {
                        if (reqHeaders.Length != 0)
                        {
                            reqHeaders.Append('\r'); reqHeaders.Append('\n');
                        }

                        reqHeaders.AppendFormat("{0}:{1}", request.Headers.GetKey(x), request.Headers[x]);
                    }
                }

                string content = "";
                bool isXmlTxt = false;

                //Try to parse the MIME and request encoding 
                if (!string.IsNullOrWhiteSpace(ContentType))
                {
                    try
                    {
                        System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(ContentType);
                        if (ct.MediaType.IndexOf("text/xml", StringComparison.CurrentCultureIgnoreCase) != -1)
                            isXmlTxt = true;

                        //Try to get the encoding
                        if (contentEncoding == null && !string.IsNullOrWhiteSpace(ct.CharSet))
                            contentEncoding = Encoding.GetEncoding(ct.CharSet);
                    }
                    catch { }
                }

                if (binContent != null)
                {
                    if (contentEncoding != null)
                    {
                        content = contentEncoding.GetString(binContent);
                    }
                    else
                        content = "BASE64 BIN CONTENT: " + Convert.ToBase64String(binContent, Base64FormattingOptions.InsertLineBreaks);
                }

                try
                {
                    //Save request in logs first
                    if (!string.IsNullOrWhiteSpace(contextCall))
                    {
                        if (!string.IsNullOrEmpty(request.ContentType) && isXmlTxt)
                            _dBWsCallLogOps.WriteToLogAsXML(DataFlowDirection.Request, contextTransactionID, contextServiceID, contextCall, content, contextGuid, binContent, completeUrlStr, reqHeaders.ToString());
                        else
                            _dBWsCallLogOps.WriteToLogAsText(DataFlowDirection.Request, contextTransactionID, contextServiceID, contextCall, content, contextGuid, completeUrlStr, reqHeaders.ToString());
                    }

                    if (binContent != null && binContent.Length != 0)
                    {
                        request.ContentLength = binContent.Length;

                        using (Stream st = request.GetRequestStream())
                        {
                            st.Write(binContent, 0, binContent.Length);
                            st.Flush();
                        }

                        webResponse = request.GetResponse();
                    }
                    else
                    {
                        webResponse = request.GetResponse();
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response != null && ex.Response is HttpWebResponse)
                        webResponse = httpResp = (HttpWebResponse)ex.Response;
                    else
                        throw new Exception("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }

                httpResp = (HttpWebResponse)webResponse;

                qbResult.StatusCode = httpResp.StatusCode;

                string respHeaders = "";
                List<string> respHeadersList = new List<string>();

                if (httpResp.Headers != null && httpResp.Headers.Count != 0)
                {
                    for (int x = 0; x < httpResp.Headers.Count; x++)
                    {
                        respHeadersList.Add(string.Format("{0}:{1}", httpResp.Headers.GetKey(x), httpResp.Headers[x]));
                    }
                }

                if (respHeadersList.Count != 0)
                {
                    qbResult.Headers = respHeadersList.ToArray();

                    respHeaders = string.Join("\r\n", qbResult.Headers);
                }

                using (Stream responseStream = httpResp.GetResponseStream())
                {

                    string characterSet = httpResp.CharacterSet;
                    Encoding encoding = null;

                    if (!UseEncodingFromStream)
                    {
                        if (!string.IsNullOrEmpty(characterSet))
                            try { encoding = Encoding.GetEncoding(characterSet); }
                            catch { encoding = null; }
                    }

                    byte[] buffer = new byte[1024];

                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        qbResult.BinaryContent = ms.ToArray();
                    }

                    using (MemoryStream ms = new MemoryStream(qbResult.BinaryContent))
                    {
                        using (StreamReader sr = (encoding != null) ? new StreamReader(ms, encoding) :
                                    new StreamReader(ms, true))
                        {
                            qbResult.TextContent = sr.ReadToEnd();
                            qbResult.ContentEncoding = sr.CurrentEncoding;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(contextCall))
                {
                    if (!string.IsNullOrEmpty(httpResp.ContentType) && httpResp.ContentType.IndexOf("text/xml", StringComparison.CurrentCultureIgnoreCase) != -1)
                        _dBWsCallLogOps.WriteToLogAsXML(DataFlowDirection.Response, contextTransactionID, contextServiceID, contextCall, qbResult.TextContent, contextGuid, qbResult.BinaryContent, completeUrlStr, respHeaders, (int)qbResult.StatusCode);
                    else
                        _dBWsCallLogOps.WriteToLogAsText(DataFlowDirection.Response, contextTransactionID, contextServiceID, contextCall, qbResult.TextContent, contextGuid, completeUrlStr, respHeaders, (int)qbResult.StatusCode);
                }

                return qbResult;
            }
            finally
            {
                if (webResponse != null)
                    try { webResponse.Close(); }
                    catch { }
            }
        }

        /// <summary>
        /// Perform the GET action using the internal data to build querystring
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="contextCall"></param>
        /// <param name="contextTransactionID"></param>
        /// <returns></returns>
        public Result Get(string resource = "", string contextCall = "", int contextTransactionID = 0, int contextServiceID = 0)
        {
            return DoMethod("GET", "", resource, contextCall, contextTransactionID, contextServiceID);
        }

        /// <summary>
        /// Perform the POST action using the internal data to build querystring
        /// </summary>
        /// <param name="content"></param>
        /// <param name="resource"></param>
        /// <param name="contextCall"></param>
        /// <param name="contextTransactionID"></param>
        /// <returns></returns>
        public Result Post(string content = "", string resource = "", string contextCall = "", int contextTransactionID = 0, int contextServiceID = 0)
        {
            return DoMethod("POST", content, resource, contextCall, contextTransactionID, contextServiceID);
        }

        /// <summary>
        /// Perform the POST action using a binary content to send
        /// </summary>
        /// <param name="content"></param>
        /// <param name="resource"></param>
        /// <param name="contextCall"></param>
        /// <param name="contextTransactionID"></param>
        /// <returns></returns>
        public Result Post(byte[] content = null, string resource = "", string contextCall = "", int contextTransactionID = 0, int contextServiceID = 0, Encoding contentEncoding = null)
        {
            return DoMethod("POST", content, resource, contextCall, contextTransactionID, contextServiceID, contentEncoding);
        }
    }
}
