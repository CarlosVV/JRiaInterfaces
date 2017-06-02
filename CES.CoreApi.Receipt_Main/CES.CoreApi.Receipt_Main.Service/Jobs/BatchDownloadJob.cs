using CES.CoreApi.Receipt_Main.Application.Core.Document;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.Infrastructure.Data;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Jobs
{
    public class BatchDownloadJob
    {
        private IDocumentService _documentService;
        private ITaxEntityService _taxEntityService;
        private ITaxAddressService _taxAddressService;
        private ISequenceService _sequenceService;
        private IStoreService _storeService;

        public void BatchDownload()
        {

        }
        public void Execute(int folioStart, int folioEnd)
        {

            _documentService = new CES.CoreApi.Receipt_Main.Application.Core.DocumentService(new DocumentRepository(new ReceiptDbContext()));
            _taxEntityService = new CES.CoreApi.Receipt_Main.Application.Core.TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            _taxAddressService = new CES.CoreApi.Receipt_Main.Application.Core.TaxAddressService(new TaxAddressRepository(new ReceiptDbContext()));
            _sequenceService = new CES.CoreApi.Receipt_Main.Application.Core.SequenceService(new SequenceRepository(new ReceiptDbContext()));
            _storeService = new CES.CoreApi.Receipt_Main.Application.Core.StoreService(new StoreRepository(new ReceiptDbContext()));

            var _documentDownloader = new DocumentDownloader();
            var _documentHelper = new DocumentHandlerService(_documentService, _taxEntityService, _taxAddressService, _sequenceService, _storeService);

            var docType = "39";

            for (var i = folioStart; i <= folioEnd; i++)
            {
                var folio = i;
                var respuesta = string.Empty;
                var _parserBoletas = new XmlDocumentParser<EnvioBOLETA>();
                var indexchunk = 1;
                var acumchunk = 0;

                if (_documentDownloader.RetrieveXML(int.Parse(docType), folio, out respuesta))
                {
                    var documentXmlObject = _parserBoletas.GetDocumentObjectFromString(respuesta);

                    List<int> detailids = null;
                    List<int> docids = null;
                    _documentHelper.SaveDocument(folio, folio, ref indexchunk, ref acumchunk, documentXmlObject, ref detailids, ref docids);
                }
            }

        }
    }
}