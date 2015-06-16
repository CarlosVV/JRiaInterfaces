using CES.CoreApi.Common.Attributes;

namespace CES.CoreApi.Common.Enumerations
{
    public enum SubSystemError
    {
        [SubSystemErrorNumber("00000")]
        [ErrorMessage("Undefined")]
        Undefined,

        #region Generic errors

        [SubSystemErrorNumber("00001")]
        [ErrorMessage("Required parameter is null or empty. Parameter name = '{0}'")]
        GeneralRequiredParameterIsUndefined,

        [SubSystemErrorNumber("00002")]
        [ErrorMessage("Invalid string parameter length. Parameter name = '{0}'. Required parameter length = '{1}', Actual parameter length = '{2}'")]
        GeneralInvalidStringParameterLength,

        [SubSystemErrorNumber("00003")]
        [ErrorMessage("Invalid parameter value. Parameter name = '{0}'. Parameter value = '{1}'")]
        GeneralInvalidParameterValue,

        #endregion

        #region Application registration, validation and identification related errors

        [SubSystemErrorNumber("00100")]
        [ErrorMessage("Client application not found or inactive. ApplicationID='{0}'")]
        ClientApplicationDoesNotExistOrInactive,
        
        [SubSystemErrorNumber("00101")]
        [ErrorMessage("Host application not found or inactive. ApplicationID='{0}'")]
        HostApplicationDoesNotExistOrInactive,

        [SubSystemErrorNumber("00102")]
        [ErrorMessage("Host application not found or inactive on server. ApplicationID='{0}', ServerID='{1}'")]
        HostApplicationDoesNotExistOrInactiveOnServer,
        
        [SubSystemErrorNumber("00103")]
        [ErrorMessage("Application ID was not found or incorrect in host application configuration file.")]
        ApplicationIdNotFoundInConfigFile,
        
        [SubSystemErrorNumber("00104")]
        [ErrorMessage("Server ID was not found or incorrect in host application configuration file.")]
        ServerIdNotFoundInConfigFile,

        [SubSystemErrorNumber("00105")]
        [ErrorMessage("Application was not found in database. ApplicationID='{0}'")]
        ApplicationNotFoundInDatabase,

        [SubSystemErrorNumber("00106")]
        [ErrorMessage("Service operation was not found. ApplicationID='{0}', Service Operation='{1}'")]
        ServiceOperationNotFound,

        [SubSystemErrorNumber("00107")]
        [ErrorMessage("Service operation is inactive. ApplicationID='{0}', Service Operation='{1}'")]
        ServiceOperationIsNotActive,

        #endregion

        #region Authentication and Authorization related errors

        [SubSystemErrorNumber("00200")]
        [ErrorMessage("Client application was not authenticated. ApplicationID='{0}'")]
        SecurityClientApplicationNotAuthenticated,

        [SubSystemErrorNumber("00201")]
        [ErrorMessage("Application is not assigned to service operation. ApplicationID='{0}', Service Operation='{1}'")]
        SecurityApplicationIsNotAssignedToServiceOperation,

        [SubSystemErrorNumber("00202")]
        [ErrorMessage("Application assigned to service operation but it is not active. ApplicationID='{0}', Service Operation='{1}'")]
        SecurityApplicationAssignedToServiceOperationNotActive,

        [SubSystemErrorNumber("00203")]
        [ErrorMessage("The caller was not authenticated by the service. See previous exception for details.")]
        SecurityTheCallerWasNotAuthenticatedByTheService,

        #endregion

        #region Service Initialization errors

        [SubSystemErrorNumber("00300")]
        [ErrorMessage("Service initialization failed. IOC Container was not initialized before using.")]
        ServiceIntializationIoCContainerIsNotInitialized,

        [SubSystemErrorNumber("00301")]
        [ErrorMessage("Invalid or unsupported binding defined. Binding = '{0}'.")]
        ServiceIntializationInvalidBinding,

        [SubSystemErrorNumber("00302")]
        [ErrorMessage("Error occurred while trying to read service configuration.")]
        ServiceIntializationConfigurationParseError,

        [SubSystemErrorNumber("00303")]
        [ErrorMessage("No endpoints defined in configuration.")]
        ServiceIntializationNoEndpointsFound,
        
        [SubSystemErrorNumber("00304")]
        [ErrorMessage("The binding name does not match the binding type. Binding Name = '{0}'. Binding Type = '{1}'.")]
        ServiceIntializationBindingNameDoesNotMatch,

        [SubSystemErrorNumber("00305")]
        [ErrorMessage("Service configuration not found.")]
        ServiceIntializationConfigurationNotFound,

        [SubSystemErrorNumber("00306")]
        [ErrorMessage("No endpoints defined in configuration for interface. Interface = '{0}'.")]
        ServiceIntializationNoEndpointsFoundForInterface,

        [SubSystemErrorNumber("00307")]
        [ErrorMessage("Error occurred while trying to read client endpoints configuration.")]
        ServiceIntializationClientEndpointConfigurationParseError,

        [SubSystemErrorNumber("00308")]
        [ErrorMessage("Client endpoints configuration not found.")]
        ServiceIntializationClientEndpointConfigurationNotFound,

        [SubSystemErrorNumber("00309")]
        [ErrorMessage("No client endpoints defined in configuration.")]
        ServiceIntializationNoClientEndpointsFound,

        [SubSystemErrorNumber("00310")]
        [ErrorMessage("No client endpoint found in configuration. Service Interface: '{0}'.")]
        ServiceIntializationNoClientEndpointFound,
        
        #endregion

        #region Geolocation service related errors

        [SubSystemErrorNumber("00400")]
        [ErrorMessage("Url template not found in configuration. Data Provider: '{0}'. Configuration Name: '{1}'")]
        GeolocationUrlTemplateNotFound,

        [SubSystemErrorNumber("00401")]
        [ErrorMessage("Factory entity is not registered in IoC container. Data Provider: '{0}'. Factory Entity: '{1}'")]
        GeolocationFactoryEntityNotRegisteredInContainer,

        [SubSystemErrorNumber("00402")]
        [ErrorMessage("The license key was not found in configuration. Data Provider: '{0}'.")]
        GeolocationLicenseKeyNotFound,

        [SubSystemErrorNumber("00403")]
        [ErrorMessage("The reverse geocoding is not supported by selected data provider. Data Provider: '{0}'.")]
        GeolocationReverseGeocodingIsNotSupported,

        [SubSystemErrorNumber("00404")]
        [ErrorMessage("Data provider for specified service was not found. Data Provider Service Type: '{0}'.")]
        GeolocationDataProviderNotFound,

        [SubSystemErrorNumber("00405")]
        [ErrorMessage("Unsupported factory entity detected. Factory Entity: '{0}'.")]
        GeolocationUnsupportedFactoryEntity,

        [SubSystemErrorNumber("00406")]
        [ErrorMessage("The mapping is not supported by selected data provider. Data Provider: '{0}'.")]
        GeolocationMappingIsNotSupported,

        [SubSystemErrorNumber("00407")]
        [ErrorMessage("The requested location information was not found in data provider response. Data Provider: '{0}'.")]
        GeolocationLocationIsNotFoundInResponse,

        [SubSystemErrorNumber("00408")]
        [ErrorMessage("The country configuration was not found in the database. Application ID = '{0}'. Country Code: '{1}'.")]
        GeolocationContryConfigurationIsNotFound,
        
        #endregion

        #region Order Validaiton service related errors

        [SubSystemErrorNumber("00500")]
        [ErrorMessage("The {0} OFAC match found. CustomerID = '{1}'. FirstName: '{2}'. MiddleName: '{3}'. LastName1: '{4}'. LastName2: '{5}'.")]
        OrderValidationOfacMatchFound,

        [SubSystemErrorNumber("00501")]
        [ErrorMessage("The SAR validation failed. CustomerID = '{0}'. BeneficiaryID: '{1}'. OrderDate: '{2}'. AmountTotal: '{3}'.")]
        OrderValidationSarValidationFailed,

        [SubSystemErrorNumber("00502")]
        [ErrorMessage("The customer is on hold. CustomerID = '{0}'.")]
        OrderValidationCustomerIsOnHold,

        [SubSystemErrorNumber("00503")]
        [ErrorMessage("The beneficiary is blocked. BeneficiaryID [fBenNameID] = '{0}'. CorrespondentID = '{1}'.")]
        OrderValidationBeneficiaryIsBlocked,

        [SubSystemErrorNumber("00504")]
        [ErrorMessage("Duplicated order found. CustomerID = '{0}'. PayAgentId = '{1}'. RecAgentLocationId = '{2}'. RecAgentId = '{3}'. AmountLocal = '{4}'. Currency = '{5}'.")]
        OrderValidationOrderDuplicationFound,

        #endregion
    }
}