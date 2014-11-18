USE [Test]
GO
/****** Object:  Table [dbo].[ServiceOperation]    Script Date: 11/18/2014 9:33:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ServiceOperation](
	[ServiceOperationID] [int] NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[MethodName] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Description] [varchar](100) NULL,
 CONSTRAINT [PK_ServiceMethod] PRIMARY KEY CLUSTERED 
(
	[ServiceOperationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[ServiceOperation]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceOperation_systblapp] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[systblapp] ([fAppID])
GO
ALTER TABLE [dbo].[ServiceOperation] CHECK CONSTRAINT [FK_ServiceOperation_systblapp]
GO
