using Microsoft.VisualStudio.TestTools.UnitTesting;
using CES.CoreApi.Receipt_Main.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Receipt_Main.Model.Documents;

namespace CES.CoreApi.Receipt_Main.Repositories.Tests
{
    [TestClass()]
    public class CAFRepositoryTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            //var repository = new CAFRepository();

            //var obj = new Caf
            //{
            //    CompanyTaxId = "123456",
            //    CompanyLegalName = "company1",
            //    DocumentType = 88,
            //    FolioStartNumber = 1,
            //    FolioEndNumber = 10,
            //    DateAuthorization = "2017-03-16",
            //    FileContent = "<xml>"
            //};
            //repository.Create(obj);
            //Assert.IsNotNull(obj.Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            //var repository = new CAFRepository();

            //var obj = new Caf
            //{
            //    Id = Guid.Parse("9E98325F-EB83-43D1-B80E-C66117C45CE9"),
            //    CompanyTaxId = "123456",
            //    CompanyLegalName = "company1 Update",
            //    DocumentType = 66,
            //    FolioStartNumber = 10,
            //    FolioEndNumber = 100,
            //    DateAuthorization = "2017-04-16",
            //    FileContent = "<xml>"
            //};

            //var result = repository.Update(obj);
            //Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //var repository = new CAFRepository();

            //var id = "9E98325F-EB83-43D1-B80E-C66117C45CE9";

            //var result = repository.Delete(id);

            //Assert.IsTrue(result);
        }

        //[TestMethod()]
        public void GetTest()
        {
            //var repository = new CAFRepository();

            ////var id = "9E98325F-EB83-43D1-B80E-C66117C45CE9";

            //var result = repository.Get(id: null, documentType: 88, folioCurrentNumber: null, folioStartNumber: null, folioEndNumber: null);

            //Assert.IsFalse(result.Count() > 0);
        }
    }
}