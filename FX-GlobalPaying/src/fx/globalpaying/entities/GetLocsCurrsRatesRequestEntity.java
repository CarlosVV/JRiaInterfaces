/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package fx.globalpaying.entities;

import java.util.List;

/**
 *
 * @author cvalderrama
 */
public class GetLocsCurrsRatesRequestEntity extends BaseRequestEntity {
    private List<LocsCurrRatesRequestElementEntity> requests;
    public List<LocsCurrRatesRequestElementEntity> getRequests(){
        return requests;
    }
    public void setRequests(List<LocsCurrRatesRequestElementEntity> requests){
        this.requests = requests;
    }
}
