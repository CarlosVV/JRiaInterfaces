﻿using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Models
{
    public class GeocodeAddressResponseModel
    {
        /// <summary>
        /// Address geocode status.
        /// Returns true if address was found with requested MinimumConfidence level,
        /// otherwise false
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Address geo location coordinates
        /// </summary>
        public LocationModel Location { get; set; }

        /// <summary>
        /// A string specifying the confidence of the result.
        /// </summary>
        public LevelOfConfidence Confidence { get; set; }

        /// <summary>
        /// Specify data provider used to geocode address
        /// </summary>
        public DataProviderType DataProvider { get; set; }

        /// <summary>
        /// Address which was really geo coded by data provider
        /// </summary>
        public AddressModel Address { get; set; }
    }
}