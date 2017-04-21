USE [fxDB6]
GO
/****** Object:  Table [dbo].[systblApp_CoreAPI_Document_Detail]    Script Date: 4/20/2017 7:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_Document_Detail

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_Document_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_Document_Detail](
	[Id] [uniqueidentifier] NOT NULL,
	[LineNumber] [int] NOT NULL,
	[DocumentId] [uniqueidentifier] NOT NULL,
	[ItemId] [uniqueidentifier] NOT NULL,
	[DocRefFolio] [nvarchar](50) NULL,
	[DocRefType] [nvarchar](50) NULL,
	[Count] [int] NULL,
	[Code] [nvarchar](50) NULL,
	[Price] [money] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_DocumentDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Document_Detail_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_Document_Detail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Document_Detail_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Document_Detail_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_Document_Detail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Document_Detail_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Document_Detail_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_Document_Detail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Document_Detail_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Document_Detail_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_Document_Detail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Document_Detail_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_Document_Detail] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'systblApp_CoreAPI_Document_Detail' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty
 @name=N'systblApp_CoreAPI_Document_Detail'
,@value=N'Store items for each document where each detail row represents a line in each document parent' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'Id' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Id' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'Id'
,@value=N'Document Detail ID Code' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'Id'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'LineNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'LineNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'LineNumber'
,@value=N'Correlative identifier when the item was entered' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'LineNumber'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'DocumentId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'DocumentId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'DocumentId'
,@value=N'Document ID Referenced by Detail' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'DocumentId'
GO
 
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'ItemId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'ItemId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'ItemId'
,@value=N'Item Id which identified the item referenced in Detail' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'ItemId'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'DocRefFolio' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'DocRefFolio' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'DocRefFolio'
,@value=N'Folio Number referenced when the item is related to another document for example in Nota de Credito' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'DocRefFolio'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'DocRefType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'DocRefType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'DocRefType'
,@value=N'Document Type referenced when the item is related to another document as in Nota de Credito' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'DocRefType'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'Count' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Count' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'Count'
,@value=N'Quantity of Items related to this Detail Line' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'Count'
GO
 
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'Code' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Code' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'Code'
,@value=N'Item Code related to this Line Detail' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'Code'
GO
 
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'Price' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Price' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'Price'
,@value=N'Price assigned when item is related to this detail' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'Price'
GO
 
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
,@value=N'Disable Flag. If true the record is disabled' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'fDisabled'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
,@value=N'Delete Flag. If true the record is deleted' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'fDelete'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
,@value=N'Change Flag. If true the record is changed' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'fChanged'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
,@value=N'Creation Date  of record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'fTime'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
,@value=N'Edition Date of record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'fModified'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document_Detail')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
,@value=N'User id who modified the record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_Document_Detail'
,@level2type=N'COLUMN'
,@level2name=N'fModifiedID'
GO