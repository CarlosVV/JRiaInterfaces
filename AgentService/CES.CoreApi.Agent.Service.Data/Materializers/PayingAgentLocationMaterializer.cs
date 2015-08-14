using System.Collections.ObjectModel;
using System.Data;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Shared.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;
using CES.CoreApi.Shared.Business.Contract.Models.Common;

namespace CES.CoreApi.Agent.Service.Data.Materializers
{
    public class PayingAgentLocationMaterializer : IPayingAgentLocationMaterializer
    {
        #region IPayingAgentLocationMaterializer implementation

        public PayingAgentLocationModel Materialize(IDataReader reader, int locationId)
        {
            return MaterializeLocation(reader);
        } 

        #endregion

        #region Private methods

        private static PayingAgentLocationModel MaterializeLocation(IDataReader reader)
        {
            return new PayingAgentLocationModel
            {
                Id = reader.ReadValue<int>("fNameIDLoc"),
                IsOnHold = reader.ReadValue<bool>("fOnHold"),
                OnHoldReason = reader.ReadValue<string>("fOnHoldReason", true),
                IsDisabled = reader.ReadValue<bool>("fDisabled"),
                Name = reader.ReadValue<string>("fName"),
                BranchNumber = reader.ReadValue<string>("fBranchNo", true),
                TimeZoneId = reader.ReadValue<int>("fTimeZoneID"),
                Rating = reader.ReadValue<string>("fRating", true),
                Note = reader.ReadValue<string>("fNote", true),
                NoteEnglish = reader.ReadValue<string>("fNoteEN", true),
                Address = MaterializeAdrress(reader),
                Contact = MaterializeContact(reader)
            };
        }
        
        private static ContactModel MaterializeContact(IDataReader reader)
        {
            var workPhone = reader.ReadValue<string>("fTelNo", true);
            var fax = reader.ReadValue<string>("fFaxNo", true);
            var email = reader.ReadValue<string>("fEmail", true);

            if (string.IsNullOrEmpty(workPhone) && string.IsNullOrEmpty(fax) && string.IsNullOrEmpty(email))
                return null;

            var response = new ContactModel { PhoneList = new Collection<TelephoneModel>(), Email = email };

            if (!string.IsNullOrEmpty(workPhone))
                response.PhoneList.Add(new TelephoneModel { Number = workPhone, PhoneType = PhoneType.Work });

            if (!string.IsNullOrEmpty(fax))
                response.PhoneList.Add(new TelephoneModel { Number = fax, PhoneType = PhoneType.Fax });

            return response;
        }

        private static AddressModel MaterializeAdrress(IDataReader reader)
        {
            return new AddressModel
            {
                Address1 = reader.ReadValue<string>("fAddress1", true),
                Address2 = reader.ReadValue<string>("fAddress2", true),
                City = reader.ReadValue<string>("fCity", true),
                State = reader.ReadValue<string>("fState", true),
                PostalCode = reader.ReadValue<string>("fPostalCode", true),
                Country = reader.ReadValue<string>("fCountry", true),
                CityId = reader.ReadValue<int>("fCityID"),
            };
        } 

        #endregion
    }
}
