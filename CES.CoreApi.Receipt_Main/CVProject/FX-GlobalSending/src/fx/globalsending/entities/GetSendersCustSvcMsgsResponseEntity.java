/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalsending.entities;

import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author cvalderrama
 */
public class GetSendersCustSvcMsgsResponseEntity {

    public GetSendersCustSvcMsgsResponseEntity() {
        requestResponses = new ArrayList<CSMessageEntity>();
    }

    /**
     * @return the requestResponses
     */
    public List<CSMessageEntity> getRequestResponses() {
        return requestResponses;
    }

    /**
     * @param requestResponses the requestResponses to set
     */
    public void setRequestResponses(List<CSMessageEntity> requestResponses) {
        this.requestResponses = requestResponses;
    }
    private List<CSMessageEntity> requestResponses;
}
