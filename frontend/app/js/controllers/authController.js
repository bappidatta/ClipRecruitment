function authController(authService) {
    'ngInject';
    const vm = this;  
    // default for testing  
    vm.userInfo = {
        userName: 'contact@testcompany.com',
        password: 'Hello123.'
    };


    vm.signIn = function (userInfo) {        
        authService.signIn(userInfo);

    }
}

export default {
    name: 'authController',
    fn: authController
}