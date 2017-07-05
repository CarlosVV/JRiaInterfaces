USE [fxDB6]
GO
/****** Object:  Table [dbo].[systblApp_CoreAPI_Task]    Script Date: 4/20/2017 7:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_Task

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_Task]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_Task](
	[fTaskId] int NOT NULL,
	[fTaskType] [int] NOT NULL,
	[fMethod] [nvarchar](120) NULL,
	[fRequestObject] [varchar](8000) NULL,
	[fThreadId] [int] NULL,
	[fStartDateTime] [datetime] NULL,
	[fEndDateTime] [datetime] NULL,
	[fLastExecutionDateTime] [datetime] NULL,
	[fCountExecution] [int] NULL,	
	[fStatus] [int] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_Task] PRIMARY KEY CLUSTERED 
(
	[fTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Task_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_Task] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Task_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Task_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_Task] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Task_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Task_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_Task] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Task_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Task_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_Task] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Task_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_Task] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'systblApp_CoreAPI_Task' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty 
 @name=N'systblApp_CoreAPI_Task'
, @value=N'Store a task to process like execute a batch download of several documents from SII'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fTaskId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTaskId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fTaskId'
, @value=N'Task Id Code'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fTaskId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fTaskType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTaskType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fTaskType'
, @value=N'Task Type Id '
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fTaskType'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fMethod' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fMethod' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fMethod'
, @value=N'Task Method Name'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fMethod'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fRequestObject' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fRequestObject' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fRequestObject'
, @value=N'Object which stores the request in Json format'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fRequestObject'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fThreadId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fThreadId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fThreadId'
, @value=N'Thread Id which identifies the thread or process id of the Task executing o executed'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fThreadId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fStartDateTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fStartDateTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fStartDateTime'
, @value=N'Task Start Date and Time when the task starts executing'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fStartDateTime'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fEndDateTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fEndDateTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fEndDateTime'
, @value=N'Task End Date and Time when the task stops executing'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fEndDateTime'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fLastExecutionDateTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fLastExecutionDateTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fLastExecutionDateTime'
, @value=N'Task Last Execution Date and Time'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fLastExecutionDateTime'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fCountExecution' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCountExecution' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fCountExecution'
, @value= N'Task Last Count Execution'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fCountExecution'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fStatus' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fStatus' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'fStatus'
, @value=N'Task Status'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fStatus'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO
