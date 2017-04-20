USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Document_SelectByOrderNumberAndFolio', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Document_SelectByOrderNumberAndFolio AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Select a   systblApp_CoreAPI_Document record by OrderNo and Folio

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Document_SelectByOrderNumberAndFolio] 
    @OrderNo nvarchar(50),
	@Folio int 
AS 

	SELECT *
	FROM   systblApp_CoreAPI_Document WITH(NOLOCK)
	WHERE  (OrderNo = @OrderNo)  AND Folio = @Folio



GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Document_SelectByOrderNumberAndFolio] to [fxCoreAPI_Role] as [dbo]
GO

