using CES.CoreApi.Foundation.Contract.Attributes;

namespace CES.CoreApi.Foundation.Contract.Enumerations
{
    public enum SubSystemError
    {
        [SubSystemErrorNumber("00000")]
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
        ClientApplicationNotAuthenticated,

        [SubSystemErrorNumber("00201")]
        [ErrorMessage("Application is not assigned to service operation. ApplicationID='{0}', Service Operation='{1}'")]
        ApplicationIsNotAssignedToServiceOperation,

        [SubSystemErrorNumber("00202")]
        [ErrorMessage("Application assigned to service operation but it is not active. ApplicationID='{0}', Service Operation='{1}'")]
        ApplicationAssignedToServiceOperationNotActive,

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
        

        #endregion
    }
}