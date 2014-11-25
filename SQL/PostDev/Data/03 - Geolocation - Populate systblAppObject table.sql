
USE [fxDB6]
Go

print 'BEGIN'

declare @fAppObjectID as int, @fAppID as int, @fObjectTypeID as int, @currentTime as datetime, @fCreatedByID as int

--Service
select @fAppObjectID = 31000, @fAppID = 8000, @fObjectTypeID = 500, @currentTime = GETDATE(), @fCreatedByID = 34177711

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID, @fAppID, 'Geolocation', @fObjectTypeID, 'Geolocation CoreAPI service', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

--Service operations
select @fObjectTypeID = 501

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+1, @fAppID, 'ValidateAddress', @fObjectTypeID, 'ValidateAddress Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+2, @fAppID, 'ValidateFormattedAddress', @fObjectTypeID, 'ValidateFormattedAddress Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+3, @fAppID, 'GetAutocompleteList', @fObjectTypeID, 'GetAutocompleteList Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+4, @fAppID, 'GeocodeAddress', @fObjectTypeID, 'GeocodeAddress Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+5, @fAppID, 'GeocodeFormattedAddress', @fObjectTypeID, 'GeocodeFormattedAddress Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+6, @fAppID, 'ReverseGeocodePoint', @fObjectTypeID, 'ReverseGeocodePoint Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+7, @fAppID, 'GetMap', @fObjectTypeID, 'GetMap Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+8, @fAppID, 'ClearCache', @fObjectTypeID, 'ClearCache Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+9, @fAppID, 'Ping', @fObjectTypeID, 'Ping Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+10, @fAppID, 'GetProviderKey', @fObjectTypeID, 'GetProviderKey Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

insert into systblAppObject ([fAppObjectID], [fAppID], [fName], [fObjectTypeID], [fDescription], [fTime], [fCreated], [fCreatedByID], fParentID, fDisabled, fDelete, fChanged, fModified, fModifiedID) 
	values (@fAppObjectID+11, @fAppID, 'LogEvent', @fObjectTypeID, 'LogEvent Geolocation service method', @currentTime, @currentTime,@fCreatedByID, 0, 0, 0, 1, @currentTime, @fCreatedByID)

print 'END'

--select * from systblAppObject where fAppID = 8000
