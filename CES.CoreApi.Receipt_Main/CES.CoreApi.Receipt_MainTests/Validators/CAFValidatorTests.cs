using Microsoft.VisualStudio.TestTools.UnitTesting;
using CES.CoreApi.Receipt_Main.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Validators.Tests
{
    [TestClass()]
    public class CAFValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            CafValidator validator = new CafValidator();
            var result = validator.Validate(GetDataToTest());
            Assert.IsTrue(result);
        }
        public static string GetDataToTest()
        {
            return @"<?xml version=""1.0""?>
                        <AUTORIZACION>
                          <CAF version=""1.0"">
                            <DA>
                              <RE>76134934-1</RE>
                              <RS>RIA CHILE SERVICIOS FINANCIEROS SPA</RS>
                              <TD>33</TD>
                              <RNG>
                                <D>1</D>
                                <H>20</H>
                              </RNG>
                              <FA>2015-02-27</FA>
                              <RSAPK>
                                <M>2wQp2rgzOqBe9qKEUENEEq1qFHO9+MBgIQWAEBoGP/PE2guvTBKPiHvMMc5yhOaJl3GXnDWq23NZD1AD9PLlNw==</M>
                                <E>Aw==</E>
                              </RSAPK>
                              <IDK>300</IDK>
                            </DA>
                            <FRMA algoritmo=""SHA1withRSA"">jV51Ui4PfEBAmrKg7031BPZrWPd8fTT/F9rYI5XOOMrJ4teeGsOgD9u0wtEG4qLCpxi/drG1tdhvTgro43AD/g==</FRMA>
                          </CAF>
                          <RSASK>
                            -----BEGIN RSA PRIVATE KEY-----
                            MIIBOwIBAAJBANsEKdq4MzqgXvaihFBDRBKtahRzvfjAYCEFgBAaBj/zxNoLr0wS
                            j4h7zDHOcoTmiZdxl5w1qttzWQ9QA/Ty5TcCAQMCQQCSAsaR0CInFZSkbFg1gi1h
                            yPFi99P7KurArlVgEVl/9pzZ9hupHJQZ8Xxgw4EpRO7hDothVG5gC910uAqk/o+L
                            AiEA7QwQK+iWv4NrogOUBRhhUm9Z+w951Te5b6FZEJEgshkCIQDshwpZ5dDx3iXv
                            nRUrrp3Q1oHLer0wE6gdPuLjbFRbzwIhAJ4ICsfwZH+s8mwCYq4QQOGfkVIKUTjP
                            0PUWO2BgwHa7AiEAna9cO+6LS+lun74OHR8T4I8BMlHTdWJwE39B7PLi598CIQCk
                            f39104+5bH5clog7VQzCsGJhuWvA41M/CPbeE4pUWQ==
                            -----END RSA PRIVATE KEY-----
                          </RSASK>
                          <RSAPUBK>
                            -----BEGIN PUBLIC KEY-----
                            MFowDQYJKoZIhvcNAQEBBQADSQAwRgJBANsEKdq4MzqgXvaihFBDRBKtahRzvfjA
                            YCEFgBAaBj/zxNoLr0wSj4h7zDHOcoTmiZdxl5w1qttzWQ9QA/Ty5TcCAQM=
                            -----END PUBLIC KEY-----
                          </RSAPUBK>
                        </AUTORIZACION>";

        }
    }
}