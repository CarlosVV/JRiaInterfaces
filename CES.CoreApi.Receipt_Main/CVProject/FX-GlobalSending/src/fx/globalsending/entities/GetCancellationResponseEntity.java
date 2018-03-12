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
public class GetCancellationResponseEntity {

    public GetCancellationResponseEntity() {
        cancellationRequests = new ArrayList<CancelRequestResponseEntity>();
    }

    /**
     * @return the cancellationRequests
     */
    public List<CancelRequestResponseEntity> getCancellationRequests() {
        return cancellationRequests;
    }

    /**
     * @param cancellationRequests the cancellationRequests to set
     */
    public void setCancellationRequests(List<CancelRequestResponseEntity> cancellationRequests) {
        this.cancellationRequests = cancellationRequests;
    }

   private List<CancelRequestResponseEntity> cancellationRequests;
}
