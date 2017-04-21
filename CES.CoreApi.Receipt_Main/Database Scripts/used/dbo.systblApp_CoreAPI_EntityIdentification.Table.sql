USE [fxDB6]
GO
/****** Object:  Table [dbo].[systblApp_CoreAPI_EntityIdentification]    Script Date: 4/20/2017 7:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_EntityIdentification

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
--*/DROP TABLE [dbo].[systblApp_CoreAPI_EntityIdentification]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_EntityIdentification]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_EntityIdentification](
	[Id] [uniqueidentifier] NOT NULL,
	[Entity] [uniqueidentifier] NULL,
	[Number] [nvarchar](50) NULL,
	[Type] [uniqueidentifier] NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_SubjectIdentification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_EntityIdentification_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_EntityIdentification] ADD  CONSTRAINT [DF_systblApp_CoreAPI_EntityIdentification_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_EntityIdentification_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_EntityIdentification] ADD  CONSTRAINT [DF_systblApp_CoreAPI_EntityIdentification_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_EntityIdentification_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_EntityIdentification] ADD  CONSTRAINT [DF_systblApp_CoreAPI_EntityIdentification_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_EntityIdentification_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_EntityIdentification] ADD  CONSTRAINT [DF_systblApp_CoreAPI_EntityIdentification_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_EntityIdentification] to [fxCoreAPI_Role]
GO

--Table extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'systblApp_CoreAPI_EntityIdentification' AND [minor_id] = 0)
EXEC sys.sp_addextendedproperty 
@name=N'systblApp_CoreAPI_EntityIdentification'
, @value=N'Stores information for each type of Identification related a TaxEntity'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_EntityIdentification'
GO

--Column extended property
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'Id' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Id' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty 
@name=N'Id'
, @value=N'Id'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_EntityIdentification'
, @level2type=N'COLUMN'
, @level2name=N'Id'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'Entity' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Entity' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty 
@name=N'Entity'
, @value=N'Entity Id relating TaxEntity who belongs this ID'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_EntityIdentification'
, @level2type=N'COLUMN'
, @level2name=N'Entity'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'Number' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Number' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty 
@name=N'Number'
, @value=N'Identification Number'
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_EntityIdentification'
, @level2type=N'COLUMN'
, @level2name=N'Number'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'Type' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Type' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty 
@name=N'Type'
, @value=N'Identification Type Id '
, @level0type=N'SCHEMA'
, @level0name=N'dbo'
, @level1type=N'TABLE'
, @level1name=N'systblApp_CoreAPI_EntityIdentification'
, @level2type=N'COLUMN'
, @level2name=N'Type'
GO

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'fDisabled' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDisabled' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty
 @name=N'fDisabled'
,@value=N'Disable Flag. If true the record is disabled' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_EntityIdentification'
,@level2type=N'COLUMN'
,@level2name=N'fDisabled'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'fDelete' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fDelete' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty
 @name=N'fDelete'
,@value=N'Delete Flag. If true the record is deleted' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_EntityIdentification'
,@level2type=N'COLUMN'
,@level2name=N'fDelete'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'fChanged' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fChanged' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty
 @name=N'fChanged'
,@value=N'Change Flag. If true the record is changed' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_EntityIdentification'
,@level2type=N'COLUMN'
,@level2name=N'fChanged'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'fTime' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fTime' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty
 @name=N'fTime'
,@value=N'Creation Date  of record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_EntityIdentification'
,@level2type=N'COLUMN'
,@level2name=N'fTime'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'fModified' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModified' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty
 @name=N'fModified'
,@value=N'Edition Date of record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_EntityIdentification'
,@level2type=N'COLUMN'
,@level2name=N'fModified'
GO
  
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification') AND [name] = N'fModifiedID' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'fModifiedID' AND [object_id] = OBJECT_ID('systblApp_CoreAPI_EntityIdentification')))
EXEC sys.sp_addextendedproperty
 @name=N'fModifiedID'
,@value=N'User id who modified the record' 
,@level0type=N'SCHEMA'
,@level0name=N'dbo'
,@level1type=N'TABLE'
,@level1name=N'systblApp_CoreAPI_EntityIdentification'
,@level2type=N'COLUMN'
,@level2name=N'fModifiedID'
GO