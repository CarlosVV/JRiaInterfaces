select * from [dbo].[Role]
select * from [dbo].[User]
select * from [dbo].[UserRole]

select * from [dbo].[Menu]
order by  MenuOrder

delete from  [dbo].[Menu] 

insert into [dbo].[Menu] 
select newid(),'Actions', 'Actions',null, 0,'1',1, getdate(), null, null  UNION
select newid(),'Search Documents',null,null,1,'1.1', 1, getdate(), null, null  UNION
select newid(),'Send to EIS',null,null,2,'1.2', 1, getdate(), null, null  UNION
select newid(),'CAF Load',null,null,3,'1.3', 1, getdate(), null, null  UNION
select newid(),'New CAF',null,null,4,'1.3.1', 1, getdate(), null, null  UNION
select newid(),'Search CAFs',null,null,5,'1.3.2', 1, getdate(), null, null  UNION
select newid(),'New Tax Document',null,null,6,'1.4', 1, getdate(), null, null  UNION
select newid(),'Generate Credit Notes',null,null,7,'1.5', 1, getdate(), null, null  UNION
select newid(),'Download SII Docs',null,null,8,'1.6', 1, getdate(), null, null  UNION
select newid(),'Conciliation SIIVAT',null,null,9,'1.7', 1, getdate(), null, null  UNION
select newid(),'Security',null,null,10,'2', 1, getdate(), null, null  UNION
select newid(),'Users and Roles',null,null,11,'2.1', 1, getdate(), null, null  UNION
select newid(),'New User',null,null,12,'2.1.1', 1, getdate(), null, null  UNION
select newid(),'Search Users',null,null,13,'2.1.2', 1, getdate(), null, null  UNION
select newid(),'Stores',null,null,14,'2.2', 1, getdate(), null, null  UNION
select newid(),'New Store',null,null,15,'2.2.1', 1, getdate(), null, null  UNION
select newid(),'Search Stores',null,null,16,'2.2.2', 1, getdate(), null, null  UNION
select newid(),'Configurations',null,null,17,'3', 1, getdate(), null, null  UNION
select newid(),'Printers',null,null,18,'3.1', 1, getdate(), null, null  UNION
select newid(),'Cashiers',null,null,19,'3.2', 1, getdate(), null, null  UNION
select newid(),'Services',null,null,20,'3.3', 1, getdate(), null, null  UNION
select newid(),'Parameters',null,null,21,'3.4', 1, getdate(), null, null  UNION
select newid(),'Checklist',null,null,22,'3.5', 1, getdate(), null, null  UNION
select newid(),'Reports',null,null,23,'4', 1, getdate(), null, null  UNION
select newid(),'Boletas x Hacer',null,null,24,'4.1', 1, getdate(), null, null  UNION
select newid(),'Boletas x Anular',null,null,25,'4.2', 1, getdate(), null, null  UNION
select newid(),'Boletas x Enviar',null,null,26,'4.3', 1, getdate(), null, null

update [dbo].[Menu] set Description = Name
UPDATE [dbo].[Menu] SET Parent = (select Id from Menu where Name = 'Actions') where Name in ('Search Documents','Send to EIS', 'CAF Load', 'New Tax Document','Generate Credit Notes','Download SII Docs','Conciliation SIIVAT')
UPDATE [dbo].[Menu] SET Parent = (select Id from Menu where Name = 'CAF Load') where Name in ('New CAF','Search CAFs')
UPDATE [dbo].[Menu] SET Parent = (select Id from Menu where Name = 'Security') where Name in ('Users and Roles','Stores')
UPDATE [dbo].[Menu] SET Parent = (select Id from Menu where Name = 'Users and Roles') where Name in ('New User','Search Users')
UPDATE [dbo].[Menu] SET Parent = (select Id from Menu where Name = 'Stores') where Name in ('New Store','Search Stores')
UPDATE [dbo].[Menu] SET Parent = (select Id from Menu where Name = 'Configurations') where Name in ('Printers','Cashiers','Services','Parameters','Checklist')
UPDATE [dbo].[Menu] SET Parent = (select Id from Menu where Name = 'Reports') where Name in ('Boletas x Hacer','Boletas x Anular','Boletas x Enviar')


