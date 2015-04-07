USE [fxDB6]
Go

print 'BEGIN'

declare @fAppID as int, @fType as int, @fName as varchar(255), @fDescription as varchar(255), @fModifiedID as int, @currentTime as dateTime

--Insert Geolocation Service application
select @fAppID = 8000, @fType = 10, @fName = 'Geolocation Service', @fDescription = 'Geolocation Core API service', @fModifiedID = 34177711, @currentTime = GETDATE()

IF NOT EXISTS (SELECT TOP 1 1 FROM systblapp WITH (NOLOCK) WHERE fAppID = @fAppID)
begin
	insert into systblapp ([fAppID],[fType],[fName],[fDescription],[fModifiedID],[fTime])
		values (@fAppID, @fType, @fName, @fDescription, @fModifiedID, @currentTime)
end
else
begin
	print 'Error: The record already exists:'
		+ 'fAppID = ' + ISNULL(CAST(@fAppID AS VARCHAR), '')
end

--Insert Geolocation Service Test application
select @fAppID = 8010, @fName = 'Geolocation Service Test', @fDescription = 'Geolocation Core API service test application'

IF NOT EXISTS (SELECT TOP 1 1 FROM systblapp WITH (NOLOCK) WHERE fAppID = @fAppID)
begin
	insert into systblapp ([fAppID],[fType],[fName],[fDescription],[fModifiedID],[fTime])
		values (@fAppID, @fType, @fName, @fDescription, @fModifiedID, @currentTime)
end
else
begin
	print 'Error: The record already exists:'
		+ 'fAppID = ' + ISNULL(CAST(@fAppID AS VARCHAR), '')
end
print 'END'
	