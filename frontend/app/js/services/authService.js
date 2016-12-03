function authService($http) {
    'ngInject';
    var serviceBase = 'http://localhost:57154/';
    const service = {

    };

    service.authData = {
        isAuth: false,
        userName: ''
    }

    service.signIn = function (userInfo) {
        var data = 'grant_type=password&username=' + userInfo.userName +
            '&password=' + userInfo.password;

        return $http({
            method: 'POST',
            url: serviceBase+'token',
            data: data,
            headers: { 
                'Content-Type': 'application/x-www-form-urlencoded'
             }
        });

        //return $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    }

    service.signUp = function (userInfo) {
        return $http.post(serviceBase + 'api/Account/Register', userInfo);
    }

    service.signOut = function () {        
        return $http.get(serviceBase + 'api/Account/SignOut/');
    }


    return service;
}


export default {
    name: 'authService',
    fn: authService
}