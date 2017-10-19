/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.interfaces;

import fx.globalpaying.entities.GetOrderCommissionRequestEntity;
import fx.globalpaying.entities.HeaderEntity;
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
public class OrderCommission {

    private static final String XML_DOC = "xmlDoc";
    private static final String ROOT = "Root";
    private static final String ORDER_COMMISSION = "GetOrderCommission";
    private static final String SOAP_ACTION = "CES.Services.FXGlobal/IRiaAsPayer/GetOrderCommission";
    private static boolean generateToStdOut = false;
    private static final boolean IS_DEBUG = false;
    private static String orderCommission;

    public static String getOrderCommission() {
        return orderCommission;
    }

    public static GetOrderCommissionRequestEntity parseInputArgsToRequest(String[] args) {
        GetOrderCommissionRequestEntity request = new GetOrderCommissionRequestEntity();
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
            if (requestArray.length >= 11) {
                request.setRequestType("OrderCommission");
                if (!"-".equals(requestArray[1])) {
                    request.setDateDesired(requestArray[1]);
                }

                if (!"-".equals(requestArray[2])) {
                    request.setCountryFrom(requestArray[2]);

                }

                if (!"-".equals(requestArray[3])) {
                    request.setCountryTo(requestArray[3]);

                }
                if (!"-".equals(requestArray[4])) {
                    request.setPayingCorrespID(requestArray[4]);

                }
                if (!"-".equals(requestArray[5])) {
                    request.setPayingCorrespLocID(requestArray[5]);

                }
                if (!"-".equals(requestArray[6])) {
                    request.setPaymentCurrency(requestArray[6]);

                }
                if (!"-".equals(requestArray[7])) {
                    request.setPaymentAmount(requestArray[7]);

                }
                if (!"-".equals(requestArray[8])) {
                    request.setBeneficiaryCurrency(requestArray[8]);

                }
                if (!"-".equals(requestArray[9])) {
                    request.setBeneficiaryAmount(requestArray[9]);

                }
                if (!"-".equals(requestArray[10])) {
                    request.setDeliveryMethod(requestArray[10]);
                }
            }
        }

        return request;
    }

    public static void callSoapWebService(String soapEndpointUrl,
            GetOrderCommissionRequestEntity request) {
        callSoapWebService(soapEndpointUrl, request, true);
    }

    public static void callSoapWebService(String soapEndpointUrl,
            GetOrderCommissionRequestEntity request, boolean generateToStdOutPrint) {
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
            GetOrderCommissionRequestEntity request) throws SOAPException {
        SOAPPart soapPart = soapMessage.getSOAPPart();

        String cesNamespace = "ces";
        String fxGlobalNamespaceURI = "CES.Services.FXGlobal";

        // SOAP Envelope
        SOAPEnvelope envelope = soapPart.getEnvelope();
        envelope.addNamespaceDeclaration(cesNamespace, fxGlobalNamespaceURI);

        // SOAP Body
        SOAPBody soapBody = envelope.getBody();

        // <ces:GetCurrencies>
        SOAPElement getCurrenciesElem = soapBody.addChildElement(ORDER_COMMISSION, cesNamespace);

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

        //  <Request CountryCode="CO" StateName="ANT" ></Request>
        SOAPElement requestElement = root.addChildElement("Request");

        requestElement.addAttribute(new QName("", "DateDesired"), request.getDateDesired());
        requestElement.addAttribute(new QName("", "CountryFrom"), request.getCountryFrom());
        requestElement.addAttribute(new QName("", "CountryTo"), request.getCountryTo());
        requestElement.addAttribute(new QName("", "PayingCorrespID"), request.getPayingCorrespID());
        requestElement.addAttribute(new QName("", "PayingCorrespLocID"), request.getPayingCorrespLocID());
        requestElement.addAttribute(new QName("", "PaymentCurrency"), request.getPaymentCurrency());
        requestElement.addAttribute(new QName("", "PaymentAmount"), request.getPaymentAmount());
        requestElement.addAttribute(new QName("", "BeneficiaryCurrency"), request.getBeneficiaryCurrency());
        requestElement.addAttribute(new QName("", "BeneficiaryAmount"), request.getBeneficiaryAmount());
        requestElement.addAttribute(new QName("", "DeliveryMethod"), request.getDeliveryMethod());
    }

    private static void parseAndReturnResponse(SOAPMessage soapResponse) {
        try {
            orderCommission = "";

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
                                        String CommissionCurrency = "";
                                        String CommissionAmount = "";
                                        orderCommission = "";
                                        while (it7.hasNext()) {
                                            SOAPElement element7 = (SOAPElement) it7.next();
                                            if ("CommissionCurrency".equals(element7.getElementName().getLocalName())) {
                                                CommissionCurrency = element7.getTextContent();
                                            }
                                            if ("CommissionAmount".equals(element7.getElementName().getLocalName())) {
                                                CommissionAmount = element7.getTextContent();
                                            }

                                        }
                                        orderCommission = CommissionCurrency + "|" + CommissionAmount + "|";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (generateToStdOut) {
                System.out.println(orderCommission);
            }

        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
    }

    private static SOAPMessage createSOAPRequest(String soapAction,
            GetOrderCommissionRequestEntity request) throws Exception {
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
