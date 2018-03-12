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
public class InputCancelRequestResponsesResponseEntity {

    public InputCancelRequestResponsesResponseEntity() {
        acknowledgements = new ArrayList<CancelRequestResponseAcknowledgementEntity>();
    }
   
    /**
     * @return the acknowledgements
     */
    public List<CancelRequestResponseAcknowledgementEntity> getAcknowledgements() {
        return acknowledgements;
    }

    /**
     * @param acknowledgements the acknowledgements to set
     */
    public void setAcknowledgements(List<CancelRequestResponseAcknowledgementEntity> acknowledgements) {
        this.acknowledgements = acknowledgements;
    }

   private List<CancelRequestResponseAcknowledgementEntity> acknowledgements;
}
