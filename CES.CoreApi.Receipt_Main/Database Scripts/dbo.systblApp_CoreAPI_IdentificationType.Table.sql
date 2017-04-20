USE [fxDB6]
GO
/****** Object:  Table [dbo].[systblApp_CoreAPI_IdentificationType]    Script Date: 4/20/2017 7:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_IdentificationType

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_IdentificationType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_IdentificationType](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nchar](10) NULL,
	[Description] [nchar](10) NULL,
	[DescriptionNative] [nchar](10) NULL,
	[IssuerInstitution] [nchar](10) NULL,
	[Country] [nchar](10) NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_IdentificationType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_IdentificationType_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_IdentificationType] ADD  CONSTRAINT [DF_systblApp_CoreAPI_IdentificationType_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_IdentificationType_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_IdentificationType] ADD  CONSTRAINT [DF_systblApp_CoreAPI_IdentificationType_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_IdentificationType_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_IdentificationType] ADD  CONSTRAINT [DF_systblApp_CoreAPI_IdentificationType_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_IdentificationType_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_IdentificationType] ADD  CONSTRAINT [DF_systblApp_CoreAPI_IdentificationType_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_IdentificationType] to [fxCoreAPI_Role]
GO

