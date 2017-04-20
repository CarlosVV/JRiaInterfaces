USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Caf_Update', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Caf_Update AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Update a systblApp_CoreAPI_Caf record

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

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Caf_Update] to [fxCoreAPI_Role] as [dbo]
GO

