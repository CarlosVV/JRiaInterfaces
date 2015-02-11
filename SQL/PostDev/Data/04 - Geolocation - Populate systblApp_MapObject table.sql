
use [fxDB6]
GO

print 'BEGIN'

--Assign permissions to test application
declare @fAppID as int, @fModifiedID as int, @currentDate as datetime
select @fAppID = 8010, @fModifiedID = 34177711, @currentDate = GETDATE()

insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31001, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31002, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31003, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31004, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31005, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31006, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31007, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31008, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31009, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31010, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31011, @currentDate, @fModifiedID, @currentDate)

--Assign permissions to digital application
select @fAppID = 220

--GeocodeAddress
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31004, @currentDate, @fModifiedID, @currentDate)
--GeocodeFormattedAddress
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31005, @currentDate, @fModifiedID, @currentDate)
--Map
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31007, @currentDate, @fModifiedID, @currentDate)
--GetProviderKey
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31010, @currentDate, @fModifiedID, @currentDate)
--LogEvent
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31011, @currentDate, @fModifiedID, @currentDate)

--Assign permissions to FXOnline
select @fAppID = 501

insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31001, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31002, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31003, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31004, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31005, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31006, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31007, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31010, @currentDate, @fModifiedID, @currentDate)
insert into systblApp_MapObject ([fAppID], [fAppObjectID], [fModified], [fModifiedID], [fTime]) 
	values (@fAppID, 31011, @currentDate, @fModifiedID, @currentDate)


print 'END'









