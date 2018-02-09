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
public class GetBankBranchesResponseEntity {

    public GetBankBranchesResponseEntity() {    
        this.branchList = new ArrayList<BranchEntity>();
    }   

    /**
     * @return the branchList
     */
    public List<BranchEntity> getBranchList() {
        return branchList;
    }

    /**
     * @param branchList the branchList to set
     */
    public void setBranchList(List<BranchEntity> branchList) {
        this.branchList = branchList;
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
    
    private String inputParams;
    private List<BranchEntity> branchList;
}
