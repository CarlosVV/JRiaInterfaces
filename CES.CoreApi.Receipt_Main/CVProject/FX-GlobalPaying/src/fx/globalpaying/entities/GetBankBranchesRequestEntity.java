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

    private String dateDesired;
    private String countryFrom;
    private String countryTo;
    private String payingCorrespID;
    private String payingCorrespLocID;
    private String paymentCurrency;
    private String paymentAmount;
    private String beneficiaryCurrency;
    private String beneficiaryAmount;
    private String deliveryMethod;

    public String getDateDesired() {
        return dateDesired;
    }

    public void setDateDesired(String dateDesired) {
        this.dateDesired = dateDesired;
    }

    public String getCountryFrom() {
        return countryFrom;
    }

    public void setCountryFrom(String countryFrom) {
        this.countryFrom = countryFrom;
    }

    public String getCountryTo() {
        return countryTo;
    }

    public void setCountryTo(String countryTo) {
        this.countryTo = countryTo;
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
}
