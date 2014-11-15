using System;
using System.ServiceModel;

namespace CES.CoreApi.Foundation.Contract.Models
{
    public class BindingConfiguration
    {
        public BindingConfiguration()
        {
            UseDefaultWebProxy = true;
            TransactionProtocol = TransactionProtocol.OleTransactions;
            HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            MessageEncoding = WSMessageEncoding.Text;
        }

        public string Binding { get; set; }
        public string Name { get; set; }
        public bool AllowCookies { get; set; }
        public bool BypassProxyOnLocal { get; set; }
        public TimeSpan CloseTimeout { get; set; }
        public HostNameComparisonMode HostNameComparisonMode { get; set; }
        public long MaxBufferPoolSize { get; set; }
        public int MaxBufferSize { get; set; }
        public long MaxReceivedMessageSize { get; set; }
        public WSMessageEncoding MessageEncoding { get; set; }
        public TimeSpan OpenTimeout { get; set; }
        public string ProxyAddress { get; set; }
        public TimeSpan ReceiveTimeout { get; set; }
        public TimeSpan SendTimeout { get; set; }
        public string TextEncoding { get; set; }
        public TransferMode TransferMode { get; set; }
        public bool UseDefaultWebProxy { get; set; }
        public ReaderQuotasConfiguration ReaderQuotas { get; set; }
        public int MaxConnections { get; set; }
        public bool TransactionFlow { get; set; }
        public TransactionProtocol TransactionProtocol { get; set; }
        public int ListenBacklog { get; set; }
        public bool PortSharingEnabled { get; set; }
        public ReliableSessionConfiguration ReliableSession { get; set; }
        public bool CrossDomainScriptAccessEnabled { get; set; }
        public string Namespace { get; set; }
    }
}
