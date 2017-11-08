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
public class HeaderEntity {
    private String callID;
    private String callDateTimeLocal;
    private String correspLocNo;
    private String correspLocNo_Ria;
    private String correspLocName;
    private String correspLocCountry;
    private String userID;
    private String terminalID;
    private String languageCultureCode;
     
    public HeaderEntity() {
      // Constructor
    }
    
    public String getCallID() {
     return callID;
    }
 
    public void setCallID(String callID) {
     this.callID = callID;
    }
    
    public String getCallDateTimeLocal() {
     return callDateTimeLocal;
    }
 
    public void setCallDateTimeLocal(String callDateTimeLocal) {
     this.callDateTimeLocal = callDateTimeLocal;
    }
    
    public String getCorrespLocNo() {
     return correspLocNo;
    }
 
    public void setCorrespLocNo(String correspLocNo) {
     this.correspLocNo = correspLocNo;
    }
    
    public String getCorrespLocNo_Ria() {
     return correspLocNo_Ria;
    }
 
    public void setCorrespLocNo_Ria(String correspLocNo_Ria) {
     this.correspLocNo_Ria = correspLocNo_Ria;
    }
    
    public String getCorrespLocName() {
     return correspLocName;
    }
 
    public void setCorrespLocName(String correspLocName) {
     this.correspLocName = correspLocName;
    }
    
    public String getCorrespLocCountry() {
     return correspLocCountry;
    }
 
    public void setCorrespLocCountry(String correspLocCountry) {
     this.correspLocCountry = correspLocCountry;
    }
    
    public String getUserID() {
     return userID;
    }
 
    public void setUserID(String userID) {
     this.userID = userID;
    }
    
    public String getTerminalID() {
     return terminalID;
    }
 
    public void setTerminalID(String terminalID) {
     this.terminalID = terminalID;
    }
    
    public String getLanguageCultureCode() {
     return languageCultureCode;
    }
 
    public void setLanguageCultureCode(String languageCultureCode) {
     this.languageCultureCode = languageCultureCode;
    }
}
