SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('CoreApiFoundation_GetApplicationByID', 'P') IS NULL
      EXEC('CREATE PROCEDURE dbo.CoreApiFoundation_GetApplicationByID AS SELECT 1')
GO

ALTER PROCEDURE [dbo].[CoreApiFoundation_GetApplicationByID] 
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
                EXEC [dbo].[CoreApiFoundation_GetApplicationByID]  @applicationId = 8010
*/
-- =============================================

BEGIN
    SET NOCOUNT ON;      

	--Select applicaiton details
	select	IsActive = case when fDisabled = 1 then 0 else 1 end,
			fName as Name
	from [systblapp] with (nolock)
	where [fAppID] = @applicationId
	
	--Select list of application configuration items
	select	ConfigurationName,
			ConfigurationValue
	from ApplicationConfiguration with (nolock)
	where ApplicationID = @applicationId

	--Select list of service operations for application
	select ServiceOperationID, 
		   MethodName, 
		   IsActive
	from ServiceOperation with (nolock)
	where ApplicationID = @applicationId

	--Select list of applications assigned to operation
	select aso.ApplicationID,
		   aso.IsActive,
		   so.ServiceOperationID
	from Application_ServiceOperation aso with (nolock)
	inner join ServiceOperation so with (nolock)
		on aso.ServiceOperationID = so.ServiceOperationID
	where so.ApplicationID = @applicationId

	--Select list of servers where application installed
	select s.ApplicationServerID,
		   s.Name,
		   aas.IsActive
	from Application_ApplicationServer aas  with (nolock)
	inner join ApplicationServer s  with (nolock)
		on aas.ApplicationServerID = s.ApplicationServerID
	where aas.ApplicationID = @applicationId

END

GO

--GRANT EXEC on [CoreApiFoundation_GetApplicationByID] to [fxRIAMT_Role] as [dbo]
--GO
