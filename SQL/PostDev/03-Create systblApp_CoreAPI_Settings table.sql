use [fxDB6]
go


--ALTER TABLE [dbo].[systblApp_CoreAPI_Settings] DROP CONSTRAINT [FK_systblApp_CoreAPI_Settings_systblapp]
--GO

--DROP TABLE [dbo].[systblApp_CoreAPI_Settings]
--GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[systblApp_CoreAPI_Settings](
	[fCoreAPISettingsID] [int] NOT NULL,
	[fAppID] [int] NOT NULL,
	[fName] [varchar](100) NOT NULL,
	[fValue] [varchar](max) NOT NULL,
	[fDescription] [varchar](250) NULL,
	[fDisabled] [bit] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_Settings] PRIMARY KEY CLUSTERED 
(
	[fCoreAPISettingsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [MTConfig]
) ON [MTConfig]  

GO

SET ANSI_PADDING ON
GO

--ALTER TABLE [dbo].[systblApp_CoreAPI_Settings]  WITH CHECK ADD  CONSTRAINT [FK_systblApp_CoreAPI_Settings_systblapp] FOREIGN KEY([fAppID])
--REFERENCES [dbo].[systblapp] ([fAppID])
--GO

--ALTER TABLE [dbo].[systblApp_CoreAPI_Settings] CHECK CONSTRAINT [FK_systblApp_CoreAPI_Settings_systblapp]
--GO

ALTER TABLE [dbo].[systblApp_CoreAPI_Settings] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Settings_fModified] DEFAULT (GETDATE()) FOR [fModified]
GO

ALTER TABLE [dbo].[systblApp_CoreAPI_Settings] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Settings_fTime]  DEFAULT (GETDATE()) FOR [fTime]
GO

ALTER TABLE [dbo].[systblApp_CoreAPI_Settings] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Settings_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO

ALTER TABLE [dbo].[systblApp_CoreAPI_Settings] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Settings_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO

ALTER TABLE [dbo].[systblApp_CoreAPI_Settings] ADD  CONSTRAINT [DF_systblApp_CoreAPI_Settings_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[systblApp_CoreAPI_Settings] TO [fxCoreAPI_Role] AS [dbo]
GO
