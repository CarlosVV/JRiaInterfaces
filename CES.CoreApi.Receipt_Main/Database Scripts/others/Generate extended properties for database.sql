--select  TABLE_NAME, COLUMN_NAME, 'EXEC sys.sp_addextendedproperty @name=N'''+COLUMN_NAME+''', @value=N'''+COLUMN_NAME+''' , @level0type=N''SCHEMA'',@level0name=N''dbo'', @level1type=N''TABLE'',@level1name=N'''+TABLE_NAME+''', @level2type=N''COLUMN'',@level2name=N'''+COLUMN_NAME+''''
--from INFORMATION_SCHEMA.COLUMNS where TABLE_CATALOG = 'CES.CoreApi.Receipt_Main.Service.DB' and TABLE_NAME like 'systblApp_CoreAPI%'

--select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_CATALOG = 'CES.CoreApi.Receipt_Main.Service.DB' and TABLE_NAME like 'systblApp_CoreAPI%'
DECLARE @TABLA AS VARCHAR(1000) = 'systblApp_CoreAPI_Task';
select TABLE_NAME, 'IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('''+TABLE_NAME+''') AND [name] = N'''+TABLE_NAME+''' AND [minor_id] = 0)'
from INFORMATION_SCHEMA.TABLES where TABLE_CATALOG = 'CES.CoreApi.Receipt_Main.Service.DB' and TABLE_NAME like @TABLA

select  'IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('''+TABLE_NAME+''') AND [name] = N'''+COLUMN_NAME+''' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = '''+COLUMN_NAME+''' AND [object_id] = OBJECT_ID('''+TABLE_NAME+''')))' 
+ CHAR(13)+CHAR(10)  + REPLACE('EXEC sys.sp_addextendedproperty ' + CHAR(13)+CHAR(10)+ '@name=N'''+COLUMN_NAME+''', @value=N'''+COLUMN_NAME+''', @level0type=N''SCHEMA'', @level0name=N''dbo'', @level1type=N''TABLE'', @level1name=N'''+TABLE_NAME+''', @level2type=N''COLUMN'', @level2name=N'''+COLUMN_NAME+'''', ', @', CHAR(13)+CHAR(10) + ', @')+ CHAR(13)+CHAR(10) + 'GO' + CHAR(13)+CHAR(10) + CHAR(13)+CHAR(10)
from INFORMATION_SCHEMA.COLUMNS where TABLE_CATALOG = 'CES.CoreApi.Receipt_Main.Service.DB' and TABLE_NAME like @TABLA
ORDER BY ORDINAL_POSITION

