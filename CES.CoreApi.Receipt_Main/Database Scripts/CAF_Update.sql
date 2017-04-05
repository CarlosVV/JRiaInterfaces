USE [CES.CoreApi.Receipt_MainDB]
GO
/****** Object:  StoredProcedure [dbo].[CAF_Update]    Script Date: 3/24/2017 11:49:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CAF_Update]
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
UPDATE [dbo].[CAF]
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

