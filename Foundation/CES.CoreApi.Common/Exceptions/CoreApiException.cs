﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using CES.CoreApi.Common.Attributes;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Tools;
using Newtonsoft.Json;

namespace CES.CoreApi.Common.Exceptions
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class CoreApiException: Exception
    {
        private const string ErrorCodeTemplate = "{0}|{1}|{2}|{3}";

        public CoreApiException(TechnicalSubSystem subSystem, SubSystemError subSystemError, Exception ex, params object[] parameters)
            : this(Organization.Ria, TechnicalSystem.CoreApi, subSystem, subSystemError, parameters)
        {
        }

        public CoreApiException(TechnicalSubSystem subSystem, SubSystemError subSystemError, params object[] parameters)
            : this(Organization.Ria, TechnicalSystem.CoreApi, subSystem, subSystemError, parameters)
        {
        }

        public CoreApiException(Organization organization, TechnicalSystem system, 
            TechnicalSubSystem subSystem, SubSystemError error, params object[] parameters)
        {
            ErrorCode = GetErrorCode(organization, system, subSystem, error);
            ClientMessage = string.Format(CultureInfo.InvariantCulture, error.GetAttributeValue<ErrorMessageAttribute, string>(x => x.Message), parameters);
            ErrorId = Guid.NewGuid();
            Organization = organization;
            System = system;
            SubSystem = subSystem;
            SubSystemError = error;
        }

        public CoreApiException(Exception exception)
            : this(exception, TechnicalSubSystem.Undefined, SubSystemError.Undefined)
        {
        }

        public CoreApiException(Exception exception, TechnicalSubSystem subSystem, SubSystemError subSystemError)
        {
            Organization = Organization.Ria;
            System = TechnicalSystem.CoreApi;
            SubSystem = subSystem;
            SubSystemError = subSystemError;
            ErrorCode = GetErrorCode(Organization, System, SubSystem, SubSystemError);
            ClientMessage = exception.Message;
            ErrorId = Guid.NewGuid();
            CallStack = exception.StackTrace;
            Source = exception.Source;
            TargetSite = exception.TargetSite;
        }

        [JsonProperty]
        public string CallStack { get; private set; }
        [JsonProperty]
        public string ErrorCode { get; private set; }
        [JsonProperty]
        public string ClientMessage { get; private set; }
        [JsonProperty]
        public Guid ErrorId { get; private set; }
        [JsonIgnore]
        public Organization Organization { get; private set; }
        [JsonIgnore]
        public TechnicalSystem System { get; private set; }
        [JsonIgnore]
        public TechnicalSubSystem SubSystem { get; private set; }
        [JsonIgnore]
        public SubSystemError SubSystemError { get; private set; }
        [JsonProperty]
        public new string Source { get; private set; }
        [JsonProperty]
        public new MethodBase TargetSite { get; private set; }

        private static string GetErrorCode(Organization organization, TechnicalSystem system,
            TechnicalSubSystem subSystem, SubSystemError error)
        {
            return string.Format(CultureInfo.InvariantCulture,
                ErrorCodeTemplate,
                organization.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                system.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                subSystem.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                error.GetAttributeValue<SubSystemErrorNumberAttribute, string>(x => x.ErrorNumber));
        }
    }
}