
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Shared.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Utilities
{
    public static class PayouFieldstListExtension
    {
        private const string ReadValueException = "An exception happened during reading field with name = '{0}' from the data collection.";
        public static PayoutFieldsModel GetByName(this IList<PayoutFieldsModel> payoutFiedls, string name)
        {
            try
            {
                var value = payoutFiedls.FirstOrDefault(f => f.FieldName == name);
                return value;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format(CultureInfo.InvariantCulture, ReadValueException, name));
            }
        }


        public static List<PayoutFieldsModel> GetRequiredPayoutFiledsByPersistenceID(this List<PayoutFieldsModel> fieldsList, int persistenceID, IPersistenceHelper persistenceHelper)
        {
           

            //Find required field from persistence

            var getTransactionInfoPersistence = persistenceHelper.GetPersistence(persistenceID);
            if (getTransactionInfoPersistence == null) return fieldsList;
            var persistenceEvent = getTransactionInfoPersistence.PersistenceEvents.FirstOrDefault(ev => ev.PersistenceEventTypeID == Shared.Persistence.Model.PersistenceEventType.PayoutPinRequestInfoResponse);
            if (persistenceEvent==null) return fieldsList;
            var contentObject = persistenceEvent.GetPersistenceObject<GetTransactionInfoResponseModel>();
            if(contentObject==null) return fieldsList;
            return  contentObject.PayoutRequiredFields;

           /*
            fieldsList = new List<PayoutFieldsModel>()
            {
                //new PayoutFieldsModel() { FieldID = 2511, FieldName ="fBenTaxID", DisplayName ="Ben Tax ID", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 2611, FieldName ="fBenOccupation", DisplayName ="Ben Occupation", FieldRequired =false},
                new PayoutFieldsModel() { FieldID = 2711, FieldName ="fBenIDNo", DisplayName ="Ben ID No", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 2811, FieldName ="fBenIDIssuedByState", DisplayName ="Ben ID Issued By State", FieldRequired =false},
                new PayoutFieldsModel() { FieldID = 2911, FieldName ="fBenIDIssuedByCountry", DisplayName ="Ben ID Issued By Country", FieldRequired =false},
                new PayoutFieldsModel() { FieldID = 3011, FieldName ="fBenIDIssuedBy", DisplayName ="Ben ID Issued By", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 3111, FieldName ="fBenIDExp", DisplayName ="Ben ID Expiration Date", FieldRequired =false},
                new PayoutFieldsModel() { FieldID = 3211, FieldName ="fBenIDType", DisplayName ="Ben ID Type", FieldRequired =false},
                new PayoutFieldsModel() { FieldID = 3311, FieldName ="fBenBirthDate", DisplayName ="Ben Birth Date", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 3411, FieldName ="fTransferReason", DisplayName ="Transfer Reason", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 3511, FieldName ="fBenTelNo", DisplayName ="Beneficiary Telephone Number", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 3811, FieldName ="fBenCountryOfBirth", DisplayName ="Beneficiary Country of Birth", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 3911, FieldName ="fBenNationality", DisplayName ="Beneficiary Nationality", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 4311, FieldName ="fBenCustRelationshipID", DisplayName ="Customer Beneficiary Relationship", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 4411, FieldName ="fBenStateOfBirth", DisplayName ="Beneficiary State of Birth", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 4511, FieldName ="fBenCityOfBirth", DisplayName ="Beneficiary City of Birth", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 4611, FieldName ="fBenSex", DisplayName ="Beneficiary Gender", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 4711, FieldName ="fCountryOfResidence", DisplayName ="Beneficiary Country of Residence", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 4811, FieldName ="fBenIDIssuedDate", DisplayName ="Beneficiary ID Issued Date", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 10012, FieldName ="BeneficiaryFullAddress", DisplayName ="Beneficiary Full Address", FieldRequired =false},
                new PayoutFieldsModel() { FieldID = 10214, FieldName ="pmtBenCity", DisplayName ="PMT Beneficiary City", FieldRequired =false},
                new PayoutFieldsModel() { FieldID = 10314, FieldName ="pmtBenAddress", DisplayName ="PMT Beneficiary Address", FieldRequired =false},
                new PayoutFieldsModel() { FieldID = 10414, FieldName ="pmtBenPostalCode", DisplayName ="PMT Beneficiary Postal Code", FieldRequired =false},
                new PayoutFieldsModel() { FieldID = 10514, FieldName ="pmtBenState", DisplayName ="PMT Beneficiary State", FieldRequired =false},
                //new PayoutFieldsModel() { FieldID = 10614, FieldName ="pmtBenCountry", DisplayName ="PMT Beneficiary Country", FieldRequired =false}
            };
            */

            //return fieldsList;

            //return persistenceHelper.GetPersistence(persistenceID).PersistenceEvents.FirstOrDefault(ev => ev.PersistenceEventTypeID == Shared.Persistence.Model.PersistenceEventType.PayoutPinRequestInfoRequest).GetPersistenceObject<GetTransactionInfoResponseModel>().PayoutRequiredFields;
        }
    }
}
