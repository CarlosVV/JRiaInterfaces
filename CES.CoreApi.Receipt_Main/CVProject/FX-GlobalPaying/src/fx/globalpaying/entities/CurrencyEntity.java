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
public class CurrencyEntity {
 public CurrencyEntity() {
        this.requiredFields = new ArrayList<RequiredField>();
    }
    public CurrencyEntity(String symbol, String name, List<RequiredField> requiredFields) {
        this.symbol = symbol;
        this.name = name;
        this.requiredFields = requiredFields;
    }

    private String symbol;
    private String name;
    private List<RequiredField> requiredFields;

    /**
     * @return the symbol
     */
    public String getSymbol() {
        return symbol;
    }

    /**
     * @param symbol the symbol to set
     */
    public void setSymbol(String symbol) {
        this.symbol = symbol;
    }

    /**
     * @return the name
     */
    public String getName() {
        return name;
    }

    /**
     * @param name the name to set
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * @return the requiredFields
     */
    public List< RequiredField> getRequiredFields() {
        return requiredFields;
    }

    /**
     * @param requiredFields the requiredFields to set
     */
    public void setRequiredFields(List< RequiredField> requiredFields) {
        this.requiredFields = requiredFields;
    }

}
