function candidateVideoProfileSearchService($http,AppSettings) {

  'ngInject';

  const service = {};

  service.getAllCandidates = function (pageNo) {    
    return $http.get(AppSettings.apiUrl + 'api/Candidate/GetAllCandidates', { params: { pageNo: pageNo } });
  };

  service.searchCandidates = function (candidateVM) {
    return $http.post(AppSettings.apiUrl + 'api/Candidate/SearchCandidate',candidateVM);
  };

  service.fetchVideo = function(fileName){
    return $http.get(AppSettings.apiUrl + 'api/Candidate/ClipStream/', {params: {fileName: fileName}});
  }

  return service;
}

export default {
  name: 'candidateVideoProfileSearchService',
  fn: candidateVideoProfileSearchService
};
