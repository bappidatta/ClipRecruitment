function authController(authService){
    'ngInject';
    
    const vm = this;
    vm.userInfo = {};
    
    
    vm.signIn = function(){
        authService.signIn(vm.userInfo).then(function(res){
            localStorage.setItem('authData', {token: res.access_token, userName: userInfo.userName});
                 service.authData.isAuth = true;
                 service.userName = userInfo.userName;
        });
    }
}


export default {
    name: 'authController',
    fn: authController
}