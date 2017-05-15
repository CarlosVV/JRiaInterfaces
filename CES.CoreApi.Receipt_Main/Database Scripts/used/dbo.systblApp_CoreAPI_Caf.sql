USE [fxDB6]
GO
/****** Object:  Table [dbo].[systblApp_CoreAPI_Caf]    Script Date: 4/20/2017 7:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON

GO
/*
:Description:
	systblApp_CoreAPI_Caf, stores information about CAF XML FILES

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_Caf]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_Caf](
	[Id] int NOT NULL,
	[CompanyRUT] [nvarchar](15) NOT NULL,
	[CompanyLegalName] [nvarchar](50) NOT NULL,
	[DocumentType] [int] NOT NULL,
	[FolioCurrentNumber] [int] NOT NULL,
	[FolioStartNumber] [int] NOT NULL,
	[FolioEndNumber] [int] NOT NULL,
	[AuthorizationDate] [date] NOT NULL,
	[FileContent] [nvarchar](max) NOT NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_CAF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Caf_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_Caf] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Caf_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Caf_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_Caf] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Caf_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Caf_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_Caf] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Caf_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Caf_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_Caf] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Caf_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_Caf] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'systblApp_CoreAPI_Caf' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty 
@name=N'systblApp_CoreAPI_Caf'
, @value=N'Store information of CAF Files'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'Id' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Id' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty 
@name=N'Id'
, @value=N'systblApp_CoreAPI_Caf Id Code'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'Id'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'CompanyRUT' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'CompanyRUT' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty 
@name=N'CompanyRUT'
, @value=N'Company TaxAmount Id for Example RUT' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'CompanyRUT'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'CompanyLegalName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'CompanyLegalName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty 
@name=N'CompanyLegalName'
, @value=N'Name registered in TaxAmount Institution that identifies in the government to pay taxes for example against SII Chile' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo', @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'CompanyLegalName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'DocumentType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'DocumentType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'DocumentType'
, @value=N'Document Type Value as boleta, factura, nota de credito or debito ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'DocumentType'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'FolioCurrentNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'FolioCurrentNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'FolioCurrentNumber'
, @value=N'Folio Current Number available to assign to a new document issued by SII TaxAmount System' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'FolioCurrentNumber'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'FolioStartNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'FolioStartNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'FolioStartNumber'
, @value=N'Folio Start Number available to assign to the first folio issued in the SII TaxAmount System ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'FolioStartNumber'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'FolioEndNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'FolioEndNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'FolioEndNumber'
, @value=N'Folio End Number available to assign to the last folio used for the document issued in the SII TaxAmount System' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'FolioEndNumber'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'AuthorizationDate' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'AuthorizationDate' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'AuthorizationDate'
, @value=N'Date issued by SII TaxAmount System when the CAF was generated' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'AuthorizationDate'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'FileContent' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'FileContent' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'FileContent'
, @value=N'File Content of CAF generated by SII TaxAmount System in XML Format' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'FileContent'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'fDisabled'
, @value=N'Disabled Flag, if value=1 the record disabled else enabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'fDelete'
, @value=N'Delete Flag, if value=1 the record deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'fChanged'
, @value=N'Changed Flag, if value=1 the record changed ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'fTime'
, @value=N'Record Creation Date and Time' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'fModified'
, @value=N'Record Change Date and Time' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'fModifiedID'
, @value=N'User who modified record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO