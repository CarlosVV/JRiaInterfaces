USE [fxDB6]
GO
 
SET ANSI_NULLS ON
GO
GO
/*
:Description:
	systblApp_CoreAPI_VatProcessDetailProcessDetail

:Database Target:
	fxDB6

:Revision History:
	2017-10-25	CV	Created

*/
--DROP TABLE systblApp_CoreAPI_VatProcessDetail
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_VatProcessDetail]') AND type in (N'U'))
BEGIN

CREATE TABLE [dbo].[systblApp_CoreAPI_VatProcessDetailProcessDetail](
	[fVatProcessDetailId] [int] NOT NULL,
	[fVatId] [int] NOT NULL,
	[fDate] [datetime] NULL,
	[fDetailType] [varchar](10) NULL,
	[fSource] [varchar](10) NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_VatProcessDetailProcessDetail] PRIMARY KEY CLUSTERED 
(
	[fVatProcessDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_VatProcessDetail_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_VatProcessDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_VatProcessDetail_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_VatProcessDetail_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_VatProcessDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_VatProcessDetail_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_VatProcessDetail_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_VatProcessDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_VatProcessDetail_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_VatProcessDetail_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_VatProcessDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_VatProcessDetail_fTime]  DEFAULT (getdate()) FOR [fTime]
GO
GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_VatProcessDetail] to [fxCoreAPI_Role]
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'systblApp_CoreAPI_VatProcessDetail' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty
  @name=N'systblApp_CoreAPI_VatProcessDetail'
, @value=N'Store information of Vat Process Detail' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fVatProcessDetailId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fVatProcessDetailId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fVatProcessDetailId'
, @value=N'Vat Process Detail Id ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fVatProcessDetailId'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fVatId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fVatId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fVatId'
, @value=N'Vat Id ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fVatId'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fDate' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDate' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fDate'
, @value=N'Process Detail Date  ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fDate'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fDetailType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDetailType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fDetailType'
, @value=N'Detail Type (1,2,3,4) for each field in Vat Table' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fDetailType'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fSource' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSource' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fSource'
, @value=N'Source where is located this Orders (A=Accounting Fx Client or O=Operation DB Core API)' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fSource'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatProcessDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatProcessDetail'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO


