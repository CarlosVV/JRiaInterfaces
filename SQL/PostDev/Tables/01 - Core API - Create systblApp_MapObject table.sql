USE [fxDB6]
GO
--sp_helpfilegroup
--ALTER TABLE [dbo].[systblApp_MapObject] DROP CONSTRAINT [DF_systblApp_MapObject_fChanged]
--GO

--ALTER TABLE [dbo].[systblApp_MapObject] DROP CONSTRAINT [DF_systblApp_MapObject_fDelete]
--GO

--ALTER TABLE [dbo].[systblApp_MapObject] DROP CONSTRAINT [DF_systblApp_MapObject_fDisabled]
--GO

--DROP TABLE [dbo].[systblApp_MapObject]
--GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[systblApp_MapObject](
	[fAppID] [int] NOT NULL,
	[fAppObjectID] [int] NOT NULL,
	[fDisabled] [bit] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
 CONSTRAINT [PK_systblApp_MapObject] PRIMARY KEY CLUSTERED 
(
	[fAppID] ASC,
	[fAppObjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [MTConfig]
) ON [MTConfig]

GO

ALTER TABLE [dbo].[systblApp_MapObject] ADD  CONSTRAINT [DF_systblApp_MapObject_fModified] DEFAULT (GETDATE()) FOR [fModified]
GO

ALTER TABLE [dbo].[systblApp_MapObject] ADD  CONSTRAINT [DF_systblApp_MapObject_fTime]  DEFAULT (GETDATE()) FOR [fTime]
GO

ALTER TABLE [dbo].[systblApp_MapObject] ADD  CONSTRAINT [DF_systblApp_MapObject_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO

ALTER TABLE [dbo].[systblApp_MapObject] ADD  CONSTRAINT [DF_systblApp_MapObject_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO

ALTER TABLE [dbo].[systblApp_MapObject] ADD  CONSTRAINT [DF_systblApp_MapObject_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO

GRANT SELECT, INSERT, UPDATE ON [dbo].[systblApp_MapObject] TO [fxCoreAPI_Role] AS [dbo]
GO



