USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Document_Detail_Get', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Document_Detail_Get AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Select a systblApp_CoreAPI_Document_Detail record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Document_Detail_Get] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [LineNumber], [DocumentId], [ItemId], [DocRefFolio], [DocRefType], [Count], [Code], [Price], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID] 
	FROM   [dbo].[systblApp_CoreAPI_Document_Detail] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT


GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Document_Detail_Get] to [fxCoreAPI_Role] as [dbo]
GO

