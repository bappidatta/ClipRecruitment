function candidateService($http, AppSettings) {
    'ngInject';    
    const service = {};

    service.signUp = function (userInfo) {
        return $http.post(AppSettings.apiUrl + 'api/Candidate/SignUp/', userInfo);
    }

    service.applyForJobs = function (selectedJobs) {
        return $http.post(AppSettings.apiUrl + 'api/Candidate/ApplyToJobs/', selectedJobs);
    }


    return service;
}


export default {
    name: 'candidateService',
    fn: candidateService
}