USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_IdentificationType_Create', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_IdentificationType_Create AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Create a new systblApp_CoreAPI_IdentificationType record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_IdentificationType_Create] 
    @Id uniqueidentifier,
    @Code nchar(10) = NULL,
    @Description nchar(10) = NULL,
    @DescriptionNative nchar(10) = NULL,
    @IssuerInstitution nchar(10) = NULL,
    @Country nchar(10) = NULL,
    @fDisabled bit = NULL,
    @fDelete bit = NULL,
    @fChanged bit = NULL,
    @fTime datetime = NULL,
    @fModified datetime = NULL,
    @fModifiedID int = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[systblApp_CoreAPI_IdentificationType] ([Id], [Code], [Description], [DescriptionNative], [IssuerInstitution], [Country], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID])
	SELECT @Id, @Code, @Description, @DescriptionNative, @IssuerInstitution, @Country, @fDisabled, @fDelete, @fChanged, @fTime, @fModified, @fModifiedID
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Code], [Description], [DescriptionNative], [IssuerInstitution], [Country], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_IdentificationType]
	WHERE  [Id] = @Id
	-- End Return Select <- do not remove
               
	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_IdentificationType_Create] to [fxCoreAPI_Role] as [dbo]
GO

