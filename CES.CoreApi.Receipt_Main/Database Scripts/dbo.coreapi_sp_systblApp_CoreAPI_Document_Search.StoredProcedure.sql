USE [fxDB6]
GO
IF OBJECT_ID('dbo.coreapi_sp_systblApp_CoreAPI_Document_Search', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.coreapi_sp_systblApp_CoreAPI_Document_Search AS SELECT 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************************************        
Description: 
	Search systblApp_CoreAPI_Document records with advanced filters 

Target Database:
	Ops Main Server

Revision History:
	2017-04-20	cv		Created
********************************************************************************************************************/

ALTER PROCEDURE [dbo].[coreapi_sp_systblApp_CoreAPI_Document_Search] 
@DocumentId uniqueidentifier = null,
@DocumentTypeCode nvarchar (60) = null,
@ItemCode nvarchar(10) = null,
@ReceiverFirstName nvarchar(150) = null,
@ReceiverMiddleName nvarchar(150) = null,
@ReceiverLastName1 nvarchar(150) = null,
@ReceiverLastName2 nvarchar(150) = null,
@SenderFirstName nvarchar(150) = null,
@SenderMiddleName nvarchar(150) = null,
@SenderLastName1 nvarchar(150) = null,
@SenderLastName2 nvarchar(150) = null,
@DocumentFolio nvarchar(150) = null,
@DocumentBranch nvarchar (100) = null,
@DocumentTellerNumber nvarchar (100) = null,
@DocumentTellerName nvarchar (100) = null,
@DocumentIssued datetime = null,
@DocumentTotalAmount money = null
AS
SELECT        
Item.Code AS Item, DocumentType.Code AS Type, Subject_1.FirstName AS ReceiverFirstName, Subject_1.MiddleName AS ReceiverMiddleName, 
Subject_1.LastName1 AS ReceiverLastName1, Subject_1.LastName2 AS ReceiverLastName2, Subject.FirstName AS SenderFirstName, Subject.MiddleName AS SenderMiddleName, 
Subject.LastName1 AS SenderLastName1, Subject.LastName2 AS SenderLastName2, Document.Folio, Document.Branch, Document.TellerNumber, Document.TellerName, Document.Issued, 
Document.TotalAmount
FROM           
systblApp_CoreAPI_Document  AS Document INNER JOIN
systblApp_CoreAPI_Document_Detail AS DocumentDetail ON Document.Id = DocumentDetail.Id INNER JOIN 
systblApp_CoreAPI_Document_Type AS DocumentType ON Document.Id = DocumentType.Id INNER JOIN 
systblApp_CoreAPI_Item AS Item ON DocumentDetail.ItemId = Item.Id LEFT JOIN 
systblApp_CoreAPI_TaxEntity  AS Subject ON Document.SenderId = Subject.Id LEFT JOIN 
systblApp_CoreAPI_TaxEntity AS Subject_1 ON Document.ReceiverId = Subject_1.Id
WHERE       
((Document.Id = @DocumentId)  OR (@DocumentId IS NULL)) 
AND ((Item.Code = @ItemCode)  OR (@ItemCode IS NULL) )
AND ((DocumentType.Code = @DocumentTypeCode )  OR (@DocumentTypeCode IS NULL) )
AND ((Subject_1.FirstName like '%'  + @ReceiverFirstName + '%') OR (@ReceiverFirstName IS NULL) )
AND ((Subject_1.MiddleName like '%' + @ReceiverMiddleName + '%') OR (@ReceiverMiddleName IS NULL))
AND ((Subject_1.LastName1 like '%' + @ReceiverLastName1 + '%') OR (@ReceiverLastName1  IS NULL) )
AND ((Subject_1.LastName2 like '%' + @ReceiverLastName2 + '%') OR (@ReceiverLastName2 IS NULL))
AND ((Subject.FirstName like '%'  +  @SenderFirstName + '%')	 OR (@SenderFirstName  IS NULL))
AND ((Subject.MiddleName like '%' + @SenderMiddleName + '%') OR	(@SenderMiddleName IS NULL))
AND ((Subject.LastName1 like '%'  +  @SenderLastName1  + '%') OR (@SenderLastName1 IS NULL))
AND ((Subject.LastName2 like '%'  +  @SenderLastName2 + '%') OR (@SenderLastName2  IS NULL))
AND ((Document.Folio = @DocumentFolio) OR	(@DocumentFolio IS NULL))
AND ((Document.Branch like '%' + @DocumentBranch + '%') OR (@DocumentBranch IS NULL))
AND ((Document.TellerNumber = @DocumentTellerNumber) OR (@DocumentTellerNumber IS NULL) )
AND ((Document.TellerName like '%'+@DocumentTellerName+'%') OR (@DocumentTellerName IS NULL) )
AND ((Document.Issued = @DocumentIssued)	OR	(@DocumentIssued IS NULL)) 	
AND ((Document.TotalAmount = @DocumentTotalAmount) OR (@DocumentTotalAmount IS NULL))

GO

GRANT EXEC on [coreapi_sp_systblApp_CoreAPI_Document_Search] to [fxCoreAPI_Role] as [dbo]
GO
