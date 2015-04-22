
use [fxDB6]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('coreapi_sp_GetServiceList', 'P') IS NULL
      EXEC('CREATE PROCEDURE dbo.coreapi_sp_GetServiceList AS SELECT 1')
GO

ALTER PROCEDURE [dbo].[coreapi_sp_GetServiceList] 
	@fAppID int = null,
	@fAppObjectID int = null,
	@lUserNameID int = null
AS
-- =============================================
/*  
:Description:   
                Gets Core API service list
:Database Target:   
                fxDb6
:Revision History:  
                03-30-2015 OK  Initial version   
:Example Query:              
                EXEC [dbo].[coreapi_sp_GetServiceList]
*/
-- =============================================

BEGIN
    SET NOCOUNT ON;      

	SELECT	ap.fAppID as ServiceId,
			ap.fName as Name
	FROM systblConst2 c  with (nolock)
	inner join systblAppObject ao  with (nolock)
		on c.fKey2 = ao.fObjectTypeID
	inner join systblapp ap  with (nolock)
		on ap.fAppID = ao.fAppID
	where c.fKey1 = 501 and c.fKey2 = 500

END

GO

GRANT EXEC on [coreapi_sp_GetServiceList] to [fxCoreAPI_Role] as [dbo]
GO
