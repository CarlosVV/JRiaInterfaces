using System.Xml.Linq;

namespace CES.CoreApi.Data.Tools
{
    public static class XmlTools
    {
        public static T GetValue<T>(this XElement xElement, string name, XNamespace xNamespace = null)
        {
            var element = xNamespace == null ?
                xElement.Element(name) :
                xElement.Element(xNamespace + name);

            var value = element == null ? null : element.Value;

            return value.ConvertValue<T>();
        }
    }
}
