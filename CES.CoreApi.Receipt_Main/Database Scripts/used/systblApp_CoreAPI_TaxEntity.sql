USE [fxDB6]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_TaxEntity

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_TaxEntity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_TaxEntity](
	[fTaxEntityId] int NOT NULL,
	[fRUT] [nvarchar](15) NOT NULL,
	[fFirstName] [nvarchar](50) NOT NULL,
	[fMiddleName] [nvarchar](50) NULL,
	[fLastName1] [nvarchar](50) NULL,
	[fLastName2] [nvarchar](50) NULL,
	[fFullName] [nvarchar](200) NULL,
	[fGender] [nvarchar](2) NULL,
	[fOccupation] [nvarchar](200) NULL,
	[fDateOfBirth] [date] NULL,
	[fNationality] [nvarchar](2) NULL,
	[fCountryOfBirth] [nvarchar](2) NULL,
	[fPhone] [nvarchar](30) NULL,
	[fCellPhone] [nvarchar](30) NULL,
	[fEmail] [nvarchar](100) NULL,
	[fLineOfBusiness] [nvarchar](150) NULL,
	[fEconomicActivity] [int] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[fTaxEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
--SELECT OBJECT_NAME(OBJECT_ID) AS NameofConstraint, SCHEMA_NAME(schema_id) AS SchemaName, OBJECT_NAME(parent_object_id) AS TableName,type_desc AS ConstraintType FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaxEntity_fDisabled'

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaxEntity_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaxEntity] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaxEntity_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaxEntity_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaxEntity] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaxEntity_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaxEntity_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaxEntity] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaxEntity_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaxEntity_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaxEntity] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaxEntity_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_TaxEntity] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'systblApp_CoreAPI_TaxEntity' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty 
@name=N'systblApp_CoreAPI_TaxEntity'
, @value=N'Store information of entities related a Document, an entity can be a document receiver or sender if there is a nota de credito or debito' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fTaxEntityId' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTaxEntityId' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fTaxEntityId'
, @value=N'Tax Entity Id Code'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fTaxEntityId'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fRUT' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fRUT' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fRUT'
, @value=N'Customer RUT (Rol Unico Tributario), ID which identifies each person or company in SII (Servicio de Impuestos Internos) Tax System'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fRUT'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fFirstName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fFirstName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fFirstName'
, @value=N'Customer FirstName'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fFirstName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fMiddleName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fMiddleName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fMiddleName'
, @value=N'Customer MiddleName'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fMiddleName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fLastName1' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fLastName1' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fLastName1'
, @value=N'Customer LastName1'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fLastName1'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fLastName2' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fLastName2' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fLastName2'
, @value=N'Customer LastName2'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fLastName2'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fFullName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fFullName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fFullName'
, @value=N'Customer FullName'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fFullName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fGender' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fGender' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fGender'
, @value=N'Customer Gender'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fGender'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fOccupation' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fOccupation' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fOccupation'
, @value=N'Customer Occupation'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fOccupation'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fDateOfBirth' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDateOfBirth' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fDateOfBirth'
, @value=N'Customer Date Of Birth'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fDateOfBirth'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fNationality' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fNationality' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fNationality'
, @value=N'Customer Nationality'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fNationality'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fCountryOfBirth' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCountryOfBirth' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fCountryOfBirth'
, @value=N'Customer Country Of Birth'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fCountryOfBirth'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fPhone' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fPhone' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fPhone'
, @value=N'Customer Land Phone'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fPhone'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fCellPhone' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fCellPhone' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fCellPhone'
, @value=N'Customer Cell Phone'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fCellPhone'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fEmail' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fEmail' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fEmail'
, @value=N'Customer Email'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fEmail'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fLineOfBusiness' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fLineOfBusiness' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fLineOfBusiness'
, @value=N'Customer Line Of Business'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fLineOfBusiness'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fEconomicActivity' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fEconomicActivity' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'fEconomicActivity'
, @value=N'Customer Economic Activity assigned by SII internally which represents the purpose of the business'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fEconomicActivity'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
, @value=N'Disable Flag. If true the record is disabled' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fDisabled'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
, @value=N'Delete Flag. If true the record is deleted' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fDelete'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
, @value=N'Change Flag. If true the record is changed' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fChanged'
GO



IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
, @value=N'Creation Date  of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fTime'
GO


IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
, @value=N'Edition Date of record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fModified'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
, @value=N'User id who modified the record' 
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'fModifiedID'
GO
