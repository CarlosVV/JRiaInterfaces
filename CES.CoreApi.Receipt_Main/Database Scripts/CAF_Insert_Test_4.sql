USE [CES.CoreApi.Receipt_MainDB]
GO

DECLARE	@return_value int
DECLARE	@New_Id UNIQUEIDENTIFIER = NEWID()

EXEC	@return_value = [dbo].[CAF_Insert]
		@Id = @New_Id,
		@CompanyTaxId = N'76134934-1',
		@CompanyLegalName = N'RIA CHILE SERVICIOS FINANCIEROS SPA',
		@DocumentType = 61,
		@StartNumber = 10,
		@EndNumber = 13,
		@DateAuthorization = N'2015-04-16',
		@FileContent = N'<?xml version="1.0"?>
<AUTORIZACION>
  <CAF version="1.0">
    <DA>
      <RE>76134934-1</RE>
      <RS>RIA CHILE SERVICIOS FINANCIEROS SPA</RS>
      <TD>61</TD>
      <RNG>
        <D>10</D>
        <H>13</H>
      </RNG>
      <FA>2015-04-16</FA>
      <RSAPK>
        <M>9d7XRE1ONLUdLC5ZtWWZWCQaQBGhkfPYsn7scYEfG5pR7yu+IOXlxxNW3QBpv9b/1fSCe5MM2kLTBfX5ypbEsQ==</M>
        <E>Aw==</E>
      </RSAPK>
      <IDK>300</IDK>
    </DA>
    <FRMA algoritmo="SHA1withRSA">N49dXQAHhg0MUqjauPvDR/L9lisdQUhD8WTsjBuSj4bcxJ9eYC8uqeRTo3XT7MqKx8Nmv4SxmoVQXh4b3bhLcg==</FRMA>
  </CAF>
  <RSASK>
    -----BEGIN RSA PRIVATE KEY-----
    MIIBOwIBAAJBAPXe10RNTjS1HSwuWbVlmVgkGkARoZHz2LJ+7HGBHxuaUe8rviDl
    5ccTVt0Aab/W/9X0gnuTDNpC0wX1+cqWxLECAQMCQQCj6eTYM4l4eL4ddDvOQ7uQ
    GBGAC8EL9+XMVJ2hAL9nuugWCvL9BB1loLRNgXH/0VozCS1umTRWn2EkN2L6Gwej
    AiEA/fGjf6yQE07mdEuG3SSddb3S07Hvx51qEXBEN8lvd2MCIQD33HfR+M+mX7vU
    HTdhm3+Cy5Pqo712uumv316tif7B2wIhAKlLwlUdtWI0maLdBJNtvk5+jI0hSoUT
    nAugLXqGSk+XAiEApT2lNqXfxD/SjWjPlmeqVzJinG0o+dHxH+o/Hlv/K+cCIQDV
    tBssAR54WKXOGcFjEj2oF/+ciTH+fcjrANpprr7Yvg==
    -----END RSA PRIVATE KEY-----
  </RSASK>
  <RSAPUBK>
    -----BEGIN PUBLIC KEY-----
    MFowDQYJKoZIhvcNAQEBBQADSQAwRgJBAPXe10RNTjS1HSwuWbVlmVgkGkARoZHz
    2LJ+7HGBHxuaUe8rviDl5ccTVt0Aab/W/9X0gnuTDNpC0wX1+cqWxLECAQM=
    -----END PUBLIC KEY-----
  </RSAPUBK>
</AUTORIZACION>'

SELECT	'Return Value' = @return_value

GO
