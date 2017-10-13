/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying;

import fx.globalpaying.entities.GetCommissionsRequestEntity;
import fx.globalpaying.interfaces.CountriesStates;
import fx.globalpaying.interfaces.Currencies;
import fx.globalpaying.entities.GetCountriesStatesRequestEntity;
import fx.globalpaying.entities.GetCurrenciesRequestEntity;
import fx.globalpaying.entities.GetEnumerationValuesRequestEntity;
import fx.globalpaying.entities.GetLocsCurrsRatesRequestEntity;
import fx.globalpaying.entities.GetOrderStatusNoticesRequestEntity;
import fx.globalpaying.entities.GetRequirementsRequestEntity;
import fx.globalpaying.entities.GetStatesCitiesRequestEntity;
import fx.globalpaying.interfaces.Commissions;
import fx.globalpaying.interfaces.EnumerationValues;
import fx.globalpaying.interfaces.LocsCurrsRates;
import fx.globalpaying.interfaces.OrderStatusNotices;
import fx.globalpaying.interfaces.Requirements;
import fx.globalpaying.interfaces.StatesCities;

/**
 *
 * @author Carlos Valderrama @ RIA
 */
public class FXGlobalPaying {

    public enum RequestTypeEnum {
        Currencies,
        CountriesStates,
        StatesCities,
        Commissions,
        OrderStatusNotifications,
        EnumerationValues,
        Requirements,
        LocsCurrsRates
    }

    static boolean isDebug = false;
    static String soapEndpointUrl = "http://stagingfxglobalwebsvcnocert.riaenvia.net:9771/FXGlobalPaying.svc/Binding_Basic_NoCert";
    static String soapActionPrefix = "CES.Services.FXGlobal/IRiaAsPayer/";
    static String soapAction = "CES.Services.FXGlobal/IRiaAsPayer/GetCurrencies";

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

        if (args.length > 5 && "-url".equals(args[args.length - 2])) {
            soapEndpointUrl = args[args.length - 1];
        }

        String requestType = args[3].split(";")[0];

        if ("General".equals(requestType)) {
            for (RequestTypeEnum requestTypeEnum : RequestTypeEnum.values()) {
                String requestTypeItem = args[3] = requestTypeEnum.name();
                soapAction = soapActionPrefix + "Get" + requestTypeItem;
                ExecuteWebMethod(RequestTypeEnum.valueOf(requestTypeItem), args);
            }
        } else {
            boolean isValidRequestType = ExistsRequestType(requestType);
            if (!isValidRequestType) {
                // Invalid Request Type, exit Program
                return;
            }
            String requesTypeAction = requestType;
            if ("OrderStatusNotifications".equals(requestType)) {
                requesTypeAction = "OrderStatusNotices";
            }

            soapAction = soapActionPrefix + "Get" + requesTypeAction;
            ExecuteWebMethod(RequestTypeEnum.valueOf(requestType), args);
        }
    }

    public static void ExecuteWebMethod(RequestTypeEnum requestType, String[] args) {
        Object request;

        switch (requestType) {
            case Currencies:
                request = Currencies.parseInputArgsToRequest(args);
                Currencies.callSoapWebService(soapEndpointUrl, soapAction, (GetCurrenciesRequestEntity) request);
                break;
            case CountriesStates:
                request = CountriesStates.parseInputArgsToRequest(args);
                CountriesStates.callSoapWebService(soapEndpointUrl, soapAction, (GetCountriesStatesRequestEntity) request);
                break;
            case StatesCities:
                request = StatesCities.parseInputArgsToRequest(args);
                StatesCities.callSoapWebService(soapEndpointUrl, soapAction, (GetStatesCitiesRequestEntity) request);
                break;
            case Commissions:
                request = Commissions.parseInputArgsToRequest(args);
                Commissions.callSoapWebService(soapEndpointUrl, soapAction, (GetCommissionsRequestEntity) request);
                break;
            case OrderStatusNotifications:
                request = OrderStatusNotices.parseInputArgsToRequest(args);
                OrderStatusNotices.callSoapWebService(soapEndpointUrl, soapAction, (GetOrderStatusNoticesRequestEntity) request);
                break;
            case EnumerationValues:
                request = EnumerationValues.parseInputArgsToRequest(args);
                EnumerationValues.callSoapWebService(soapEndpointUrl, soapAction, (GetEnumerationValuesRequestEntity) request);
                break;
            case Requirements:
                request = Requirements.parseInputArgsToRequest(args);
                Requirements.callSoapWebService(soapEndpointUrl, soapAction, (GetRequirementsRequestEntity) request);
                break;
            case LocsCurrsRates:
                request = LocsCurrsRates.parseInputArgsToRequest(args);
                LocsCurrsRates.callSoapWebService(soapEndpointUrl, soapAction, (GetLocsCurrsRatesRequestEntity) request);
                break;
        }
    }

    private static boolean ExistsRequestType(String requesTypeName) {
        boolean isValidRequestType = false;
        for (RequestTypeEnum requestTypeEnum : RequestTypeEnum.values()) {
            if (requestTypeEnum.name().equalsIgnoreCase(requesTypeName)) {
                isValidRequestType = true;
                break;
            }
        }
        return isValidRequestType;
    }
}
