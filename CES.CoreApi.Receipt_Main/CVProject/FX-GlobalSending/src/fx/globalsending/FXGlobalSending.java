/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalsending;

import fx.globalsending.entities.InputPayersCustSvcMsgsRequestEntity;
import fx.globalsending.entities.InputPayersCustSvcMsgsResponseEntity;
import fx.globalsending.entities.VerifyOrderForPayoutServiceRequest;
import fx.globalsending.entities.VerifyOrderForPayoutServiceResponse;
import fx.globalsending.interfaces.Configurations;
import fx.globalsending.interfaces.InputPayersCustSvcMsgsService;
import fx.globalsending.interfaces.VerifyOrderForPayoutService;

/**
 *
 * @author cvalderrama
 */
public class FXGlobalSending {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        if (args.length < 4 || (args.length > 3 && (args[2] == null || args[2].split(";").length < 9))) {
            System.out.println("Error: argumentos inválidos o faltantes");
            return;
        }

        String soapEndpointUrl = null;
        if (args.length > 5 && "-url".equals(args[args.length - 2])) {
            soapEndpointUrl = args[args.length - 1];
        }
        
        Configurations config = new Configurations(soapEndpointUrl);
        
        String requestType = args[3].split(";")[0];
        
        if("VerifyOrderForPayout".equals(requestType)){
            VerifyOrderForPayoutService service = new VerifyOrderForPayoutService(config);
            VerifyOrderForPayoutServiceRequest request = service.parseInputArgsToRequest(args);
            VerifyOrderForPayoutServiceResponse response = service.invoke(request);
        }
        
        if("InputPayersCustSvcMsgs".equals(requestType)){
            InputPayersCustSvcMsgsService service = new InputPayersCustSvcMsgsService(config);
            InputPayersCustSvcMsgsRequestEntity request = service.parseInputArgsToRequest(args);
            InputPayersCustSvcMsgsResponseEntity response = service.invoke(request);
        }
    }
    
}
