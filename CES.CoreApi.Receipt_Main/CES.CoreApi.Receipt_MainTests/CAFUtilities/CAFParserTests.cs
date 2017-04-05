using Microsoft.VisualStudio.TestTools.UnitTesting;
using CES.CoreApi.Receipt_Main.CAFUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Receipt_Main.Validators.Tests;

namespace CES.CoreApi.Receipt_Main.CAFUtilities.Tests
{
    [TestClass()]
    public class CAFParserTests
    {
        [TestMethod()]
        public void GetCAFObjectFromStringTest()
        {
            var cafparser = new CAFParser();

            var obj = cafparser.GetCAFObjectFromString(CAFValidatorTests.GetDataToTest());

            Assert.IsNotNull(obj);
        }

        [TestMethod()]
        public void GetStringFromCAFObjectTest()
        {
            var cafparser = new CAFParser();

            var str = cafparser.GetStringFromCAFObject(new AUTORIZACION
            {
                CAF = new AUTORIZACIONCAF
                {
                    DA = new AUTORIZACIONCAFDA
                    {
                        FA = new DateTime(2017, 3, 20),
                        RNG = new AUTORIZACIONCAFDARNG
                        {
                            D = 1,
                            H = 100
                        },
                        IDK = 1,
                        RE = "abc",
                        RS = "xyz",
                        TD = 5,
                        RSAPK = new AUTORIZACIONCAFDARSAPK
                        {
                            E = "99",
                            M = "121"
                        }
                    }
                },
                RSAPUBK = "RSAPUBK",
                RSASK = "RSASK"
            });

            Assert.IsNotNull(str);
        }
    }
}