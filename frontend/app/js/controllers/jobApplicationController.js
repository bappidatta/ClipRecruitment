function jobApplicationController(jobService, candidateService){
    'ngInject';
    const vm = this;
    vm.selectedJobs = [];
    vm.init = function(){
        vm.selectedJobs = jobService.selectedJobs;        
    }

    vm.handleFormSubmission = function(){

        if(vm.documents.files.length > 0){
            // upload files
            vm.documents.upload();
        }else{
            // try to apply to jobs
            vm.applyForJobs(vm.selectedJobs);
        }

               
    }

   vm.onFileUploadSuccess = function (response, nextFile) {
        response = JSON.parse(response);
        console.log(response);
        // vm.signUpModel.DocumentList.push(response);
        if (nextFile) {
            nextFile.upload();
        } else {
            vm.applyForJobs(vm.selectedJobs);
        }
    }

    vm.applyForJobs = function(selectedJobs){
        candidateService.applyForJobs(selectedJobs).then(function(res){
            if(res.data.Success){
                alert(res.data.Success);
            }else{
                alert(res.data.Error);
            }
        });
    }
     
    vm.browseFile = function (id) {
        $('#' + id).click();
    }

    vm.upload = function(){
        console.log(vm.documents);
    }

    vm.removeJob = function(index){
        vm.selectedJobs.splice(index, 1);
    }
}

export default{
    name: 'jobApplicationController',
    fn: jobApplicationController
};