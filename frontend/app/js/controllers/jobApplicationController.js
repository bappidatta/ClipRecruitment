function jobApplicationController(jobService, candidateService){
    'ngInject';
    const vm = this;
    vm.selectedJobs = [];
    vm.init = function(){
        vm.selectedJobs = jobService.selectedJobs;        
    }

    vm.applyForJobs = function(selectedJobs){
        candidateService.applyForJobs(selectedJobs).then(function(res){
            if(res.data.Success){
                alert(res.data.Success);
            }else{
                alert(res.data.Error);
            }
        })
    }

    vm.removeJob = function(index){
        vm.selectedJobs.splice(index, 1);
    }
}

export default{
    name: 'jobApplicationController',
    fn: jobApplicationController
};