/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.entities;

/**
 *
 * @author cvalderrama
 */
public class BankEntity {
    private String bankID;
    private String bankName;
    private String bankState;

    /**
     * @return the bankID
     */
    public String getBankID() {
        return bankID;
    }

    /**
     * @param bankID the bankID to set
     */
    public void setBankID(String bankID) {
        this.bankID = bankID;
    }

    /**
     * @return the bankName
     */
    public String getBankName() {
        return bankName;
    }

    /**
     * @param bankName the bankName to set
     */
    public void setBankName(String bankName) {
        this.bankName = bankName;
    }

    /**
     * @return the bankState
     */
    public String getBankState() {
        return bankState;
    }

    /**
     * @param bankState the bankState to set
     */
    public void setBankState(String bankState) {
        this.bankState = bankState;
    }
}
