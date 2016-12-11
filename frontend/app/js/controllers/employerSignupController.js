function employerSignupController(commonService, employerService, $location) {
    'ngInject';
    
    const vm = this;
    vm.signUpModel = {
        AddressList: [],
        IndustryList: []
    }

    vm.signUp = function () {
        if (vm.employerSignupForm.$valid) {
            if (vm.signUpModel.AddressList.length < 1 || vm.signUpModel.IndustryList.length < 1) {              
                return;
            }
            employerService.signUp(vm.signUpModel).then(function (res) {
                if (res.data.Success) {
                    $location.path('/SignIn');
                }
            });
        } 

    }
    
    //ehecking if two email addresses are same 
    vm.confirmEmail = function () {
        if (vm.signUpModel.Email != vm.signUpModel.ConfirmEmail) {
            vm.employerSignupForm.confirmEmail.$setValidity('comfirmEmail', false);
        }
        else {
            vm.employerSignupForm.confirmEmail.$setValidity('comfirmEmail', true);
        }
    }
    //ehecking if two Passwords are same 
    vm.confirmPassword = function () {
        if (vm.signUpModel.Password != vm.signUpModel.ConfirmPassword) {
            vm.employerSignupForm.confirmPassword.$setValidity('confirmPassword', false);
        }
        else {
            vm.employerSignupForm.confirmPassword.$setValidity('confirmPassword', true);
        }
    }
    // toggle job industry type
    vm.toggleIndustry = function (isTrue, industry) {
        let index = vm.signUpModel.IndustryList.indexOf(industry);
        if (isTrue && index < 0) {
            vm.signUpModel.IndustryList.push(industry);
        } else {
            vm.signUpModel.IndustryList.splice(index, 1);
        }
    }

    vm.addAddress = function(address){
        if(address){            
            let index = vm.signUpModel.AddressList.indexOf(address);
            if(index < 0){
                vm.signUpModel.AddressList.unshift(address);
                vm.address = '';
            }else{
                alert('Already added!');
            }
        }
    }

     vm.removeAddress = function(index){
        vm.signUpModel.AddressList.splice(index, 1);
    }
}



export default {
    name: 'employerSignupController',
    fn: employerSignupController
}