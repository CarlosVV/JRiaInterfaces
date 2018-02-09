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
public class BankEntity {

    public BankEntity() {     
        this.currencyList = new ArrayList<CurrencyEntity>();
    }
    
     public BankEntity(String bankID, String bankName, String bankState, List<CurrencyEntity> currencyList) {
        this.bankID = bankID;
        this.bankName = bankName;
        this.bankState = bankState;
        this.currencyList = currencyList;
    }

    /**
     * @return the currencyList
     */
    public List<CurrencyEntity> getCurrencyList() {
        return currencyList;
    }

    /**
     * @param currencyList the currencyList to set
     */
    public void setCurrencyList(List<CurrencyEntity> currencyList) {
        this.currencyList = currencyList;
    }

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

    private String bankID;
    private String bankName;
    private String bankState;
    private List<CurrencyEntity> currencyList;
}
