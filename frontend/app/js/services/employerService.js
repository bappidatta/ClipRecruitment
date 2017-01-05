function employerService($http, AppSettings) {
    'ngInject';    
    const service = {};


    service.signUp = function (userInfo) {
        return $http.post(AppSettings.apiUrl + 'api/Employer/SignUp/', userInfo);
    }
    
    return service;
}


export default {
    name: 'employerService',
    fn: employerService
}