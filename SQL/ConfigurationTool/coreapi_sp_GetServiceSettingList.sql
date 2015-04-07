
use [fxDB6]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('coreapi_sp_GetServiceSettingList', 'P') IS NULL
      EXEC('CREATE PROCEDURE dbo.coreapi_sp_GetServiceSettingList AS SELECT 1')
GO

ALTER PROCEDURE [dbo].[coreapi_sp_GetServiceSettingList] 
	@fAppID int = null,
	@fAppObjectID int = null,
	@lUserNameID int = null,
	@serviceAppId int
AS
-- =============================================
/*  
:Description:   
                Gets Core API service setting list
:Database Target:   
                fxDb6
:Revision History:  
                03-30-2015 OK  Initial version   
:Example Query:              
                EXEC [dbo].[coreapi_sp_GetServiceSettingList]
*/
-- =============================================

BEGIN
    SET NOCOUNT ON;      

	select	[fCoreApiSettingsID] as Id,
			[fName] as Name,
			[fValue] as Value,
			[fDescription] as [Description]
	from [dbo].[systblApp_CoreAPI_Settings] with (nolock)
	where [fAppID] = @serviceAppId

END

GO

GRANT EXEC on [coreapi_sp_GetServiceSettingList] to [fxCoreAPI_Role] as [dbo]
GO
