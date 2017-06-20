USE [fxDB6]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_DocumentReference

:Database Target:
	fxDB6

:Revision History:
	2017-05-15	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_DocumentReference]') AND type in (N'U'))
BEGIN

CREATE TABLE [dbo].[systblApp_CoreAPI_DocumentReference](
	[fDocumentReferenceId] [int] NOT NULL,
	[fDocumentId] [int] NOT NULL,
	[fLineNumber] [int] NOT NULL,
	[fDocRefFolio] [nvarchar](10) NULL,
	[fDocRefType] [nvarchar](10) NULL,
	[fDocRefDate] [datetime] NULL,
	[fCodeRef] [nvarchar](10) NULL,
	[fReasonRef] [nvarchar](100) NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_DocumentReference] PRIMARY KEY CLUSTERED 
(
	[fDocumentReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentReference_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentReference] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentReference_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentReference_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentReference] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentReference_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentReference_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentReference] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentReference_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_DocumentReference_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_DocumentReference] ADD  CONSTRAINT [DF_systblApp_CoreAPI_DocumentReference_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_DocumentReference] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'systblApp_CoreAPI_DocumentReference' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty 
@name=N'systblApp_CoreAPI_DocumentReference'
, @value=N'Store information of documents related to a document for example purchase or dispatch orders' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fDocumentReferenceId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocumentReferenceId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty 
@name=N'fDocumentReferenceId'
, @value=N'systblApp_CoreAPI_DocumentReference Id Code'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fDocumentReferenceId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fDocumentId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocumentId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty 
@name=N'fDocumentId'
, @value=N'Document Id related with this reference document'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fDocumentId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fLineNumber' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fLineNumber' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty 
@name=N'fLineNumber'
, @value=N'Document Reference Line Number'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fLineNumber'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fDocRefFolio' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocRefFolio' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty 
@name=N'fDocRefFolio'
, @value=N'Doc Ref Folio related with document referenced'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fDocRefFolio'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fDocRefType' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocRefType' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty 
@name=N'fDocRefType'
, @value=N'Document Reference Type'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fDocRefType'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fDocRefDate' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDocRefDate' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty 
@name=N'fDocRefDate'
, @value=N'Document Reference Date'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fDocRefDate'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fCodeRef' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCodeRef' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty 
@name=N'fCodeRef'
, @value=N'Document Code Reference'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fCodeRef'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fReasonRef' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fReasonRef' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty 
@name=N'fReasonRef'
, @value=N'Document Reference Reason '
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fReasonRef'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_DocumentReference')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_DocumentReference'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO


