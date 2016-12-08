function candidateService($http) {
    'ngInject';
    var serviceBase = 'http://localhost:57154/';
    const service = {

    };

    service.signUp = function (userInfo) {
        return $http.post(serviceBase + 'api/Candidate/SignUp/', userInfo);
    }

    service.applyForJobs = function (selectedJobs) {
        return $http.post(serviceBase + 'api/Candidate/ApplyToJobs/', selectedJobs);
    }


    return service;
}


export default {
    name: 'candidateService',
    fn: candidateService
}