﻿using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ITaxAddressService
    {
        List<systblApp_CoreAPI_TaxAddress> GetAllTaxAddresss();
        void CreateTaxAddress(systblApp_CoreAPI_TaxAddress objectEntry);
        void UpdateTaxAddress(systblApp_CoreAPI_TaxAddress objectEntry);
        void RemoveTaxAddress(systblApp_CoreAPI_TaxAddress objectEntry);
        
    }
}
