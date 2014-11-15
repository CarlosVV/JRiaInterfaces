
USE Test --fxDB6
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('CoreApiFoundation_GetApplicationByIDAndServerID', 'P') IS NULL
      EXEC('CREATE PROCEDURE dbo.CoreApiFoundation_GetApplicationByIDAndServerID AS SELECT 1')
GO

ALTER PROCEDURE [dbo].[CoreApiFoundation_GetApplicationByIDAndServerID] 
       @applicationId int,
	   @serverId int
AS
-- =============================================
/*  
:Description:   
                Used to get application details by application and server identifiers
:Database Target:   
                fxDb6
:Revision History:  
                09-25-2014 OK  Initial version   
:Example Query:              
                EXEC [dbo].[CoreApiFoundation_GetApplicationByIDAndServerID]  @applicationId = 1000
*/
-- =============================================

BEGIN
    SET NOCOUNT ON;      

	WITH Configuration(ApplicationID, ConfigurationName, ConfigurationValue) AS
	(
		select	ApplicationID,
				ConfigurationName,
				ConfigurationValue
		from ApplicationConfiguration
		where ApplicationID = @applicationId

		UNION ALL
    
		select	@applicationId,
				ConfigurationName,
				ConfigurationValue
		from [Application] a
		left join ApplicationConfiguration ac
			on a.ApplicationID = ac.ApplicationID
		where a.ParentApplicationID is null
	)

	select	a.ApplicationTypeID,
			a.ApplicationToken,
			a.IsActive,
			a.Name,
			c.ConfigurationName,
			c.ConfigurationValue
	from Configuration c
	inner join [Application] a
		on a.ApplicationID = c.ApplicationID
	where a.ApplicationID = @applicationId
		and a.ApplicationServerID = @serverId

END

GO
--GRANT EXEC on [CoreApiFoundation_GetApplicationByIDAndServerID] to [fxRIAMT_Role] as [dbo]
--GO
