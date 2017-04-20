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
	[Id] [uniqueidentifier] NOT NULL,
	[OrderNo] [nvarchar](50) NULL,
	[Type] [uniqueidentifier] NULL,
	[Folio] [int] NULL,
	[Branch] [nvarchar](100) NULL,
	[TellerNumber] [nvarchar](100) NULL,
	[TellerName] [nvarchar](100) NULL,
	[Issued] [datetime] NULL,
	[Amount] [money] NULL,
	[Tax] [money] NULL,
	[TotalAmount] [money] NULL,
	[SenderId] [uniqueidentifier] NULL,
	[ReceiverId] [uniqueidentifier] NULL,
	[SentToSII] [bit] NULL,
	[DownloadedSII] [bit] NULL,
	[Date] [nchar](10) NULL,
	[RecAgent] [nchar](10) NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
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

