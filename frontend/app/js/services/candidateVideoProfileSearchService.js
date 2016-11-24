function candidateVideoProfileSearchService($http) {

  'ngInject';

  const service = {};

  service.getAllCandidates = function (pageNo) {
    return $http.get('http://localhost:57154/api/Candidate/GetAllCandidates', { params: { pageNo: pageNo } });
  };

  service.searchCandidates = function (candidateVM) {
    return $http.post('http://localhost:57154/api/Candidate/SearchCandidate',candidateVM);
  };

  return service;
}

export default {
  name: 'candidateVideoProfileSearchService',
  fn: candidateVideoProfileSearchService
};
