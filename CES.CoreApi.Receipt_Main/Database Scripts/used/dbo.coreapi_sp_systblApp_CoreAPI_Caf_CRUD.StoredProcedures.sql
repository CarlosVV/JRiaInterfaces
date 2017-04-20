USE [fxDB6]
GO

IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Caf_Create', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Caf_Create AS SELECT 1')
GO

IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Caf_Update', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Caf_Update AS SELECT 1')
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Caf_Get', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Caf_Get AS SELECT 1')
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Caf_Delete', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Caf_Delete AS SELECT 1')
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	CRUD for systblApp_CoreAPI_Caf Table

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Caf_Update]
@Id UNIQUEIDENTIFIER, 
@CompanyTaxId NVARCHAR(1000), 
@CompanyLegalName NVARCHAR(1000), 
@DocumentType INT, 
@FolioCurrentNumber INT, 
@FolioStartNumber INT, 
@FolioEndNumber INT,  
@DateAuthorization nvarchar(10), 
@FileContent nvarchar(max)
AS 
UPDATE [dbo].[systblApp_CoreAPI_Caf]
SET 
CompanyTaxId = @CompanyTaxId, 
CompanyLegalName = @CompanyLegalName, 
DocumentType = @DocumentType, 
FolioCurrentNumber = @FolioCurrentNumber, 
FolioStartNumber = @FolioStartNumber, 
FolioEndNumber = @FolioEndNumber,
DateAuthorization = @DateAuthorization, 
FileContent = @FileContent
WHERE Id = @Id

GO

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Caf_Update]
@Id UNIQUEIDENTIFIER, 
@CompanyTaxId NVARCHAR(1000), 
@CompanyLegalName NVARCHAR(1000), 
@DocumentType INT, 
@FolioCurrentNumber INT, 
@FolioStartNumber INT, 
@FolioEndNumber INT,  
@DateAuthorization nvarchar(10), 
@FileContent nvarchar(max)
AS 
UPDATE [dbo].[systblApp_CoreAPI_Caf]
SET 
CompanyTaxId = @CompanyTaxId, 
CompanyLegalName = @CompanyLegalName, 
DocumentType = @DocumentType, 
FolioCurrentNumber = @FolioCurrentNumber, 
FolioStartNumber = @FolioStartNumber, 
FolioEndNumber = @FolioEndNumber,
DateAuthorization = @DateAuthorization, 
FileContent = @FileContent
WHERE Id = @Id

GO



ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Caf_Get] 
@Id UNIQUEIDENTIFIER = NULL, 
@DocumentType INT = NULL, 
@FolioCurrentNumber INT = NULL, 
@FolioStartNumber INT = NULL, 
@FolioEndNumber INT = NULL
AS 
SELECT Id, CompanyTaxId, CompanyLegalName, DocumentType, FolioCurrentNumber, FolioStartNumber, FolioEndNumber, DateAuthorization, FileContent
FROM  [dbo].[systblApp_CoreAPI_Caf] 
WHERE 
(@Id IS NULL OR (@Id = Id)) AND
(@DocumentType IS NULL OR (@DocumentType = DocumentType)) AND
(@FolioCurrentNumber IS NULL OR (@FolioCurrentNumber = FolioCurrentNumber)) AND
(@FolioStartNumber IS NULL OR (@FolioStartNumber = FolioStartNumber)) AND
(@FolioEndNumber IS NULL OR (@FolioEndNumber = FolioEndNumber))

GO

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Caf_Delete] 
@Id UNIQUEIDENTIFIER
AS 
DELETE FROM [dbo].[systblApp_CoreAPI_Caf]
WHERE Id = @Id

GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Caf_Update] to [fxCoreAPI_Role] as [dbo]
GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Caf_Update] to [fxCoreAPI_Role] as [dbo]
GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Caf_Get] to [fxCoreAPI_Role] as [dbo]
GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Caf_Delete] to [fxCoreAPI_Role] as [dbo]
GO

