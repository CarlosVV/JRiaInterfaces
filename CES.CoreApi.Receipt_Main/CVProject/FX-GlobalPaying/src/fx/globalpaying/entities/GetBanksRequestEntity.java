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
    private String countryCode;
    private String searchCriteria;
    private int itemsPerPage;
    private int pageNoDesired;

       public String getCountryCode() {
        return countryCode;
    }

    public void setCountryCode(String countryCode) {
        this.countryCode = countryCode;
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
