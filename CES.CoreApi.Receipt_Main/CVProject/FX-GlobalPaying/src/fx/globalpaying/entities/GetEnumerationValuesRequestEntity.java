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
public class GetEnumerationValuesRequestEntity extends BaseRequestEntity {

    private String enumType;
    private String countryFrom;
    public String getEnumType(){
        return enumType;
    }
    public void setEnumType(String enumType){
        this.enumType = enumType;
    }
     public String getCountryFrom(){
        return countryFrom;
    }
    public void setCountryFrom(String countryFrom){
        this.countryFrom = countryFrom;
    }
}
