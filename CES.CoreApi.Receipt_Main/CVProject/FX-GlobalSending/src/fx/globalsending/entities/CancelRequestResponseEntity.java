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
public class CancelRequestResponseEntity {

    public CancelRequestResponseEntity() {
    }

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
     * @return the responseCode
     */
    public String getResponseCode() {
        return responseCode;
    }

    /**
     * @param responseCode the responseCode to set
     */
    public void setResponseCode(String responseCode) {
        this.responseCode = responseCode;
    }

    /**
     * @return the responseDesc
     */
    public String getResponseDesc() {
        return responseDesc;
    }

    /**
     * @param responseDesc the responseDesc to set
     */
    public void setResponseDesc(String responseDesc) {
        this.responseDesc = responseDesc;
    }

    /**
     * @return the responseDate
     */
    public String getResponseDate() {
        return responseDate;
    }

    /**
     * @param responseDate the responseDate to set
     */
    public void setResponseDate(String responseDate) {
        this.responseDate = responseDate;
    }

    /**
     * @return the responseTime
     */
    public String getResponseTime() {
        return responseTime;
    }

    /**
     * @param responseTime the responseTime to set
     */
    public void setResponseTime(String responseTime) {
        this.responseTime = responseTime;
    }

    /**
     * @return the comments
     */
    public String getComments() {
        return comments;
    }

    /**
     * @param comments the comments to set
     */
    public void setComments(String comments) {
        this.comments = comments;
    }
    private String pcOrderNo;
    private String scOrderNo;
    private String responseCode;
    private String responseDesc;
    private String responseDate;
    private String responseTime;
    private String comments;
}
