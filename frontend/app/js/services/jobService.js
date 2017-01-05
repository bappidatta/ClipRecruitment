function jobService($http, AppSettings) {
  'ngInject';
  const service = {};
  service.selectedJobs = [];
  service.searchCriteria = null;
  
  service.getAllJob = function(pageNo){
      return $http.get( AppSettings.apiUrl + 'api/Job/GetAllJob', {params: {pageNo: pageNo}});
  };
  
  service.searchJobs = function(searchCriteria, pageNo){
    return $http.post( AppSettings.apiUrl + 'api/Job/SearchJob?pageNo='+pageNo,searchCriteria);
  };

  service.createJob = function(job){
      return $http.post(AppSettings.apiUrl + 'api/Job/CreateJob/', job);
  };

  service.updateJob = function(job){
      return $http.post(AppSettings.apiUrl + 'api/Job/Update/', job);
  };

  service.isApplied = function(jobId){
    return $http.get(AppSettings.apiUrl + 'api/Job/IsApplied/', {params: {jobId: jobId}});
  }




  return service;

}

export default {
  name: 'jobService',
  fn: jobService
};
