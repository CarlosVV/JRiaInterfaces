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
public class GetBankBranchesRequestEntity extends BaseRequestEntity {

    /**
     * @return the routingCode
     */
    public String getRoutingCode() {
        return routingCode;
    }

    /**
     * @param routingCode the routingCode to set
     */
    public void setRoutingCode(String routingCode) {
        this.routingCode = routingCode;
    }

    /**
     * @return the routingType
     */
    public String getRoutingType() {
        return routingType;
    }

    /**
     * @param routingType the routingType to set
     */
    public void setRoutingType(String routingType) {
        this.routingType = routingType;
    }

    /**
     * @return the branchNumber
     */
    public String getBranchNumber() {
        return branchNumber;
    }

    /**
     * @param branchNumber the branchNumber to set
     */
    public void setBranchNumber(String branchNumber) {
        this.branchNumber = branchNumber;
    }

    /**
     * @return the branchAddress
     */
    public String getBranchAddress() {
        return branchAddress;
    }

    /**
     * @param branchAddress the branchAddress to set
     */
    public void setBranchAddress(String branchAddress) {
        this.branchAddress = branchAddress;
    }

    /**
     * @return the branchCity
     */
    public String getBranchCity() {
        return branchCity;
    }

    /**
     * @param branchCity the branchCity to set
     */
    public void setBranchCity(String branchCity) {
        this.branchCity = branchCity;
    }

    /**
     * @return the branchState
     */
    public String getBranchState() {
        return branchState;
    }

    /**
     * @param branchState the branchState to set
     */
    public void setBranchState(String branchState) {
        this.branchState = branchState;
    }

    private String countryCode;
    private String routingCode;
    private String routingType;
    private String bankID;
    private String branchNumber;
    private String branchAddress;
    private String branchCity;
    private String branchState;

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
