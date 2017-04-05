USE [CES.CoreApi.Receipt_MainDB]
GO
/****** Object:  StoredProcedure [dbo].[CAF_Insert]    Script Date: 3/24/2017 11:40:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CAF_Insert] 
@Id UNIQUEIDENTIFIER, 
@CompanyTaxId NVARCHAR(1000), 
@CompanyLegalName NVARCHAR(1000), 
@DocumentType INT, 
@FolioCurrenttNumber INT, 
@FolioStartNumber INT, 
@FolioEndNumber INT,  
@DateAuthorization nvarchar(10), 
@FileContent nvarchar(max)
AS 
INSERT INTO [dbo].[CAF] (Id, CompanyTaxId, CompanyLegalName, DocumentType, FolioCurrentNumber, FolioStartNumber, FolioEndNumber, DateAuthorization, FileContent)
VALUES (@Id, @CompanyTaxId, @CompanyLegalName, @DocumentType, @FolioCurrenttNumber, @FolioStartNumber, @FolioEndNumber, @DateAuthorization, @FileContent)

