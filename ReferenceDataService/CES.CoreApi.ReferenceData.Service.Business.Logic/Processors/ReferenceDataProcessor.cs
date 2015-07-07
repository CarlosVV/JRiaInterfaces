using System;
using System.Collections.Generic;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Enumerations;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Interfaces;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Models;

namespace CES.CoreApi.ReferenceData.Service.Business.Logic.Processors
{
    public class ReferenceDataProcessor : IReferenceDataProcessor
    {
        #region Core
        
        private readonly IReferenceDataRepository _repository;

        public ReferenceDataProcessor(IReferenceDataRepository repository)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.ReferenceDataService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "repository");

            _repository = repository;
        } 

        #endregion

        #region IReferenceDataProcessor implementation
        
        public IEnumerable<IdentificationTypeModel> GetIdentificationTypeList(int locationDepartmentId)
        {
            return _repository.GetByDataType(locationDepartmentId, ReferenceDataType.IdentificationType);
        }

        public IdentificationTypeModel GetIdentificationType(int identificationTypeId, int locationDepartmentId)
        {
            var identificationTypeList = GetIdentificationTypeList(locationDepartmentId);
            return identificationTypeList.FirstOrDefault(p => p.Id == identificationTypeId);
        } 

        #endregion
    }
}
