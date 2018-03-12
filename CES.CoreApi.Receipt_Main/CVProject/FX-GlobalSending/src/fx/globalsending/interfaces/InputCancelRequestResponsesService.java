/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalsending.interfaces;

import fx.globalsending.entities.CSMessageAcknowledgementEntity;
import fx.globalsending.entities.CSMessageEntity;
import fx.globalsending.entities.CancelRequestResponseAcknowledgementEntity;
import fx.globalsending.entities.CancelRequestResponseEntity;
import fx.globalsending.entities.HeaderEntity;
import fx.globalsending.entities.InputCancelRequestResponsesRequestEntity;
import fx.globalsending.entities.InputCancelRequestResponsesResponseEntity;
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
public class InputCancelRequestResponsesService {

    private Configurations configurations;
    private boolean isDebug;
    private String cesNamespace;
    private String fxGlobalNamespaceURI;
    private String WarningMsg;
    private String ErrorMsg;
    private String resultString;
    private boolean generateToStdOut = true;

    public InputCancelRequestResponsesService(Configurations configurations) {
        this.configurations = configurations;
        isDebug = false;
        cesNamespace = "ces";
        fxGlobalNamespaceURI = "CES.Services.FXGlobal";
        WarningMsg = "";
        ErrorMsg = "";
    }

    private static final String SOAP_ACTION = "CES.Services.FXGlobal/IRiaAsSender/InputCancelRequestResponses";

    private static final String OPERATION = "InputCancelRequestResponses";

    private void createSoapEnvelope(SOAPMessage soapMessage,
            InputCancelRequestResponsesRequestEntity request) throws SOAPException {
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

        SOAPElement cancelRequestResponsesElement = root.addChildElement("CancelRequestResponses");

        for (int i = 0; i < request.getCancelRequestResponses().size(); i++) {
            SOAPElement cancelRequestResponseElement = cancelRequestResponsesElement.addChildElement("CancelRequestResponse");

            SOAPElement pcOrderNoElement = cancelRequestResponseElement.addChildElement("PCOrderNo");
            pcOrderNoElement.addTextNode(request.getCancelRequestResponses().get(i).getPcOrderNo());

            SOAPElement scOrderNoElement = cancelRequestResponseElement.addChildElement("SCOrderNo");
            scOrderNoElement.addTextNode(request.getCancelRequestResponses().get(i).getScOrderNo());
            
            SOAPElement responseCodeElement = cancelRequestResponseElement.addChildElement("ResponseCode");
            responseCodeElement.addTextNode(request.getCancelRequestResponses().get(i).getResponseCode());

            SOAPElement responseDescElement = cancelRequestResponseElement.addChildElement("ResponseDesc");
            responseDescElement.addTextNode(request.getCancelRequestResponses().get(i).getResponseDesc());

            SOAPElement responseDateElement = cancelRequestResponseElement.addChildElement("ResponseDate");
            responseDateElement.addTextNode(request.getCancelRequestResponses().get(i).getResponseDate());
            
            SOAPElement responseTimeElement = cancelRequestResponseElement.addChildElement("ResponseTime");
            responseTimeElement.addTextNode(request.getCancelRequestResponses().get(i).getResponseTime());
            
            SOAPElement commentsElement = cancelRequestResponseElement.addChildElement("Comments");
            commentsElement.addTextNode(request.getCancelRequestResponses().get(i).getComments());
        }

    }

    private SOAPMessage createSoapRequest(InputCancelRequestResponsesRequestEntity request) throws Exception {
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

    private InputCancelRequestResponsesResponseEntity parseAndReturnResponse(SOAPMessage soapResponse) {
        InputCancelRequestResponsesResponseEntity response = new InputCancelRequestResponsesResponseEntity();
        resultString = "";
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

                                    if ("CancelRequestResponseAcknowledgement".equals(element5.getElementName().getLocalName())) {
                                        Iterator it6 = element5.getChildElements();

                                        CancelRequestResponseAcknowledgementEntity entity = new CancelRequestResponseAcknowledgementEntity();
                                        while (it6.hasNext()) {
                                            SOAPElement element6 = (SOAPElement) it6.next();
                                            if ("PCOrderNo".equals(element6.getElementName().getLocalName())) {
                                                entity.setPcOrderNo(element6.getTextContent());
                                            }
                                            if ("SCOrderNo".equals(element6.getElementName().getLocalName())) {
                                                entity.setScOrderNo(element6.getTextContent());
                                            }
                                            if ("ProcessDate".equals(element6.getElementName().getLocalName())) {
                                                entity.setProcessDate(element6.getTextContent());
                                            }
                                            if ("ProcessTime".equals(element6.getElementName().getLocalName())) {
                                                entity.setProcessTime(element6.getTextContent());
                                            }
                                            if ("NotificationCode".equals(element6.getElementName().getLocalName())) {
                                                entity.setNotificationCode(element6.getTextContent());
                                            }
                                            if ("NotificationDesc".equals(element6.getElementName().getLocalName())) {
                                                entity.setNotificationDesc(element6.getTextContent());
                                            }
                                        }

                                        response.getAcknowledgements().add(entity);
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
                resultString = "";
                for (CancelRequestResponseAcknowledgementEntity ack : response.getAcknowledgements()) {
                    resultString = resultString
                            + ack.getPcOrderNo() + "|"
                            + ack.getScOrderNo() + "|"
                            + ack.getProcessDate() + "|"
                            + ack.getProcessTime() + "|"
                            + ack.getNotificationCode() + "|"
                            + ack.getNotificationDesc() + "|"
                            + "\n";
                }

                if (WarningMsg.length() == 0 && ErrorMsg.length() == 0 && !resultString.isEmpty()) {
                    System.out.println("00|OK");
                }

                if (WarningMsg.length() > 0 || ErrorMsg.length() > 0 || resultString.isEmpty()) {
                    System.out.println("99|" + WarningMsg + ErrorMsg);
                }
                System.out.println(resultString);
            }

        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }

        return response;
    }

    public InputCancelRequestResponsesRequestEntity parseInputArgsToRequest(String[] args) {
        InputCancelRequestResponsesRequestEntity request = new InputCancelRequestResponsesRequestEntity();
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
            CancelRequestResponseEntity entity = new CancelRequestResponseEntity();
            if (row.length >= 1) {
                entity.setPcOrderNo(row[0]);
            }
            if (row.length >= 2) {
                entity.setScOrderNo(row[1]);
            }
            if (row.length >= 3) {
                entity.setResponseCode(row[2]);
            }
            if (row.length >= 4) {
                entity.setResponseDesc(row[3]);
            }
            if (row.length >= 5) {
                entity.setResponseDate(row[4]);
            }
            if (row.length >= 6) {
                entity.setResponseTime(row[5]);
            }
            if (row.length >= 7) {
                entity.setComments(row[6]);
            }
            request.getCancelRequestResponses().add(entity);
        }

        return request;
    }

    public InputCancelRequestResponsesResponseEntity invoke(InputCancelRequestResponsesRequestEntity request) {
        InputCancelRequestResponsesResponseEntity response = new InputCancelRequestResponsesResponseEntity();
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
