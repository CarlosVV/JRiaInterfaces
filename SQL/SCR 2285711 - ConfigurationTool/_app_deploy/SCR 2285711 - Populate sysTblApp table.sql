USE [fxDB6]
Go


declare @fAppID as int, @fType as int, @fName as varchar(255), @fDescription as varchar(255), @fModifiedID as int, @currentTime as dateTime

select @fAppID = 8020, @fType = 10, @fName = 'Core API Services Configuration Utility', @fDescription = 'Allows to edit Core API Services configuration settings', @fModifiedID = 34177711, @currentTime = GETDATE()

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

