use [fxDB6]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('coreapi_sp_GetApplicationByID', 'P') IS NULL
      EXEC('CREATE PROCEDURE dbo.coreapi_sp_GetApplicationByID AS SELECT 1')
GO

ALTER PROCEDURE [dbo].[coreapi_sp_GetApplicationByID] 
       @applicationId int
AS
-- =============================================
/*  
:Description:   
                Used to get application details by application identifier
:Database Target:   
                fxDb6
:Revision History:  
                09-25-2014 OK  Initial version   
:Example Query:              
                EXEC [dbo].[coreapi_sp_GetApplicationByID]  @applicationId = 8000
*/
-- =============================================

BEGIN
    SET NOCOUNT ON;      

	--Select applicaiton details
	select	case when fDisabled = 1 then 0 else 1 end as IsActive,
			fName as Name
	from [systblapp] with (nolock)
	where [fAppID] = @applicationId
	
	--Select list of application configuration items
	select	[fName] as ConfigurationName,
			[fValue] as ConfigurationValue
	from [dbo].[systblApp_CoreAPI_Settings] with (nolock)
	where [fAppID] = @applicationId

	--Select list of service operations for application
	select [fAppObjectID] as ServiceOperationID, 
		   [fName] as MethodName, 
		   case when fDisabled = 1 then 0 else 1 end as IsActive
	from [dbo].[systblAppObject] with (nolock)
	where [fAppID] = @applicationId

	--Select list of applications assigned to operation
	select aso.[fAppID] as ApplicationID,
		   aso.[fAppObjectID] as ServiceOperationID, 
		   case when aso.fDisabled = 1 then 0 else 1 end as IsActive
	from [dbo].[systblApp_MapObject] aso with (nolock)
	inner join [dbo].[systblAppObject] so with (nolock) on aso.[fAppObjectID] = so.[fAppObjectID]
	where so.[fAppID] = @applicationId

END

GO

GRANT EXEC on [coreapi_sp_GetApplicationByID] to [fxCoreAPI_Role] as [dbo]
GO
