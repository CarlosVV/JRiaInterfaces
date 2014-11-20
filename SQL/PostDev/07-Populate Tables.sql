




--Populate ApplicationServer table
insert into ApplicationServer ([ApplicationServerID], Name, [Description]) values (1, 'CoreAPISrv1', 'Core API services server #1')

--Populate Application_ApplicationServer table
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (8000, 1, 1)
insert into Application_ApplicationServer (ApplicationID, ApplicationServerID, IsActive) values (8010, 1, 1)

--Populate ServiceOperation table

--Address Verification
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1001, 8010, 'ValidateAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1002, 8010, 'ValidateFormattedAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1003, 8010, 'GetAutocompleteList', 1)

--Geocode
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1006, 8010, 'GeocodeAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1007, 8010, 'GeocodeFormattedAddress', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1008, 8010, 'ReverseGeocodePoint', 1)

--Mapping
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1009, 8010, 'GetMap', 1)

--Health check
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1004, 8010, 'ClearCache', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1005, 8010, 'Ping', 1)

--Client side support
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1010, 8010, 'GetProviderKey', 1)
insert into ServiceOperation (ServiceOperationID, ApplicationID, MethodName, IsActive)
	values (1011, 8010, 'LogEvent', 1)

--Populate Application_ServiceOperation table

--8000 - Test application
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1001, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1002, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1003, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1006, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1007, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1008, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1009, 1)
--health monitoring operations
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1004, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1005, 1)
--Client side support operations
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1010, 1)
insert into Application_ServiceOperation (ApplicationID, ServiceOperationID, IsActive) values (8000, 1011, 1)


