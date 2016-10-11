using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;

namespace CES.CoreApi.Payout.Service.Business.Logic.Email
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IRiaRepository _repository;
        private static IPersistenceHelper _persistenceHelper;
        public EmailHelper(IRiaRepository repository, IPersistenceHelper persistenceHelper)
        {
            _repository = repository;
            _persistenceHelper = persistenceHelper;

        }
        public SendEmailResponseModel EnviarMail(SendEmailRequestModel requestModel)
        {
            var log = string.Empty;
            
            var response =  _repository.EnviarMail(requestModel, ref log);

            //(W18) 
            var persistenceEventEnviarMailRequestModel = new PersistenceEventModel(requestModel.PersistenceID , requestModel.ProviderID, PersistenceEventType.SendEmailRequest);
            persistenceEventEnviarMailRequestModel.SetContentObject<string>(log);
            _persistenceHelper.CreatePersistenceEvent(persistenceEventEnviarMailRequestModel);


            //(W19) 
            var persistenceEventEnviarMailResponseModel = new PersistenceEventModel(requestModel.PersistenceID, requestModel.ProviderID, PersistenceEventType.SendEmailResponse);
            persistenceEventEnviarMailResponseModel.SetContentObject<SendEmailResponseModel>(response);
            _persistenceHelper.CreatePersistenceEvent(persistenceEventEnviarMailResponseModel);


            return response;

        }
    }
}
