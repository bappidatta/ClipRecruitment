function authController(authService, localStorageService, locationService, $rootScope){
    'ngInject';
    
    const vm = this;
    vm.userInfo = {};
    
    
    vm.signIn = function(userInfo){        
        authService.signIn(userInfo).then(function(res){
            console.log(res);
            localStorage.setItem('authData', {token: res.access_token, userName: userInfo.userName});                
                 localStorageService.set({userName: userInfo.userName});
                 $rootScope.userName = userInfo.userName;
                 sessionStorage.setItem('token', res.data.access_token);
                 $rootScope.signOut = vm.signOut;
                 locationService.redirectTo('/landing');
        });
    }

    vm.signOut = function(){
            sessionStorage.removeItem('token');
            $rootScope.userName = '';
            locationService.redirectTo('/landing');
    }
}

export default {
    name: 'authController',
    fn: authController
}