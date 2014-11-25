USE [fxDB6]
Go

print 'BEGIN'

declare @fKey1 as int, @fKey2 as int, @fName as VARCHAR(255), @fDesc as VARCHAR(255)

--Insert service record
select @fKey1 = 501, @fKey2 = 500, @fName = 'Core API Service', @fDesc = 'Core API Service'

IF NOT EXISTS (SELECT TOP 1 1 FROM systblConst2 WITH (NOLOCK) WHERE fKey1 = @fKey1 and fKey2 = @fKey2)
BEGIN
	insert into systblConst2 (fKey1, fKey2, fName, fDesc, fTime) 
		values (@fKey1, @fKey2, @fName, @fDesc, GETDATE())
END
ELSE
BEGIN
	print 'Error: The record already exists:'
		+ 'fKey1 = ' + ISNULL(CAST(@fKey1 AS VARCHAR), '')
		+ 'fKey2 = ' + ISNULL(CAST(@fKey2 AS VARCHAR), '')
END

--Insert service operation record
select @fKey1 = 501, @fKey2 = 501, @fName = 'Core API Service Operation', @fDesc = 'Core API Service Operation'

IF NOT EXISTS (SELECT TOP 1 1 FROM systblConst2 WITH (NOLOCK) WHERE fKey1 = @fKey1 and fKey2 = @fKey2)
BEGIN
	insert into systblConst2 (fKey1, fKey2, fName, fDesc, fTime) 
		values (@fKey1, @fKey2, @fName, @fDesc, GETDATE())
END
ELSE
BEGIN
	print 'Error: The record already exists:'
		+ 'fKey1 = ' + ISNULL(CAST(@fKey1 AS VARCHAR), '')
		+ 'fKey2 = ' + ISNULL(CAST(@fKey2 AS VARCHAR), '')
END

print 'END'

--select *  from  systblConst2 where fkey1 = 501 and fkey2 in (500, 501)

