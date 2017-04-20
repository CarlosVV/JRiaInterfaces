USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_TaxAddress_Update', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_TaxAddress_Update AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Update a systblApp_CoreAPI_TaxAddress record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_TaxAddress_Update] 
    @Id uniqueidentifier,
    @Entity uniqueidentifier = NULL,
    @Line1 nvarchar(150) = NULL,
    @Line2 nvarchar(150) = NULL,
    @State nvarchar(150) = NULL,
    @Country nvarchar(150) = NULL,
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

	UPDATE [dbo].[systblApp_CoreAPI_TaxAddress]
	SET    [Id] = @Id, [Entity] = @Entity, [Line1] = @Line1, [Line2] = @Line2, [State] = @State, [Country] = @Country, [fDisabled] = @fDisabled, [fDelete] = @fDelete, [fChanged] = @fChanged, [fTime] = @fTime, [fModified] = @fModified, [fModifiedID] = @fModifiedID
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Entity], [Line1], [Line2], [State], [Country], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_TaxAddress]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_TaxAddress_Update] to [fxCoreAPI_Role] as [dbo]
GO

