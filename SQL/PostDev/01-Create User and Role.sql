--@ppC0r3ap1

USE [master]
GO
CREATE LOGIN [appUser_CoreAPI] WITH PASSWORD=N'@ppC0r3ap1', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE [fxDB6]
GO
CREATE USER [appUser_CoreAPI] FOR LOGIN [appUser_CoreAPI]
GO
USE [fxDB6]
GO
EXEC sp_addrolemember N'fxCoreAPI_Role', N'appUser_CoreAPI'
GO





