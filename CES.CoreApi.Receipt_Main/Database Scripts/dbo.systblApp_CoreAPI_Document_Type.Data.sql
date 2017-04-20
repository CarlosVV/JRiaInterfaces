USE [fxDB6]
GO
/********************************************************************************************************************        
Description: 
	Load Document Types 

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/
SELECT * FROM [systblApp_CoreAPI_Document_Type]
GO
DELETE FROM [systblApp_CoreAPI_Document_Type]
GO
DECLARE @fModifiedID INT = 1

INSERT [dbo].[systblApp_CoreAPI_Document_Type] ([Id], [Code], [Description], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]) VALUES (N'fdf255ad-23cc-487c-8ac8-0f243f451f30', N'56', N'DebitNote/Nota de Débito', 0, 0, 0, getdate(), NULL, @fModifiedID)
INSERT [dbo].[systblApp_CoreAPI_Document_Type] ([Id], [Code], [Description], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]) VALUES (N'bc80537b-352c-41d2-84bb-837789e7f404', N'39', N'SalesTicketTaxIncluded/Boleta', 0, 0, 0, getdate(), NULL, @fModifiedID)
INSERT [dbo].[systblApp_CoreAPI_Document_Type] ([Id], [Code], [Description], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]) VALUES (N'cd09c212-4ef3-422c-ae86-d71c9e145e04', N'61', N'CreditNote/Nota de Crédito',  0, 0, 0, getdate(), NULL, @fModifiedID)
INSERT [dbo].[systblApp_CoreAPI_Document_Type] ([Id], [Code], [Description], [fDisabled], [fDelete], [fChanged], [fTime], [fModified], [fModifiedID]) VALUES (N'00db85e3-5d87-45fe-ad12-dbc7332c20b4', N'33', N'Invoice/Factura', 0, 0, 0, getdate(), NULL, @fModifiedID)
GO