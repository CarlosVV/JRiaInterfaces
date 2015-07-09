using System.Collections.Generic;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Models;

namespace CES.CoreApi.ReferenceData.Service.Business.Contract.Interfaces
{
    public interface IReferenceDataProcessor
    {
        IEnumerable<IdentificationTypeModel> GetIdentificationTypeList(int locationDepartmentId);
        IdentificationTypeModel GetIdentificationType(int identificationTypeId, int locationDepartmentId);
    }
}