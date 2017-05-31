using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Core
{
    public class XmlDocumentParser<T> where T : class
    {
        public T GetDocumentObjectFromString(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stream = XmlHelper.GenerateStreamFromString(xml);
            var obj = serializer.Deserialize(stream) as T;
            return obj;
        }

        public string GetStringFromDocumentObject(T obj)
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stringwriter, obj);
            return stringwriter.ToString();
        }
    }
}
