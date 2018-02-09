/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.interfaces;

import fx.globalpaying.entities.BankEntity;
import fx.globalpaying.entities.BranchEntity;
import fx.globalpaying.entities.CurrencyEntity;
import fx.globalpaying.entities.GetBankBranchesRequestEntity;
import fx.globalpaying.entities.GetBankBranchesResponseEntity;
import fx.globalpaying.entities.HeaderEntity;
import fx.globalpaying.entities.RequiredField;
import java.util.Iterator;
import javax.xml.namespace.QName;
import javax.xml.soap.MessageFactory;
import javax.xml.soap.MimeHeaders;
import javax.xml.soap.SOAPBody;
import javax.xml.soap.SOAPBodyElement;
import javax.xml.soap.SOAPConnection;
import javax.xml.soap.SOAPConnectionFactory;
import javax.xml.soap.SOAPElement;
import javax.xml.soap.SOAPEnvelope;
import javax.xml.soap.SOAPException;
import javax.xml.soap.SOAPMessage;
import javax.xml.soap.SOAPPart;

/**
 *
 * @author cvalderrama
 */
public class BankBranches {

    private static final String XML_DOC = "xmlDoc";
    private static final String ROOT = "Root";
    private static final String GET_BANK_BRANCHES_METHOD = "GetBankBranches";
    private static final String SOAP_ACTION = "CES.Services.FXGlobal/IRiaAsPayer/GetBankBranches";
    private static boolean generateToStdOut = false;
    private static final boolean IS_DEBUG = false;
    private static String getBankBranchesStringReturn;

    public static String getGetBankBranchesStringReturn() {
        return getBankBranchesStringReturn;
    }

    public static GetBankBranchesRequestEntity parseInputArgsToRequest(String[] args) {
        GetBankBranchesRequestEntity request = new GetBankBranchesRequestEntity();
        String[] header = args[2].split(";");

        request.setCorrespID(args[0]);
        request.setLayoutVersion(args[1]);
        request.setHeader(new HeaderEntity());
        request.getHeader().setCallID(header[0]);
        request.getHeader().setCallDateTimeLocal(header[1]);
        request.getHeader().setCorrespLocNo(header[2]);
        request.getHeader().setCorrespLocNo_Ria(header[3]);
        request.getHeader().setCorrespLocName(header[4]);
        request.getHeader().setCorrespLocCountry(header[5]);
        request.getHeader().setUserID(header[6]);
        request.getHeader().setTerminalID(header[7]);
        request.getHeader().setLanguageCultureCode(header[8]);

        if (args[3] != null) {
            String[] requestArray = args[3].split(";", -1);
            if (requestArray.length >= 9) {
                request.setRequestType("BankBranches");
                if (!"-".equals(requestArray[1])) {
                    request.setCountryCode(requestArray[1]);
                }

                if (!"-".equals(requestArray[2])) {
                    request.setRoutingCode(requestArray[2]);

                }

                if (!"-".equals(requestArray[3])) {
                    request.setRequestType(requestArray[3]);

                }
                if (!"-".equals(requestArray[4])) {
                    request.setBankID(requestArray[4]);

                }
                if (!"-".equals(requestArray[5])) {
                    request.setBranchNumber(requestArray[5]);

                }
                if (!"-".equals(requestArray[6])) {
                    request.setBranchAddress(requestArray[6]);

                }
                if (!"-".equals(requestArray[7])) {
                    request.setBranchCity(requestArray[7]);

                }
                if (!"-".equals(requestArray[8])) {
                    request.setBranchState(requestArray[8]);

                }
            }
        }

        return request;
    }

    public static void callSoapWebService(String soapEndpointUrl,
            GetBankBranchesRequestEntity request) {
        callSoapWebService(soapEndpointUrl, request, true);
    }

    public static void callSoapWebService(String soapEndpointUrl,
            GetBankBranchesRequestEntity request, boolean generateToStdOutPrint) {
        try {
            generateToStdOut = generateToStdOutPrint;
            // Create SOAP Connection
            SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
            SOAPConnection soapConnection = soapConnectionFactory.createConnection();

            // Send SOAP Message to SOAP Server
            SOAPMessage soapResponse = soapConnection.call(createSOAPRequest(SOAP_ACTION, request), soapEndpointUrl);

            // Return parsed Response
            parseAndReturnResponse(soapResponse);

            if (IS_DEBUG) {
                // Print the SOAP Response
                System.out.println("Response SOAP Message:");
                soapResponse.writeTo(System.out);
                System.out.println();
            }

            soapConnection.close();
        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
    }

    private static void createSoapEnvelope(SOAPMessage soapMessage,
            GetBankBranchesRequestEntity request) throws SOAPException {
        SOAPPart soapPart = soapMessage.getSOAPPart();

        String cesNamespace = "ces";
        String fxGlobalNamespaceURI = "CES.Services.FXGlobal";

        // SOAP Envelope
        SOAPEnvelope envelope = soapPart.getEnvelope();
        envelope.addNamespaceDeclaration(cesNamespace, fxGlobalNamespaceURI);

        // SOAP Body
        SOAPBody soapBody = envelope.getBody();

        // <ces:GetCurrencies>
        SOAPElement getCurrenciesElem = soapBody.addChildElement(GET_BANK_BRANCHES_METHOD, cesNamespace);

        // <ces:xmlDoc>
        SOAPElement xmlDoc = getCurrenciesElem.addChildElement(XML_DOC, cesNamespace);

        // <Root>
        SOAPElement root = xmlDoc.addChildElement(ROOT);

        // <CorrespID>59470211</CorrespID>
        SOAPElement correspID = root.addChildElement("CorrespID");
        correspID.addTextNode(request.getCorrespID());

        // <LayoutVersion>2.0</LayoutVersion>
        SOAPElement layoutVersion = root.addChildElement("LayoutVersion");
        layoutVersion.addTextNode(request.getLayoutVersion());

        // <Header>
        SOAPElement headerElement = root.addChildElement("Header");

        // <CallID>50e07864-104f-4f68-adb6-d5d7af615ffb</CallID>
        SOAPElement CallID = headerElement.addChildElement("CallID");
        CallID.addTextNode(request.getHeader().getCallID());

        // <CallDateTimeLocal>20170912135959</CallDateTimeLocal>        
        SOAPElement callDateTimeLocal = headerElement.addChildElement("CallDateTimeLocal");
        callDateTimeLocal.addTextNode(request.getHeader().getCallDateTimeLocal());

        // <CorrespLocNo>0</CorrespLocNo>      
        SOAPElement correspLocNo = headerElement.addChildElement("CorrespLocNo");
        correspLocNo.addTextNode(request.getHeader().getCorrespLocNo());
        // <CorrespLocNo_Ria>0</CorrespLocNo_Ria>

        SOAPElement correspLocNo_Ria = headerElement.addChildElement("CorrespLocNo_Ria");
        correspLocNo_Ria.addTextNode(request.getHeader().getCorrespLocNo_Ria());

        // <CorrespLocName>Main Branch</CorrespLocName>       
        SOAPElement correspLocName = headerElement.addChildElement("CorrespLocName");
        correspLocName.addTextNode(request.getHeader().getCorrespLocName());

        // <CorrespLocCountry>CL</CorrespLocCountry>       
        SOAPElement correspLocCountry = headerElement.addChildElement("CorrespLocCountry");
        correspLocCountry.addTextNode(request.getHeader().getCorrespLocCountry());

        // <UserID>3648587</UserID>
        SOAPElement userID = headerElement.addChildElement("UserID");
        userID.addTextNode(request.getHeader().getUserID());

        // <TerminalID>16</TerminalID>
        SOAPElement terminalID = headerElement.addChildElement("TerminalID");
        terminalID.addTextNode(request.getHeader().getTerminalID());

        // <LanguageCultureCode>es-ES</LanguageCultureCode>
        SOAPElement languageCultureCode = headerElement.addChildElement("LanguageCultureCode");
        languageCultureCode.addTextNode(request.getHeader().getLanguageCultureCode());

        // <RequestType>StatesCities</RequestType>
        SOAPElement requestType = root.addChildElement("RequestType");
        requestType.addTextNode(request.getRequestType());

        //  <Request CountryCode="CO" RoutingCode="" RoutingType="" BankID="2718301" 
        //        BranchNumber="" BranchAddress="" BranchCity="" BranchState=""/>
        SOAPElement requestElement = root.addChildElement("Request");

        requestElement.addAttribute(new QName("", "CountryCode"), request.getCountryCode());
        requestElement.addAttribute(new QName("", "RoutingCode"), request.getRoutingCode());
        requestElement.addAttribute(new QName("", "RoutingType"), request.getRoutingType());
        requestElement.addAttribute(new QName("", "BankID"), request.getBankID());
        requestElement.addAttribute(new QName("", "BranchNumber"), request.getBranchNumber());
        requestElement.addAttribute(new QName("", "BranchAddress"), request.getBranchAddress());
        requestElement.addAttribute(new QName("", "BranchCity"), request.getBranchCity());
        requestElement.addAttribute(new QName("", "BranchState"), request.getBranchState());
    }

    private static void parseAndReturnResponse(SOAPMessage soapResponse) {
        try {
            GetBankBranchesResponseEntity response = new GetBankBranchesResponseEntity();
            getBankBranchesStringReturn = "";
            String WarningMsg = "";
            String ErrorMsg = "";
            SOAPPart sp = soapResponse.getSOAPPart();
            SOAPEnvelope se = sp.getEnvelope();
            SOAPBody sb = se.getBody();
            Iterator it = sb.getChildElements();
            while (it.hasNext()) {
                // GetCurrenciesResponse
                SOAPBodyElement bodyElement = (SOAPBodyElement) it.next();
                Iterator it2 = bodyElement.getChildElements();
                while (it2.hasNext()) {
                    SOAPElement element2 = (SOAPElement) it2.next();
                    Iterator it3 = element2.getChildElements();
                    while (it3.hasNext()) {
                        SOAPElement element3 = (SOAPElement) it3.next();
                        Iterator it4 = element3.getChildElements();
                        while (it4.hasNext()) {
                            SOAPElement element4 = (SOAPElement) it4.next();
                            if ("RequestResponses".equals(element4.getElementName().getLocalName())) {
                                Iterator it5 = element4.getChildElements();
                                while (it5.hasNext()) {
                                    SOAPElement element5 = (SOAPElement) it5.next();
                                    //<BankBranches InputParams="BankID='2718301';CountryCode='CO';BranchNumber='';BranchAddress='';BranchCity='';BranchState='';RoutingType='';RoutingCode='';">
                                    response.setInputParams(element5.getAttribute("InputParams"));

                                    //<Branch BankID="2718301" BankName="BANCO DAVIVIENDA S.A." BranchID="21323402" BranchName="BANCO DAVIVIENDA S.A." BranchNo="" BranchState="" BranchCity="Armenia" BranchPostalCode="" BranchAddress="" BranchRoutingType="" BranchRoutingCode=""/>
                                    Iterator it6 = element5.getChildElements();
                                    while (it6.hasNext()) {
                                        SOAPElement element6 = (SOAPElement) it6.next();
                                        BranchEntity branch = new BranchEntity();
                                        branch.setBankID(element6.getAttribute("BankID"));
                                        branch.setBankName(element6.getAttribute("BankName"));
                                        branch.setBranchID(element6.getAttribute("BranchID"));
                                        branch.setBranchName(element6.getAttribute("BranchName"));
                                        branch.setBranchNo(element6.getAttribute("BranchNo"));
                                        branch.setBranchState(element6.getAttribute("BranchState"));
                                        branch.setBranchCity(element6.getAttribute("BranchCity"));
                                        branch.setBranchPostalCode(element6.getAttribute("BranchPostalCode"));
                                        branch.setBranchAddress(element6.getAttribute("BranchAddress"));
                                        branch.setBranchRoutingType(element6.getAttribute("BranchRoutingType"));
                                        branch.setBranchRoutingCode(element6.getAttribute("BranchRoutingCode"));
                                        response.getBranchList().add(branch);
                                    }
                                }
                            }
                            //Warning and errors
                            if ("RequestWarnings".equals(element4.getElementName().getLocalName())) {
                                Iterator it5 = element4.getChildElements();
                                while (it5.hasNext()) {
                                    SOAPElement element5 = (SOAPElement) it5.next();
                                    WarningMsg = element5.getAttribute("WarningMsg") + "|";
                                }
                            }

                            if ("RequestErrors".equals(element4.getElementName().getLocalName())) {
                                Iterator it5 = element4.getChildElements();
                                while (it5.hasNext()) {
                                    SOAPElement element5 = (SOAPElement) it5.next();
                                    ErrorMsg = element5.getAttribute("ErrorMsg");
                                }
                            }
                        }
                    }
                }
            }

            if (generateToStdOut) {
                getBankBranchesStringReturn = "";
                for (BranchEntity branch : response.getBranchList()) {
                    getBankBranchesStringReturn = getBankBranchesStringReturn + response.getInputParams() + "|"
                            + branch.getBankID() + "|"
                            + branch.getBankName() + "|"
                            + branch.getBranchID() + "|"
                            + branch.getBranchName() + "|"
                            + branch.getBranchNo() + "|"
                            + branch.getBranchState() + "|"
                            + branch.getBranchCity() + "|"
                            + branch.getBranchPostalCode() + "|"
                            + branch.getBranchAddress() + "|"
                            + branch.getBranchRoutingType() + "|"
                            + branch.getBranchRoutingCode() + "|" + "\n";
                }

                if (WarningMsg.length() == 0 && ErrorMsg.length() == 0 && !getBankBranchesStringReturn.isEmpty()) {
                    System.out.println("00|OK");
                }

                if (WarningMsg.length() > 0 || ErrorMsg.length() > 0 || getBankBranchesStringReturn.isEmpty()) {
                    System.out.println("99|" + WarningMsg + ErrorMsg);
                }
                System.out.println(getBankBranchesStringReturn);
            }

        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
    }

    private static SOAPMessage createSOAPRequest(String soapAction,
            GetBankBranchesRequestEntity request) throws Exception {
        MessageFactory messageFactory = MessageFactory.newInstance();
        SOAPMessage soapMessage = messageFactory.createMessage();

        createSoapEnvelope(soapMessage, request);

        MimeHeaders headers = soapMessage.getMimeHeaders();
        headers.addHeader("SOAPAction", soapAction);

        soapMessage.saveChanges();

        if (IS_DEBUG) {
            /* Print the request message, just for debugging purposes */
            System.out.println("Request SOAP Message:");
            soapMessage.writeTo(System.out);
            System.out.println("\n");
        }

        return soapMessage;
    }
}
