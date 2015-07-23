﻿using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces
{
    public interface ITransactionProcessor
    {
        TransactionDetailsModel GetDetails(int orderId, int databaseId);
    }
}