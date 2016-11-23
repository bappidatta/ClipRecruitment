function jobSearchController(jobService, commonService)
{
    'ngInject';
    const vm = this;
    vm.jobList = [];
    vm.selectedJobs = [];
    vm.searchResult = [];

    vm.industryTypes = [{id: 1, name: 'Industry 1'},{id: 2, name: 'Industry 2'},{id: 3, name: 'Industry 3'},
                        {id: 4, name: 'Industry 4'},{id: 5, name: 'Industry 5'},{id: 6, name: 'Industry 6'}];

    vm.insolvencyTypes = [{id: 1, name: 'Insolvency 1'},{id: 2, name: 'Insolvency 2'},{id: 3, name: 'Insolvency 3'},
                        {id: 4, name: 'Insolvency 4'},{id: 5, name: 'Insolvency 5'},{id: 6, name: 'Insolvency 6'}];

    vm.salaryFrom = [15000, 25000, 35000, 45000, 55000, 65000];
    vm.salaryTo = [25000, 35000, 45000, 55000, 65000, 75000, 85000, 95000];

    vm.searchCriteria = {
        IndustryID: 0,
        InsolvencyID: 0,
        PositionList: [],        
        IsFullTime: false,
        IsPartTime: false,
        IsPermanent: false,
        IsTemporary: false,
        IsLocal: false,
        IsRemote: false,
        LocationList: [],
        SalaryFrom: 15000,
        SalaryTo: 55000
    }

    vm.init = function(){
        vm.searchJobs(vm.searchCriteria);
    }

    vm.onIndustryChange = function(industryID){        
        if(!commonService.isReal(industryID)){
            vm.searchCriteria.IndustryID = 0;
            vm.searchJobs(vm.searchCriteria);
        }
        else{
            vm.searchJobs(vm.searchCriteria);
        }
    }

    vm.onSolvencyChange = function(insolvencyID){
        if(!commonService.isReal(insolvencyID)){
            vm.searchCriteria.InsolvencyID = 0;
            vm.searchJobs(vm.searchCriteria);
        }else{
            vm.searchJobs(vm.searchCriteria);
        }
    }

    vm.addPosition = function(position){        
        if(position){            
            let index = vm.searchCriteria.PositionList.indexOf(position);
            if(index < 0){
                vm.searchCriteria.PositionList.push(position);
                vm.position = '';                
                vm.searchJobs(vm.searchCriteria);
            }else{
                alertify.warning('Already Added!');
            }
        }
    }
    
    vm.removePosition = function(index){
        vm.searchCriteria.PositionList.splice(index, 1);
        vm.searchJobs(vm.searchCriteria);
    }

    vm.addLocation = function(location){
        if(location){            
            let index = vm.searchCriteria.LocationList.indexOf(location);
            if(index < 0){
                vm.searchCriteria.LocationList.unshift(location);
                vm.searchJobs(vm.searchCriteria);
                vm.location = '';
            }else{
                alertify.warning('Already Added!');
            }
        }        
    }

    vm.removeLocation = function(index){
        vm.searchCriteria.LocationList.splice(index, 1);
        vm.searchJobs(vm.searchCriteria);
    }

    vm.searchJobs = function(searchCriteria){
        jobService.searchJobs(searchCriteria).then(function(res){            
             vm.searchResult = res.data.Success;
             vm.selectedJobs = [];
             console.log(vm.searchResult);
        });
    }

    //select job for application
    vm.selectJob = function(job){
        job.selected = !job.selected;
        let index = commonService.indexOfObjectInArray(vm.selectedJobs, '_id', job._id);
        if( index > -1){            
            vm.searchResult.push(vm.selectedJobs.splice(index, 1)[0]);
        }
        else{
            vm.selectedJobs.push(job);
            let index2 = commonService.indexOfObjectInArray(vm.searchResult, '_id', job._id);
            vm.searchResult.splice(index2, 1);
        }        
    }

    vm.getLocations = function(viewValue){
       if(viewValue != null && viewValue != ''){
            return commonService.getLocations(viewValue).then(function(res){
                return res.data.Success;                    
        });
       }
    }

    vm.getPositions = function(viewValue){
       if(viewValue != null && viewValue != ''){
            return commonService.getPositions(viewValue).then(function(res){
                return res.data.Success;                    
        });
       }
    }
   
    vm.resetSearchFilter = function(){        
            vm.searchCriteria = {
            IndustryID: 0,
            InsolvencyID: 0,
            PositionList: [],            
            IsFullTime: false,
            IsPartTime: false,
            IsPermanent: false,
            IsTemporary: false,
            IsLocal: false,
            IsRemote: false,
            LocationList: [],
            SalaryFrom: 15000,
            SalaryTo: 55000
        }
        vm.searchJobs(vm.searchCriteria); 
    }

}

export default{
    name: 'jobSearchController',
    fn: jobSearchController
};