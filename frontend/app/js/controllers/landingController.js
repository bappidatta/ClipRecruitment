function landingController(jobService){
    'ngInject';
    const vm = this;
    vm.pageNo = 0;    
    vm.totalCount = 0;
    vm.jobPostings = [];    

    vm.init = function(){
        vm.getAllJob();
    }
        
    vm.getAllJob = function(){
        jobService.getAllJob(vm.pageNo).then(function(res){
            if(res.data.Success){
                vm.totalCount = res.data.Count;
                for(var i in res.data.Success){
                    vm.jobPostings.push(res.data.Success[i]);
                }
                                
                vm.pageNo += 1;
            }
        });
    };
}

export default{
    name: 'landingController',
    fn: landingController
};