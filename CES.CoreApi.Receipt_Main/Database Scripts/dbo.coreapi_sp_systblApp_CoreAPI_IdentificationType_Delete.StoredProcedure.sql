USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_IdentificationType_Delete', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_IdentificationType_Delete AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Delete a systblApp_CoreAPI_IdentificationType record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_IdentificationType_Delete] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[systblApp_CoreAPI_IdentificationType]
	WHERE  [Id] = @Id

	COMMIT

GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_IdentificationType_Delete] to [fxCoreAPI_Role] as [dbo]
GO

