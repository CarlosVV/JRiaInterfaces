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
public class GetStatesCitiesRequestEntity extends BaseRequestEntity {

    private String countryCode;
    private String stateName;

    public String getCountryCode() {
        return this.countryCode;
    }
    
    public void setCountryCode(String countryCode) {
        this.countryCode = countryCode;
    }
    
    public String getStateName() {
        return this.stateName;
    }
    
    public void setStateName(String stateName) {
        this.stateName = stateName;
    }
}

