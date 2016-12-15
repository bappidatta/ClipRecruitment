function authService($http, $rootScope, $location) {
    'ngInject';
    var serviceBase = 'http://localhost:57154/';
    const service = {

    };

    service.isAuth = function () {
        if (localStorage.token) {
            return true;
        }
        return false;
    };


    service.signIn = function (userInfo) {
        var data = 'grant_type=password&username=' + userInfo.userName +
            '&password=' + userInfo.password;

        $http({
            method: 'POST',
            url: serviceBase + 'token',
            data: data,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }).then(function (res) {

            if (!res.data.access_token) {
                return;
            }

            if (!$rootScope.notifications) {
                $rootScope.notifications = [];
            }

            // get unread notifications 
            service.getUnreadNotifications(userInfo.userName).then(function (res) {
                localStorage.setItem('notifications', JSON.stringify(res.data.Success));
                $rootScope.notifications = res.data.Success;
            });

            // set login info    
            localStorage.setItem('token', res.data.access_token);
            localStorage.setItem('userName', userInfo.userName);

            // set logout function to rootscope 
            // this needed because we don't have any controller for navication area
            $rootScope.signOut = service.signOut;
            $location.path('/landing');
        })

        //return $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    }

    service.signOut = function () {
        localStorage.removeItem('token');
        localStorage.removeItem('userName');
        $rootScope.userName = null;
        $rootScope.notifications = [];
        $location.path('/landing');
    }


    service.getUnreadNotifications = function (userName) {
        return $http.get(serviceBase + 'api/Notification/GetNotificationListByUserId/', { params: { userId: userName } });
    }


    return service;
}


export default {
    name: 'authService',
    fn: authService
}