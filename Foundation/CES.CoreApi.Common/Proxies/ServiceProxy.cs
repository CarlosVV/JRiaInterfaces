using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace CES.CoreApi.Common.Proxies
{
    public sealed class ServiceProxy<TContract> : IDisposable 
        where TContract : class
    {
        #region Core

        private readonly ChannelFactory<TContract> _channelFactory;
        private TContract _channel;

        public ServiceProxy(Binding binding, Uri remoteAddress)
        {
            var endpointAddress = new EndpointAddress(remoteAddress);
            _channelFactory = new ChannelFactory<TContract>(binding, endpointAddress);
        }

        #endregion

        #region Public methods

        public void Execute(Action<TContract> serviceMethod, Action addCustomHeader = null)
        {
            using (new OperationContextScope((IContextChannel)Channel))
            {
                if (addCustomHeader != null)
                    addCustomHeader();

                serviceMethod.Invoke(Channel);
            }
        }

        public TResult Execute<TResult>(Func<TContract, TResult> serviceMethod, Action addCustomHeader = null)
        {
            using (new OperationContextScope((IContextChannel) Channel))
            {
                if (addCustomHeader != null)
                    addCustomHeader();

                return serviceMethod.Invoke(Channel);
            }
        }
        
        public async Task<TResult> ExecuteAsync<TResult>(Func<TContract, TResult> serviceMethod, Action addCustomHeader = null)
        {
            return await Task.Run(() =>
            {
                using (new OperationContextScope((IContextChannel) Channel))
                {
                    if (addCustomHeader != null)
                        addCustomHeader();

                    return serviceMethod.Invoke(Channel);
                }
            });
        }

        public void Dispose()
        {
            try
            {
                if (_channel == null)
                    return;

                var currentChannel = (IClientChannel)_channel;
                if (currentChannel.State == CommunicationState.Faulted)
                {
                    currentChannel.Abort();
                }
                else
                {
                    if (currentChannel.State != CommunicationState.Closed)
                        currentChannel.Close();
                }
            }
            finally
            {
                _channel = null;
            }
        }

        #endregion

        #region Private methods

        private TContract Channel
        {
            get { return _channel ?? (_channel = _channelFactory.CreateChannel()); }
        }

        #endregion
    }
}
