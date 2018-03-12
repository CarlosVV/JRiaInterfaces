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
public class CancelRequestResponseAcknowledgementEntity {
    
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
     * @return the processDate
     */
    public String getProcessDate() {
        return processDate;
    }

    /**
     * @param processDate the processDate to set
     */
    public void setProcessDate(String processDate) {
        this.processDate = processDate;
    }

    /**
     * @return the processTime
     */
    public String getProcessTime() {
        return processTime;
    }

    /**
     * @param processTime the processTime to set
     */
    public void setProcessTime(String processTime) {
        this.processTime = processTime;
    }

    /**
     * @return the notificationCode
     */
    public String getNotificationCode() {
        return notificationCode;
    }

    /**
     * @param notificationCode the notificationCode to set
     */
    public void setNotificationCode(String notificationCode) {
        this.notificationCode = notificationCode;
    }

    /**
     * @return the notificationDesc
     */
    public String getNotificationDesc() {
        return notificationDesc;
    }

    /**
     * @param notificationDesc the notificationDesc to set
     */
    public void setNotificationDesc(String notificationDesc) {
        this.notificationDesc = notificationDesc;
    }
    
    private String pcOrderNo;
    private String scOrderNo;
    private String processDate;
    private String processTime;
    private String notificationCode;
    private String notificationDesc;
}
