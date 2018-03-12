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
public class InputCancelRequestResponsesRequestEntity extends BaseRequestEntity {

    public InputCancelRequestResponsesRequestEntity() {
        cancelRequestResponses = new ArrayList<CancelRequestResponseEntity>();
    }

    private List<CancelRequestResponseEntity> cancelRequestResponses;

    /**
     * @return the cancelRequestResponses
     */
    public List<CancelRequestResponseEntity> getCancelRequestResponses() {
        return cancelRequestResponses;
    }

    /**
     * @param cancelRequestResponses the cancelRequestResponses to set
     */
    public void setCancelRequestResponses(List<CancelRequestResponseEntity> cancelRequestResponses) {
        this.cancelRequestResponses = cancelRequestResponses;
    }
}
