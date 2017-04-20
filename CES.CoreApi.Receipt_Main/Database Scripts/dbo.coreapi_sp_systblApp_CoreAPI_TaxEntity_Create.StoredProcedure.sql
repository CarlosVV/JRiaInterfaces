USE [fxDB6]
GO
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_TaxEntity_Create', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_TaxEntity_Create AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Create a new systblApp_CoreAPI_TaxEntity record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_TaxEntity_Create] 
    @Id uniqueidentifier,
    @RUT nvarchar(20) = NULL,
    @FirstName nvarchar(150) = NULL,
    @MiddleName nvarchar(150) = NULL,
    @LastName1 nvarchar(150) = NULL,
    @LastName2 nvarchar(150) = NULL,
    @FullName nvarchar(620) = NULL,
    @Gender nvarchar(100) = NULL,
    @Occupation nvarchar(400) = NULL,
    @DateOfBirth datetime = NULL,
    @Nationality nvarchar(400) = NULL,
    @CountryOfBirth nvarchar(400) = NULL,
    @Phone nvarchar(30) = NULL,
    @CellPhone nvarchar(30) = NULL,
    @Email nvarchar(300) = NULL,
    @LineOfBusiness nvarchar(150) = NULL,
    @EconomicActivity int = NULL,
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
	
	INSERT INTO [dbo].[systblApp_CoreAPI_TaxEntity] ([Id], [RUT], [FirstName], [MiddleName], [LastName1], [LastName2], [FullName], [Gender], [Occupation], [DateOfBirth], [Nationality], [CountryOfBirth], [Phone], [CellPhone], [Email], [LineOfBusiness], [EconomicActivity], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID])
	SELECT @Id, @RUT, @FirstName, @MiddleName, @LastName1, @LastName2, @FullName, @Gender, @Occupation, @DateOfBirth, @Nationality, @CountryOfBirth, @Phone, @CellPhone, @Email, @LineOfBusiness, @EconomicActivity, @fDisabled, @fDelete, @fChanged, @fTime, @fModified, @fModifiedID
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [RUT], [FirstName], [MiddleName], [LastName1], [LastName2], [FullName], [Gender], [Occupation], [DateOfBirth], [Nationality], [CountryOfBirth], [Phone], [CellPhone], [Email], [LineOfBusiness], [EconomicActivity], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_TaxEntity]
	WHERE  [Id] = @Id
	-- End Return Select <- do not remove
               
	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_TaxEntity_Create] to [fxCoreAPI_Role] as [dbo]
GO

