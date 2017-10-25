USE [fxDB6]
GO
 
SET ANSI_NULLS ON
GO
GO
/*
:Description:
	systblApp_CoreAPI_Vat

:Database Target:
	fxDB6

:Revision History:
	2017-10-24	CV	Created
	2017-10-25	CV	Modified column names

*/
--DROP TABLE systblApp_CoreAPI_Vat
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_Vat]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_Vat](
	[fVatId] [int] NOT NULL,
	[fDate] [date] NULL,
	[fAccountTotalAmountVat] [money] NULL,
	[fAccountTotalVoidAmountVat] [money] NULL,
	[fAccountResultTotalAmounVat] [money] NULL,
	[fSiiTotalAmountVat] [money] NULL,
	[fSiiTotalAmountVatNotInSii] [money] NULL,
	[fSiiTotalAmountSiiNotInVat] [money] NULL,
	[fSiiTotalVoidCurrentMonthAmountVat] [money] NULL,
	[fSiiTotalOtherMonthsVat] [money] NULL,
	[fSiiStoreTotalAmountCalculatedVat] [money] NULL,
	[fTotalDifferenceAmount] [money] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_Vat] PRIMARY KEY CLUSTERED 
(
	[fVatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Vat_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_Vat] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Vat_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Vat_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_Vat] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Vat_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Vat_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_Vat] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Vat_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Vat_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_Vat] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Vat_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_Vat] to [fxCoreAPI_Role]
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'systblApp_CoreAPI_Vat' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty
  @name=N'systblApp_CoreAPI_Vat'
, @value=N'Store information of Vat Processes' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fVatId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fVatId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fVatId'
, @value=N'Vat Id ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fVatId'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fDate' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDate' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fDate'
, @value=N'Vat Process Date ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fDate'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fAccountTotalAmountVat' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fAccountTotalAmountVat' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fAccountTotalAmountVat'
, @value=N'Accounting Total Amount Vat from FxClient VATReport' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fAccountTotalAmountVat'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fAccountTotalVoidAmountVat' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fAccountTotalVoidAmountVat' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fAccountTotalVoidAmountVat'
, @value=N'Accounting Total Void Amount Vat from FxClient VATReport' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fAccountTotalVoidAmountVat'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fAccountResultTotalAmounVat' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fAccountResultTotalAmounVat' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fAccountResultTotalAmounVat'
, @value=N'Accounting Total Amount Vat from Stores' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fAccountResultTotalAmounVat'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fSiiTotalAmountVat' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSiiTotalAmountVat' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fSiiTotalAmountVat'
, @value=N'GDExpress/SII Total Amount VAT' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fSiiTotalAmountVat'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fSiiTotalAmountVatNotInSii' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSiiTotalAmountVatNotInSii' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fSiiTotalAmountVatNotInSii'
, @value=N'GDExpress/SII Total amount VAT  which are in FxClient VATReport and not in GDExpress/SII' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fSiiTotalAmountVatNotInSii'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fSiiTotalAmountSiiNotInVat' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSiiTotalAmountSiiNotInVat' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fSiiTotalAmountSiiNotInVat'
, @value=N'GDExpress/SII Total amount VAT  which are in GDExpress/SII and not in FxClient VATReport' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fSiiTotalAmountSiiNotInVat'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fSiiTotalVoidCurrentMonthAmountVat' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSiiTotalVoidCurrentMonthAmountVat' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fSiiTotalVoidCurrentMonthAmountVat'
, @value=N'Total void amount in the month' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fSiiTotalVoidCurrentMonthAmountVat'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fSiiTotalOtherMonthsVat' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSiiTotalOtherMonthsVat' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fSiiTotalOtherMonthsVat'
, @value=N'Total void amount from other months' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fSiiTotalOtherMonthsVat'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fSiiStoreTotalAmountCalculatedVat' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSiiStoreTotalAmountCalculatedVat' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fSiiStoreTotalAmountCalculatedVat'
, @value=N'Total VAT amount calculated in Stores' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fSiiStoreTotalAmountCalculatedVat'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fTotalDifferenceAmount' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTotalDifferenceAmount' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
  @name=N'fTotalDifferenceAmount'
, @value=N'Total VAT difference amount between calculated and VATReport' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fTotalDifferenceAmount'
GO
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Vat') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Vat')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Vat'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO
