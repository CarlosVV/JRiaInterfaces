USE [fxDB6]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_TaxAddress

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created
	2017-06-29	DA	fix fCountryId extended property

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_TaxAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_TaxAddress](
	[fTaxAddressId] int NOT NULL,
	[fTaxEntityId] int NULL,
	[fAddress] [nvarchar](150) NULL,
	[fComuna] [nvarchar](50) NULL,
	[fCity] [nvarchar](50) NULL,	
	[fState] [nvarchar](50) NULL,
	[fCountryId] [char](2) NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_TaxAddress] PRIMARY KEY CLUSTERED 
(
	[fTaxAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaxAddress_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaxAddress] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaxAddress_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaxAddress_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaxAddress] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaxAddress_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaxAddress_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaxAddress] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaxAddress_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaxAddress_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaxAddress] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaxAddress_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_TaxAddress] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'systblApp_CoreAPI_TaxAddress' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty 
@name=N'systblApp_CoreAPI_TaxAddress'
, @value=N'Store addresses of each TaxEntity' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fTaxAddressId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTaxAddressId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty 
@name=N'fTaxAddressId'
, @value=N'Tax Address Id Code'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fTaxAddressId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fTaxEntityId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTaxEntityId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty 
@name=N'fTaxEntityId'
, @value=N'Entity Id who belongs this address'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fTaxEntityId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fAddress' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fAddress' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty 
@name=N'fAddress'
, @value=N'Address'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fAddress'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fComuna' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fComuna' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty 
@name=N'fComuna'
, @value=N'Address Comuna'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fComuna'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fCity' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCity' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty 
@name=N'fCity'
, @value=N'Address City'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fCity'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fState' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fState' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty 
@name=N'fState'
, @value=N'Customer Address State'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fState'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fCountryId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCountryId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty 
@name=N'fCountryId'
, @value=N'Customer CountryId '
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fCountry'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxAddress')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxAddress'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO
