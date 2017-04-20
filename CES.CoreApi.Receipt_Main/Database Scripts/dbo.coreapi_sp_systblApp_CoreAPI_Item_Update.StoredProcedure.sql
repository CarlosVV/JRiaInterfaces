USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Item_Update', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Item_Update AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Update a systblApp_CoreAPI_Item record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Item_Update] 
    @Id uniqueidentifier,
    @Code nvarchar(10),
    @Description nvarchar(4000) = NULL,
    @Value money = NULL,
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

	UPDATE [dbo].[systblApp_CoreAPI_Item]
	SET    [Id] = @Id, [Code] = @Code, [Description] = @Description, [Value] = @Value, [fDisabled] = @fDisabled, [fDelete] = @fDelete, [fChanged] = @fChanged, [fTime] = @fTime, [fModified] = @fModified, [fModifiedID] = @fModifiedID
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Code], [Description], [Value], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_Item]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Item_Update] to [fxCoreAPI_Role] as [dbo]
GO

