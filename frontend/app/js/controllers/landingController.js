function landingController(jobService){
    'ngInject';
    const vm = this;
    vm.pageNo = 0;
    
    vm.jobPostings = [];

    vm.getAllJob = function(){
        vm.pageNo += 1;
        jobService.getAllJob(vm.pageNo).then(function(res){
            console.log(res.data);
        })
    

    console.log(vm.pageNo);
    
    };
}


export default{
    name: 'landingController',
    fn: landingController
};