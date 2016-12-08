function jobService($http) {
  'ngInject';
  var serviceBase = 'http://localhost:57154/';
  const service = {};
  service.selectedJobs = [];
  service.searchCriteria = null;
  
  service.getAllJob = function(pageNo){
      return $http.get( serviceBase + 'api/Job/GetAllJob', {params: {pageNo: pageNo}});
  };
  
  service.searchJobs = function(searchCriteria, pageNo){
    return $http.post( serviceBase + 'api/Job/SearchJob?pageNo='+pageNo,searchCriteria);
  };

  service.createJob = function(job){
      return $http.post(serviceBase + 'api/Job/CreateJob/', job);
  };

  service.updateJob = function(job){
      return $http.post(serviceBase + 'api/Job/Update/', job);
  };

  service.isApplied = function(jobId){
    return $http.get(serviceBase + 'api/Job/IsApplied/', {params: {jobId: jobId}});
  }




  return service;

}

export default {
  name: 'jobService',
  fn: jobService
};
