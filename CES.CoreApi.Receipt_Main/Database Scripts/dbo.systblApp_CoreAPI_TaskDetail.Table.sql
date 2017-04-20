USE [fxDB6]
GO
/****** Object:  Table [dbo].[systblApp_CoreAPI_TaskDetail]    Script Date: 4/20/2017 7:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
:Description:
	systblApp_CoreAPI_TaskDetail

:Database Target:
	fxDB6

:Revision History:
	2017-04-20	CV	Created

*/
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systblApp_CoreAPI_TaskDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[systblApp_CoreAPI_TaskDetail](
	[Id] [uniqueidentifier] NOT NULL,
	[Task] [uniqueidentifier] NULL,
	[StateObject] [nvarchar](max) NULL,
	[ResultObject] [nvarchar](max) NULL,
	[fDisabled] [bit] NULL,
	[fDelete] [bit] NULL,
	[fChanged] [bit] NULL,
	[fTime] [datetime] NULL,
	[fModified] [datetime] NULL,
	[fModifiedID] [int] NULL,
 CONSTRAINT [PK_systblApp_CoreAPI_TaskDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaskDetail_fDisabled')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaskDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaskDetail_fDisabled]  DEFAULT ((0)) FOR [fDisabled]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaskDetail_fDelete')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaskDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaskDetail_fDelete]  DEFAULT ((0)) FOR [fDelete]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaskDetail_fChanged')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaskDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaskDetail_fChanged]  DEFAULT ((0)) FOR [fChanged]
GO
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID)='DF_systblApp_CoreAPI_TaskDetail_fTime')
ALTER TABLE [dbo].[systblApp_CoreAPI_TaskDetail] ADD  CONSTRAINT [DF_systblApp_CoreAPI_TaskDetail_fTime]  DEFAULT (getdate()) FOR [fTime]
GO

GRANT SELECT, INSERT,UPDATE,DELETE on [systblApp_CoreAPI_TaskDetail] to [fxCoreAPI_Role]
GO

