USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Document_Type_Update', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Document_Type_Update AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Update a systblApp_CoreAPI_Document_Type record  

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Document_Type_Update] 
    @Id uniqueidentifier,
    @Code nvarchar(60),
    @Description nvarchar(400) = NULL,
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

	UPDATE [dbo].[systblApp_CoreAPI_Document_Type]
	SET    [Id] = @Id, [Code] = @Code, [Description] = @Description, [fDisabled] = @fDisabled, [fDelete] = @fDelete, [fChanged] = @fChanged, [fTime] = @fTime, [fModified] = @fModified, [fModifiedID] = @fModifiedID
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Code], [Description], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_Document_Type]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Document_Type_Update] to [fxCoreAPI_Role] as [dbo]
GO

