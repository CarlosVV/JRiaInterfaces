USE [fxDB6]
GO
/****** Object:  StoredProcedure [dbo].[coreApi_sp_PresistenceEventCreate]    Script Date: 5/18/2016 10:54:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[coreApi_sp_PresistenceEventCreate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[coreApi_sp_PresistenceEventCreate]
GO
*/
/*
    :Description: Create row of Persistence event Data
    :Database Target: fxDB6 (Main)
    :Revision History: 2016-05-13 cp, SCR 2587711, Created
    :Example Query: coreApi_sp_PresistenceEventCreate 
		@fPersistenceID =6,
		@fAppID =999,
		@fProviderID =5000,
		@fPersistenceEventTypeID =1 ,
		@fRequesterInfoID =10,
		@fContent ='',
		@fDisabled  = 0,
		@fDelete  = 0,
		@fChanged   = 0,
		@fTime=getdate() ,
		@fModified =null,
		@fModifiedID  = 0
*/

ALTER PROCEDURE [dbo].[coreApi_sp_PresistenceEventCreate]
@fPersistenceEventID int out,
@fPersistenceID int,
@fAppID int,
@fProviderID int,
@fPersistenceEventTypeID int ,
@fRequesterInfoID int,
@fContent varchar(max),
@fDisabled bit = 0,
@fDelete bit = 0,
@fChanged bit  = 0,
@fTime datetime ,
@fModified datetime =null,
@fModifiedID int = 0

AS

INSERT INTO fxDB6WorkSpace.dbo.systblApp_CoreAPI_Request_Persistence_Event (fPersistenceID
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
,fModifiedID)
VALUES (@fPersistenceID
,@fAppID
,@fProviderID
,@fPersistenceEventTypeID
,@fRequesterInfoID
,@fContent
,@fDisabled
,@fDelete
,@fChanged
,@fTime
,@fModified
,@fModifiedID)

SET @fPersistenceEventID =  SCOPE_IDENTITY()

