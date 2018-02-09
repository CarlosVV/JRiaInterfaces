/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying;

import static fx.globalpaying.FXGlobalPaying.soapEndpointUrl;
import fx.globalpaying.entities.GetBankBranchesRequestEntity;
import fx.globalpaying.entities.GetBankInfoRequestEntity;
import fx.globalpaying.entities.GetBanksRequestEntity;
import fx.globalpaying.entities.GetCommissionsRequestEntity;
import fx.globalpaying.entities.GetCountriesStatesRequestEntity;
import fx.globalpaying.entities.GetCurrenciesRequestEntity;
import fx.globalpaying.entities.GetEnumerationValuesRequestEntity;
import fx.globalpaying.entities.GetLocsCurrsRatesRequestEntity;
import fx.globalpaying.entities.GetOrderCommissionRequestEntity;
import fx.globalpaying.entities.GetOrderStatusNoticesRequestEntity;
import fx.globalpaying.entities.GetOrdersValidatedRequestEntity;
import fx.globalpaying.entities.GetRequirementsRequestEntity;
import fx.globalpaying.entities.GetStatesCitiesRequestEntity;
import fx.globalpaying.entities.GetStatesCitiesResponseEntity;
import fx.globalpaying.interfaces.BankBranches;
import fx.globalpaying.interfaces.BankInfo;
import fx.globalpaying.interfaces.Banks;
import fx.globalpaying.interfaces.Commissions;
import fx.globalpaying.interfaces.OrderCommission;
import fx.globalpaying.interfaces.CountriesStates;
import fx.globalpaying.interfaces.Currencies;
import fx.globalpaying.interfaces.EnumerationValues;
import fx.globalpaying.interfaces.LocsCurrsRates;
import fx.globalpaying.interfaces.OrderStatusNotices;
import fx.globalpaying.interfaces.OrdersValidated;
import fx.globalpaying.interfaces.Requirements;
import fx.globalpaying.interfaces.StatesCities;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 *
 * @author cvalderrama
 */
public class ServiceManager {

    public enum RequestTypeEnum {
        Currencies,
        CountriesStates,
        StatesCities,
        Commissions,
        OrderStatusNotifications,
        EnumerationValues,
        Requirements,
        LocsCurrsRates,
        OrderCommission,
        OrdersValidated,
        Banks,
        BankInfo,
        BankBranches
    }

    public static Object ExecuteWebMethod(RequestTypeEnum requestType, String[] args) {
        return ExecuteWebMethod(requestType, args, true);
    }

    public static Object ExecuteWebMethod(RequestTypeEnum requestType, String[] args, boolean generateToStdOut) {
        Object request;
        Object response;

        response = null;

        switch (requestType) {
            case Currencies:
                request = Currencies.parseInputArgsToRequest(args);
                Currencies.callSoapWebService(soapEndpointUrl, (GetCurrenciesRequestEntity) request);
                break;
            case CountriesStates:
                request = CountriesStates.parseInputArgsToRequest(args);
                CountriesStates.callSoapWebService(soapEndpointUrl, (GetCountriesStatesRequestEntity) request, generateToStdOut);
                response = CountriesStates.getStatesList();
                break;
            case StatesCities:
                request = StatesCities.parseInputArgsToRequest(args);
                if ("*".equals(((GetStatesCitiesRequestEntity) request).getCountryCode())) {
                    Object countriesStatesRequest = CountriesStates.parseInputArgsToRequest(args);
                    CountriesStates.callSoapWebService(soapEndpointUrl, (GetCountriesStatesRequestEntity) countriesStatesRequest, false);
                    HashMap<String, String> countries = CountriesStates.getCountriesList();
                    List<Map.Entry<String, String>> sortedCountries = new ArrayList<Map.Entry<String, String>>(countries.entrySet());
                    Collections.sort(sortedCountries, new Comparator<Map.Entry<String, String>>() {
                        @Override
                        public int compare(Map.Entry<String, String> c1, Map.Entry<String, String> c2) {
                            return c1.getKey().compareTo(c2.getKey());
                        }
                    });

                    List<String> results = new ArrayList<String>();
                    for (Map.Entry<String, String> country : sortedCountries) {
                        ((GetStatesCitiesRequestEntity) request).setCountryCode(country.getKey());
                        StatesCities.callSoapWebService(soapEndpointUrl, (GetStatesCitiesRequestEntity) request, false);
                        List<GetStatesCitiesResponseEntity> cities = (List<GetStatesCitiesResponseEntity>) StatesCities.getCitiesList();

                        for (GetStatesCitiesResponseEntity item : cities) {
                            results.add(item.getCtryCode() + "|"
                                    + item.getCtryName() + "|"
                                    + item.getStateCode() + "|"
                                    + item.getStateName() + "|"
                                    + item.getCityName() + "|");

                        }
                    }

                    if (results.isEmpty()) {
                        System.out.println("99|No Results");
                    } else {
                        System.out.println("00|OK");
                    }

                    for (String r : results) {
                        System.out.println(r);
                    }
                } else {
                    StatesCities.callSoapWebService(soapEndpointUrl, (GetStatesCitiesRequestEntity) request, true);
                    response = StatesCities.getCitiesList();
                }
                break;

            case Commissions:
                request = Commissions.parseInputArgsToRequest(args);
                Commissions.callSoapWebService(soapEndpointUrl, (GetCommissionsRequestEntity) request);
                break;
            case OrderStatusNotifications:
                request = OrderStatusNotices.parseInputArgsToRequest(args);
                OrderStatusNotices.callSoapWebService(soapEndpointUrl, (GetOrderStatusNoticesRequestEntity) request);
                break;
            case EnumerationValues:
                request = EnumerationValues.parseInputArgsToRequest(args);
                EnumerationValues.callSoapWebService(soapEndpointUrl, (GetEnumerationValuesRequestEntity) request);
                break;
            case Requirements:
                request = Requirements.parseInputArgsToRequest(args);
                Requirements.callSoapWebService(soapEndpointUrl, (GetRequirementsRequestEntity) request);
                break;
            case LocsCurrsRates:
                request = LocsCurrsRates.parseInputArgsToRequest(args);
                String country = ((GetLocsCurrsRatesRequestEntity) request).getRequests().get(0).getCtryCode();
                String state = ((GetLocsCurrsRatesRequestEntity) request).getRequests().get(0).getStateCode();
                String city = ((GetLocsCurrsRatesRequestEntity) request).getRequests().get(0).getCityName();
                // "[a-zA-Z]+\\.?"  "\\w+\\.?"
                String valid_pattern = "[a-zA-ZñÑáéíóúÁÉÍÓÚ\\-\\s\\.]+";
                String invalid_pattern = "[^a-zA-ZñÑáéíóúÁÉÍÓÚ\\-\\s\\.]+";
                //String pattern = "\\w+\\-\\s\\.+";
                //System.out.println(city);
                if (!city.matches(valid_pattern)) {
                    String searchCityName = city.replaceAll(invalid_pattern, ".");
                    //System.out.println(searchCityName);
                    GetStatesCitiesRequestEntity requestCities = new GetStatesCitiesRequestEntity();
                    requestCities.setCorrespID(((GetLocsCurrsRatesRequestEntity) request).getCorrespID());
                    requestCities.setLayoutVersion(((GetLocsCurrsRatesRequestEntity) request).getLayoutVersion());
                    requestCities.setHeader(((GetLocsCurrsRatesRequestEntity) request).getHeader());
                    requestCities.setCountryCode(country);
                    requestCities.setStateName(state);
                    requestCities.setRequestType("StatesCities");
                    StatesCities.callSoapWebService(soapEndpointUrl, requestCities, false);
                    List<GetStatesCitiesResponseEntity> cities = StatesCities.getCitiesList();

                    String hitCity = "";
                    for (GetStatesCitiesResponseEntity item : cities) {
                        String cityItem = item.getCityName();
                        if (cityItem.matches(searchCityName)) {
                            hitCity = item.getCityName();
                            break;
                        }
                    }
                    //System.out.println(hitCity);
                    city = hitCity;
                }

                ((GetLocsCurrsRatesRequestEntity) request).getRequests().get(0).setCityName(city);
                LocsCurrsRates.callSoapWebService(soapEndpointUrl, (GetLocsCurrsRatesRequestEntity) request);
                break;
            case OrderCommission:
                request = OrderCommission.parseInputArgsToRequest(args);
                OrderCommission.callSoapWebService(soapEndpointUrl, (GetOrderCommissionRequestEntity) request);
                break;
            case OrdersValidated:
                request = OrdersValidated.parseInputArgsToRequest(args);
                OrdersValidated.callSoapWebService(soapEndpointUrl, (GetOrdersValidatedRequestEntity) request, true);
                break;
            case Banks:
                request = Banks.parseInputArgsToRequest(args);
                Banks.callSoapWebService(soapEndpointUrl, (GetBanksRequestEntity) request, true);
                break;
            case BankInfo:
                request = BankInfo.parseInputArgsToRequest(args);
                BankInfo.callSoapWebService(soapEndpointUrl, (GetBankInfoRequestEntity) request, true);
                break;
            case BankBranches:
                request = BankBranches.parseInputArgsToRequest(args);
                BankBranches.callSoapWebService(soapEndpointUrl, (GetBankBranchesRequestEntity) request, true);
                break;
        }
        return response;
    }

    public static boolean ExistsRequestType(String requesTypeName) {
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
