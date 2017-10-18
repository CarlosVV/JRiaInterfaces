/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying;

import fx.globalpaying.ServiceManager.RequestTypeEnum;
import java.io.StringReader;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;

/**
 *
 * @author Carlos Valderrama @ RIA
 */
public class FXGlobalPaying {

    static String soapEndpointUrl = "http://stagingfxglobalwebsvcnocert.riaenvia.net:9771/FXGlobalPaying.svc/Binding_Basic_NoCert";
    static String soapActionPrefix = "CES.Services.FXGlobal/IRiaAsPayer/";

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        /*
        Se espera lo siguiente:
        java -jar FX-GlobalPaying.jar "59470211" "2.0" "50e07864-104f-4f68-adb6-d5d7af615ffb;20170912135959;0;0;Main Branch;CL;3648587;16;es-ES" "Currencies" -url "http://stagingfxglobalwebsvcnocert.riaenvia.net:9771/FXGlobalPaying.svc/Binding_Basic_NoCert" 
        
        Los parámetros de entrada se obtienen de la siguiente manera:
        
        args[0] -> CorrespID
        args[1] -> LayoutVersion
        args[2] -> Header
            Header[0] -> CallID
            Header[1] -> CallDateTimeLocal
            Header[2] -> CorrespLocNo
            Header[3] -> CorrespLocNo
            Header[4] -> CorrespLocName
            Header[5] -> CorrespLocCountry
            Header[6] -> UserID
            Header[7] -> TerminalID
            Header[8] -> LanguageCultureCode
        args[3] -> RequestType
        args[4] -> -url
        args[5] -> url
         */
        if (args.length < 4 || (args.length > 3 && (args[2] == null || args[2].split(";").length < 9))) {
            System.out.println("Error: argumentos inválidos o faltantes");
            return;
        }

        String x = "<Order>"
                + "<OrderNo>681024</OrderNo>"
                + "<PayingCorrespSeqID>0</PayingCorrespSeqID>"
                + "<SalesDate>20170920</SalesDate>"
                + "<SalesTime>093224</SalesTime>"
                + "<CountryFrom>CL</CountryFrom>"
                + "<CountryTo>CO</CountryTo>"
                + "<SendingCorrespBranchNo>0001</SendingCorrespBranchNo>"
                + "<PayingCorrespID>32249011</PayingCorrespID>"
                + "<PayingCorrespLocID>39536811</PayingCorrespLocID>"
                + "<BeneQuestion/>"
                + "<BeneAnswer/>"
                + "<PmtInstruction/>"
                + "<BeneficiaryCurrency>COP</BeneficiaryCurrency>"
                + "<BeneficiaryAmount>357330</BeneficiaryAmount>"
                + "<DeliveryMethod>2</DeliveryMethod>"
                + "<PaymentCurrency>CLP</PaymentCurrency>"
                + "<PaymentAmount>100000</PaymentAmount>"
                + "<CommissionCurrency>CLP</CommissionCurrency>"
                + "<CommissionAmount>2000</CommissionAmount>"
                + "<BenID>202</BenID>"
                + "<BeneFirstName>BENE1</BeneFirstName>"
                + "<BeneLastName>TESTE</BeneLastName>"
                + "<BeneLastName2>TESTE</BeneLastName2>"
                + "<BeneAddress>TEST</BeneAddress>"
                + "<BeneCity>TEST</BeneCity>"
                + "<BeneState>TEST</BeneState>"
                + "<BeneZipCode>33333</BeneZipCode>"
                + "<BeneCountry>IL</BeneCountry>"
                + "<BenePhoneNo>5454545454</BenePhoneNo>"
                + "<BeneMessage/>"
                + "<CustID>83100552</CustID>"
                + "<CustFirstName>jane</CustFirstName>"
                + "<CustLastName>test</CustLastName>"
                + "<CustLastName2>test</CustLastName2>"
                + "<CustAddress>123 main ave.</CustAddress>"
                + "<CustCity>santa ana</CustCity>"
                + "<CustState>OAX</CustState>"
                + "<CustZipCode>92844</CustZipCode>"
                + "<CustCountry>MX</CustCountry>"
                + "<CustPhoneNo>233442222</CustPhoneNo>"
                + "<CustID1Type>CPF</CustID1Type>"
                + "<CustID1No>24592221163</CustID1No>"
                + "<CustID1IssuedBy>BRAZIL</CustID1IssuedBy>"
                + "<CustID1ExpDate>20151231</CustID1ExpDate>"
                + "<CustID2Type/>"
                + "<CustID2No/>"
                + "<CustID2IssuedBy/>"
                + "<CustID2ExpDate/>"
                + "<CustTaxID>000-00-0000</CustTaxID>"
                + "<CustTaxCountry>BR</CustTaxCountry>"
                + "<CustCountryOfBirth>CH</CustCountryOfBirth>"
                + "<CustNationality>BR</CustNationality>"
                + "<CustDateBirth>19781201</CustDateBirth>"
                + "<CustOccupation>TY</CustOccupation>"
                + "<CustSourceFunds>WORK</CustSourceFunds>"
                + "<TransferReason>DONATION</TransferReason>"
                + "<ProviderID>0</ProviderID>"
                + "<BankID>0</BankID>"
                + "<BankBranchName/>"
                + "<BankBranchNo/>"
                + "<BankBranchCity/>"
                + "<BankAccountCountry/>"
                + "<BankAccountType></BankAccountType>"
                + "<BankAccountNo/>"
                + "<Valuetype></Valuetype>"
                + "<BankRoutingCode>0</BankRoutingCode>"
                + "<BankRoutingType></BankRoutingType>"
                + "<BIC_SWIFT/>"
                + "<UnitaryBankAccountNo/>"
                + "<UnitaryType></UnitaryType>"
                + "<BeneIDNo>99010112312</BeneIDNo>"
                + "<BeneIDType>Passport</BeneIDType>"
                + "<BeneTaxID/>"
                + "</Order>";

        try {

            Document doc = loadXMLFromString(x);
            doc.getDocumentElement().normalize();
            System.out.println("Root element " + doc.getDocumentElement().getNodeName());
            NodeList nodeList = doc.getElementsByTagName("*");
            for (int i = 0; i < nodeList.getLength(); i++) {
                // Get element
                Element element = (Element) nodeList.item(i);
                System.out.println(element.getNodeName());
            }
        } catch (Exception ex) {
        }
        if (args.length > 5 && "-url".equals(args[args.length - 2])) {
            soapEndpointUrl = args[args.length - 1];
        }

        String requestType = args[3].split(";")[0];

        boolean isValidRequestType = ServiceManager.ExistsRequestType(requestType);
        if (!isValidRequestType) {
            // Invalid Request Type, exit Program
            return;
        }
        Object response;
        response = ServiceManager.ExecuteWebMethod(RequestTypeEnum.valueOf(requestType), args);
    }

    public static Document loadXMLFromString(String xml) throws Exception {
        DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
        DocumentBuilder builder = factory.newDocumentBuilder();
        InputSource is = new InputSource(new StringReader(xml));
        return builder.parse(is);
    }
}
