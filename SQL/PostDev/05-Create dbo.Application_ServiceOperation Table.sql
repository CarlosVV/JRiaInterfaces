SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application_ServiceOperation](
	[ApplicationID] [int] NOT NULL,
	[ServiceOperationID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Application_ServiceOperation] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC,
	[ServiceOperationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Application_ServiceOperation]  WITH CHECK ADD  CONSTRAINT [FK_Application_ServiceOperation_ServiceOperation] FOREIGN KEY([ServiceOperationID])
REFERENCES [dbo].[ServiceOperation] ([ServiceOperationID])
GO
ALTER TABLE [dbo].[Application_ServiceOperation] CHECK CONSTRAINT [FK_Application_ServiceOperation_ServiceOperation]
GO
ALTER TABLE [dbo].[Application_ServiceOperation]  WITH CHECK ADD  CONSTRAINT [FK_Application_ServiceOperation_systblapp] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[systblapp] ([fAppID])
GO
ALTER TABLE [dbo].[Application_ServiceOperation] CHECK CONSTRAINT [FK_Application_ServiceOperation_systblapp]
GO
