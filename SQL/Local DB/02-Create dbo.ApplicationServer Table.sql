USE [Test]
GO
/****** Object:  Table [dbo].[ApplicationServer]    Script Date: 11/18/2014 9:33:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplicationServer](
	[ApplicationServerID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ApplicationServer] PRIMARY KEY CLUSTERED 
(
	[ApplicationServerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
