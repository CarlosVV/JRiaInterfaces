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
	[EntityName] [nvarchar](40) NOT NULL,
	[StartId] [int] NULL,
	[CurrentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityName] ASC
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
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'EntityName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'EntityName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'EntityName'
,@value=N'Entity Name to generate code' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'EntityName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'StartId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'StartId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'StartId'
,@value=N'ID to start to generate IDs for tables' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'StartId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Sequence') AND [name] = N'CurrentId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'CurrentId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Sequence')))
EXEC sys.sp_addextendedproperty
 @name=N'CurrentId'
,@value=N'Current ID for this Entity' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'CurrentId'
GO