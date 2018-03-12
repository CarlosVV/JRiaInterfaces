/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalsending;

import fx.globalsending.entities.GetCancellationRequestEntity;
import fx.globalsending.entities.GetCancellationResponseEntity;
import fx.globalsending.entities.GetSendersCustSvcMsgsRequestEntity;
import fx.globalsending.entities.GetSendersCustSvcMsgsResponseEntity;
import fx.globalsending.entities.InputCancelRequestResponsesRequestEntity;
import fx.globalsending.entities.InputCancelRequestResponsesResponseEntity;
import fx.globalsending.entities.InputPayersCustSvcMsgsRequestEntity;
import fx.globalsending.entities.InputPayersCustSvcMsgsResponseEntity;
import fx.globalsending.entities.VerifyOrderForPayoutServiceRequest;
import fx.globalsending.entities.VerifyOrderForPayoutServiceResponse;
import fx.globalsending.interfaces.CancellationRequestsService;
import fx.globalsending.interfaces.Configurations;
import fx.globalsending.interfaces.InputCancelRequestResponsesService;
import fx.globalsending.interfaces.InputPayersCustSvcMsgsService;
import fx.globalsending.interfaces.SendersCustSvcMsgsService;
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

        if ("VerifyOrderForPayout".equals(requestType)) {
            VerifyOrderForPayoutService service = new VerifyOrderForPayoutService(config);
            VerifyOrderForPayoutServiceRequest request = service.parseInputArgsToRequest(args);
            VerifyOrderForPayoutServiceResponse response = service.invoke(request);
        }

        if ("InputPayersCustSvcMsgs".equals(requestType)) {
            InputPayersCustSvcMsgsService service = new InputPayersCustSvcMsgsService(config);
            InputPayersCustSvcMsgsRequestEntity request = service.parseInputArgsToRequest(args);
            InputPayersCustSvcMsgsResponseEntity response = service.invoke(request);
        }

        if ("InputCancelRequestResponses".equals(requestType)) {
            InputCancelRequestResponsesService service = new InputCancelRequestResponsesService(config);
            InputCancelRequestResponsesRequestEntity request = service.parseInputArgsToRequest(args);
            InputCancelRequestResponsesResponseEntity response = service.invoke(request);
        }

        if ("CancelationRequests".equals(requestType)) {
            CancellationRequestsService service = new CancellationRequestsService(config);
            GetCancellationRequestEntity request = service.parseInputArgsToRequest(args);
            GetCancellationResponseEntity response = service.invoke(request);
        }

        if ("SendersCustSvcMsgs".equals(requestType)) {
            SendersCustSvcMsgsService service = new SendersCustSvcMsgsService(config);
            GetSendersCustSvcMsgsRequestEntity request = service.parseInputArgsToRequest(args);
            GetSendersCustSvcMsgsResponseEntity response = service.invoke(request);
        }
    }

}
