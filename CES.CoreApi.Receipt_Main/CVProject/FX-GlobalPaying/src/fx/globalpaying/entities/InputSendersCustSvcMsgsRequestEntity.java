/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.entities;

import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author cvalderrama
 */
public class InputSendersCustSvcMsgsRequestEntity extends BaseRequestEntity {

    public InputSendersCustSvcMsgsRequestEntity() {
        this.requests = new ArrayList<CSMessageEntity>();
    }

    public InputSendersCustSvcMsgsRequestEntity(List<CSMessageEntity> requests) {
        this.requests = requests;
    }

    /**
     * @return the Requests
     */
    public List<CSMessageEntity> getRequests() {
        return requests;
    }

    /**
     * @param Requests the Requests to set
     */
    public void setRequests(List<CSMessageEntity> requests) {
        this.requests = requests;
    }

    private List<CSMessageEntity> requests;
}
