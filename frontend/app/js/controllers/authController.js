function authController(authService, localStorageService, $rootScope){
    'ngInject';
    
    const vm = this;
    vm.userInfo = {};
    
    
    vm.signIn = function(userInfo){
        authService.signIn(userInfo).then(function(res){
            localStorage.setItem('authData', {token: res.access_token, userName: userInfo.userName});
                 authService.authData.isAuth = true;
                 authService.authData.userName = userInfo.userName;
                 localStorageService.set({token: res.data.access_token, userName: userInfo.userName});
                 $rootScope.userName = userInfo.userName;
                 sessionStorage.setItem('token', res.data.access_token);
                 console.log(res);                 
        });
    }

    vm.signOut = function(){
        authService.signOut().then(function(){
            sessionStorage.removeItem('token');  
            console.log(sessionStorage.getItem('token'));          
        });
    }
}

export default {
    name: 'authController',
    fn: authController
}