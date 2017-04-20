USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Caf_Delete', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Caf_Delete AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Delete  a systblApp_CoreAPI_Caf record

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Caf_Delete] 
@Id UNIQUEIDENTIFIER
AS 
DELETE FROM [dbo].[systblApp_CoreAPI_Caf]
WHERE Id = @Id




GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Caf_Delete] to [fxCoreAPI_Role] as [dbo]
GO

