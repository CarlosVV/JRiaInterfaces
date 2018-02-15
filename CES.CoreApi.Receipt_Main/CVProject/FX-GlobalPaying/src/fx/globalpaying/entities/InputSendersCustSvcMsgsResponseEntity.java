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
public class InputSendersCustSvcMsgsResponseEntity {

    public InputSendersCustSvcMsgsResponseEntity() {    
        this.acknowledgements = new ArrayList<AcknowledgementEntity>();
    }   

  
    private List<AcknowledgementEntity> acknowledgements;

    /**
     * @return the acknowledgements
     */
    public List<AcknowledgementEntity> getAcknowledgements() {
        return acknowledgements;
    }

    /**
     * @param acknowledgements the acknowledgements to set
     */
    public void setAcknowledgements(List<AcknowledgementEntity> acknowledgements) {
        this.acknowledgements = acknowledgements;
    }
}
