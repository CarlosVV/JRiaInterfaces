USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_TaxEntity_Get', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_TaxEntity_Get AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Get systblApp_CoreAPI_TaxEntity records

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_TaxEntity_Get] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [RUT], [FirstName], [MiddleName], [LastName1], [LastName2], [FullName], [Gender], [Occupation], [DateOfBirth], [Nationality], [CountryOfBirth], [Phone], [CellPhone], [Email], [LineOfBusiness], [EconomicActivity], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID] 
	FROM   [dbo].[systblApp_CoreAPI_TaxEntity] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_TaxEntity_Get] to [fxCoreAPI_Role] as [dbo]
GO

