using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;

namespace CES.CoreApi.Agent.Service.Business.Logic.Processors
{
    public class AgentUserProcessor : IAgentUserProcessor
    {
        #region Core

        private readonly IImageRepository _imageRepository;

        public AgentUserProcessor(IImageRepository imageRepository)
        {
            if (imageRepository == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "imageRepository");

            _imageRepository = imageRepository;
        }

        #endregion

        #region Public methods

        public ProcessSignatureResponseModel ProcessSignature(ProcessSignatureRequestModel requestModel)
        {
            //do whatever needs to be done to save agent signature to database, use _imageRepository to access Image DB

            return new ProcessSignatureResponseModel {IsCompleted = true};
        } 

        #endregion
    }
}
