function employerService($http) {
    'ngInject';
    var serviceBase = 'http://localhost:57154/';
    const service = {

    };


    service.signUp = function (userInfo) {
        return $http.post(serviceBase + 'api/Employer/SignUp/', userInfo);
    }
    
    return service;
}


export default {
    name: 'employerService',
    fn: employerService
}