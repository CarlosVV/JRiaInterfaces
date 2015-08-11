using System.Collections.ObjectModel;
using System.Data;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Shared.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Data.Materializers
{
    public class LocationMaterializer : ILocationMaterializer
    {
        #region ILocationMaterializer implementation

        public AgentLocationModel Materialize(IDataReader reader, int locationId)
        {
            return new AgentLocationModel
            {
                Id = locationId,
                IsOnHold = reader.ReadValue<bool>("fOnHold"),
                OnHoldReason = reader.ReadValue<string>("fOnHoldReason"),
                IsDisabled = reader.ReadValue<bool>("fDisabled"),
                Name = reader.ReadValue<string>("fNameAgent"),
                BranchNumber = reader.ReadValue<string>("fBranchNo"),
                TimeZoneId = reader.ReadValue<int>("fTimeZoneID"),
                Rating = reader.ReadValue<string>("fRating"),
                Note = reader.ReadValue<string>("fNote"),
                NoteEnglish = reader.ReadValue<string>("fNoteEN"),
                Address = MaterializeAdrress(reader),
                Contact = MaterializeContact(reader)
            };
        } 

        #endregion

        #region Private methods

        private static ContactModel MaterializeContact(IDataReader reader)
        {
            var workPhone = reader.ReadValue<string>("fTelNo");
            var fax = reader.ReadValue<string>("fFaxNo");
            var email = reader.ReadValue<string>("fEmail");

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
                Address1 = reader.ReadValue<string>("fAddress1"),
                Address2 = reader.ReadValue<string>("fAddress2"),
                City = reader.ReadValue<string>("fCity"),
                State = reader.ReadValue<string>("fState"),
                PostalCode = reader.ReadValue<string>("fPostalCode"),
                Country = reader.ReadValue<string>("fCountry"),
                CityId = reader.ReadValue<int>("fCityID"),
            };
        } 

        #endregion
    }
}
