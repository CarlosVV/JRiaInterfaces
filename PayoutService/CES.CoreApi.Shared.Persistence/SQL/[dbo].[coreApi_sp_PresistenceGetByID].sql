USE [fxDB6]
GO
/****** Object:  StoredProcedure [dbo].[coreApi_sp_PresistenceGetByID]    Script Date: 5/18/2016 10:52:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[coreApi_sp_PresistenceGetByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[coreApi_sp_PresistenceGetByID]
GO
*/
/*
    :Description: Get Persitence Data by ID
    :Database Target: fxDB6 (Main)
    :Revision History: 2016-05-13 cp, SCR 2587711, Created
    :Example Query: exec coreApi_sp_PresistenceGetByID @fPersistenceID =6
*/


ALTER PROCEDURE [dbo].[coreApi_sp_PresistenceGetByID]
@fPersistenceID  int 

AS
--Persistence (1)
Select fPersistenceID
,fDisabled
,fDelete
,fChanged
,fTime
,fModified
,fModifiedID from [dbo].[systblApp_CoreAPI_Request_Persistence]  with(nolock)
where fPersistenceID = @fPersistenceID


--Persistence events (2)
select fPersistenceEventID
,fPersistenceID
,fAppID
,fProviderID
,fPersistenceEventTypeID
,fRequesterInfoID
,fContent
,fDisabled
,fDelete
,fChanged
,fTime
,fModified
,fModifiedID from [dbo].[systblApp_CoreAPI_Request_Persistence_Event] with(nolock)
where fPersistenceID = @fPersistenceID 

--RequesterInfo (3)

select fRequesterInfoID
,fPersistenceEventID
,fAppID
,fAppObjectID
,fAgentID
,fLocAgentID
,fUserID
,fTerminalID
,fTerminalUserID
,fLocalTime
,fUtcTime
,fTimezone
,fLocale
,fVersion
,fDisabled
,fDelete
,fChanged
,fTime
,fModified
,fModifiedID 
from [dbo].[systblApp_CoreAPI_Request_From] with(nolock)
where fRequesterInfoID  in (select fRequesterInfoID from [dbo].[systblApp_CoreAPI_Request_Persistence_Event] with(nolock) where fPersistenceID = @fPersistenceID )

