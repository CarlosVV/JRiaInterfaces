/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.interfaces;

import fx.globalpaying.entities.AcknowledgementEntity;
import fx.globalpaying.entities.BankEntity;
import fx.globalpaying.entities.BranchEntity;
import fx.globalpaying.entities.CSMessageEntity;
import fx.globalpaying.entities.CancellationRequestEntity;
import fx.globalpaying.entities.CurrencyEntity;
import fx.globalpaying.entities.InputCancelationRequestsRequestEntity;
import fx.globalpaying.entities.GetBankBranchesResponseEntity;
import fx.globalpaying.entities.HeaderEntity;
import fx.globalpaying.entities.InputCancelationRequestsResponseEntity;
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
public class CancellationRequests {

    private static final String XML_DOC = "xmlDoc";
    private static final String ROOT = "Root";
    private static final String INPUT_CANCELATION_REQUESTS = "InputCancelationRequests";
    private static final String SOAP_ACTION = "CES.Services.FXGlobal/IRiaAsPayer/InputCancelationRequests";
    private static boolean generateToStdOut = false;
    private static final boolean IS_DEBUG = false;
    private static String inputCancelationRequestsStringReturn;

    public static String getInputCancelationRequestsStringReturn() {
        return inputCancelationRequestsStringReturn;
    }

    public static InputCancelationRequestsRequestEntity parseInputArgsToRequest(String[] args) {
        InputCancelationRequestsRequestEntity request = new InputCancelationRequestsRequestEntity();
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
            if (requestArray.length >= 1) {
                for (int i = 1; i < requestArray.length; i++) {
                    CancellationRequestEntity requestEntity = new CancellationRequestEntity();
                    requestEntity.setScOrderNo(requestArray[i]);
                    request.getRequests().add(requestEntity);
                }
            }
        }

        return request;
    }

    public static void callSoapWebService(String soapEndpointUrl,
            InputCancelationRequestsRequestEntity request) {
        callSoapWebService(soapEndpointUrl, request, true);
    }

    public static void callSoapWebService(String soapEndpointUrl,
            InputCancelationRequestsRequestEntity request, boolean generateToStdOutPrint) {
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
            InputCancelationRequestsRequestEntity request) throws SOAPException {
        SOAPPart soapPart = soapMessage.getSOAPPart();

        String cesNamespace = "ces";
        String fxGlobalNamespaceURI = "CES.Services.FXGlobal";

        // SOAP Envelope
        SOAPEnvelope envelope = soapPart.getEnvelope();
        envelope.addNamespaceDeclaration(cesNamespace, fxGlobalNamespaceURI);

        // SOAP Body
        SOAPBody soapBody = envelope.getBody();

        // <ces:GetCurrencies>
        SOAPElement getCurrenciesElem = soapBody.addChildElement(INPUT_CANCELATION_REQUESTS, cesNamespace);

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

        SOAPElement requestElement = root.addChildElement("CancellationRequests");

        for (int i = 0; i < request.getRequests().size(); i++) {
            SOAPElement cancelationRequestElement = requestElement.addChildElement("CancellationRequest");
            cancelationRequestElement.addAttribute(new QName("", "SCOrderNo"), request.getRequests().get(i).getScOrderNo());
        }
    }

    private static void parseAndReturnResponse(SOAPMessage soapResponse) {
        try {
            InputCancelationRequestsResponseEntity response = new InputCancelationRequestsResponseEntity();
            inputCancelationRequestsStringReturn = "";
            String WarningMsg = "";
            String ErrorMsg = "";
            SOAPPart sp = soapResponse.getSOAPPart();
            SOAPEnvelope se = sp.getEnvelope();
            SOAPBody sb = se.getBody();
            Iterator it = sb.getChildElements();
            while (it.hasNext()) {
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
                            if ("Acknowledgements".equals(element4.getElementName().getLocalName())) {
                                Iterator it5 = element4.getChildElements();
                                while (it5.hasNext()) {
                                    SOAPElement element5 = (SOAPElement) it5.next();

                                    if ("CancellationRequestAcknowledgement".equals(element5.getElementName().getLocalName())) {
                                        Iterator it6 = element5.getChildElements();

                                        AcknowledgementEntity cre = new AcknowledgementEntity();
                                        while (it6.hasNext()) {
                                            SOAPElement element6 = (SOAPElement) it6.next();
                                            if ("SCOrderNo".equals(element6.getElementName().getLocalName())) {
                                                cre.setScOrderNo(element6.getTextContent());
                                            }
                                            if ("ProcessDate".equals(element6.getElementName().getLocalName())) {
                                                cre.setProcessDate(element6.getTextContent());
                                            }
                                            if ("ProcessTime".equals(element6.getElementName().getLocalName())) {
                                                cre.setProcessTime(element6.getTextContent());
                                            }
                                            if ("NotificationCode".equals(element6.getElementName().getLocalName())) {
                                                cre.setNotificationCode(element6.getTextContent());
                                            }
                                            if ("NotificationDesc".equals(element6.getElementName().getLocalName())) {
                                                cre.setNotificationDesc(element6.getTextContent());
                                            }
                                        }

                                        response.getAcknowledgements().add(cre);
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

                            if ("InputErrors".equals(element4.getElementName().getLocalName())) {
                                Iterator it5 = element4.getChildElements();
                                while (it5.hasNext()) {
                                    SOAPElement element5 = (SOAPElement) it5.next();
                                    ErrorMsg = ErrorMsg + element5.getAttribute("ErrorMsg") + "|";
                                }
                            }
                        }
                    }
                }
            }

            if (generateToStdOut) {
                inputCancelationRequestsStringReturn = "";
                for (AcknowledgementEntity ack : response.getAcknowledgements()) {
                    inputCancelationRequestsStringReturn = inputCancelationRequestsStringReturn
                            + ack.getScOrderNo() + "|"
                            + ack.getProcessDate() + "|"
                            + ack.getProcessTime() + "|"
                            + ack.getNotificationCode() + "|"
                            + ack.getNotificationDesc() + "|"
                            + "\n";
                }

                if (WarningMsg.length() == 0 && ErrorMsg.length() == 0 && !inputCancelationRequestsStringReturn.isEmpty()) {
                    System.out.println("00|OK");
                }

                if (WarningMsg.length() > 0 || ErrorMsg.length() > 0 || inputCancelationRequestsStringReturn.isEmpty()) {
                    System.out.println("99|" + WarningMsg + ErrorMsg);
                }
                System.out.println(inputCancelationRequestsStringReturn);
            }

        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
    }

    private static SOAPMessage createSOAPRequest(String soapAction,
            InputCancelationRequestsRequestEntity request) throws Exception {
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
