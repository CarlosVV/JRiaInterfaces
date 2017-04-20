USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Document_Detail_Update', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Document_Detail_Update AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Update  a systblApp_CoreAPI_Document_Detail record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Document_Detail_Update] 
    @Id uniqueidentifier,
    @LineNumber int,
    @DocumentId uniqueidentifier,
    @ItemId uniqueidentifier,
    @DocRefFolio nvarchar(50) = NULL,
    @DocRefType nvarchar(50) = NULL,
    @Count int = NULL,
    @Code nvarchar(50) = NULL,
    @Price money = NULL,
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

	UPDATE [dbo].[systblApp_CoreAPI_Document_Detail]
	SET    [Id] = @Id, [LineNumber] = @LineNumber, [DocumentId] = @DocumentId, [ItemId] = @ItemId, [DocRefFolio] = @DocRefFolio, [DocRefType] = @DocRefType, [Count] = @Count, [Code] = @Code, [Price] = @Price, [fDisabled] = @fDisabled, [fDelete] = @fDelete, [fChanged] = @fChanged, [fTime] = @fTime, [fModified] = @fModified, [fModifiedID] = @fModifiedID
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [LineNumber], [DocumentId], [ItemId], [DocRefFolio], [DocRefType], [Count], [Code], [Price], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]
	FROM   [dbo].[systblApp_CoreAPI_Document_Detail]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Document_Detail_Update] to [fxCoreAPI_Role] as [dbo]
GO

