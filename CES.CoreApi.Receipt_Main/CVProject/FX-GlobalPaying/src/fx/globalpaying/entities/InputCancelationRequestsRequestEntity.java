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
public class InputCancelationRequestsRequestEntity extends BaseRequestEntity {

    public InputCancelationRequestsRequestEntity() {
        this.requests = new ArrayList<CancellationRequestEntity>();
    }

    public InputCancelationRequestsRequestEntity(List<CancellationRequestEntity> requests) {
        this.requests = requests;
    }

    /**
     * @return the Requests
     */
    public List<CancellationRequestEntity> getRequests() {
        return requests;
    }

    /**
     * @param Requests the Requests to set
     */
    public void setRequests(List<CancellationRequestEntity> requests) {
        this.requests = requests;
    }

    private List<CancellationRequestEntity> requests;

}
