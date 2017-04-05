using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Models.DTOs;
using CES.CoreApi.Receipt_Main.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Services
{
    public class TaxService
    {
        private TaxRepositoryCached _repository;

        public TaxService()
        {
            _repository = new TaxRepositoryCached();
        }

        internal Tax GenerateTax(TaxGenerateRequest taxGenerateRequest)
        {
            return _repository.GenerateTax(taxGenerateRequest);
        }

        internal object UpdateCAF(TaxUpdateCAFRequest taxUpdateCAFInternalRequest)
        {
            throw new NotImplementedException();
        }

        internal object DeleteCAF(TaxDeleteCAFRequest taxDeleteCAFInternalRequest)
        {
            throw new NotImplementedException();
        }

        internal object GetFolio(TaxGetFolioRequest taxGetFolioInternalRequest)
        {
            throw new NotImplementedException();
        }
    }
}