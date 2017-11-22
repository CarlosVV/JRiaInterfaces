/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalsending.interfaces;

import fx.globalsending.entities.HeaderEntity;
import fx.globalsending.entities.VerifyOrderForPayoutServiceRequest;
import fx.globalsending.entities.VerifyOrderForPayoutServiceResponse;
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
public class VerifyOrderForPayoutService {

    private Configurations configurations;
    private boolean isDebug;
    private String cesNamespace;
    private String fxGlobalNamespaceURI;
    private String WarningMsg;
    private String ErrorMsg;

    public VerifyOrderForPayoutService(Configurations configurations) {
        this.configurations = configurations;
        isDebug = false;
        cesNamespace = "ces";
        fxGlobalNamespaceURI = "CES.Services.FXGlobal";
        WarningMsg = "";
        ErrorMsg = "";
    }

    private static final String SOAP_ACTION = "CES.Services.FXGlobal/IRiaAsSender/OP_VerifyOrderForPayout";

    private static final String OPERATION = "OP_VerifyOrderForPayout";

    private boolean isParsableAsLong(final String s) {
        try {
            Long.valueOf(s);
            return true;
        } catch (NumberFormatException numberFormatException) {
            return false;
        }
    }

    private void createSoapEnvelope(SOAPMessage soapMessage,
            VerifyOrderForPayoutServiceRequest request) throws SOAPException {
        SOAPPart soapPart = soapMessage.getSOAPPart();

        // SOAP Envelope
        SOAPEnvelope envelope = soapPart.getEnvelope();
        envelope.addNamespaceDeclaration(cesNamespace, fxGlobalNamespaceURI);

        // SOAP Body
        SOAPBody soapBody = envelope.getBody();

        // <ces:OP_VerifyOrderForPayout>
        SOAPElement getCurrenciesElem = soapBody.addChildElement(OPERATION, cesNamespace);

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

        //  <DateTimeLocal>20170927135959</DateTimeLocal>
        SOAPElement dateTimeLocal = root.addChildElement("DateTimeLocal");
        dateTimeLocal.addTextNode(request.getDateTimeLocal());

        //  <DateTimeUTC>20170927151612</DateTimeUTC>
        SOAPElement dateTimeUTC = root.addChildElement("DateTimeUTC");
        dateTimeUTC.addTextNode(request.getDateTimeUTC());

        //  <PIN>11470087617</PIN>
        SOAPElement pin = root.addChildElement("PIN");
        pin.addTextNode(request.getPin());

        //  <BeneAmount>74679</BeneAmount>
        SOAPElement beneAmount = root.addChildElement("BeneAmount");
        beneAmount.addTextNode(request.getBeneAmount());

        //  <CorrespLocID>001</CorrespLocID>
        SOAPElement correspLocID = root.addChildElement("CorrespLocID");
        correspLocID.addTextNode(request.getCorrespLocID());
    }

    private SOAPMessage createSoapRequest(VerifyOrderForPayoutServiceRequest request) throws Exception {
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

    private VerifyOrderForPayoutServiceResponse parseAndReturnResponse(SOAPMessage soapResponse) {
        VerifyOrderForPayoutServiceResponse response = new VerifyOrderForPayoutServiceResponse();
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
                            if ("RequestResponses".equals(element4.getElementName().getLocalName())) {
                                Iterator it5 = element4.getChildElements();
                                while (it5.hasNext()) {
                                    SOAPElement element5 = (SOAPElement) it5.next();
                                    if ("VerifyOrderResponse".equals(element5.getElementName().getLocalName())) {
                                        Iterator it6 = element5.getChildElements();
                                        while (it6.hasNext()) {
                                            SOAPElement element7 = (SOAPElement) it6.next();
                                            if ("TransRefID".equals(element7.getElementName().getLocalName())) {
                                                response.setTransRefID(element7.getTextContent());
                                            }
                                            if ("OrderFound".equals(element7.getElementName().getLocalName())) {
                                                response.setOrderFound(element7.getTextContent());
                                            }
                                            if ("PIN".equals(element7.getElementName().getLocalName())) {
                                                response.setPIN(element7.getTextContent());
                                            }
                                            if ("OrderNo".equals(element7.getElementName().getLocalName())) {
                                                response.setOrderNo(element7.getTextContent());
                                            }
                                            if ("SeqIDRA".equals(element7.getElementName().getLocalName())) {
                                                response.setSeqIDRA(element7.getTextContent());
                                            }
                                            if ("SeqIDPA".equals(element7.getElementName().getLocalName())) {
                                                response.setSeqIDPA(element7.getTextContent());
                                            }
                                            if ("OrderDate".equals(element7.getElementName().getLocalName())) {
                                                response.setOrderDate(element7.getTextContent());
                                            }
                                            if ("CountryFrom".equals(element7.getElementName().getLocalName())) {
                                                response.setCountryFrom(element7.getTextContent());
                                            }
                                            if ("CustNameFirst".equals(element7.getElementName().getLocalName())) {
                                                response.setCustNameFirst(element7.getTextContent());
                                            }
                                            if ("CustNameLast1".equals(element7.getElementName().getLocalName())) {
                                                response.setCustNameLast1(element7.getTextContent());
                                            }
                                            if ("CustAddress".equals(element7.getElementName().getLocalName())) {
                                                response.setCustAddress(element7.getTextContent());
                                            }
                                            if ("CustCity".equals(element7.getElementName().getLocalName())) {
                                                response.setCustCity(element7.getTextContent());
                                            }
                                            if ("CustState".equals(element7.getElementName().getLocalName())) {
                                                response.setCustState(element7.getTextContent());
                                            }
                                            if ("CustCountry".equals(element7.getElementName().getLocalName())) {
                                                response.setCustCountry(element7.getTextContent());
                                            }
                                            if ("CustTelNo".equals(element7.getElementName().getLocalName())) {
                                                response.setCustTelNo(element7.getTextContent());
                                            }
                                            if ("BeneNameFirst".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneNameFirst(element7.getTextContent());
                                            }
                                            if ("BeneNameMiddle".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneNameMiddle(element7.getTextContent());
                                            }
                                            if ("BeneNameLast1".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneNameLast1(element7.getTextContent());
                                            }
                                            if ("BeneAddress".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneAddress(element7.getTextContent());
                                            }
                                            if ("BeneCity".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneCity(element7.getTextContent());
                                            }
                                            if ("BeneState".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneState(element7.getTextContent());
                                            }
                                            if ("BeneCountry".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneCountry(element7.getTextContent());
                                            }
                                            if ("BeneZip".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneZip(element7.getTextContent());
                                            }
                                            if ("BeneTelNo".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneTelNo(element7.getTextContent());
                                            }
                                            if ("BeneCurrency".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneCurrency(element7.getTextContent());
                                            }
                                            if ("BeneAmount".equals(element7.getElementName().getLocalName())) {
                                                response.setBeneAmount(element7.getTextContent());
                                            }
                                            if ("TransferReason".equals(element7.getElementName().getLocalName())) {
                                                response.setTransferReason(element7.getTextContent());
                                            }
                                            if ("ResponseDateTimeUTC".equals(element7.getElementName().getLocalName())) {
                                                response.setResponseDateTimeUTC(element7.getTextContent());
                                            }
                                            if ("ResponseCode".equals(element7.getElementName().getLocalName())) {
                                                response.setResponseCode(element7.getTextContent());
                                            }
                                            if ("ResponseText".equals(element7.getElementName().getLocalName())) {
                                                response.setResponseText(element7.getTextContent().replace("\r\n", " ").replace("\n", " "));
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
        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }

        return response;
    }

    public VerifyOrderForPayoutServiceRequest parseInputArgsToRequest(String[] args) {
        VerifyOrderForPayoutServiceRequest request = new VerifyOrderForPayoutServiceRequest();
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

        String dateTimeLocal = params[1];
        String dateTimeUTC = params[2];
        String pin = params[3];
        String beneAmount = "-".equals(params[4].trim()) ? "" : params[4];
        String correspLocID = params[5];

        request.setDateTimeLocal(dateTimeLocal);
        request.setDateTimeUTC(dateTimeUTC);
        request.setPin(pin);
        request.setBeneAmount(beneAmount);
        request.setCorrespLocID(correspLocID);
        request.setRequestType("VerifyOrderForPayout");
        return request;
    }

    public VerifyOrderForPayoutServiceResponse invoke(VerifyOrderForPayoutServiceRequest request) {
        VerifyOrderForPayoutServiceResponse response = new VerifyOrderForPayoutServiceResponse();
        try {
            // Create SOAP Connection
            SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
            SOAPConnection soapConnection = soapConnectionFactory.createConnection();

            // Send SOAP Message to SOAP Server
            SOAPMessage soapResponse = soapConnection.call(createSoapRequest(request), configurations.getSoapEndPointUrl());

            if (isDebug) {
                // Print the SOAP Response
                System.out.println("Response SOAP Message:");
                soapResponse.writeTo(System.out);
                System.out.println();
            }

            // Return parsed Response
            response = parseAndReturnResponse(soapResponse);
            soapConnection.close();

            if ("true".equals(response.getOrderFound())) {
                System.out.println("00|OK");
            }

            if ("false".equals(response.getOrderFound())) {
                System.out.println("99|" + response.getResponseText());
            }

            System.out.println(response);

        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
        return response;
    }
}
