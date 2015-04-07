using System;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Utilities;
using Newtonsoft.Json;

namespace CES.CoreApi.Logging.Formatters
{
    public class JsonDataContainerFormatter : IJsonDataContainerFormatter
    {
        #region Public methods

        /// <summary>
        /// Gets log entry formatted
        /// </summary>
        /// <param name="dataContainer">Log entry data</param>
        /// <returns></returns>
        public string Format(IDataContainer dataContainer)
        {
            if (dataContainer == null)
                throw new ArgumentNullException("dataContainer");

            return JsonConvert.SerializeObject(dataContainer, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    ContractResolver = new SkipEmptyContractResolver(),
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        #endregion //Public methods
    }
}
