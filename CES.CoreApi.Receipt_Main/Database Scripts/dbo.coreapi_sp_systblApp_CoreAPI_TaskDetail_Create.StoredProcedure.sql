USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_TaskDetail_Create', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_TaskDetail_Create AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Create a new systblApp_CoreAPI_TaskDetail

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_TaskDetail_Create] 
    @Id uniqueidentifier,
    @Task uniqueidentifier = NULL,
    @StateObject nvarchar(MAX) = NULL,
    @ResultObject nvarchar(MAX) = NULL,
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
	
	INSERT INTO [dbo].[systblApp_CoreAPI_TaskDetail] ([Id], [Task], [StateObject], [ResultObject], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID])
	SELECT @Id, @Task, @StateObject, @ResultObject, @fDisabled, @fDelete, @fChanged, @fTime, @fModified, @fModifiedID
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Task], [StateObject], [ResultObject], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_TaskDetail]
	WHERE  [Id] = @Id
	-- End Return Select <- do not remove
               
	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_TaskDetail_Create] to [fxCoreAPI_Role] as [dbo]
GO

