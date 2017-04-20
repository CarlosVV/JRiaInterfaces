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
	[RUT] [nvarchar](20) NULL,
	[FirstName] [nvarchar](150) NULL,
	[MiddleName] [nvarchar](150) NULL,
	[LastName1] [nvarchar](150) NULL,
	[LastName2] [nvarchar](150) NULL,
	[FullName] [nvarchar](620) NULL,
	[Gender] [nvarchar](100) NULL,
	[Occupation] [nvarchar](400) NULL,
	[DateOfBirth] [datetime] NULL,
	[Nationality] [nvarchar](400) NULL,
	[CountryOfBirth] [nvarchar](400) NULL,
	[Phone] [nvarchar](30) NULL,
	[CellPhone] [nvarchar](30) NULL,
	[Email] [nvarchar](300) NULL,
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

