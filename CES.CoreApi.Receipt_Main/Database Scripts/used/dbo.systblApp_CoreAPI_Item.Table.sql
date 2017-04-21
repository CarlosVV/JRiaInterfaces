USE [fxDB6]
GO
/****** Object:  Table [dbo].[systblApp_CoreAPI_Item]    Script Date: 4/20/2017 7:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_Item

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_Item]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_Item](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](4000) NULL,
	[Value] [money] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Item_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_Item] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Item_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Item_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_Item] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Item_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Item_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_Item] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Item_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Item_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_Item] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Item_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_Item] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'systblApp_CoreAPI_Item' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty 
@name=N'systblApp_CoreAPI_Item'
, @value=N'Store information of products or conceptos sold o billed by the company, in this case a commission as general in other case  an accountable concept' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Item'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'Id' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Id' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty 
@name=N'Id'
, @value=N'Item Id Code'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Item'
, @level2type=N'COLUMN'
, @level2name=N'Id'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'Code' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Code' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty 
@name=N'Code'
, @value=N'Item surrogate Code in human readability'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Item'
, @level2type=N'COLUMN'
, @level2name=N'Code'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'Description' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Description' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty 
@name=N'Description'
, @value=N'Item Description'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Item'
, @level2type=N'COLUMN'
, @level2name=N'Description'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'Value' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Value' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty 
@name=N'Value'
, @value=N'Item Value in money'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Item'
, @level2type=N'COLUMN'
, @level2name=N'Value'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
,@value=N'Disable Flag. If true the record is disabled' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Item'
,@level2type=N'COLUMN'
,@level2name=N'fDisabled'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
,@value=N'Delete Flag. If true the record is deleted' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Item'
,@level2type=N'COLUMN'
,@level2name=N'fDelete'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
,@value=N'Change Flag. If true the record is changed' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Item'
,@level2type=N'COLUMN'
,@level2name=N'fChanged'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
,@value=N'Creation Date  of record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Item'
,@level2type=N'COLUMN'
,@level2name=N'fTime'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
,@value=N'Edition Date of record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Item'
,@level2type=N'COLUMN'
,@level2name=N'fModified'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Item') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Item')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
,@value=N'User id who modified the record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Item'
,@level2type=N'COLUMN'
,@level2name=N'fModifiedID'
GO