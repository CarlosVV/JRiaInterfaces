USE [CES.CoreApi.Receipt_MainDB]
GO

DECLARE	@return_value int
DECLARE	@newid uniqueidentifier = newid()

EXEC	@return_value = [dbo].[CAF_Insert]
		@Id = @newid,
		@CompanyTaxId = N'76134934-1',
		@CompanyLegalName = N'RIA CHILE SERVICIOS FINANCIEROS SPA',
		@DocumentType = 39,
		@StartNumber = 1,
		@EndNumber = 200000,
		@DateAuthorization = N'2015-02-27',
		@FileContent = N'<?xml version="1.0"?>
<AUTORIZACION>
  <CAF version="1.0">
    <DA>
      <RE>76134934-1</RE>
      <RS>RIA CHILE SERVICIOS FINANCIEROS SPA</RS>
      <TD>39</TD>
      <RNG>
        <D>1</D>
        <H>200000</H>
      </RNG>
      <FA>2015-02-27</FA>
      <RSAPK>
        <M>yHmigjm4mdyfiNOfS6hf4tMgJRlm2nRNb10Z0pU+ClOF4fkxRGZAUhkLMH4yZWlwqFE4T2T2DNha5weS6K/ISw==</M>
        <E>Aw==</E>
      </RSAPK>
      <IDK>300</IDK>
    </DA>
    <FRMA algoritmo="SHA1withRSA">T2U4CZ4AstA6vp0P8U40Kf/GuGou7fewDoU9M4DIBoZvlEQVlI8MvL5TK/9scBP3kNSHmMEg4IL59pQc9WeACA==</FRMA>
  </CAF>
  <RSASK>
    -----BEGIN RSA PRIVATE KEY-----
    MIIBOwIBAAJBAMh5ooI5uJncn4jTn0uoX+LTICUZZtp0TW9dGdKVPgpTheH5MURm
    QFIZCzB+MmVpcKhROE9k9gzYWucHkuivyEsCAQMCQQCFpmxW0SW76GpbN7+HxZVB
    4hVuEO88TYj06LvhuNQG4SnJlxtmrO/AyjrvwkbZ370H3Qo9471V4m2OGKVFy8m7
    AiEA+YU4H/S8KLuAcudZ/O224FlD2GRTvowLf4AO6zTkov8CIQDNrl5oNaav9Wk/
    4YDLMOL0w0HQjjubf/k3EdOvyxl2tQIhAKZY0BVN0sXSVaHvkVNJJJWQ1+WYN9Rd
    XP+qtJzN7cH/AiEAiR7pms5vH/jw1UEAh3XsoyzWiwl9ElVQz2E3yodmTyMCIQDe
    mZpoqv68up0c4Trs/xDNgbt53N3Chpv1KNw3hdXSeA==
    -----END RSA PRIVATE KEY-----
  </RSASK>
  <RSAPUBK>
    -----BEGIN PUBLIC KEY-----
    MFowDQYJKoZIhvcNAQEBBQADSQAwRgJBAMh5ooI5uJncn4jTn0uoX+LTICUZZtp0
    TW9dGdKVPgpTheH5MURmQFIZCzB+MmVpcKhROE9k9gzYWucHkuivyEsCAQM=
    -----END PUBLIC KEY-----
  </RSAPUBK>
</AUTORIZACION>'

SELECT	'Return Value' = @return_value

GO
