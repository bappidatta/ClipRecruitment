function candidateSignupController(commonService, candidateService, $location, authService, jobService) {
    'ngInject';

    const vm = this;
    vm.salaryRange = [10000, 20000, 30000, 40000, 50000, 60000];

    vm.signUpModel = {
        FirstName: '',
        Surname: '',
        CurrentSalary: '',
        IndustryList: [],
        DocumentList: []
    }

    vm.uploads = {
        start: function () {            
            this.cv.upload();
        }
    };

    // enable file browser window
    vm.browseFile = function (id) {
        $('#' + id).click();
    }
    // stores the response.fileName into DocumentList and attempts to upload next file if available, otherwise attempts to signup
    vm.onFileUploadSuccess = function (response, nextFile) {
        response = JSON.parse(response);
        console.log(response);
        vm.signUpModel.DocumentList.push(response);
        if (nextFile) {
            nextFile.upload();
        } else {
            vm.signUp();
        }
    }
    // handles form submission and form validation. if form is valid initiates file uploading chain
    vm.handleFormSubmission = function () {      
        if (!vm.candidateForm.$valid) {
            return;
        }
        
        if (vm.signUpModel.IndustryList.length < 1) {
            return;
        }
        vm.uploads.start();
    }
    // sign up process starts
    vm.signUp = function () {
        console.log(vm.signUpModel);
        candidateService.signUp(vm.signUpModel).then(function (res) {
            if (res.data.Success) {
                console.log('signing in user...');
                authService.signIn({userName: vm.signUpModel.Email, password: vm.signUpModel.Password});
                // $location.path('/');
            }
        });
    }
    // get location suggestions from server
    vm.getLocation = function (viewValue) {
        if (viewValue != null && viewValue != '') {
            return commonService.getLocations(viewValue).then(function (res) {
                return res.data.Success;
            });
        }
    }

    vm.confirmEmail = function () {
        if (vm.signUpModel.Email != vm.signUpModel.ConfirmEmail) {
            vm.candidateForm.confirmEmail.$setValidity('comfirmEmail', false);
        }
        else {
            vm.candidateForm.confirmEmail.$setValidity('comfirmEmail', true);
        }
    }

    // match the Password with ConfirmPassword
    vm.confirmPassword = function () {
        if (vm.signUpModel.Password != vm.signUpModel.ConfirmPassword) {
            vm.candidateForm.confirmPassword.$setValidity('confirmPassword', false);
        }
        else {
            vm.candidateForm.confirmPassword.$setValidity('confirmPassword', true);
        }
    }

    vm.resetForm = function () {

    }

    // if checkbox state checked is true then push 
    vm.toggleIndustry = function (isTrue, industry) {        
        let index = vm.signUpModel.IndustryList.indexOf(industry);
        if (isTrue && index < 0) {
            vm.signUpModel.IndustryList.push(industry);
        } else {
            vm.signUpModel.IndustryList.splice(index, 1);
        }
    }

    // search Jobs 
    vm.searchCriteria = {LocationList: [], PositionList: []};
    vm.searchJobs = function(keyword, place){   
        console.log(place,keyword)
        if(place){
            vm.searchCriteria.LocationList.push(place);
        }             
        if(keyword){
            vm.searchCriteria.PositionList.push(keyword);
        }
        jobService.searchCriteria = vm.searchCriteria;

        $location.path('/Search-Jobs');
    }

}



export default {
    name: 'candidateSignupController',
    fn: candidateSignupController
}