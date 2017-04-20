USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Caf_Create', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Caf_Create AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
Create new systblApp_CoreAPI_Caf record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Caf_Create] 
@Id UNIQUEIDENTIFIER, 
@CompanyTaxId NVARCHAR(1000) = NULL, 
@CompanyLegalName NVARCHAR(1000) = NULL, 
@DocumentType INT = NULL, 
@FolioCurrentNumber INT = NULL, 
@FolioStartNumber INT = NULL, 
@FolioEndNumber INT = NULL,  
@DateAuthorization nvarchar(10) = NULL, 
@FileContent nvarchar(max) = NULL
AS 
INSERT INTO [dbo].[systblApp_CoreAPI_Caf] (Id, CompanyTaxId, CompanyLegalName, DocumentType, FolioCurrentNumber, FolioStartNumber, FolioEndNumber, DateAuthorization, FileContent)
VALUES (@Id, @CompanyTaxId, @CompanyLegalName, @DocumentType, @FolioCurrentNumber, @FolioStartNumber, @FolioEndNumber, @DateAuthorization, @FileContent)


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Caf_Create] to [fxCoreAPI_Role] as [dbo]
GO

