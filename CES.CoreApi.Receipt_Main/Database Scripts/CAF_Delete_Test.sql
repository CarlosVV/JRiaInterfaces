USE [CES.CoreApi.Receipt_MainDB]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[CAF_Delete]
		@Id = '5FD172AB-C352-40CC-BFCE-862BD9A455A9'

SELECT	'Return Value' = @return_value

GO
