function candidateSignupController(commonService, candidateService, $location) {
    'ngInject';
    
    const vm = this;
    vm.salaryRange = [10000, 20000, 30000, 40000, 50000, 60000];

    vm.signUpModel = {
        FirstName: '',
        Surname: '',
        CurrentSalary: '',
        IndustryList: []
    }

    vm.redirectSignIn = function(){  
        console.log('redireting...');      
        $location.path('/SignIn')
    }

    vm.browseFile = function(id){        
        $('#'+id).click();
    }

    vm.checkSingle = function(flow){
        console.log(flow);
    }

    vm.signUp = function () {        
        if (vm.candidateForm.$valid) {
            if (vm.signUpModel.IndustryList.length < 1) {
                // error
                console.log('error');
                return;
            }
            candidateService.signUp(vm.signUpModel).then(function (res) {
                if (res.data.Success) {
                    vm.CV.upload();
                    //$location.path('/SignIn');
                }
            });
        } else {
            console.log('invalid');
        }
    }

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

    vm.toggleIndustry = function (isTrue, industry) {
        let index = vm.signUpModel.IndustryList.indexOf(industry);
        if (isTrue && index < 0) {
            vm.signUpModel.IndustryList.push(industry);
        } else {
            vm.signUpModel.IndustryList.splice(index, 1);
        }
    }

    vm.empty = function (object) {
        for (var prop in object) {
            object[prop] = '';
        }
        return object;
    }
}



export default {
    name: 'candidateSignupController',
    fn: candidateSignupController
}