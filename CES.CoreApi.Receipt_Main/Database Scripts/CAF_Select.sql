USE [CES.CoreApi.Receipt_MainDB]
GO
/****** Object:  StoredProcedure [dbo].[CAF_Select]    Script Date: 3/24/2017 11:43:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CAF_Select]
@Id UNIQUEIDENTIFIER = NULL, 
@DocumentType INT = NULL, 
@FolioCurrentNumber INT = NULL, 
@FolioStartNumber INT = NULL, 
@FolioEndNumber INT = NULL
AS 
SELECT Id, CompanyTaxId, CompanyLegalName, DocumentType, FolioCurrentNumber, FolioStartNumber, FolioEndNumber, DateAuthorization, FileContent
FROM  [dbo].[CAF] 
WHERE 
(@Id IS NULL OR (@Id = Id)) AND
(@DocumentType IS NULL OR (@DocumentType = DocumentType)) AND
(@FolioCurrentNumber IS NULL OR (@FolioCurrentNumber = FolioCurrentNumber)) AND
(@FolioStartNumber IS NULL OR (@FolioStartNumber = FolioStartNumber)) AND
(@FolioEndNumber IS NULL OR (@FolioEndNumber = FolioEndNumber))
