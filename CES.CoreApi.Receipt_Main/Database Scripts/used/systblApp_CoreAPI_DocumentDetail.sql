USE [fxDB6]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_DocumentDetail

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_DocumentDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_DocumentDetail](
	[fDocumentDetailId] int NOT NULL,
	[fDocumentId] int NOT NULL,
	[fLineNumber] [int] NOT NULL,	
	[fDocRefFolio] [nvarchar](50) NULL,
	[fDocRefType] [nvarchar](10) NULL,
	[fDescription] [nvarchar](250) NULL,
	[fItemCount] [int] NULL,
	[fCode] [nvarchar](50) NULL,
	[fPrice] [money] NULL,
	[fAmount] [money] NULL,	
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_DocumentDetail] PRIMARY KEY CLUSTERED 
(
	[fDocumentDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentDetail_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentDetail_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentDetail_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentDetail_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentDetail_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentDetail_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentDetail_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentDetail_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_DocumentDetail] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'systblApp_CoreAPI_DocumentDetail' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty
 @name=N'systblApp_CoreAPI_DocumentDetail'
,@value=N'Store items for each document where each detail row represents a line in each document parent' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fDocumentDetailId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocumentDetailId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDocumentDetailId'
,@value=N'Document Detail ID Code' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fDocumentDetailId'
GO
 
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fDocumentId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocumentId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDocumentId'
,@value=N'Document ID Referenced by Detail' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fDocumentId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fLineNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fLineNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fLineNumber'
,@value=N'Correlative identifier when the item was entered' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fLineNumber'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fDocRefFolio' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocRefFolio' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDocRefFolio'
,@value=N'Folio Number referenced when the item is related to another document for example in Nota de Credito' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fDocRefFolio'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fDocRefType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocRefType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDocRefType'
,@value=N'Document Type referenced when the item is related to another document as in Nota de Credito' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fDocRefType'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fDescription' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDescription' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDescription'
,@value=N'Description' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fDescription'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fItemCount' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fItemCount' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fItemCount'
,@value=N'Quantity of Items related to this Detail Line' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fItemCount'
GO
 
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fCode' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCode' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fCode'
,@value=N'Item Code related to this Line Detail' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fCode'
GO
 
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fPrice' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fPrice' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fPrice'
,@value=N'Price assigned when item is related to this detail' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fPrice'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fAmount' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fAmount' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fAmount'
,@value=N'Price assigned when item is related to this detail' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fAmount'
GO
 
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
,@value=N'Disable Flag. If true the record is disabled' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fDisabled'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
,@value=N'Delete Flag. If true the record is deleted' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fDelete'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
,@value=N'Change Flag. If true the record is changed' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fChanged'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
,@value=N'Creation Date  of record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fTime'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
,@value=N'Edition Date of record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fModified'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
,@value=N'User id who modified the record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_DocumentDetail'
,@level2type=N'COLUMN'
,@level2name=N'fModifiedID'
GO