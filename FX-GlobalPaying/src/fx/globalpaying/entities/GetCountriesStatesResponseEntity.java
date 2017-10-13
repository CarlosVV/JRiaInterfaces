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
public class GetCountriesStatesResponseEntity {

    private String ctryCode;
    private String ctryName;
    private String stateCode;
    private String stateName;

    public String getCtryCode() {
        return ctryCode;
    }
    
    public void setCtryCode(String ctryCode) {
        this.ctryCode = ctryCode;
    }
    
    public String getCtryName() {
        return ctryName;
    }
    
    public void setCtryName(String ctryName) {
        this.ctryName = ctryName;
    }
    
    public String getStateCode() {
        return stateCode;
    }
    
    public void setStateCode(String stateCode) {
        this.stateCode = stateCode;
    }
    
    public String getStateName() {
        return stateName;
    }
    
    public void setStateName(String stateName) {
        this.stateName = stateName;
    }
}
