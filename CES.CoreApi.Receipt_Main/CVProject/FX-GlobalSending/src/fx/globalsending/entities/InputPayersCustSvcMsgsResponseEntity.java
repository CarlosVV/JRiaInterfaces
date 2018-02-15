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
public class InputPayersCustSvcMsgsResponseEntity {

    public InputPayersCustSvcMsgsResponseEntity() {
        acknowledgements = new ArrayList<CSMessageAcknowledgementEntity>();
    }

    /**
     * @return the acknowledgements
     */
    public List<CSMessageAcknowledgementEntity> getAcknowledgements() {
        return acknowledgements;
    }

    /**
     * @param acknowledgements the acknowledgements to set
     */
    public void setAcknowledgements(List<CSMessageAcknowledgementEntity> acknowledgements) {
        this.acknowledgements = acknowledgements;
    }

   private List<CSMessageAcknowledgementEntity> acknowledgements;
}
