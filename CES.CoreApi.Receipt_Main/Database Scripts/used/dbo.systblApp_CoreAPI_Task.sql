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
	[Id] int NOT NULL,
	[TaskType] [int] NOT NULL,
	[Method] [nvarchar](120) NULL,
	[RequestObject] [nvarchar](max) NULL,
	[ThreadId] [int] NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[LastExecutionDateTime] [datetime] NULL,
	[CountExecution] [int] NULL,	
	[Status] [int] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
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
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'Id' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Id' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'Id'
, @value=N'Task Id Code'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'Id'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'TaskType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'TaskType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'TaskType'
, @value=N'Task Type Id '
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'TaskType'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'Method' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Method' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'Method'
, @value=N'Task Method Name'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'Method'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'RequestObject' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'RequestObject' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'RequestObject'
, @value=N'Object which stores the request in Json format'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'RequestObject'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'ThreadId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'ThreadId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'ThreadId'
, @value=N'Thread Id which identifies the thread or process id of the Task executing o executed'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'ThreadId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'StartDateTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'StartDateTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'StartDateTime'
, @value=N'Task Start Date and Time when the task starts executing'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'StartDateTime'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'EndDateTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'EndDateTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'EndDateTime'
, @value=N'Task End Date and Time when the task stops executing'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'EndDateTime'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'LastExecutionDateTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'LastExecutionDateTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'Task Last Execution Date and Time'
, @value=N'LastExecutionDateTime'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'LastExecutionDateTime'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'CountExecution' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'CountExecution' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'Task Last Count Execution'
, @value=N'CountExecution'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'CountExecution'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Task') AND [name] = N'Status' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Status' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Task')))
EXEC sys.sp_addextendedproperty 
@name=N'Status'
, @value=N'Task Status Id'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Task'
, @level2type=N'COLUMN'
, @level2name=N'Status'
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
