/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.interfaces;

import fx.globalpaying.entities.GetLocsCurrsRatesRequestEntity;
import fx.globalpaying.entities.HeaderEntity;
import fx.globalpaying.entities.LocsCurrRatesRequestElementEntity;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.regex.Pattern;
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
public class LocsCurrsRates {

    private static String WarningMsg = "";
    private static String ErrorMsg = "";
    private static List<String> locsCurrRates = new ArrayList<String>();
    private static final String soapAction = "CES.Services.FXGlobal/IRiaAsPayer/GetLocsCurrsRates";
    private static final boolean isDebug = false;
    private static long t1 = 0;
    private static long t2 = 0;
    private static long t3 = 0;

    public static GetLocsCurrsRatesRequestEntity parseInputArgsToRequest(String[] args) {
        GetLocsCurrsRatesRequestEntity request = new GetLocsCurrsRatesRequestEntity();
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

        String requesType = "";
        String countryCode = "";
        String stateName = "";
        String cityName = "";

        if (args[3] != null) {
            String[] requestArray = args[3].split(";");
            if (requestArray.length >= 3) {
                requesType = requestArray[0];
                String x = args[3];
                x = x.replace(requesType + ";", "");

                String[] y = x.split(Pattern.quote("|"));

                request.setRequests(new ArrayList<LocsCurrRatesRequestElementEntity>());

                for (String z : y) {
                    String[] p = z.split(";");
                    LocsCurrRatesRequestElementEntity w = new LocsCurrRatesRequestElementEntity();
                    countryCode = p[0];
                    stateName = p[1];
                    cityName = p[2];

                    if ("-".equals(stateName)) {
                        stateName = "";
                    }

                    if ("-".equals(cityName)) {
                        cityName = "";
                    }

                    w.setCtryCode(countryCode);
                    w.setStateName(stateName);
                    w.setStateCode(stateName);
                    w.setCityName(cityName);

                    request.getRequests().add(w);
                }
            }
        }

        request.setRequestType(requesType);

        return request;
    }

    public static void callSoapWebService(String soapEndpointUrl,
            GetLocsCurrsRatesRequestEntity request) {
        try {
            // Create SOAP Connection
            SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
            SOAPConnection soapConnection = soapConnectionFactory.createConnection();

            Date d1 = new Date();
            // Send SOAP Message to SOAP Server
            SOAPMessage soapResponse = soapConnection.call(createSOAPRequest(soapAction, request), soapEndpointUrl);
            Date d2 = new Date();

            long diff = d2.getTime() - d1.getTime();
            t1 = diff / 1000;

            if (isDebug) {
                // Print the SOAP Response
                System.out.println("Response SOAP Message:");
                soapResponse.writeTo(System.out);
                System.out.println();
            }

            d1 = new Date();
            // Return parsed Response
            parseAndReturnResponse(soapResponse);
            d2 = new Date();
            diff = d2.getTime() - d1.getTime();
            t2 = diff / 1000;

            d1 = new Date();
            if (WarningMsg.length() == 0 && ErrorMsg.length() == 0 && !locsCurrRates.isEmpty()) {
                System.out.println("00|OK|t1=" + t1 + "|t2=" + t2 + "|");
            }

            if (WarningMsg.length() > 0 || ErrorMsg.length() > 0 || locsCurrRates.isEmpty()) {
                System.out.println("99|" + WarningMsg + ErrorMsg);
            }

            for (String item : locsCurrRates) {
                System.out.println(item);
            }

            d2 = new Date();
            t3 = diff / 1000;
            System.out.println("EOF|t3=" + t3);
            soapConnection.close();
        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
    }

    private static void createSoapEnvelope(SOAPMessage soapMessage,
            GetLocsCurrsRatesRequestEntity request) throws SOAPException {
        SOAPPart soapPart = soapMessage.getSOAPPart();

        String cesNamespace = "ces";
        String fxGlobalNamespaceURI = "CES.Services.FXGlobal";

        // SOAP Envelope
        SOAPEnvelope envelope = soapPart.getEnvelope();
        envelope.addNamespaceDeclaration(cesNamespace, fxGlobalNamespaceURI);

        // SOAP Body
        SOAPBody soapBody = envelope.getBody();

        // <ces:GetCurrencies>
        SOAPElement getCurrenciesElem = soapBody.addChildElement("GetLocsCurrsRates", cesNamespace);

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

        SOAPElement requestsElement = root.addChildElement("Requests");
        for (LocsCurrRatesRequestElementEntity lcr : request.getRequests()) {
            SOAPElement requestsItem = requestsElement.addChildElement("Request");
            requestsItem.addAttribute(new QName("", "CountryCode"), lcr.getCtryCode());
            requestsItem.addAttribute(new QName("", "StateName"), lcr.getStateName());
            requestsItem.addAttribute(new QName("", "CityName"), lcr.getCityName());
        }

    }

    private static void parseAndReturnResponse(SOAPMessage soapResponse) {
        try {
            WarningMsg = "";
            ErrorMsg = "";
            locsCurrRates = new ArrayList<String>();

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
                                    Iterator it6 = element5.getChildElements();
                                    while (it6.hasNext()) {
                                        SOAPElement element7 = (SOAPElement) it6.next();
                                        Iterator it7 = element7.getChildElements();
                                        while (it7.hasNext()) {
                                            SOAPElement element8 = (SOAPElement) it7.next();
                                            String correspNo = element8.getAttribute("CorrespNo");
                                            String correspID = element8.getAttribute("CorrespID");
                                            String correspName = element8.getAttribute("CorrespName");
                                            Iterator it8 = element8.getChildElements();
                                            while (it8.hasNext()) {
                                                SOAPElement element9 = (SOAPElement) it8.next();
                                                Iterator it9 = element9.getChildElements();
                                                while (it9.hasNext()) {
                                                    SOAPElement element10 = (SOAPElement) it9.next();
                                                    Iterator it10 = element10.getChildElements();
                                                    String LocID = "";
                                                    String LocBranchNo = "";
                                                    String LocName = "";
                                                    String LocAddress1 = "";
                                                    String LocAddress2 = "";
                                                    String LocCity = "";
                                                    String LocState = "";
                                                    String LocPostalCode = "";
                                                    String LocCountry = "";
                                                    String LocTelNo = "";
                                                    String LocFaxNo = "";
                                                    String LocEmail = "";
                                                    String Directions = "";
                                                    String Notes = "";
                                                    List<String> curr = new ArrayList<String>();
                                                    while (it10.hasNext()) {
                                                        SOAPElement element11 = (SOAPElement) it10.next();
                                                        Iterator it11 = element11.getChildElements();
                                                        if ("LocID".equals(element11.getElementName().getLocalName())) {
                                                            LocID = element11.getTextContent();
                                                        }
                                                        if ("LocBranchNo".equals(element11.getElementName().getLocalName())) {
                                                            LocBranchNo = element11.getTextContent();
                                                        }
                                                        if ("LocAddress1".equals(element11.getElementName().getLocalName())) {
                                                            LocAddress1 = element11.getTextContent();
                                                        }
                                                        if ("LocCity".equals(element11.getElementName().getLocalName())) {
                                                            LocCity = element11.getTextContent();
                                                        }
                                                        if ("LocAddress2".equals(element11.getElementName().getLocalName())) {
                                                            LocAddress2 = element11.getTextContent();
                                                        }
                                                        if ("LocName".equals(element11.getElementName().getLocalName())) {
                                                            LocName = element11.getTextContent();
                                                        }
                                                        if ("LocState".equals(element11.getElementName().getLocalName())) {
                                                            LocState = element11.getTextContent();
                                                        }
                                                        if ("LocPostalCode".equals(element11.getElementName().getLocalName())) {
                                                            LocPostalCode = element11.getTextContent();
                                                        }
                                                        if ("LocCountry".equals(element11.getElementName().getLocalName())) {
                                                            LocCountry = element11.getTextContent();
                                                        }
                                                        if ("LocTelNo".equals(element11.getElementName().getLocalName())) {
                                                            LocTelNo = element11.getTextContent();
                                                        }
                                                        if ("LocFaxNo".equals(element11.getElementName().getLocalName())) {
                                                            LocFaxNo = element11.getTextContent();
                                                        }
                                                        if ("LocEmail".equals(element11.getElementName().getLocalName())) {
                                                            LocEmail = element11.getTextContent();
                                                        }
                                                        if ("Directions".equals(element11.getElementName().getLocalName())) {
                                                            Directions = element11.getTextContent();
                                                        }
                                                        if ("Notes".equals(element11.getElementName().getLocalName())) {
                                                            Notes = element11.getTextContent();
                                                        }
                                                        if ("Currencies".equals(element11.getElementName().getLocalName())) {
                                                            List<String> deliveryMethods = new ArrayList<String>();
                                                            List<String> rates = new ArrayList<String>();

                                                            while (it11.hasNext()) {
                                                                SOAPElement element12 = (SOAPElement) it11.next();
                                                                String Currency = element12.getAttribute("Currency");
                                                                String OrderMin = element12.getAttribute("OrderMin");
                                                                String OrderMax = element12.getAttribute("OrderMax");
                                                                String DailyMax = element12.getAttribute("DailyMax");
                                                                Iterator it13 = element12.getChildElements();
                                                                deliveryMethods = new ArrayList<String>();
                                                                rates = new ArrayList<String>();
                                                                while (it13.hasNext()) {
                                                                    SOAPElement element13 = (SOAPElement) it13.next();
                                                                    Iterator it14 = element13.getChildElements();
                                                                    if ("DeliveryMethods".equals(element13.getElementName().getLocalName())) {
                                                                        while (it14.hasNext()) {
                                                                            SOAPElement element14 = (SOAPElement) it14.next();
                                                                            String Method = element14.getAttribute("Method");
                                                                            String MethodDesc = element14.getAttribute("MethodDesc");
                                                                            deliveryMethods.add(Method + "|" + MethodDesc + "|");
                                                                        }
                                                                    }

                                                                    if ("Rates".equals(element13.getElementName().getLocalName())) {
                                                                        while (it14.hasNext()) {
                                                                            SOAPElement element15 = (SOAPElement) it14.next();
                                                                            String CountryTo = element15.getAttribute("CountryTo");
                                                                            String CurrFrom = element15.getAttribute("CurrFrom");
                                                                            String TodaysRate = element15.getAttribute("TodaysRate");
                                                                            rates.add(CountryTo + "|" + CurrFrom + "|" + TodaysRate + "|");
                                                                        }
                                                                    }
                                                                }
                                                                for (String r : rates) {
                                                                    for (String d : deliveryMethods) {
                                                                        curr.add(
                                                                                Currency + "|"
                                                                                + OrderMin + "|"
                                                                                + OrderMax + "|" + DailyMax + "|" + r + "|" + d + "|");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    for (String c : curr) {
                                                        locsCurrRates.add(correspNo + "|" + correspID + "|" + correspName + "|"
                                                                + LocID + "|"
                                                                + LocBranchNo + "|"
                                                                + LocName + "|"
                                                                + LocAddress1 + "|"
                                                                + LocAddress2 + "|"
                                                                + LocCity + "|"
                                                                + LocState + "|"
                                                                + LocPostalCode + "|"
                                                                + LocCountry + "|"
                                                                + LocTelNo + "|"
                                                                + LocFaxNo + "|"
                                                                + LocEmail + "|"
                                                                + Directions + "|"
                                                                + Notes + "|" + c + "|");

                                                    }
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

        } catch (Exception e) {
            System.err.println("99|Error occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
    }

    private static SOAPMessage createSOAPRequest(String soapAction,
            GetLocsCurrsRatesRequestEntity request) throws Exception {
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
