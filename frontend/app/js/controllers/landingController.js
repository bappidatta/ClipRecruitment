function landingController(jobService, signalRService, commonService, $location, authService){
    'ngInject';
 signalRService.init();
    const vm = this;
    vm.pageNo = 0;    
    vm.totalCount = 0;
    vm.jobPostings = [];    
    vm.selectedJobs = [];
    vm.init = function(){
        vm.getAllJob();        
        //toastr.warning('test');
    }
        
    vm.getAllJob = function(){
        
        jobService.getAllJob(vm.pageNo).then(function(res){
            if(res.data.Success){
                vm.totalCount = res.data.Count;
                for(var i in res.data.Success){
                    vm.jobPostings.push(res.data.Success[i]);                    
                }                                
                vm.pageNo += 1;
                console.log(vm.jobPostings);
            }
        });
    };


    vm.applyJobs = function(selectedJobs){
        jobService.selectedJobs = selectedJobs;
        $location.path('/Apply-Jobs');
    }

    vm.searchCriteria = {LocationList: [], PositionList: []};
    vm.searchJobs = function(keyword, place){   
        if(place){
            vm.searchCriteria.LocationList.push(place);
        }             
        if(keyword){
            vm.searchCriteria.PositionList.push(keyword);
        }
        jobService.searchCriteria = vm.searchCriteria;

        $location.path('/Search-Jobs');
    }

    //select job for application
    vm.selectJob = function (job) {
        if(!authService.isAuth()){
            alert('Please Login to Apply Now!');
            return;
        }
        job.selected = !job.selected;
        let index = commonService.indexOfObjectInArray(vm.selectedJobs, '_id', job._id);
        if (index > -1) {
            vm.jobPostings.push(vm.selectedJobs.splice(index, 1)[0]);
        }
        else {
            vm.selectedJobs.push(job);
            let index2 = commonService.indexOfObjectInArray(vm.jobPostings, '_id', job._id);
            vm.jobPostings.splice(index2, 1);
        }
    };
}

export default{
    name: 'landingController',
    fn: landingController
};