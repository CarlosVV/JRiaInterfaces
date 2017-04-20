USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Task_Create', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Task_Create AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Create a new [systblApp_CoreAPI_Task] record  

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Task_Create] 
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
	
	INSERT INTO [dbo].[systblApp_CoreAPI_Task] ([Id], [Type], [Method], [RequestObject], [ThreadId], [StartDateTime], [EndDateTime], [LastExecutionDateTime], [Status], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID])
	SELECT @Id, @Type, @Method, @RequestObject, @ThreadId, @StartDateTime, @EndDateTime, @LastExecutionDateTime, @Status, @fDisabled, @fDelete, @fChanged, @fTime, @fModified, @fModifiedID
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Type], [Method], [RequestObject], [ThreadId], [StartDateTime], [EndDateTime], [LastExecutionDateTime], [Status], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_Task]
	WHERE  [Id] = @Id
	-- End Return Select <- do not remove
               
	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Task_Create] to [fxCoreAPI_Role] as [dbo]
GO

