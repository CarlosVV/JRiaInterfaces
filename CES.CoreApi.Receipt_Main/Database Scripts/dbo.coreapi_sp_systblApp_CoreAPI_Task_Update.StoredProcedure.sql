USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Task_Update', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Task_Update AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Update a systblApp_CoreAPI_Task record  

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Task_Update] 
    @Id uniqueidentifier,
    @Type uniqueidentifier = NULL,
    @Method nvarchar(120) = NULL,
    @RequestObject nvarchar(MAX) = NULL,
    @ThreadId int = NULL,
    @StartDateTime datetime = NULL,
    @EndDateTime datetime = NULL,
    @LastExecutionDateTime datetime = NULL,
    @Status uniqueidentifier = NULL,
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

	UPDATE [dbo].[systblApp_CoreAPI_Task]
	SET    [Id] = @Id, [Type] = @Type, [Method] = @Method, [RequestObject] = @RequestObject, [ThreadId] = @ThreadId, [StartDateTime] = @StartDateTime, [EndDateTime] = @EndDateTime, [LastExecutionDateTime] = @LastExecutionDateTime, [Status] = @Status, [fDisabled] = @fDisabled, [fDelete] = @fDelete, [fChanged] = @fChanged, [fTime] = @fTime, [fModified] = @fModified, [fModifiedID] = @fModifiedID
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Type], [Method], [RequestObject], [ThreadId], [StartDateTime], [EndDateTime], [LastExecutionDateTime], [Status], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_Task]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Task_Update] to [fxCoreAPI_Role] as [dbo]
GO

