
USE Test --fxDB6
GO

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
	from [systblapp]
	where [fAppID] = @applicationId
	
	--Select list of application configuration items
	select	ConfigurationName,
			ConfigurationValue
	from ApplicationConfiguration
	where ApplicationID = @applicationId

	--Select list of service operations for application
	select ServiceOperationID, 
		   MethodName, 
		   IsActive
	from ServiceOperation
	where ApplicationID = @applicationId

	--Select list of applications assigned to operation
	select aso.ApplicationID,
		   aso.IsActive,
		   so.ServiceOperationID
	from Application_ServiceOperation aso
	inner join ServiceOperation so
		on aso.ServiceOperationID = so.ServiceOperationID
	where so.ApplicationID = @applicationId

	--Select list of servers where application installed
	select s.ApplicationServerID,
		   s.Name,
		   aas.IsActive
	from Application_ApplicationServer aas
	inner join ApplicationServer s
		on aas.ApplicationServerID = s.ApplicationServerID
	where aas.ApplicationID = @applicationId

END

GO
--GRANT EXEC on [CoreApiFoundation_GetApplicationByID] to [fxRIAMT_Role] as [dbo]
--GO
