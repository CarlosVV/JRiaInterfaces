/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying;

import fx.globalpaying.ServiceManager.RequestTypeEnum;
import static fx.globalpaying.Utils.stripAccents;
import java.io.StringReader;
import java.text.Normalizer;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;

/**
 *
 * @author Carlos Valderrama @ RIA
 */
public class FXGlobalPaying {

    static String soapEndpointUrl = "http://stagingfxglobalwebsvcnocert.riaenvia.net:9771/FXGlobalPaying.svc/Binding_Basic_NoCert";
    static String soapActionPrefix = "CES.Services.FXGlobal/IRiaAsPayer/";

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
      
        if (args.length < 4 || (args.length > 3 && (args[2] == null || args[2].split(";").length < 9))) {
            System.out.println("Error: argumentos inválidos o faltantes");
            return;
        }

        if (args.length > 5 && "-url".equals(args[args.length - 2])) {
            soapEndpointUrl = args[args.length - 1];
        }

        String requestType = args[3].split(";")[0];

        boolean isValidRequestType = ServiceManager.ExistsRequestType(requestType);
        if (!isValidRequestType) {
            // Invalid Request Type, exit Program
            return;
        }
        Object response;
        response = ServiceManager.ExecuteWebMethod(RequestTypeEnum.valueOf(requestType), args);
    }   
}
