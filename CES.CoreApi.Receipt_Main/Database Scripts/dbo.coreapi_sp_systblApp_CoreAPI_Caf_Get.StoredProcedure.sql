USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Caf_Get', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Caf_Get AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Select systblApp_CoreAPI_Caf records

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

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

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Caf_Get] to [fxCoreAPI_Role] as [dbo]
GO

