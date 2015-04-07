
use [fxDB6]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('coreapi_sp_UpdateServiceSetting', 'P') IS NULL
      EXEC('CREATE PROCEDURE dbo.coreapi_sp_UpdateServiceSetting AS SELECT 1')
GO

ALTER PROCEDURE [dbo].[coreapi_sp_UpdateServiceSetting] 
	@fAppID			int,
	@fAppObjectID	int,
	@lUserNameID	int,
	@settingId		int,
	@value			varchar(max),
	@description	varchar(250)

AS
-- =============================================
/*  
:Description:   
                Updates Core API service setting value and description
:Database Target:   
                fxDb6
:Revision History:  
                03-30-2015 OK  Initial version   
:Example Query:              
                EXEC [dbo].[coreapi_sp_UpdateServiceSetting]
*/
-- =============================================

BEGIN
    SET NOCOUNT ON;      

	update [dbo].[systblApp_CoreAPI_Settings]
	set [fValue] = @value,
		[fDescription] = @description,
		[fModified] = 1,
		[fModifiedID] = @lUserNameID, 
		[fTime] = GETUTCDATE()
	where [fCoreApiSettingsID] = @settingId

END

GO

GRANT EXEC on [coreapi_sp_UpdateServiceSetting] to [fxCoreAPI_Role] as [dbo]
GO
