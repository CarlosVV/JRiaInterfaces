USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Document_Update', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Document_Update AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Update a systblApp_CoreAPI_Document record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Document_Update] 
    @Id uniqueidentifier,
    @OrderNo nvarchar(50) = NULL,
    @Type uniqueidentifier = NULL,
    @Folio int = NULL,
    @Branch nvarchar(100) = NULL,
    @TellerNumber nvarchar(100) = NULL,
    @TellerName nvarchar(100) = NULL,
    @Issued datetime = NULL,
    @Amount money = NULL,
    @Tax money = NULL,
    @TotalAmount money = NULL,
    @SenderId uniqueidentifier = NULL,
    @ReceiverId uniqueidentifier = NULL,
    @SentToSII bit = NULL,
    @DownloadedSII bit = NULL,
    @Date nchar(10) = NULL,
    @RecAgent nchar(10) = NULL,
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

	UPDATE [dbo].[systblApp_CoreAPI_Document]
	SET    [Id] = @Id, [OrderNo] = @OrderNo, [Type] = @Type, [Folio] = @Folio, [Branch] = @Branch, [TellerNumber] = @TellerNumber, [TellerName] = @TellerName, [Issued] = @Issued, [Amount] = @Amount, [Tax] = @Tax, [TotalAmount] = @TotalAmount, [SenderId] = @SenderId, [ReceiverId] = @ReceiverId, [SentToSII] = @SentToSII, [DownloadedSII] = @DownloadedSII, [Date] = @Date, [RecAgent] = @RecAgent, [fDisabled] = @fDisabled, [fDelete] = @fDelete, [fChanged] = @fChanged, [fTime] = @fTime, [fModified] = @fModified, [fModifiedID] = @fModifiedID
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [OrderNo], [Type], [Folio], [Branch], [TellerNumber], [TellerName], [Issued], [Amount], [Tax], [TotalAmount], [SenderId], [ReceiverId], [SentToSII], [DownloadedSII], [Date], [RecAgent], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_Document]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Document_Update] to [fxCoreAPI_Role] as [dbo]
GO

