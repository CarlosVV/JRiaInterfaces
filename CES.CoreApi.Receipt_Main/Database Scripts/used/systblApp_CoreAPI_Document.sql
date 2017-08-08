USE [fxDB6]
GO
/****** Object:  Table [dbo].[systblApp_CoreAPI_Document]    Script Date: 4/20/2017 7:18:45 AM ******/
SET ANSI_NULLS ON
GO
GO
/*
:Description:
	systblApp_CoreAPI_Document

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_Document]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_Document](
	[fDocumentId] int NOT NULL,
	[fOrderNo] [nvarchar](20) NOT NULL,
	[fDocumentType] [nvarchar](10) NOT NULL,
	[fFolio] [int] NOT NULL,	
	[fDescription] [nvarchar](255) NULL,
	[fStoreName] [nvarchar](50) NULL,
	[fCashRegisterNumber] [nvarchar](10) NULL,
	[fCashierName] [varchar](100) NULL,
	[fIssuedDate] [datetime] NOT NULL,
	[fExemptAmount] [money] NULL,	
	[fAmount] [money] NOT NULL,
	[fTaxAmount] [money] NOT NULL,
	[fTotalAmount] [money] NOT NULL,
	[fSenderId] int NOT NULL,
	[fReceiverId] int NOT NULL,
	[fSentToSII] [bit] NULL,
	[fDownloadedSII] [bit] NULL,
	[fPaymentDate] [datetime] NOT NULL,
	[fRecAgent] [int] NOT NULL,
	[fPayAgent] [int] NULL,	
	[fTimestampDocument] [datetime] NOT NULL,
	[fTimestampSent] [datetime] NOT NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[fDocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

ALTER TABLE [dbo].[systblApp_CoreAPI_Document] ALTER COLUMN [fCashierName] [varchar](20) null
GO

IF NOT EXISTS(SELECT 1 FROM   INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME = 'systblApp_CoreAPI_Document' AND COLUMN_NAME = 'fXmlDocumentContent')
ALTER TABLE  [dbo].[systblApp_CoreAPI_Document] ADD  fXmlDocumentContent [varchar](8000) null
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Document_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_Document] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Document_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Document_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_Document] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Document_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Document_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_Document] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Document_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_Document_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_Document] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Document_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_Document] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'systblApp_CoreAPI_Document' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty
  @name=N'systblApp_CoreAPI_Document'
, @value=N'Store information of Documents generated by locations like boletas, facturas, notas de credito and debito' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fDocumentId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocumentId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fDocumentId'
, @value=N'Document Id generated by application' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fDocumentId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fOrderNo' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fOrderNo' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fOrderNo'
, @value=N'Order Number referenced by Document' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fOrderNo'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fDocumentType'
, @value=N'Type of Document' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fDocumentType'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fFolio' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fFolio' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fFolio'
, @value=N'Folio Number assigned by System' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fFolio'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fStoreName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fStoreName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fStoreName'
, @value=N'Location where Document was generated' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fStoreName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fDescription' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDescription' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fDescription'
, @value=N'Description' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fDescription'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fCashRegisterNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCashRegisterNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fCashRegisterNumber'
, @value=N'ID identifying the person who generates the Document having the Order' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fCashRegisterNumber'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fCashierName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCashierName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fCashierName'
, @value=N'Teller Name who generated the Order or Document' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fCashierName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fIssuedDate' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fIssuedDate' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fIssuedDate'
, @value=N'Date when it was issued the Document in Chile' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fIssuedDate'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fExemptAmount' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fExemptAmount' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fExemptAmount'
, @value=N'Document Amount without taxes' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fExemptAmount'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fAmount' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fAmount' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fAmount'
, @value=N'Document Amount without taxes' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fAmount'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fTaxAmount' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTaxAmount' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fTaxAmount'
, @value=N'TaxAmount Amount generated by the tx' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fTaxAmount'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fTotalAmount' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTotalAmount' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fTotalAmount'
, @value=N'Sum of Amount and TaxAmount' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fTotalAmount'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fSenderId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSenderId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fSenderId'
, @value=N'Company or Person who is sent the Document' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fSenderId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fReceiverId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fReceiverId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fReceiverId'
, @value=N'Company or Person who receives the Document' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fReceiverId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fSentToSII' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fSentToSII' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fSentToSII'
, @value=N'Boolean Value to know if document was sent to SII.' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fSentToSII'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fDownloadedSII' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDownloadedSII' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fDownloadedSII'
, @value=N'Boolean Value to know if document was download from SII.' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fDownloadedSII'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fPaymentDate' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fPaymentDate' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fPaymentDate'
, @value=N'Payment Date of Document (Pacific Time)' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fPaymentDate'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fRecAgent' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fRecAgent' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fRecAgent'
, @value=N'Agent who receives the Order' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fRecAgent'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fPayAgent' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fPayAgent' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fPayAgent'
, @value=N'Agent who pays the Order' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fPayAgent'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fTimestampDocument' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTimestampDocument' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fTimestampDocument'
, @value=N'Timestamp Document (Pacific Time)' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fTimestampDocument'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fTimestampSent' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTimestampSent' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
 @name=N'fTimestampSent'
, @value=N'Timestamp Sent (Pacific Time)' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fTimestampSent'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fXmlDocumentContent' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fXmlDocumentContent' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
  @name=N'fXmlDocumentContent'
, @value=N'Xml Document Content downloaded from Gdexpress Service' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fXmlDocumentContent'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_Document') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_Document')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_Document'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO
