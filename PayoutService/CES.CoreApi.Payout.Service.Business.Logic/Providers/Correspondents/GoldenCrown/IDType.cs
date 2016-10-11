using CES.CoreApi.Payout.Service.Business.Logic.Exceptions;
using CES.CoreApi.Payout.Service.Business.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents.GoldenCrown
{

    /// <summary>
    /// Hold an ID Type and allow it to be translated to different vendor valid values.
    /// 
    /// Author  : David Go
    /// </summary>
    public class IDType
    {
        //Ria ID Types:
        private const string RIA_PASSPORT = "Passport";
        private const string RIA_DRIVERSLICENSE = "DriversLicens";
        private const string RIA_STATE = "StateIdentification";
        private const string RIA_MATRICULA = "MatriculaConsular";
        private const string RIA_CERT_NAT = "CertificateofNaturalization";
        private const string RIA_EMPLOYMENT = "EmploymentAuthorization";
        private const string RIA_MILITARY = "MilitaryID";
        private const string RIA_RESIDENT_ALIEN = "ResidentAlienCard";
        private const string RIA_OTHER = "Other";

        //Golden Crown ID Types:
        private const string GC_RUSSIA_PASSPORT = "PASSPORT"; //Passport of Russian Federation
        private const string GC_FOREGN_PASSPORT = "FOREGN"; //National passport of another country
        private const string GC_DRIVERS_LICENSE = "DRIVELIC"; //Driver's License
        private const string GC_OTHER_ID = "OTHER"; //All Other ID Types
        private const string RUSSIA_COUNTRYCODE = "RU";

        //Instance Vars:
        private string m_riaIDType;
        private string m_gcIDType;
        private int m_riaIDTypeID;

        /// <summary>
        /// PRIVATE CONSTRUCTOR:
        /// Private: All IDTypes should be created using the Create() methods.
        /// </summary>
        /// <param name="riaIDType"></param>
        /// <param name="gcIDType"></param>
        private IDType(string riaIDType, string gcIDType)
        {
            m_riaIDType = riaIDType;
            m_gcIDType = gcIDType;
            //TODO: ID Type ID Need to look this up:
            m_riaIDTypeID = 0;
        }


        /// <summary>
        /// PUBLIC CONSTRUCTOR:
        /// Create an ID Type from the Ria Type Description.
        /// </summary>
        /// <param name="riaType"></param>
        /// <param name="idIssuer"></param>
        /// <returns></returns>
        public static IDType S_CreateFromRiaIDType(string riaType, string idIssuer)
        {
            if (riaType == null || riaType.Length < 1)
            {
                throw new InvalidDataException(Messages.S_GetMessage("InvalidIDType"));
            }
            if (riaType.Equals(RIA_PASSPORT) && idIssuer.Equals(RUSSIA_COUNTRYCODE))
            {
                return new IDType(riaType, GC_RUSSIA_PASSPORT);
            }
            string gcType = "";
            switch (riaType)
            {
                case RIA_PASSPORT: gcType = GC_FOREGN_PASSPORT;
                    break;
                case RIA_DRIVERSLICENSE: gcType = GC_DRIVERS_LICENSE;
                    break;
                case RIA_STATE: gcType = GC_OTHER_ID;
                    break;
                case RIA_MATRICULA: gcType = GC_OTHER_ID;
                    break;
                case RIA_CERT_NAT: gcType = GC_OTHER_ID;
                    break;
                case RIA_EMPLOYMENT: gcType = GC_OTHER_ID;
                    break;
                case RIA_MILITARY: gcType = GC_OTHER_ID;
                    break;
                case RIA_RESIDENT_ALIEN: gcType = GC_OTHER_ID;
                    break;
                default: gcType = GC_OTHER_ID;
                    break;
            }
            return new IDType(riaType, gcType);
        }

        /// <summary>
        /// PUBLIC CONSTRUCTOR:
        /// Create and ID Type from a Golden Crown ID Type Description.
        /// </summary>
        /// <param name="gcType"></param>
        /// <returns></returns>
        public static IDType S_CreateFromGoldenCrownIDType(string gcType)
        {
            if (gcType == null || gcType.Length < 1)
            {
                throw new InvalidDataException(Messages.S_GetMessage("InvalidIDType"));
            }
            string riaType = "";
            switch (gcType)
            {
                case GC_RUSSIA_PASSPORT: riaType = RIA_PASSPORT;
                    break;
                case GC_FOREGN_PASSPORT: riaType = RIA_PASSPORT;
                    break;
                case GC_DRIVERS_LICENSE: riaType = RIA_DRIVERSLICENSE;
                    break;
                case GC_OTHER_ID: riaType = RIA_OTHER;
                    break;
                default: riaType = RIA_OTHER;
                    break;
            }
            return new IDType(riaType, gcType);
        }


        public string GetRiaIDTypeDesc()
        {
            return m_riaIDType;
        }
        public string GetGoldenCrownIDTypeDesc()
        {
            return m_gcIDType;
        }
        public int GetRiaIDTypeID()
        {
            return m_riaIDTypeID;
        }
    }

}
