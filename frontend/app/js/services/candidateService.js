function candidateService($http) {
    'ngInject';
    var serviceBase = 'http://localhost:57154/';
    const service = {

    };

    service.signUp = function (userInfo) {
        return $http.post(serviceBase + 'api/Candidate/SignUp/', userInfo);
    }

    return service;
}


export default {
    name: 'candidateService',
    fn: candidateService
}