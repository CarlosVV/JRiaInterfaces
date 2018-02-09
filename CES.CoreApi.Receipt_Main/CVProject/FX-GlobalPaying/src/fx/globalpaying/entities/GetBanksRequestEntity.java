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
public class GetBanksRequestEntity extends BaseRequestEntity {
   
    private String dateDesired;   
    private String countryCode;
    private String payingCorrespID;
    private String payingCorrespLocID;
    private String paymentCurrency;
    private String paymentAmount;
    private String beneficiaryCurrency;
    private String beneficiaryAmount;
    private String deliveryMethod;
    private String searchCriteria;
    private int itemsPerPage;
    private int pageNoDesired;

    public String getDateDesired() {
        return dateDesired;
    }

    public void setDateDesired(String dateDesired) {
        this.dateDesired = dateDesired;
    }

    public String getCountryCode() {
        return countryCode;
    }

    public void setCountryCode(String countryCode) {
        this.countryCode = countryCode;
    }   

    public String getPayingCorrespID() {
        return payingCorrespID;
    }

    public void setPayingCorrespID(String payingCorrespID) {
        this.payingCorrespID = payingCorrespID;
    }

    public String getPayingCorrespLocID() {
        return payingCorrespLocID;
    }

    public void setPayingCorrespLocID(String payingCorrespLocID) {
        this.payingCorrespLocID = payingCorrespLocID;
    }

    public String getPaymentCurrency() {
        return paymentCurrency;
    }

    public void setPaymentCurrency(String paymentCurrency) {
        this.paymentCurrency = paymentCurrency;
    }

    public String getPaymentAmount() {
        return paymentAmount;
    }

    public void setPaymentAmount(String paymentAmount) {
        this.paymentAmount = paymentAmount;
    }

    public String getBeneficiaryCurrency() {
        return beneficiaryCurrency;
    }

    public void setBeneficiaryCurrency(String beneficiaryCurrency) {
        this.beneficiaryCurrency = beneficiaryCurrency;
    }

    public String getBeneficiaryAmount() {
        return beneficiaryAmount;
    }

    public void setBeneficiaryAmount(String beneficiaryAmount) {
        this.beneficiaryAmount = beneficiaryAmount;
    }

    public String getDeliveryMethod() {
        return deliveryMethod;
    }

    public void setDeliveryMethod(String deliveryMethod) {
        this.deliveryMethod = deliveryMethod;
    }

    /**
     * @return the searchCriteria
     */
    public String getSearchCriteria() {
        return searchCriteria;
    }

    /**
     * @param searchCriteria the searchCriteria to set
     */
    public void setSearchCriteria(String searchCriteria) {
        this.searchCriteria = searchCriteria;
    }
    
    /**
     * @return the pageNoDesired
     */
    public int getPageNoDesired() {
        return pageNoDesired;
    }

    /**
     * @param pageNoDesired the pageNoDesired to set
     */
    public void setPageNoDesired(int pageNoDesired) {
        this.pageNoDesired = pageNoDesired;
    }

    /**
     * @return the itemsPerPage
     */
    public int getItemsPerPage() {
        return itemsPerPage;
    }

    /**
     * @param itemsPerPage the itemsPerPage to set
     */
    public void setItemsPerPage(int itemsPerPage) {
        this.itemsPerPage = itemsPerPage;
    }
}
