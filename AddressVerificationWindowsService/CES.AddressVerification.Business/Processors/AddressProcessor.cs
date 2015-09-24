using System;
using System.Collections.Generic;
using System.Linq;
using CES.AddressVerification.Business.Contract.Enumerations;
using CES.AddressVerification.Business.Contract.Interfaces;
using CES.AddressVerification.Business.Contract.Models;

namespace CES.AddressVerification.Business.Processors
{
    public class AddressProcessor : IAddressProcessor
    {
        private readonly IAddressRepository _repository;

        public AddressProcessor(IAddressRepository repository)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");
            _repository = repository;
        }

        public void Start()
        {
            //Status = ProcessorStatus.Processing;

            while (true)
            {
                //if (IsStopInitiated)
                //    break;

                //Get next record batch
                var recordList = _repository.GetAddressList();

                if (!recordList.Any())
                    break;

                //Status = ProcessorStatus.BatchProcessingStarted;

                //Process record batch
                ProcessBatch(recordList);

                //Status = ProcessorStatus.BatchProcessingCompleted;
            }
        }

        private void ProcessBatch(IEnumerable<Address> recordList)
        {
            throw new NotImplementedException();
        }
    }
}
