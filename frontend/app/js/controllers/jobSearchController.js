function jobSearchController(jobService, commonFunc)
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
        PositionExperienceList: [],
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

    vm.onIndustryChange = function(){        
        vm.searchJobs(vm.searchCriteria);     
    }

    vm.onSolvencyChange = function(){
        vm.searchJobs(vm.searchCriteria);  
    }

    vm.addPositionExperience = function(position, experience){
        if(commonFunc.isReal(position) && commonFunc.isReal(experience))
        {
            var posExp = positionExperience(position, experience);
            if(!isDuplicatePositionExperience(vm.searchCriteria.PositionExperienceList, posExp)){
                vm.searchCriteria.PositionExperienceList.push(posExp);
            }
        }
    }

    vm.addPosition = function(position, experience){
        console.log(experience, position);

            if((position != null && position != '') && (experience != null && experience != '')){

                var exp = experience.split('-');
                console.log(exp);

                // var index = vm.searchCriteria.PositionList.indexOf(position);
                // if(index < 0){
                //     vm.searchCriteria.PositionList.unshift(position);
                //     vm.searchJobs(vm.searchCriteria);
                //     vm.position = '';
                // }
            }
            console.log(vm.searchCriteria.LocationList);
        }

    vm.removePosition = function(index){
        vm.searchCriteria.PositionList.splice(index, 1);
        vm.searchJobs(vm.searchCriteria);
    }


    vm.addLocation = function(location){
        if(location != null && location != ''){
            var index = vm.searchCriteria.LocationList.indexOf(location);
            if(index < 0){
                vm.searchCriteria.LocationList.unshift(location);
                vm.searchJobs(vm.searchCriteria);
                vm.location = '';
            }
        }
        console.log(vm.searchCriteria.LocationList);
    }



    vm.removeLocation = function(index){
        vm.searchCriteria.LocationList.splice(index, 1);
        vm.searchJobs(vm.searchCriteria);
    }

    

    vm.searchJobs = function(searchCriteria){
        jobService.searchJobs(searchCriteria).then(function(res){
            console.log(res.data.Success);
             vm.searchResult = res.data.Success;
             vm.selectedJobs = [];
        });
    }

    vm.selectJob = function(job){
        job.selected = !job.selected;
        var index = commonFunc.indexOfObjectInArray(vm.selectedJobs, '_id', job._id);

        if( index > -1){            
            vm.searchResult.push(vm.selectedJobs.splice(index, 1)[0]);
        }
        else{
            vm.selectedJobs.push(job);
            var index2 = commonFunc.indexOfObjectInArray(vm.searchResult, '_id', job._id);
            vm.searchResult.splice(index2, 1);
        }        
    }


    vm.getLocations = function(viewValue){
       if(viewValue != null && viewValue != ''){
            return jobService.getLocations(viewValue).then(function(res){
                return res.data.Success;                    
        });
       }
    }

    vm.getPositions = function(viewValue){
       if(viewValue != null && viewValue != ''){
            return jobService.getPositions(viewValue).then(function(res){
                return res.data.Success;                    
        });
       }
    }

   
    vm.resetSearchFilter = function(){        
            vm.searchCriteria = {
            IndustryID: 0,
            InsolvencyID: 0,
            PositionList: [],
            PositionExperienceList: [],
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



    function positionExperience(position, experience){
        exp = experience.split('-');
        from = exp[0];
        to = exp[1];

        return {
            Position: position,
            ExperienceRange : {
                From: from,
                To: to
            }
        }
    }

    function isDuplicatePositionExperience(list, object){
        for(var i in list){
            if(list[i].Position.toLowerCase() == object.Position.toLowerCase()){
                if(list[i].ExperienceRange.From == object.ExperienceRange.From && list[i].ExperienceRange.To == object.ExperienceRange.To){
                return true;
                }
            }
        }
  
     return false;
    }

   
    


}

export default{
    name: 'jobSearchController',
    fn: jobSearchController
};