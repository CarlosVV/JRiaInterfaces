using System;
using System.ServiceProcess;
using System.Timers;
using CES.AddressVerification.Business.Contract.Interfaces;

namespace CES.AddressVerification.Service
{
    public partial class AddressVerificationService : ServiceBase
    {
        private readonly IAddressProcessor _addressProcessor;
        private readonly Timer _processTimer;
        private const int WatchTimeout = 10000;

        public AddressVerificationService(IAddressProcessor addressProcessor)
        {
            if (addressProcessor == null) 
                throw new ArgumentNullException("addressProcessor");
            
            _addressProcessor = addressProcessor;

            InitializeComponent();

            //Set up process timer
            _processTimer = new Timer(WatchTimeout);
            _processTimer.Elapsed += ProcessTimerOnElapsed;
        }

        private void ProcessTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _addressProcessor.Start();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            _processTimer.AutoReset = true;
            _processTimer.Enabled = true;
            _processTimer.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();

            _processTimer.AutoReset = false;
            _processTimer.Enabled = false;
            _processTimer.Stop();
        }

        //protected override void OnPause()
        //{

        //    //WaitBatchProcessed();

        //    base.OnPause();
        //}

        //protected override void OnContinue()
        //{
        //    base.OnContinue();
        //}
    }
}
