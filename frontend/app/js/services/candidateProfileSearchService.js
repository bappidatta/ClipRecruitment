function candidateProfileSearchService($http, AppSettings) {

  'ngInject';

  const service = {};
  
  service.getAllCandidates = function (pageNo) {
    return $http.get(AppSettings.apiUrl + 'api/Candidate/GetAllCandidates', { params: { pageNo: pageNo } });
  };

  service.searchCandidates = function (candidateVM) {
    return $http.post(AppSettings.apiUrl + 'api/Candidate/SearchCandidate',candidateVM);
  };

  return service;
}

export default {
  name: 'candidateProfileSearchService',
  fn: candidateProfileSearchService
};
