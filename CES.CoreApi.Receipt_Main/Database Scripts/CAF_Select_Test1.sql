USE [CES.CoreApi.Receipt_MainDB]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[CAF_Select]
		@Id = NULL,
		@DocumentType = NULL,
		@StartNumber = NULL,
		@EndNumber = NULL

SELECT	'Return Value' = @return_value

GO
