SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplicationConfiguration](
	[ApplicationConfigurationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[ConfigurationName] [varchar](100) NOT NULL,
	[ConfigurationValue] [varchar](max) NOT NULL,
	[Description] [varchar](250) NULL,
 CONSTRAINT [PK_ApplicationConfiguration] PRIMARY KEY CLUSTERED 
(
	[ApplicationConfigurationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[ApplicationConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationConfiguration_systblapp] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[systblapp] ([fAppID])
GO
ALTER TABLE [dbo].[ApplicationConfiguration] CHECK CONSTRAINT [FK_ApplicationConfiguration_systblapp]
GO
