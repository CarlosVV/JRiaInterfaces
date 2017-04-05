USE [fxDB6]
GO
/****** Object:  StoredProcedure [dbo].[coreApi_sp_PresistenceCreate]    Script Date: 5/18/2016 10:55:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[coreApi_sp_PresistenceCreate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[coreApi_sp_PresistenceCreate]
GO
*/
/*
    :Description: Create row of Persistence header Data
    :Database Target: fxDB6 (Main)
    :Revision History: 2016-05-13 cp, SCR 2587711, Created
    :Example Query: coreApi_sp_PresistenceCreate
		@fDisabled  = 0,
		@fDelete  = 0,
		@fChanged   = 0,
		@fTime=getdate() ,
		@fModified =null,
		@fModifiedID  = 0
*/

ALTER PROCEDURE [dbo].[coreApi_sp_PresistenceCreate]
@fPersistenceID int output,
@fDisabled bit = 0,
@fDelete bit = 0,
@fChanged bit  = 0,
@fTime datetime ,
@fModified datetime =null,
@fModifiedID int = 0

AS


INSERT INTO fxDB6WorkSpace.dbo.systblApp_CoreAPI_Request_Persistence (fDisabled
,fDelete
,fChanged
,fTime
,fModified
,fModifiedID)
VALUES (@fDisabled
,@fDelete
,@fChanged
,@fTime
,@fModified
,@fModifiedID)

SET @fPersistenceID =  SCOPE_IDENTITY()

