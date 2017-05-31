using CES.CoreApi.Receipt_Main.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CES.CoreApi.Receipt_Main.CAFUtilities
{
    public class CAFParser
    {
        public AUTORIZACION GetCAFObjectFromString(string xml)
        {
            var serializer = new XmlSerializer(typeof(AUTORIZACION));
            var stream = XmlHelper.GenerateStreamFromString(xml);
            var obj = serializer.Deserialize(stream) as AUTORIZACION;
            return obj;
        }

        public string GetStringFromCAFObject(AUTORIZACION obj)
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stringwriter, obj);
            return stringwriter.ToString();
        }
    }
}