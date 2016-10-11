USE [fxDB6]
GO
/****** Object:  StoredProcedure [dbo].[coreApi_sp_RequestFromCreate]    Script Date: 5/18/2016 10:51:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[coreApi_sp_RequestFromCreate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[coreApi_sp_RequestFromCreate]
GO
*/
/*
    :Description: Create row of Requester From Data (RequesterInfo)
    :Database Target: fxDB6 (Main)
    :Revision History: 2016-05-13 cp, SCR 2587711, Created
    :Example Query: exec coreApi_sp_RequestFromCreate  
		@fPersistenceEventID =0 
		,@fAppID =999
		,@fAppObjectID =88899
		,@fAgentID =223344
		,@fLocAgentID =223344
		,@fUserID =0
		,@fTerminalID ='CL001'
		,@fTerminalUserID = 'CL001-01'
		,@fLocalTime= getdate()
		,@fUtcTime = dateadd(hour,-3,getdate())
		,@fTimezone = '-3'
		,@fLocale ='es-cl'
		,@fVersion ='1'
		,@fDisabled  =0
		,@fDelete  = 0
		,@fChanged  = 0
		,@fTime=getdate()
		,@fModified  = null
		,@fModifiedID  = 0
*/

ALTER PROCEDURE [dbo].[coreApi_sp_RequestFromCreate]
@fRequesterInfoID int output,
@fPersistenceEventID int = 0,
@fAppID int,
@fAppObjectID int,
@fAgentID int,
@fLocAgentID int,
@fUserID int,
@fTerminalID varchar(100),
@fTerminalUserID varchar(100),
@fLocalTime datetime,
@fUtcTime datetime,
@fTimezone varchar(100),
@fLocale varchar(50),
@fVersion varchar(50),
@fDisabled bit =0,
@fDelete bit = 0,
@fChanged bit = 0,
@fTime datetime,
@fModified datetime = null,
@fModifiedID int = 0
AS

INSERT INTO fxDB6WorkSpace.dbo.systblApp_CoreAPI_Request_From (fPersistenceEventID
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
,fModifiedID)
VALUES (@fPersistenceEventID,
@fAppID,
@fAppObjectID,
@fAgentID,
@fLocAgentID,
@fUserID,
@fTerminalID,
@fTerminalUserID,
@fLocalTime ,
@fUtcTime ,
@fTimezone ,
@fLocale,
@fVersion,
@fDisabled,
@fDelete,
@fChanged,
@fTime ,
@fModified ,
@fModifiedID )

SET @fRequesterInfoID = SCOPE_IDENTITY()

