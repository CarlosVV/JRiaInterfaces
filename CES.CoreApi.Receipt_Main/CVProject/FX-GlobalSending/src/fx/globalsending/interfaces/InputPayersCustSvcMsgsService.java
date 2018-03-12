/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalsending.interfaces;

import fx.globalsending.entities.CSMessageAcknowledgementEntity;
import fx.globalsending.entities.CSMessageEntity;
import fx.globalsending.entities.HeaderEntity;
import fx.globalsending.entities.InputPayersCustSvcMsgsRequestEntity;
import fx.globalsending.entities.InputPayersCustSvcMsgsResponseEntity;
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
public class InputPayersCustSvcMsgsService {

    private Configurations configurations;
    private boolean isDebug;
    private String cesNamespace;
    private String fxGlobalNamespaceURI;
    private String WarningMsg;
    private String ErrorMsg;
    private String inputPayersCustSvcStringReturn;
    private boolean generateToStdOut = true;

    public InputPayersCustSvcMsgsService(Configurations configurations) {
        this.configurations = configurations;
        isDebug = false;
        cesNamespace = "ces";
        fxGlobalNamespaceURI = "CES.Services.FXGlobal";
        WarningMsg = "";
        ErrorMsg = "";
    }

    private static final String SOAP_ACTION = "CES.Services.FXGlobal/IRiaAsSender/InputPayersCustSvcMsgs";

    private static final String OPERATION = "InputPayersCustSvcMsgs";

    private void createSoapEnvelope(SOAPMessage soapMessage,
            InputPayersCustSvcMsgsRequestEntity request) throws SOAPException {
        SOAPPart soapPart = soapMessage.getSOAPPart();

        SOAPEnvelope envelope = soapPart.getEnvelope();
        envelope.addNamespaceDeclaration(cesNamespace, fxGlobalNamespaceURI);

        SOAPBody soapBody = envelope.getBody();

        SOAPElement getCurrenciesElem = soapBody.addChildElement(OPERATION, cesNamespace);

        SOAPElement xmlDoc = getCurrenciesElem.addChildElement("xmlDoc", cesNamespace);

        SOAPElement root = xmlDoc.addChildElement("Root");

        SOAPElement correspID = root.addChildElement("CorrespID");
        correspID.addTextNode(request.getCorrespID());

        SOAPElement layoutVersion = root.addChildElement("LayoutVersion");
        layoutVersion.addTextNode(request.getLayoutVersion());

        SOAPElement headerElement = root.addChildElement("Header");

        SOAPElement CallID = headerElement.addChildElement("CallID");
        CallID.addTextNode(request.getHeader().getCallID());

        SOAPElement callDateTimeLocal = headerElement.addChildElement("CallDateTimeLocal");
        callDateTimeLocal.addTextNode(request.getHeader().getCallDateTimeLocal());

        SOAPElement correspLocNo = headerElement.addChildElement("CorrespLocNo");
        correspLocNo.addTextNode(request.getHeader().getCorrespLocNo());

        SOAPElement correspLocNo_Ria = headerElement.addChildElement("CorrespLocNo_Ria");
        correspLocNo_Ria.addTextNode(request.getHeader().getCorrespLocNo_Ria());

        SOAPElement correspLocName = headerElement.addChildElement("CorrespLocName");
        correspLocName.addTextNode(request.getHeader().getCorrespLocName());

        SOAPElement correspLocCountry = headerElement.addChildElement("CorrespLocCountry");
        correspLocCountry.addTextNode(request.getHeader().getCorrespLocCountry());

        SOAPElement userID = headerElement.addChildElement("UserID");
        userID.addTextNode(request.getHeader().getUserID());

        SOAPElement terminalID = headerElement.addChildElement("TerminalID");
        terminalID.addTextNode(request.getHeader().getTerminalID());

        SOAPElement languageCultureCode = headerElement.addChildElement("LanguageCultureCode");
        languageCultureCode.addTextNode(request.getHeader().getLanguageCultureCode());

        SOAPElement csMessagesElement = root.addChildElement("CSMessages");

        for (int i = 0; i < request.getMessages().size(); i++) {
            SOAPElement csMessageElement = csMessagesElement.addChildElement("CSMessage");

            SOAPElement pcOrderNoElement = csMessageElement.addChildElement("PCOrderNo");
            pcOrderNoElement.addTextNode(request.getMessages().get(i).getPcOrderNo());

            SOAPElement scOrderNoElement = csMessageElement.addChildElement("SCOrderNo");
            scOrderNoElement.addTextNode(request.getMessages().get(i).getScOrderNo());

            SOAPElement messageIDElement = csMessageElement.addChildElement("MessageID");
            messageIDElement.addTextNode(request.getMessages().get(i).getMessageID());

            SOAPElement messageTextElement = csMessageElement.addChildElement("MessageText");
            messageTextElement.addTextNode(request.getMessages().get(i).getMessageText());

            SOAPElement enteredByElement = csMessageElement.addChildElement("EnteredBy");
            enteredByElement.addTextNode(request.getMessages().get(i).getEnteredBy());
        }

    }

    private SOAPMessage createSoapRequest(InputPayersCustSvcMsgsRequestEntity request) throws Exception {
        MessageFactory messageFactory = MessageFactory.newInstance();
        SOAPMessage soapMessage = messageFactory.createMessage();

        createSoapEnvelope(soapMessage, request);

        MimeHeaders headers = soapMessage.getMimeHeaders();
        headers.addHeader("SOAPAction", SOAP_ACTION);

        soapMessage.saveChanges();

        if (isDebug) {
            /* Print the request message, just for debugging purposes */
            System.out.println("Request SOAP Message:");
            soapMessage.writeTo(System.out);
            System.out.println("\n");
        }

        return soapMessage;
    }

    private InputPayersCustSvcMsgsResponseEntity parseAndReturnResponse(SOAPMessage soapResponse) {
        InputPayersCustSvcMsgsResponseEntity response = new InputPayersCustSvcMsgsResponseEntity();
        inputPayersCustSvcStringReturn = "";
        String WarningMsg = "";
        String ErrorMsg = "";

        try {

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

                                    if ("CSMessageAcknowledgement".equals(element5.getElementName().getLocalName())) {
                                        Iterator it6 = element5.getChildElements();

                                        CSMessageAcknowledgementEntity cre = new CSMessageAcknowledgementEntity();
                                        while (it6.hasNext()) {
                                            SOAPElement element6 = (SOAPElement) it6.next();
                                            if ("PCOrderNo".equals(element6.getElementName().getLocalName())) {
                                                cre.setPcOrderNo(element6.getTextContent());
                                            }
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
                inputPayersCustSvcStringReturn = "";
                for (CSMessageAcknowledgementEntity ack : response.getAcknowledgements()) {
                    inputPayersCustSvcStringReturn = inputPayersCustSvcStringReturn
                            + ack.getPcOrderNo() + "|"
                            + ack.getScOrderNo() + "|"
                            + ack.getProcessDate() + "|"
                            + ack.getProcessTime() + "|"
                            + ack.getNotificationCode() + "|"
                            + ack.getNotificationDesc() + "|"
                            + "\n";
                }

                if (WarningMsg.length() == 0 && ErrorMsg.length() == 0 && !inputPayersCustSvcStringReturn.isEmpty()) {
                    System.out.println("00|OK");
                }

                if (WarningMsg.length() > 0 || ErrorMsg.length() > 0 || inputPayersCustSvcStringReturn.isEmpty()) {
                    System.out.println("99|" + WarningMsg + ErrorMsg);
                }
                System.out.println(inputPayersCustSvcStringReturn);
            }

        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }

        return response;
    }

    public InputPayersCustSvcMsgsRequestEntity parseInputArgsToRequest(String[] args) {
        InputPayersCustSvcMsgsRequestEntity request = new InputPayersCustSvcMsgsRequestEntity();
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

        String[] params = args[3].split(";");

        for (int i = 1; i < params.length; i++) {
            String[] row = params[i].split("\\|", -1);
            CSMessageEntity csME = new CSMessageEntity();
            if (row.length >= 1) {
                csME.setPcOrderNo(row[0]);
            }
            if (row.length >= 2) {
                csME.setScOrderNo(row[1]);
            }
            if (row.length >= 3) {
                csME.setMessageID(row[2]);
            }
            if (row.length >= 4) {
                csME.setMessageText(row[3]);
            }
            if (row.length >= 5) {
                csME.setEnteredBy(row[4]);
            }
            request.getMessages().add(csME);
        }

        return request;
    }

    public InputPayersCustSvcMsgsResponseEntity invoke(InputPayersCustSvcMsgsRequestEntity request) {
        InputPayersCustSvcMsgsResponseEntity response = new InputPayersCustSvcMsgsResponseEntity();
        try {
            // Create SOAP Connection
            SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
            SOAPConnection soapConnection = soapConnectionFactory.createConnection();

            // Send SOAP Message to SOAP Server
            SOAPMessage soapResponse = soapConnection.call(createSoapRequest(request), configurations.getSoapEndPointUrl());

            // Return parsed Response
            response = parseAndReturnResponse(soapResponse);

            if (isDebug) {
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
        return response;
    }
}
