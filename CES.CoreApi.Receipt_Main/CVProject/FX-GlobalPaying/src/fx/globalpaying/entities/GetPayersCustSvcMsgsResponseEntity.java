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
public class GetPayersCustSvcMsgsResponseEntity {

    public GetPayersCustSvcMsgsResponseEntity(List<CSMessageEntity> messages) {
        this.messages = messages;
    }
    
    public GetPayersCustSvcMsgsResponseEntity() {
        this.messages = new ArrayList<CSMessageEntity>();
    }
    

    /**
     * @return the messages
     */
    public List<CSMessageEntity> getMessages() {
        return messages;
    }

    /**
     * @param messages the messages to set
     */
    public void setMessages(List<CSMessageEntity> messages) {
        this.messages = messages;
    }
private List<CSMessageEntity> messages;    
}
