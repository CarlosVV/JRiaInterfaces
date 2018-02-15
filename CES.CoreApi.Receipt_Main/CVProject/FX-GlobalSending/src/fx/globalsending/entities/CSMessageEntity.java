/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalsending.entities;

/**
 *
 * @author cvalderrama
 */
public class CSMessageEntity {

    /**
     * @return the pcOrderNo
     */
    public String getPcOrderNo() {
        return pcOrderNo;
    }

    /**
     * @param pcOrderNo the pcOrderNo to set
     */
    public void setPcOrderNo(String pcOrderNo) {
        this.pcOrderNo = pcOrderNo;
    }

    /**
     * @return the scOrderNo
     */
    public String getScOrderNo() {
        return scOrderNo;
    }

    /**
     * @param scOrderNo the scOrderNo to set
     */
    public void setScOrderNo(String scOrderNo) {
        this.scOrderNo = scOrderNo;
    }

    /**
     * @return the messageID
     */
    public String getMessageID() {
        return messageID;
    }

    /**
     * @param messageID the messageID to set
     */
    public void setMessageID(String messageID) {
        this.messageID = messageID;
    }

    /**
     * @return the messageText
     */
    public String getMessageText() {
        return messageText;
    }

    /**
     * @param messageText the messageText to set
     */
    public void setMessageText(String messageText) {
        this.messageText = messageText;
    }

    /**
     * @return the enteredBy
     */
    public String getEnteredBy() {
        return enteredBy;
    }

    /**
     * @param enteredBy the enteredBy to set
     */
    public void setEnteredBy(String enteredBy) {
        this.enteredBy = enteredBy;
    }

    private String pcOrderNo;
    private String scOrderNo;
    private String messageID;
    private String messageText;
    private String enteredBy;
}
