/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.interfaces;

import fx.globalpaying.entities.GetRequirementsRequestEntity;
import fx.globalpaying.entities.HeaderEntity;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
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
public class Requirements {

    private static List<String> requirementsList = new ArrayList<String>();
    private static final String soapAction = "CES.Services.FXGlobal/IRiaAsPayer/GetRequirements";
    private static final boolean isDebug = false;

    public static GetRequirementsRequestEntity parseInputArgsToRequest(String[] args) {
        GetRequirementsRequestEntity request = new GetRequirementsRequestEntity();
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
        request.setRequestType(args[3]);

        return request;
    }

    private static void createSoapEnvelope(SOAPMessage soapMessage,
            GetRequirementsRequestEntity request) throws SOAPException {
        SOAPPart soapPart = soapMessage.getSOAPPart();

        String cesNamespace = "ces";
        String fxGlobalNamespaceURI = "CES.Services.FXGlobal";

        // SOAP Envelope
        SOAPEnvelope envelope = soapPart.getEnvelope();
        envelope.addNamespaceDeclaration(cesNamespace, fxGlobalNamespaceURI);

        // SOAP Body
        SOAPBody soapBody = envelope.getBody();

        // <ces:GetCurrencies>
        SOAPElement getCurrenciesElem = soapBody.addChildElement("GetRequirements", cesNamespace);

        // <ces:xmlDoc>
        SOAPElement xmlDoc = getCurrenciesElem.addChildElement("xmlDoc", cesNamespace);

        // <Root>
        SOAPElement root = xmlDoc.addChildElement("Root");

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

        // <RequestType>CountriesStates</RequestType>
        SOAPElement requestType = root.addChildElement("RequestType");
        requestType.addTextNode(request.getRequestType());
    }

    public static void callSoapWebService(String soapEndpointUrl,
            GetRequirementsRequestEntity request) {
        try {
            // Create SOAP Connection
            SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
            SOAPConnection soapConnection = soapConnectionFactory.createConnection();

            // Send SOAP Message to SOAP Server
            SOAPMessage soapResponse = soapConnection.call(createSOAPRequest(soapAction, request), soapEndpointUrl);

            if (isDebug) {
                // Print the SOAP Response
                System.out.println("Response SOAP Message:");
                soapResponse.writeTo(System.out);
                System.out.println();
            }

            // Return parsed Response
            parseAndReturnResponse(soapResponse);

            soapConnection.close();
        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
    }

    private static void parseAndReturnResponse(SOAPMessage soapResponse) {
        try {
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
                                    Iterator it6 = element5.getChildElements();
                                    while (it6.hasNext()) {
                                        SOAPElement element6 = (SOAPElement) it6.next();
                                        Iterator it7 = element6.getChildElements();
                                        while (it7.hasNext()) {
                                            SOAPElement element7 = (SOAPElement) it7.next();
                                            String id = element7.getAttribute("ID");
                                            String desc = element7.getAttribute("Desc");
                                            Iterator it8 = element7.getChildElements();
                                            while (it8.hasNext()) {
                                                SOAPElement element8 = (SOAPElement) it8.next();
                                                Iterator it9 = element8.getChildElements();
                                                while (it9.hasNext()) {
                                                    SOAPElement element9 = (SOAPElement) it9.next();
                                                    String fieldName = element9.getAttribute("FieldName");
                                                    String minLength = element9.getAttribute("MinLength");
                                                    String maxLength = element9.getAttribute("MaxLength");
                                                    requirementsList.add(id + "|" + desc + "|" + fieldName + "|" + minLength + "|" + maxLength + "|");
                                                }
                                            }
                                        }
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
            if (WarningMsg.length() == 0 && ErrorMsg.length() == 0 && !requirementsList.isEmpty()) {
                System.out.println("00|OK");
            }

            if (WarningMsg.length() > 0 || ErrorMsg.length() > 0 || requirementsList.isEmpty()) {
                System.out.println("99|" + WarningMsg + ErrorMsg);
            }
            for (String item : requirementsList) {
                System.out.println(item);
            }

        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
    }

    private static SOAPMessage createSOAPRequest(String soapAction,
            GetRequirementsRequestEntity request) throws Exception {
        MessageFactory messageFactory = MessageFactory.newInstance();
        SOAPMessage soapMessage = messageFactory.createMessage();

        createSoapEnvelope(soapMessage, request);

        MimeHeaders headers = soapMessage.getMimeHeaders();
        headers.addHeader("SOAPAction", soapAction);

        soapMessage.saveChanges();

        if (isDebug) {
            /* Print the request message, just for debugging purposes */
            System.out.println("Request SOAP Message:");
            soapMessage.writeTo(System.out);
            System.out.println("\n");
        }

        return soapMessage;
    }
}
