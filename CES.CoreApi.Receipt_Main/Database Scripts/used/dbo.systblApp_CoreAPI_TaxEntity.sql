USE [fxDB6]
GO
/****** Object:  Table [dbo].[systblApp_CoreAPI_TaxEntity]    Script Date: 4/20/2017 7:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_Codes_Map

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_TaxEntity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_TaxEntity](
	[Id] [uniqueidentifier] NOT NULL,
	[RUT] [nvarchar](15) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName1] [nvarchar](50) NULL,
	[LastName2] [nvarchar](50) NULL,
	[FullName] [nvarchar](200) NULL,
	[Gender] [nvarchar](2) NULL,
	[Occupation] [nvarchar](200) NULL,
	[DateOfBirth] [datetime] NULL,
	[Nationality] [nvarchar](2) NULL,
	[CountryOfBirth] [nvarchar](2) NULL,
	[Phone] [nvarchar](30) NULL,
	[CellPhone] [nvarchar](30) NULL,
	[Email] [nvarchar](100) NULL,
	[LineOfBusiness] [nvarchar](150) NULL,
	[EconomicActivity] [int] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
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
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'Id' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Id' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'Id'
, @value=N'Tax Entity Id Code'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'Id'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'RUT' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'RUT' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'RUT'
, @value=N'Customer RUT'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'RUT'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'FirstName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'FirstName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'FirstName'
, @value=N'Customer FirstName'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'FirstName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'MiddleName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'MiddleName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'MiddleName'
, @value=N'Customer MiddleName'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'MiddleName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'LastName1' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'LastName1' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'LastName1'
, @value=N'Customer LastName1'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'LastName1'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'LastName2' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'LastName2' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'LastName2'
, @value=N'Customer LastName2'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'LastName2'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'FullName' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'FullName' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'FullName'
, @value=N'Customer FullName'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'FullName'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'Gender' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Gender' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'Gender'
, @value=N'Customer Gender'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'Gender'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'Occupation' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Occupation' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'Occupation'
, @value=N'Customer Occupation'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'Occupation'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'DateOfBirth' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'DateOfBirth' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'DateOfBirth'
, @value=N'Customer Date Of Birth'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'DateOfBirth'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'Nationality' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Nationality' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'Nationality'
, @value=N'Customer Nationality'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'Nationality'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'CountryOfBirth' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'CountryOfBirth' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'CountryOfBirth'
, @value=N'Customer Country Of Birth'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'CountryOfBirth'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'Phone' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Phone' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'Phone'
, @value=N'Customer Land Phone'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'Phone'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'CellPhone' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'CellPhone' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'CellPhone'
, @value=N'Customer Cell Phone'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'CellPhone'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'Email' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Email' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'Email'
, @value=N'Customer Email'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'Email'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'LineOfBusiness' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'LineOfBusiness' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'LineOfBusiness'
, @value=N'Customer Line Of Business'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'LineOfBusiness'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity') AND [name] = N'EconomicActivity' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'EconomicActivity' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_TaxEntity')))
EXEC sys.sp_addextendedproperty 
@name=N'EconomicActivity'
, @value=N'Customer Economic Activity'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_TaxEntity'
, @level2type=N'COLUMN'
, @level2name=N'EconomicActivity'
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
