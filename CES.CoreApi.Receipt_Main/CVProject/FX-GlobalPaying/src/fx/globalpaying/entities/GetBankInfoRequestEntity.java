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
public class GetBankInfoRequestEntity extends BaseRequestEntity {

    private String bankID;
    private String countryCode;

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
     * @return the countryCode
     */
    public String getCountryCode() {
        return countryCode;
    }

    /**
     * @param countryCode the countryCode to set
     */
    public void setCountryCode(String countryCode) {
        this.countryCode = countryCode;
    }
}
