/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.interfaces;

import fx.globalpaying.entities.GetOrdersValidatedRequestEntity;
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
public class OrdersValidated {

    private static final String soapAction = "CES.Services.FXGlobal/IRiaAsPayer/GetOrdersValidated";
    private static boolean generateToStdOut = true;
    private static final boolean isDebug = false;

    public static GetOrdersValidatedRequestEntity parseInputArgsToRequest(String[] args) {
        GetOrdersValidatedRequestEntity request = new GetOrdersValidatedRequestEntity();
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
            String[] requestArray = args[3].split(";");
            if (requestArray.length >= 3) {
                request.setRequestType("ValidateOrders");
                if (!"-".equals(requestArray[1])) {
                    request.setOrderNo(requestArray[1]);
                }
                if (!"-".equals(requestArray[2])) {
                    request.setPayingCorrespSeqID(requestArray[2]);
                }
                if (!"-".equals(requestArray[3])) {
                    request.setSalesDate(requestArray[3]);
                }
                if (!"-".equals(requestArray[4])) {
                    request.setSalesTime(requestArray[4]);
                }
                if (!"-".equals(requestArray[5])) {
                    request.setCountryFrom(requestArray[5]);
                }
                if (!"-".equals(requestArray[6])) {
                    request.setCountryTo(requestArray[6]);
                }
                if (!"-".equals(requestArray[7])) {
                    request.setSendingCorrespBranchNo(requestArray[7]);
                }
                if (!"-".equals(requestArray[8])) {
                    request.setPayingCorrespID(requestArray[8]);
                }
                if (!"-".equals(requestArray[9])) {
                    request.setPayingCorrespLocID(requestArray[9]);
                }
                if (!"-".equals(requestArray[10])) {
                    request.setBeneQuestion(requestArray[10]);
                }
                if (!"-".equals(requestArray[11])) {
                    request.setBeneAnswer(requestArray[11]);
                }
                if (!"-".equals(requestArray[12])) {
                    request.setPmtInstruction(requestArray[12]);
                }
                if (!"-".equals(requestArray[13])) {
                    request.setBeneficiaryCurrency(requestArray[13]);
                }
                if (!"-".equals(requestArray[14])) {
                    request.setBeneficiaryAmount(requestArray[14]);
                }
                if (!"-".equals(requestArray[15])) {
                    request.setDeliveryMethod(requestArray[15]);
                }
                if (!"-".equals(requestArray[16])) {
                    request.setPaymentCurrency(requestArray[16]);
                }
                if (!"-".equals(requestArray[17])) {
                    request.setPaymentAmount(requestArray[17]);
                }
                if (!"-".equals(requestArray[18])) {
                    request.setCommissionCurrency(requestArray[18]);
                }
                if (!"-".equals(requestArray[19])) {
                    request.setCommissionAmount(requestArray[19]);
                }
                if (!"-".equals(requestArray[20])) {
                    request.setCustChargeCurrency(requestArray[20]);
                }
                if (!"-".equals(requestArray[21])) {
                    request.setCustChargeAmount(requestArray[21]);
                }
                if (!"-".equals(requestArray[22])) {
                    request.setBenID(requestArray[22]);
                }
                if (!"-".equals(requestArray[23])) {
                    request.setBeneFirstName(requestArray[23]);
                }
                if (!"-".equals(requestArray[24])) {
                    request.setBeneLastName(requestArray[24]);
                }
                if (!"-".equals(requestArray[25])) {
                    request.setBeneLastName2(requestArray[25]);
                }
                if (!"-".equals(requestArray[26])) {
                    request.setBeneAddress(requestArray[26]);
                }
                if (!"-".equals(requestArray[27])) {
                    request.setBeneCity(requestArray[27]);
                }
                if (!"-".equals(requestArray[28])) {
                    request.setBeneState(requestArray[28]);
                }
                if (!"-".equals(requestArray[29])) {
                    request.setBeneZipCode(requestArray[29]);
                }
                if (!"-".equals(requestArray[30])) {
                    request.setBeneCountry(requestArray[30]);
                }
                if (!"-".equals(requestArray[31])) {
                    request.setBenePhoneNo(requestArray[31]);
                }
                if (!"-".equals(requestArray[32])) {
                    request.setBeneMessage(requestArray[32]);
                }
                if (!"-".equals(requestArray[33])) {
                    request.setCustID(requestArray[33]);
                }
                if (!"-".equals(requestArray[34])) {
                    request.setCustFirstName(requestArray[34]);
                }
                if (!"-".equals(requestArray[35])) {
                    request.setCustLastName(requestArray[35]);
                }
                if (!"-".equals(requestArray[36])) {
                    request.setCustLastName2(requestArray[36]);
                }
                if (!"-".equals(requestArray[37])) {
                    request.setCustAddress(requestArray[37]);
                }
                if (!"-".equals(requestArray[38])) {
                    request.setCustCity(requestArray[38]);
                }
                if (!"-".equals(requestArray[39])) {
                    request.setCustState(requestArray[39]);
                }
                if (!"-".equals(requestArray[40])) {
                    request.setCustZipCode(requestArray[40]);
                }
                if (!"-".equals(requestArray[41])) {
                    request.setCustCountry(requestArray[41]);
                }
                if (!"-".equals(requestArray[42])) {
                    request.setCustPhoneNo(requestArray[42]);
                }
                if (!"-".equals(requestArray[43])) {
                    request.setCustID1Type(requestArray[43]);
                }
                if (!"-".equals(requestArray[44])) {
                    request.setCustID1No(requestArray[44]);
                }
                if (!"-".equals(requestArray[45])) {
                    request.setCustID1IssuedBy(requestArray[45]);
                }
                if (!"-".equals(requestArray[46])) {
                    request.setCustID1ExpDate(requestArray[46]);
                }
                if (!"-".equals(requestArray[47])) {
                    request.setCustID2Type(requestArray[47]);
                }
                if (!"-".equals(requestArray[48])) {
                    request.setCustID2No(requestArray[48]);
                }
                if (!"-".equals(requestArray[49])) {
                    request.setCustID2IssuedBy(requestArray[49]);
                }
                if (!"-".equals(requestArray[50])) {
                    request.setCustID2ExpDate(requestArray[50]);
                }
                if (!"-".equals(requestArray[51])) {
                    request.setCustTaxID(requestArray[51]);
                }
                if (!"-".equals(requestArray[52])) {
                    request.setCustTaxCountry(requestArray[52]);
                }
                if (!"-".equals(requestArray[53])) {
                    request.setCustCountryOfBirth(requestArray[53]);
                }
                if (!"-".equals(requestArray[54])) {
                    request.setCustNationality(requestArray[54]);
                }
                if (!"-".equals(requestArray[55])) {
                    request.setCustDateBirth(requestArray[55]);
                }
                if (!"-".equals(requestArray[56])) {
                    request.setCustOccupation(requestArray[56]);
                }
                if (!"-".equals(requestArray[57])) {
                    request.setCustSourceFunds(requestArray[57]);
                }
                if (!"-".equals(requestArray[58])) {
                    request.setCustBeneRelationship(requestArray[58]);
                }
                if (!"-".equals(requestArray[59])) {
                    request.setTransferReason(requestArray[59]);
                }
                if (!"-".equals(requestArray[60])) {
                    request.setProviderID(requestArray[60]);
                }
                if (!"-".equals(requestArray[61])) {
                    request.setBankID(requestArray[61]);
                }
                if (!"-".equals(requestArray[62])) {
                    request.setBankBranchName(requestArray[62]);
                }
                if (!"-".equals(requestArray[63])) {
                    request.setBankBranchNo(requestArray[63]);
                }
                if (!"-".equals(requestArray[64])) {
                    request.setBankBranchCity(requestArray[64]);
                }
                if (!"-".equals(requestArray[65])) {
                    request.setBankAccountCountry(requestArray[65]);
                }
                if (!"-".equals(requestArray[66])) {
                    request.setBankAccountType(requestArray[66]);
                }
                if (!"-".equals(requestArray[67])) {
                    request.setBankAccountNo(requestArray[67]);
                }
                if (!"-".equals(requestArray[68])) {
                    request.setValuetype(requestArray[68]);
                }
                if (!"-".equals(requestArray[69])) {
                    request.setBankRoutingCode(requestArray[69]);
                }
                if (!"-".equals(requestArray[70])) {
                    request.setBankRoutingType(requestArray[70]);
                }
                if (!"-".equals(requestArray[71])) {
                    request.setBIC_SWIFT(requestArray[71]);
                }
                if (!"-".equals(requestArray[72])) {
                    request.setUnitaryBankAccountNo(requestArray[72]);
                }
                if (!"-".equals(requestArray[73])) {
                    request.setUnitaryType(requestArray[73]);
                }
                if (!"-".equals(requestArray[74])) {
                    request.setBeneIDNo(requestArray[74]);
                }
                if (!"-".equals(requestArray[75])) {
                    request.setBeneIDType(requestArray[75]);
                }
                if (!"-".equals(requestArray[76])) {
                    request.setBeneTaxID(requestArray[76]);
                }

            }
        }

        return request;
    }

    public static void callSoapWebService(String soapEndpointUrl,
            GetOrdersValidatedRequestEntity request) {
        callSoapWebService(soapEndpointUrl, request, true);
    }

    public static void callSoapWebService(String soapEndpointUrl,
            GetOrdersValidatedRequestEntity request, boolean generateToStdOutPrint) {
        try {
            generateToStdOut = generateToStdOutPrint;
            // Create SOAP Connection
            SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
            SOAPConnection soapConnection = soapConnectionFactory.createConnection();

            // Send SOAP Message to SOAP Server
            SOAPMessage soapResponse = soapConnection.call(createSOAPRequest(soapAction, request), soapEndpointUrl);

            // Return parsed Response
            parseAndReturnResponse(soapResponse);

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
    }

    private static void createSoapEnvelope(SOAPMessage soapMessage,
            GetOrdersValidatedRequestEntity request) throws SOAPException {
        SOAPPart soapPart = soapMessage.getSOAPPart();

        String cesNamespace = "ces";
        String fxGlobalNamespaceURI = "CES.Services.FXGlobal";

        // SOAP Envelope
        SOAPEnvelope envelope = soapPart.getEnvelope();
        envelope.addNamespaceDeclaration(cesNamespace, fxGlobalNamespaceURI);

        // SOAP Body
        SOAPBody soapBody = envelope.getBody();

        // <ces:GetCurrencies>
        SOAPElement getCurrenciesElem = soapBody.addChildElement("GetOrdersValidated", cesNamespace);

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

        // <RequestType>StatesCities</RequestType>
        SOAPElement requestType = root.addChildElement("RequestType");
        requestType.addTextNode(request.getRequestType());

        SOAPElement orders = root.addChildElement("Orders");
        SOAPElement order = orders.addChildElement("Order");

        SOAPElement orderNo = order.addChildElement("OrderNo");
        if (request.getOrderNo() != null) {
            orderNo.addTextNode(request.getOrderNo());
        }
        SOAPElement payingCorrespSeqID = order.addChildElement("PayingCorrespSeqID");
        if (request.getPayingCorrespSeqID() != null) {
            payingCorrespSeqID.addTextNode(request.getPayingCorrespSeqID());
        }
        SOAPElement salesDate = order.addChildElement("SalesDate");
        if (request.getSalesDate() != null) {
            salesDate.addTextNode(request.getSalesDate());
        }
        SOAPElement salesTime = order.addChildElement("SalesTime");
        if (request.getSalesTime() != null) {
            salesTime.addTextNode(request.getSalesTime());
        }
        SOAPElement countryFrom = order.addChildElement("CountryFrom");
        if (request.getCountryFrom() != null) {
            countryFrom.addTextNode(request.getCountryFrom());
        }
        SOAPElement countryTo = order.addChildElement("CountryTo");
        if (request.getCountryTo() != null) {
            countryTo.addTextNode(request.getCountryTo());
        }
        SOAPElement sendingCorrespBranchNo = order.addChildElement("SendingCorrespBranchNo");
        if (request.getSendingCorrespBranchNo() != null) {
            sendingCorrespBranchNo.addTextNode(request.getSendingCorrespBranchNo());
        }
        SOAPElement payingCorrespID = order.addChildElement("PayingCorrespID");
        if (request.getPayingCorrespID() != null) {
            payingCorrespID.addTextNode(request.getPayingCorrespID());
        }
        SOAPElement payingCorrespLocID = order.addChildElement("PayingCorrespLocID");
        if (request.getPayingCorrespLocID() != null) {
            payingCorrespLocID.addTextNode(request.getPayingCorrespLocID());
        }
        SOAPElement beneQuestion = order.addChildElement("BeneQuestion");
        if (request.getBeneQuestion() != null) {
            beneQuestion.addTextNode(request.getBeneQuestion());
        }
        SOAPElement beneAnswer = order.addChildElement("BeneAnswer");
        if (request.getBeneAnswer() != null) {
            beneAnswer.addTextNode(request.getBeneAnswer());
        }
        SOAPElement pmtInstruction = order.addChildElement("PmtInstruction");
        if (request.getPmtInstruction() != null) {
            pmtInstruction.addTextNode(request.getPmtInstruction());
        }
        SOAPElement beneficiaryCurrency = order.addChildElement("BeneficiaryCurrency");
        if (request.getBeneficiaryCurrency() != null) {
            beneficiaryCurrency.addTextNode(request.getBeneficiaryCurrency());
        }
        SOAPElement beneficiaryAmount = order.addChildElement("BeneficiaryAmount");
        if (request.getBeneficiaryAmount() != null) {
            beneficiaryAmount.addTextNode(request.getBeneficiaryAmount());
        }
        SOAPElement deliveryMethod = order.addChildElement("DeliveryMethod");
        if (request.getDeliveryMethod() != null) {
            deliveryMethod.addTextNode(request.getDeliveryMethod());
        }
        SOAPElement paymentCurrency = order.addChildElement("PaymentCurrency");
        if (request.getPaymentCurrency() != null) {
            paymentCurrency.addTextNode(request.getPaymentCurrency());
        }
        SOAPElement paymentAmount = order.addChildElement("PaymentAmount");
        if (request.getPaymentAmount() != null) {
            paymentAmount.addTextNode(request.getPaymentAmount());
        }
        SOAPElement commissionCurrency = order.addChildElement("CommissionCurrency");
        if (request.getCommissionCurrency() != null) {
            commissionCurrency.addTextNode(request.getCommissionCurrency());
        }
        SOAPElement commissionAmount = order.addChildElement("CommissionAmount");
        if (request.getCommissionAmount() != null) {
            commissionAmount.addTextNode(request.getCommissionAmount());
        }
        SOAPElement custChargeCurrency = order.addChildElement("CustChargeCurrency");
        if (request.getCustChargeCurrency() != null) {
            custChargeCurrency.addTextNode(request.getCustChargeCurrency());
        }
        SOAPElement custChargeAmount = order.addChildElement("CustChargeAmount");
        if (request.getCustChargeAmount() != null) {
            custChargeAmount.addTextNode(request.getCustChargeAmount());
        }
        SOAPElement benID = order.addChildElement("BenID");
        if (request.getBenID() != null) {
            benID.addTextNode(request.getBenID());
        }
        SOAPElement beneFirstName = order.addChildElement("BeneFirstName");
        if (request.getBeneFirstName() != null) {
            beneFirstName.addTextNode(request.getBeneFirstName());
        }
        SOAPElement beneLastName = order.addChildElement("BeneLastName");
        if (request.getBeneLastName() != null) {
            beneLastName.addTextNode(request.getBeneLastName());
        }
        SOAPElement beneLastName2 = order.addChildElement("BeneLastName2");
        if (request.getBeneLastName2() != null) {
            beneLastName2.addTextNode(request.getBeneLastName2());
        }
        SOAPElement beneAddress = order.addChildElement("BeneAddress");
        if (request.getBeneAddress() != null) {
            beneAddress.addTextNode(request.getBeneAddress());
        }
        SOAPElement beneCity = order.addChildElement("BeneCity");
        if (request.getBeneCity() != null) {
            beneCity.addTextNode(request.getBeneCity());
        }
        SOAPElement beneState = order.addChildElement("BeneState");
        if (request.getBeneState() != null) {
            beneState.addTextNode(request.getBeneState());
        }
        SOAPElement beneZipCode = order.addChildElement("BeneZipCode");
        if (request.getBeneZipCode() != null) {
            beneZipCode.addTextNode(request.getBeneZipCode());
        }
        SOAPElement beneCountry = order.addChildElement("BeneCountry");
        if (request.getBeneCountry() != null) {
            beneCountry.addTextNode(request.getBeneCountry());
        }
        SOAPElement benePhoneNo = order.addChildElement("BenePhoneNo");
        if (request.getBenePhoneNo() != null) {
            benePhoneNo.addTextNode(request.getBenePhoneNo());
        }
        SOAPElement beneMessage = order.addChildElement("BeneMessage");
        if (request.getBeneMessage() != null) {
            beneMessage.addTextNode(request.getBeneMessage());
        }
        SOAPElement custID = order.addChildElement("CustID");
        if (request.getCustID() != null) {
            custID.addTextNode(request.getCustID());
        }
        SOAPElement custFirstName = order.addChildElement("CustFirstName");
        if (request.getCustFirstName() != null) {
            custFirstName.addTextNode(request.getCustFirstName());
        }
        SOAPElement custLastName = order.addChildElement("CustLastName");
        if (request.getCustLastName() != null) {
            custLastName.addTextNode(request.getCustLastName());
        }
        SOAPElement custLastName2 = order.addChildElement("CustLastName2");
        if (request.getCustLastName2() != null) {
            custLastName2.addTextNode(request.getCustLastName2());
        }
        SOAPElement custAddress = order.addChildElement("CustAddress");
        if (request.getCustAddress() != null) {
            custAddress.addTextNode(request.getCustAddress());
        }
        SOAPElement custCity = order.addChildElement("CustCity");
        if (request.getCustCity() != null) {
            custCity.addTextNode(request.getCustCity());
        }
        SOAPElement custState = order.addChildElement("CustState");
        if (request.getCustState() != null) {
            custState.addTextNode(request.getCustState());
        }
        SOAPElement custZipCode = order.addChildElement("CustZipCode");
        if (request.getCustZipCode() != null) {
            custZipCode.addTextNode(request.getCustZipCode());
        }
        SOAPElement custCountry = order.addChildElement("CustCountry");
        if (request.getCustCountry() != null) {
            custCountry.addTextNode(request.getCustCountry());
        }
        SOAPElement custPhoneNo = order.addChildElement("CustPhoneNo");
        if (request.getCustPhoneNo() != null) {
            custPhoneNo.addTextNode(request.getCustPhoneNo());
        }
        SOAPElement custID1Type = order.addChildElement("CustID1Type");
        if (request.getCustID1Type() != null) {
            custID1Type.addTextNode(request.getCustID1Type());
        }
        SOAPElement custID1No = order.addChildElement("CustID1No");
        if (request.getCustID1No() != null) {
            custID1No.addTextNode(request.getCustID1No());
        }
        SOAPElement custID1IssuedBy = order.addChildElement("CustID1IssuedBy");
        if (request.getCustID1IssuedBy() != null) {
            custID1IssuedBy.addTextNode(request.getCustID1IssuedBy());
        }
        SOAPElement custID1ExpDate = order.addChildElement("CustID1ExpDate");
        if (request.getCustID1ExpDate() != null) {
            custID1ExpDate.addTextNode(request.getCustID1ExpDate());
        }
        SOAPElement custID2Type = order.addChildElement("CustID2Type");
        if (request.getCustID2Type() != null) {
            custID2Type.addTextNode(request.getCustID2Type());
        }
        SOAPElement custID2No = order.addChildElement("CustID2No");
        if (request.getCustID2No() != null) {
            custID2No.addTextNode(request.getCustID2No());
        }
        SOAPElement custID2IssuedBy = order.addChildElement("CustID2IssuedBy");
        if (request.getCustID2IssuedBy() != null) {
            custID2IssuedBy.addTextNode(request.getCustID2IssuedBy());
        }
        SOAPElement custID2ExpDate = order.addChildElement("CustID2ExpDate");
        if (request.getCustID2ExpDate() != null) {
            custID2ExpDate.addTextNode(request.getCustID2ExpDate());
        }
        SOAPElement custTaxID = order.addChildElement("CustTaxID");
        if (request.getCustTaxID() != null) {
            custTaxID.addTextNode(request.getCustTaxID());
        }
        SOAPElement custTaxCountry = order.addChildElement("CustTaxCountry");
        if (request.getCustTaxCountry() != null) {
            custTaxCountry.addTextNode(request.getCustTaxCountry());
        }
        SOAPElement custCountryOfBirth = order.addChildElement("CustCountryOfBirth");
        if (request.getCustCountryOfBirth() != null) {
            custCountryOfBirth.addTextNode(request.getCustCountryOfBirth());
        }
        SOAPElement custNationality = order.addChildElement("CustNationality");
        if (request.getCustNationality() != null) {
            custNationality.addTextNode(request.getCustNationality());
        }
        SOAPElement custDateBirth = order.addChildElement("CustDateBirth");
        if (request.getCustDateBirth() != null) {
            custDateBirth.addTextNode(request.getCustDateBirth());
        }
        SOAPElement custOccupation = order.addChildElement("CustOccupation");
        if (request.getCustOccupation() != null) {
            custOccupation.addTextNode(request.getCustOccupation());
        }
        SOAPElement custSourceFunds = order.addChildElement("CustSourceFunds");
        if (request.getCustSourceFunds() != null) {
            custSourceFunds.addTextNode(request.getCustSourceFunds());
        }
        SOAPElement custBeneRelationship = order.addChildElement("CustBeneRelationship");
        if (request.getCustBeneRelationship() != null) {
            custBeneRelationship.addTextNode(request.getCustBeneRelationship());
        }

        SOAPElement transferReason = order.addChildElement("TransferReason");
        if (request.getTransferReason() != null) {
            transferReason.addTextNode(request.getTransferReason());
        }
        SOAPElement providerID = order.addChildElement("ProviderID");
        if (request.getProviderID() != null) {
            providerID.addTextNode(request.getProviderID());
        }
        SOAPElement bankID = order.addChildElement("BankID");
        if (request.getBankID() != null) {
            bankID.addTextNode(request.getBankID());
        }
        SOAPElement bankBranchName = order.addChildElement("BankBranchName");
        if (request.getBankBranchName() != null) {
            bankBranchName.addTextNode(request.getBankBranchName());
        }
        SOAPElement bankBranchNo = order.addChildElement("BankBranchNo");
        if (request.getBankBranchNo() != null) {
            bankBranchNo.addTextNode(request.getBankBranchNo());
        }
        SOAPElement bankBranchCity = order.addChildElement("BankBranchCity");
        if (request.getBankBranchCity() != null) {
            bankBranchCity.addTextNode(request.getBankBranchCity());
        }
        SOAPElement bankAccountCountry = order.addChildElement("BankAccountCountry");
        if (request.getBankAccountCountry() != null) {
            bankAccountCountry.addTextNode(request.getBankAccountCountry());
        }

        if (request.getBankAccountType() != null) {
            SOAPElement bankAccountType = order.addChildElement("BankAccountType");
            bankAccountType.addTextNode(request.getBankAccountType());
        }
        SOAPElement bankAccountNo = order.addChildElement("BankAccountNo");
        if (request.getBankAccountNo() != null) {
            bankAccountNo.addTextNode(request.getBankAccountNo());
        }

        if (request.getValuetype() != null) {
            SOAPElement valuetype = order.addChildElement("Valuetype");
            valuetype.addTextNode(request.getValuetype());
        }

        if (request.getBankRoutingCode() != null) {
            SOAPElement bankRoutingCode = order.addChildElement("BankRoutingCode");
            bankRoutingCode.addTextNode(request.getBankRoutingCode());
        }

        if (request.getBankRoutingType() != null) {
            SOAPElement bankRoutingType = order.addChildElement("BankRoutingType");
            bankRoutingType.addTextNode(request.getBankRoutingType());
        }
        SOAPElement bIC_SWIFT = order.addChildElement("BIC_SWIFT");
        if (request.getBIC_SWIFT() != null) {
            bIC_SWIFT.addTextNode(request.getBIC_SWIFT());
        }

        if (request.getUnitaryBankAccountNo() != null) {
            SOAPElement unitaryBankAccountNo = order.addChildElement("UnitaryBankAccountNo");
            unitaryBankAccountNo.addTextNode(request.getUnitaryBankAccountNo());
        }

        if (request.getUnitaryType() != null) {
            SOAPElement unitaryType = order.addChildElement("UnitaryType");
            unitaryType.addTextNode(request.getUnitaryType());
        }
        SOAPElement beneIDNo = order.addChildElement("BeneIDNo");
        if (request.getBeneIDNo() != null) {
            beneIDNo.addTextNode(request.getBeneIDNo());
        }
        SOAPElement beneIDType = order.addChildElement("BeneIDType");
        if (request.getBeneIDType() != null) {
            beneIDType.addTextNode(request.getBeneIDType());
        }
        SOAPElement beneTaxID = order.addChildElement("BeneTaxID");
        if (request.getBeneTaxID() != null) {
            beneTaxID.addTextNode(request.getBeneTaxID());
        }
    }

    private static void parseAndReturnResponse(SOAPMessage soapResponse) {
        try {
            String passed = "";
            String WarningMsg = "";
            String ErrorMsg = "";
            List<String> ValidationErrors = new ArrayList<String>();
            List<String> RequiredFields = new ArrayList<String>();
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
                                        SOAPElement element6 = (SOAPElement) it6.next();
                                        if ("Passed".equals(element6.getElementName().getLocalName())) {
                                            passed = element6.getTextContent();
                                        } else if ("ValidationErrors".equals(element6.getElementName().getLocalName())) {
                                            Iterator it7 = element6.getChildElements();
                                            while (it7.hasNext()) {
                                                SOAPElement element7 = (SOAPElement) it7.next();
                                                String NotificationDesc = element7.getAttribute("NotificationDesc").trim();
                                                String NotificationCode = element7.getAttribute("NotificationCode").trim();
                                                ValidationErrors.add(NotificationDesc + "|" + NotificationCode + "|");
                                            }
                                        } else if ("RequiredFields".equals(element6.getElementName().getLocalName())) {
                                            Iterator it7 = element6.getChildElements();
                                            while (it7.hasNext()) {
                                                SOAPElement element7 = (SOAPElement) it7.next();
                                                String maxLength = element7.getAttribute("maxLength").trim();
                                                String minLength = element7.getAttribute("minLength").trim();
                                                String fieldName = element7.getAttribute("fieldName").trim();
                                                RequiredFields.add(maxLength + "|" + minLength + "|" + fieldName + "|");
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

            if (generateToStdOut) {
                if (WarningMsg.length() == 0 && ErrorMsg.length() == 0 && !ValidationErrors.isEmpty()) {
                    System.out.println("00|OK");
                }

                if (WarningMsg.length() > 0 || ErrorMsg.length() > 0 || ValidationErrors.isEmpty()) {
                    System.out.println("99|" + WarningMsg + ErrorMsg);
                }
                if (!passed.isEmpty()) {
                    System.out.println("Passed|" + passed + "|");
                }
                for (String item : ValidationErrors) {
                    System.out.println(item);
                }
                for (String item : RequiredFields) {
                    System.out.println(item);
                }
            }

        } catch (Exception e) {
            System.err.println("\nError occurred while sending SOAP Request to Server!\nMake sure you have the correct endpoint URL and SOAPAction!\n");
            e.printStackTrace();
        }
    }

    private static SOAPMessage createSOAPRequest(String soapAction,
            GetOrdersValidatedRequestEntity request) throws Exception {
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
