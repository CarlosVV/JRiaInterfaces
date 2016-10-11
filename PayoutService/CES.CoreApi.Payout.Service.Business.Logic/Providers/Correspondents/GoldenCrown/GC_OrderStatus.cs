using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents.GoldenCrown
{

    /// <summary>
    /// Defines the orders status states used by Golden Crown.
    /// 
    /// Author  : David Go
    /// </summary>
    class GC_OrderStatus
    {
        //Golden Crown Order States:
        public const int CANCELED = 0;
        public const int NOT_PAID_1 = 1;
        public const int NOT_PAID_2 = 2;
        public const int NOT_PAID_3 = 3;
        public const int READY_FOR_PAYOUT = 4;
        public const int REQUESTED_FOR_PAYOUT = 5;
        public const int REFUNDED_6 = 6;
        public const int PAID_OUT = 7;
        public const int REFUNDED_8 = 8;
        public const int ORDER_EXPIRED = -1;
        public const int UNKNOWN = 99;

        public const string CANCELED_DESC = "Canceled";
        public const string NOT_PAID_1_DESC = "Paid";
        public const string NOT_PAID_2_DESC = "Paid";
        public const string NOT_PAID_3_DESC = "Paid";
        public const string READY_FOR_PAYOUT_DESC = "Ready for Payout";
        public const string REQUESTED_FOR_PAYOUT_DESC = "Requested for Payout";
        public const string REFUNDED_6_DESC = "Refunded";
        public const string PAID_OUT_DESC = "Paid Out";
        public const string REFUNDED_8_DESC = "Refunded";
        public const string ORDER_EXPIRED_DESC = "Expired";
        public const string OUTDATED_EXPIRED_TAG = ".1";// This will be added to the status if the order is expired. Ex: 4.1
        public const string UNKNOWN_DESC = "Unknown";

        //Status holders:
        private int m_orderStatus;
        private string m_orderStatusDesc;

        /// <summary>
        /// CONSTRUCTOR:
        /// </summary>
        /// <param name="gcStatus"></param>
        public GC_OrderStatus(string gcStatus)
        {
            try
            {
                int gcStatusInt = Int32.Parse(gcStatus);
                m_orderStatus = gcStatusInt;
                m_orderStatusDesc = GetDescriptionFromStatus(m_orderStatus);
            }
            catch (Exception)
            {
                m_orderStatus = ORDER_EXPIRED;
                m_orderStatusDesc = ORDER_EXPIRED_DESC;
            }
        }


        public int GetOrderStatus()
        {
            return m_orderStatus;
        }
        public string GetOrderStatusDescription()
        {
            return m_orderStatusDesc;
        }

        /// <summary>
        /// Translate a status number into a description.
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        private string GetDescriptionFromStatus(int orderStatus)
        {
            string desc = "";
            switch (orderStatus)
            {
                case CANCELED: desc = CANCELED_DESC;
                    break;
                case NOT_PAID_1: desc = NOT_PAID_1_DESC;
                    break;
                case NOT_PAID_2: desc = NOT_PAID_2_DESC;
                    break;
                case NOT_PAID_3: desc = NOT_PAID_3_DESC;
                    break;
                case READY_FOR_PAYOUT: desc = READY_FOR_PAYOUT_DESC;
                    break;
                case REQUESTED_FOR_PAYOUT: desc = REQUESTED_FOR_PAYOUT_DESC;
                    break;
                case REFUNDED_6: desc = REFUNDED_6_DESC;
                    break;
                case PAID_OUT: desc = PAID_OUT_DESC;
                    break;
                case REFUNDED_8: desc = REFUNDED_8_DESC;
                    break;
                case UNKNOWN: desc = UNKNOWN_DESC;
                    break;
                default: desc = "Unknown";
                    break;
            }
            return desc;
        }


    }
}
