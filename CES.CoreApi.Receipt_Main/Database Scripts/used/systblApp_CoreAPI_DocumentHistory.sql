USE [fxDB6]
GO
 
SET ANSI_NULLS ON
GO
GO
/*
:Description:
	systblApp_CoreAPI_DocumentHistory

:Database Target:
	fxDB6

:Revision History:
	2017-10-24	CV	Created

*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_DocumentHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_DocumentHistory](
	[fDocumentHistoryId] [int] NOT NULL,
	[fDocumentId] [int] NOT NULL,
	[fOrderNo] [varchar](20) NOT NULL,
	[fOrderStatus] [varchar](10) NOT NULL,
	[fDescription] [varchar](25) NULL,
	[fDate] [datetime] NOT NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_DocumentHistory] PRIMARY KEY CLUSTERED 
(
	[fDocumentHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END

GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentHistory_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentHistory] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentHistory_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentHistory_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentHistory] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentHistory_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentHistory_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentHistory] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentHistory_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentHistory_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentHistory] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentHistory_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_DocumentHistory] to [fxCoreAPI_Role]
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'systblApp_CoreAPI_DocumentHistory' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty
  @name=N'systblApp_CoreAPI_DocumentHistory'
, @value=N'Store information about Order Status Changes' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fDocumentHistoryId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocumentHistoryId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
  @name=N'fDocumentHistoryId'
, @value=N'Vat Id ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fDocumentHistoryId'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fDocumentId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocumentId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
  @name=N'fDocumentId'
, @value=N'Document Id ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fDocumentId'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fOrderNo' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fOrderNo' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
  @name=N'fOrderNo'
, @value=N'Order Number ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fOrderNo'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fOrderStatus' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fOrderStatus' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
  @name=N'fOrderStatus'
, @value=N'Order Status ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fOrderStatus'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fDescription' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDescription' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
  @name=N'fDescription'
, @value=N'Order History Description' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fDescription'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fDate' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDate' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
  @name=N'fDate'
, @value=N'History Date ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fDate'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentHistory')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentHistory'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO
