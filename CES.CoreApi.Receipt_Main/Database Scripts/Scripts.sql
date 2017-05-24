BEGIN TRAN

update [systblApp_CoreAPI_Sequence] set CurrentId = NULL where EntityName in ( 'systblApp_CoreAPI_Document','systblApp_CoreAPI_DocumentDetail')

select * from  [dbo].[Log] (nolock)

DELETE FROM [dbo].[systblApp_CoreAPI_Document] 
DELETE FROM [dbo].[systblApp_CoreAPI_DocumentDetail] 
delete from systblApp_CoreAPI_taxentity
delete from systblApp_CoreAPI_taxaddress
update [systblApp_CoreAPI_Sequence] set CurrentId = NULL 

select * from  [dbo].[systblApp_CoreAPI_Sequence] (nolock)
select * from [dbo].[systblApp_CoreAPI_Document](nolock)
select * from  [dbo].[systblApp_CoreAPI_DocumentDetail] (nolock)
select * from [dbo].systblApp_CoreAPI_taxentity(nolock)
select * from [dbo].systblApp_CoreAPI_taxaddress(nolock)

select * from [dbo].[systblApp_CoreAPI_Document](nolock) where Folio = 1010027
select count(*) from [dbo].[systblApp_CoreAPI_Document](nolock)  
select max(Id) from  [dbo].[systblApp_CoreAPI_Document] (nolock)
select min(Folio) from [dbo].[systblApp_CoreAPI_Document](nolock)  
select max(Folio) from [dbo].[systblApp_CoreAPI_Document](nolock)  
select sum(Amount) from [dbo].[systblApp_CoreAPI_Document](nolock) --54 185 726
select avg(Amount) from [dbo].[systblApp_CoreAPI_Document](nolock) --54 185 726
select max(Amount) from [dbo].[systblApp_CoreAPI_Document](nolock)
select min(Amount) from [dbo].[systblApp_CoreAPI_Document](nolock)
select top 100 * from [dbo].[systblApp_CoreAPI_Document](nolock) order by IssuedDate desc

select StoreName, s.Location,  count(StoreName) documentos, min(Folio) desde, max(folio) hasta, sum(Amount) monto
from [dbo].[systblApp_CoreAPI_Document](nolock) d 
inner join systblApp_TaxReceipt_Store (nolock) s
on d.RecAgent = s.Id
group by StoreName, s.Location
order by min(Folio)

/*
Baquedano 482 Local 41	Antofagasta	32908
Baquedano 482 Local 7	Antofagasta	16505
Bandera 523 Local A		Santiago	4793
Catedral 1021 Local 1	Santiago	3094

*/

select * from  systblApp_TaxReceipt_Store (nolock)

SELECT fDocType, fStart, fStop FROM (


	SELECT m.Folio + 1 as fStart, m.DocumentTYpe as fDocType,
	(		
			SELECT 
				min(Folio) - 1 
			FROM [systblApp_CoreAPI_Document] (nolock) as x 

			WHERE x.Folio > m.Folio and x.DocumentType=m.DocumentType
	)
	as fStop
	FROM [systblApp_CoreAPI_Document] (nolock) as m 
	left outer join [systblApp_CoreAPI_Document] (nolock) as r on m.Folio = r.Folio - 1 and m.DocumentType=r.DocumentType 
	WHERE r.Folio is null  and m.DocumentType = 39 and m.IssuedDate >= '2017/01/01 00:00:00'  and m.IssuedDate < '2017/06/01 00:00:00'
) as x
order by fStart


ALTER PROCEDURE GetDocumentsToDowload
(
@DocType nvarchar(10) = '39',
@StartDate datetime
)
as
SELECT fDocType, fStart, fStop FROM (


	SELECT m.Folio + 1 as fStart, m.DocumentTYpe as fDocType,
	(		
			SELECT 
				min(Folio) - 1 
			FROM [systblApp_CoreAPI_Document] (nolock) as x 

			WHERE x.Folio > m.Folio and x.DocumentType=m.DocumentType
	)
	as fStop
	FROM [systblApp_CoreAPI_Document] (nolock) as m 
	left outer join [systblApp_CoreAPI_Document] (nolock) as r on m.Folio = r.Folio - 1 and m.DocumentType=r.DocumentType 
	WHERE r.Folio is null  and m.DocumentType = @DocType and m.IssuedDate >= @StartDate
) as x

EXEC GetDocumentsToDowload '2017/01/01 00:00:00'

select folio from sys

ROLLBACK