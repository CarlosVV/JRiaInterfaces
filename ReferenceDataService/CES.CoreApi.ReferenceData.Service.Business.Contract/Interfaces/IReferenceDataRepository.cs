using System.Collections.Generic;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Enumerations;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Models;

namespace CES.CoreApi.ReferenceData.Service.Business.Contract.Interfaces
{
    public interface IReferenceDataRepository
    {
        IEnumerable<IdentificationTypeModel> GetByDataType(int locationDepartmentId, ReferenceDataType dataType);
    }
}