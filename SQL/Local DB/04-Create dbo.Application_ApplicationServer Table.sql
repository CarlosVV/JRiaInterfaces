USE [Test]
GO
/****** Object:  Table [dbo].[Application_ApplicationServer]    Script Date: 11/18/2014 9:33:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application_ApplicationServer](
	[ApplicationID] [int] NOT NULL,
	[ApplicationServerID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Application_ApplicationServer] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC,
	[ApplicationServerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Application_ApplicationServer]  WITH CHECK ADD  CONSTRAINT [FK_Application_ApplicationServer_ApplicationServer] FOREIGN KEY([ApplicationServerID])
REFERENCES [dbo].[ApplicationServer] ([ApplicationServerID])
GO
ALTER TABLE [dbo].[Application_ApplicationServer] CHECK CONSTRAINT [FK_Application_ApplicationServer_ApplicationServer]
GO
ALTER TABLE [dbo].[Application_ApplicationServer]  WITH CHECK ADD  CONSTRAINT [FK_Application_ApplicationServer_systblapp] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[systblapp] ([fAppID])
GO
ALTER TABLE [dbo].[Application_ApplicationServer] CHECK CONSTRAINT [FK_Application_ApplicationServer_systblapp]
GO
