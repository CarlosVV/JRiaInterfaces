/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalsending.interfaces;

/**
 *
 * @author cvalderrama
 */
public class CancellationRequests {
    public static final String DEFAULT_SOAP_ENDPOINT_URL = "http://stagingfxglobalwebsvcnocert.riaenvia.net:9771/FXGlobalPaying.svc/Binding_Basic_NoCert";
    private String soap_endpoint_url;
    public CancellationRequests(String soap_endpoint_url){
        this.setSoapEndPointUrl(soap_endpoint_url);
    }
    public String getSoapEndPointUrl(){
        if(soap_endpoint_url == null){
            return DEFAULT_SOAP_ENDPOINT_URL;
        }
        return soap_endpoint_url;
    }
    
    private void setSoapEndPointUrl(String soap_endpoint_url){
        this.soap_endpoint_url = soap_endpoint_url;       
    }
}
