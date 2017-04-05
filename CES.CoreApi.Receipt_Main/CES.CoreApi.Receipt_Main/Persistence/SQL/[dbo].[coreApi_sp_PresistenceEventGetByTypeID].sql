USE [fxDB6]
GO
/****** Object:  StoredProcedure [dbo].[coreApi_sp_PresistenceEventGetByTypeID]    Script Date: 5/18/2016 10:53:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[coreApi_sp_PresistenceEventGetByTypeID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[coreApi_sp_PresistenceEventGetByTypeID]
GO
*/
/*
    :Description: Get PersitenceEvent  Data by ID an type
    :Database Target: fxDB6 (Main)
    :Revision History: 2016-05-13 cp, SCR 2587711, Created
    :Example Query: exec coreApi_sp_PresistenceEventGetByTypeID @fPersistenceID =6, @fPersistenceEventTypeID =1
*/


ALTER PROCEDURE [dbo].[coreApi_sp_PresistenceEventGetByTypeID]
@fPersistenceID  int ,
@fPersistenceEventTypeID int

AS

declare @fPersistenceEventID int
declare @fRequesterInfoID int

select top 1 @fPersistenceEventID = fPersistenceEventID , @fRequesterInfoID=fRequesterInfoID
from [dbo].[systblApp_CoreAPI_Request_Persistence_Event] with(nolock)
where fPersistenceID = @fPersistenceID 
and fPersistenceEventTypeID=@fPersistenceEventTypeID

--Persistence events (1)
select top 1 fPersistenceEventID
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
where fPersistenceEventID = @fPersistenceEventID

--RequesterInfo (2)
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
where fRequesterInfoID = @fRequesterInfoID

