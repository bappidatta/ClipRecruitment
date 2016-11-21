function jobService($http) {
  'ngInject';

  const service = {};
  
  service.getAllJob = function(pageNo){
      return $http.get('http://localhost:57154/api/Job/GetAllJob', {params: {pageNo: pageNo}});
  };

  
  service.searchJobs = function(searchCriteria){
    return $http.post('http://localhost:57154/api/Job/SearchJob/', searchCriteria);
  };


  service.getLocations = function(val){
    return $http.get('http://localhost:57154/api/Job/GetLocations/', {params: {inputString: val}});
  }

 service.getPositions = function(val){
    return $http.get('http://localhost:57154/api/Job/GetPositions/', {params: {inputString: val}});
  }


  return service;

}

export default {
  name: 'jobService',
  fn: jobService
};
