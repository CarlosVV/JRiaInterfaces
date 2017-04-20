USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Document_Get', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Document_Get AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Select a systblApp_CoreAPI_Document record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Document_Get] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [OrderNo], [Type], [Folio], [Branch], [TellerNumber], [TellerName], [Issued], [Amount], [Tax], [TotalAmount], [SenderId], [ReceiverId], [SentToSII], [DownloadedSII], [Date], [RecAgent], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID] 
	FROM   [dbo].[systblApp_CoreAPI_Document] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Document_Get] to [fxCoreAPI_Role] as [dbo]
GO

