USE [fxDB6]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_Sequence - used to generate table IDs

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_Sequence]') AND type in (N'U'))
BEGIN

CREATE TABLE [dbo].[systblApp_CoreAPI_Sequence](
    [fSequenceId] int NOT NULL,
	[fEntityName] [nvarchar](40) NOT NULL,
	[fStartId] [int] NULL,
	[fCurrentId] [int] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[fSequenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_Sequence] to [fxCoreAPI_Role]
GO


--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'systblApp_CoreAPI_Sequence' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty
 @name=N'systblApp_CoreAPI_Sequence'
,@value=N'Store sequence ids for each table' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Sequence'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fSequenceId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSequenceId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fSequenceId'
,@value=N'Sequence ID Code' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Sequence'
,@level2type=N'COLUMN'
,@level2name=N'fSequenceId'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fEntityName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fEntityName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fEntityName'
,@value=N'Entity Name to generate code' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Sequence'
,@level2type=N'COLUMN'
,@level2name=N'fEntityName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fStartId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fStartId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fStartId'
,@value=N'ID to start to generate IDs for tables' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Sequence'
,@level2type=N'COLUMN'
,@level2name=N'fStartId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fCurrentId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCurrentId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fCurrentId'
,@value=N'Current ID for this Entity' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Sequence'
,@level2type=N'COLUMN'
,@level2name=N'fCurrentId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Sequence'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Sequence'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Sequence'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Sequence'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Sequence'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Sequence'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO
