function candidateVideoProfileSearchController(candidateVideoProfileSearchService,commonService){
    'ngInject';

    const vm = this;
    vm.SuggestedPositionList = ["Solicitor","paralegal","MFGH","KLOP"]

    vm.searchCriteria = {
        Profile: "",
        PositionList:[],
        LocationList:[],
        IsFullTime:true,
        IsPermanent:true,
        Skills:[]
    }

vm.init= function(){
}

vm.search = function(){
    console.log(vm.searchCriteria);
}

/**
 * Get All Positions
 */
vm.getPositions = function(viewValue){
       if(viewValue != null && viewValue != ''){
          return vm.SuggestedPositionList;
       }
    }

/**
 * Add Position
 */
    vm.addPosition = function(position){        
        if(position){            
            let index = vm.searchCriteria.PositionList.indexOf(position);
            if(index < 0){
                vm.searchCriteria.PositionList.push(position);
                vm.position = '';                
                //call Search for search data from server
            }else{
                alert('Already Added!');
            }
        }
    }
    
    /**
     * remove position from position list
     */
    vm.removePosition = function(index){
        vm.searchCriteria.PositionList.splice(index, 1);
       //call Search for search data from server 
    }

    vm.onPermanentChange = function(){
        console.log(vm.searchCriteria.IsPermanent);
    }

    vm.onFullTimeChange = function(){
        console.log(vm.searchCriteria.IsFullTime);
    }

}

export default{
    name: 'candidateVideoProfileSearchController',
    fn: candidateVideoProfileSearchController
};