using System;

namespace CES.CoreApi.GeoLocation.WebApi.Controllers
{
    internal class Product
    {
        public DateTime Expiry { get; internal set; }
        public string Name { get; internal set; }
        public string[] Sizes { get; internal set; }
    }
}