USE [Test]
GO
/****** Object:  Table [dbo].[systblapp]    Script Date: 11/18/2014 9:33:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[systblapp](
	[fAppID] [int] NOT NULL,
	[fType] [int] NULL,
	[fName] [varchar](50) NULL,
	[fDescription] [varchar](100) NULL,
	[fDisabled] [bit] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
 CONSTRAINT [PK_systblapp] PRIMARY KEY CLUSTERED 
(
	[fAppID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
