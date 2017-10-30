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
public class BaseRequestEntity {
   private String correspID;
   private String layoutVersion;
   private HeaderEntity header;
   private String requestType;
   
   public BaseRequestEntity() {
      // Constructor
   }
 
   public String getCorrespID() {
     return correspID;
   }
 
   public void setCorrespID(String correspID) {
     this.correspID = correspID;
   }
   
   public String getLayoutVersion() {
     return layoutVersion;
   }
 
   public void setLayoutVersion(String layoutVersion) {
     this.layoutVersion = layoutVersion;
   }
   
   public HeaderEntity getHeader() {
     return header;
   }
 
   public void setHeader(HeaderEntity header) {
     this.header = header;
   }
  
   public String getRequestType() {
     return requestType;
   }
 
   public void setRequestType(String requestType) {
     this.requestType = requestType;
   }
}
