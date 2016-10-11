﻿using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class Location 
    {
        /// <summary>
        /// Representing degrees of longitude.
        /// </summary>
        [DataMember(IsRequired = true)]
        public double Longitude { get; set; }

        /// <summary>
        /// Representing degrees of latitude.
        /// </summary>
        [DataMember(IsRequired = true)]
        public double Latitude { get; set; }
    }
}
