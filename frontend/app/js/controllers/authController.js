function authController(authService, $rootScope, $location){
    'ngInject';    
    const vm = this;
    //signalRService.init();
    vm.userInfo = {
        userName: 'contact@testcompany.com',
        password: 'Hello123.'
    };
    
    
    vm.signIn = function(userInfo){  
        if(!$rootScope.notifications){
                $rootScope.notifications = [];
            }
            authService.signIn(userInfo).then(function(res){
            authService.getUnreadNotifications(userInfo.userName).then(function(res){
                console.log(res.data.Success);                
                localStorage.setItem('notifications', JSON.stringify(res.data.Success));
                $rootScope.notifications = res.data.Success;
            });
            console.log(res);          
            localStorage.setItem('token', res.data.access_token);
            localStorage.setItem('userName', userInfo.userName);
            $rootScope.signOut = vm.signOut;

            if(localStorage.selectedJobs){
                $location.path('/Apply-Jobs');
            }
            $location.path('/landing');

        });
    }

    vm.signOut = function(){
            localStorage.removeItem('token');
            localStorage.removeItem('userName');
            $rootScope.userName = null;
            $rootScope.notifications = [];
            $location.path('/landing');
    }
}

export default {
    name: 'authController',
    fn: authController
}