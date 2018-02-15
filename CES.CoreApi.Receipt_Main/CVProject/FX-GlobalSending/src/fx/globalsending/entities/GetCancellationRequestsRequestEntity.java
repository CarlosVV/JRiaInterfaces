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
public class GetCancellationRequestsRequestEntity extends BaseRequestEntity {

    private String dateTimeLocal;
    private String dateTimeUTC;
    private String pin;
    private String beneAmount;
    private String correspLocID;

    public void setDateTimeLocal(String dateTimeLocal) {
        this.dateTimeLocal = dateTimeLocal;
    }

    public String getDateTimeLocal() {
        return dateTimeLocal;
    }

    public void setDateTimeUTC(String dateTimeUTC) {
        this.dateTimeUTC = dateTimeUTC;
    }

    public String getDateTimeUTC() {
        return dateTimeUTC;
    }

    public void setPin(String pin) {
        this.pin = pin;
    }

    public String getPin() {
        return pin;
    }

    public void setBeneAmount(String beneAmount) {
        this.beneAmount = beneAmount;
    }

    public String getBeneAmount() {
        return beneAmount;
    }

    public void setCorrespLocID(String correspLocID) {
        this.correspLocID = correspLocID;
    }

    public String getCorrespLocID() {
        return correspLocID;
    }
}
