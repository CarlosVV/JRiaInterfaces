USE [fxDB6]
GO
 
SET ANSI_NULLS ON
GO
GO
/*
:Description:
	systblApp_CoreAPI_VatDetail

:Database Target:
	fxDB6

:Revision History:
	2017-10-24	CV	Created

*/

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_VatDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_VatDetail](
	[fVatDetailId] [int] NOT NULL,
	[fVatId] [int] NOT NULL,
	[fJENo] [varchar](20) NOT NULL,
	[fVatType] [int] NOT NULL,
	[fJEId] [int] NULL,
	[fDepartmentAbbrev] [varchar](10) NULL,
	[fJEDesc] [varchar](20) NULL,
	[fJEDetAmount2] [money] NULL,
	[fCurrencyBase] [varchar](10) NULL,
	[fRefNo] [varchar](10) NULL,
	[fDate] [date] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_VatDetail] PRIMARY KEY CLUSTERED 
(
	[fVatDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END

GO

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_VatDetail_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_VatDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_VatDetail_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_VatDetail_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_VatDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_VatDetail_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_VatDetail_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_VatDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_VatDetail_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_VatDetail_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_VatDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_VatDetail_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_VatDetail] to [fxCoreAPI_Role]
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'systblApp_CoreAPI_VatDetail' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty
  @name=N'systblApp_CoreAPI_VatDetail'
, @value=N'Store information of Vat Transactions' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fVatDetailId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fVatDetailId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fVatDetailId'
, @value=N'Vat Detail Id ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fVatDetailId'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fVatId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fVatId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fVatId'
, @value=N'Vat Id Parent' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fVatId'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fJENo' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fJENo' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fJENo'
, @value=N'Order No' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fJENo'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fVatType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fVatType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fVatType'
, @value=N'Vat Type, 39=credit note, 61=rollback' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fVatType'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fJEId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fJEId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fJEId'
, @value=N'Journal Entry Id' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fJEId'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fDepartmentAbbrev' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDepartmentAbbrev' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fDepartmentAbbrev'
, @value=N'Store Id' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fDepartmentAbbrev'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fJEDesc' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fJEDesc' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fJEDesc'
, @value=N'Journal Entry Description' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fJEDesc'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fJEDetAmount2' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fJEDetAmount2' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fJEDetAmount2'
, @value=N'Tax Amount' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fJEDetAmount2'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fCurrencyBase' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCurrencyBase' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fCurrencyBase'
, @value=N'Tax Amount Currency' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fCurrencyBase'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fRefNo' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fRefNo' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fRefNo'
, @value=N'Reference Number' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fRefNo'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fDate' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDate' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
  @name=N'fDate'
, @value=N'Document Date' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fDate'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_VatDetail')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_VatDetail'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO
