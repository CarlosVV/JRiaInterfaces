/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.entities;

import java.util.List;

/**
 *
 * @author cvalderrama
 */
public class GetBankInfoResponseEntity {

    public GetBankInfoResponseEntity() {    
        this.bankEntity = new BankEntity();
    }

    public GetBankInfoResponseEntity(String inputParams, BankEntity bankEntity) {
        this.inputParams = inputParams;
        this.bankEntity = bankEntity;
    }

    /**
     * @return the inputParams
     */
    public String getInputParams() {
        return inputParams;
    }

    /**
     * @param inputParams the inputParams to set
     */
    public void setInputParams(String inputParams) {
        this.inputParams = inputParams;
    }

    /**
     * @return the bankEntity
     */
    public BankEntity getBankEntity() {
        return bankEntity;
    }

    /**
     * @param bankEntity the bankEntity to set
     */
    public void setBankEntity(BankEntity bankEntity) {
        this.bankEntity = bankEntity;
    }

    private String inputParams;
    private BankEntity bankEntity;
}
