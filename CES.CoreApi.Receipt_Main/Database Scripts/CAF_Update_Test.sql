USE [CES.CoreApi.Receipt_MainDB]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[CAF_Update]
		@Id = '177101A2-4919-4352-8512-01A266E82050',
		@CompanyTaxId = N'76134934-1',
		@CompanyLegalName = N'RIA CHILE SERVICIOS FINANCIEROS SPA',
		@DocumentType = 56,
		@StartNumber = 1,
		@EndNumber = 4,
		@DateAuthorization = N'2015-02-27',
		@FileContent = N'<?xml version="1.0"?>
<AUTORIZACION>
  <CAF version="1.0">
    <DA>
      <RE>76134934-1</RE>
      <RS>RIA CHILE SERVICIOS FINANCIEROS SPA</RS>
      <TD>56</TD>
      <RNG>
        <D>1</D>
        <H>4</H>
      </RNG>
      <FA>2015-02-27</FA>
      <RSAPK>
        <M>+AkEMYrVs5XDsf2wbE98DNy8TS3CfoRe/7jFnipId9hCUKlniTZEjB3fPNzCLfvdbPGiUF2XtLgqi3pOQlldCQ==</M>
        <E>Aw==</E>
      </RSAPK>
      <IDK>300</IDK>
    </DA>
    <FRMA algoritmo="SHA1withRSA">jlvWov2sPt2nvWJT4YgCRYx2wDzoWlY201Va55BLkoW0TYth9xSPJ2VzbXZwmIs45ngOraPVVZBeJgbSXLAP4w==</FRMA>
  </CAF>
  <RSASK>
    -----BEGIN RSA PRIVATE KEY-----
    MIIBOgIBAAJBAPgJBDGK1bOVw7H9sGxPfAzcvE0twn6EXv+4xZ4qSHfYQlCpZ4k2
    RIwd3zzcwi373WzxolBdl7S4Kot6TkJZXQkCAQMCQQClW1ghBzkiY9fL/nWdilKz
    PdLeHoGprZSqey5pcYWlOYbeilWagO15q9MHFvMg65v7m91jZaKxnubXUh8eGVyL
    AiEA/yJvWZLT/ENxK+LF7CEYwfY1RazD3Y2oW4uQFJx7OhUCIQD44GqNjqDkEir2
    z3RpW4GxfVKQjoFGHKF0vO8K+LgYJQIhAKoW9OZh4qgs9h1B2UgWEIFOzi5zLT5e
    cD0HtWMS/NFjAiEAperxs7RrQrbHTzT4Rj0BIP43CwmrhBMWTdNKB1B6usMCIH5c
    jCdVpixCds4GRiFFVs8XTQPcTs58LDClwwqmnpWy
    -----END RSA PRIVATE KEY-----
  </RSASK>
  <RSAPUBK>
    -----BEGIN PUBLIC KEY-----
    MFowDQYJKoZIhvcNAQEBBQADSQAwRgJBAPgJBDGK1bOVw7H9sGxPfAzcvE0twn6E
    Xv+4xZ4qSHfYQlCpZ4k2RIwd3zzcwi373WzxolBdl7S4Kot6TkJZXQkCAQM=
    -----END PUBLIC KEY-----
  </RSAPUBK>
</AUTORIZACION>'

SELECT	'Return Value' = @return_value

GO
