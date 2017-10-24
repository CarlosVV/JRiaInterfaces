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
	[fCafId] int NOT NULL,
	[fCompanyRUT] [nvarchar](15) NOT NULL,
	[fCompanyLegalName] [nvarchar](50) NOT NULL,
	[fDocumentType] [nvarchar](10) NOT NULL,
	[fRecAgent] [int] NULL,
	[fFolioCurrentNumber] [int] NOT NULL,
	[fFolioStartNumber] [int] NOT NULL,
	[fFolioEndNumber] [int] NOT NULL,
	[fAuthorizationDate] [date] NOT NULL,
	[fFileContent] [varchar](8000) NOT NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_CAF] PRIMARY KEY CLUSTERED 
(
	[fCafId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
END

GO

ALTER TABLE [dbo].[systblApp_CoreAPI_Caf] ALTER COLUMN [fDocumentType] [varchar](10) null
GO

IF NOT  EXISTS(SELECT 1 FROM   INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME = 'systblApp_CoreAPI_Caf' AND COLUMN_NAME = 'fRecAgent')
ALTER TABLE  [dbo].[systblApp_CoreAPI_Caf] ADD  fRecAgent int null
GO

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
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fCafId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCafId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty 
@name=N'fCafId'
, @value=N'systblApp_CoreAPI_Caf Id Code'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fCafId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fCompanyRUT' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCompanyRUT' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty 
  @name=N'fCompanyRUT'
, @value=N'Company RUT (Rol Unico Tributario) is an ID which identifies each company in Chile. Ref: https://goo.gl/8wCDdh' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fCompanyRUT'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fCompanyLegalName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCompanyLegalName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty 
  @name=N'fCompanyLegalName'
, @value=N'Name registered in Tax Institution that identifies in the government to pay taxes in Chile. Ref: https://goo.gl/R17978' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo', @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fCompanyLegalName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fDocumentType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocumentType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
  @name=N'fDocumentType'
, @value=N'Document Type Value as boleta, factura, nota de credito or debito ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fDocumentType'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fRecAgent' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fRecAgent' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
  @name=N'fRecAgent'
, @value=N'Receiver Agent (a Ria Store) where this CAF will be deployed or used' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fRecAgent'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fFolioCurrentNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fFolioCurrentNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
  @name=N'fFolioCurrentNumber'
, @value=N'Folio Current Number available to assign to a new document issued by SII (Servicio de Impuestos Internos) Tax System' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fFolioCurrentNumber'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fFolioStartNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fFolioStartNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
  @name=N'fFolioStartNumber'
, @value=N'Folio Start Number available to assign to the first folio issued in the SII Tax System ' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fFolioStartNumber'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fFolioEndNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fFolioEndNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
@name=N'fFolioEndNumber'
, @value=N'Folio End Number available to assign to the last folio used for the document issued in the SII Tax System' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fFolioEndNumber'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fAuthorizationDate' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fAuthorizationDate' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
  @name=N'fAuthorizationDate'
, @value=N'Date issued by SII Tax System when the CAF was generated' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fAuthorizationDate'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Caf') AND [name] = N'fFileContent' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fFileContent' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Caf')))
EXEC sys.sp_addextendedproperty
  @name=N'fFileContent'
, @value=N'File Content of CAF generated by SII Tax System in XML Format' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Caf'
, @level2type=N'COLUMN'
, @level2name=N'fFileContent'
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