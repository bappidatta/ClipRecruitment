function commonService($http) {
  'ngInject';
  var serviceBase = 'http://localhost:57154/';
  const service = {};
  
  service.isReal = function(item){
        return (!undefined && item != null && item != '' );
    }

  service.indexOfObjectInArray = function(array, property, value) {
        for (var i = 0; i < array.length; i++) {
            if (array[i][property] == value) {
                return i;
            }
        }
        return -1;
    }


   
  service.getLocations = function(val){
    return $http.get(serviceBase + 'api/Common/GetLocations/', {params: {inputString: val}});
  }

  service.getPositions = function(val){
    return $http.get(serviceBase + 'api/Common/GetPositions/', {params: {inputString: val}});
  }

   service.getSkills = function(val){
    return $http.get(serviceBase + 'api/Common/GetSkills/', {params: {inputString: val}});
  }


  service.getUser = function(userName){
    return $http.get(serviceBase + 'api/Common/GetUser/', {params: {userName}});
  }
  
  return service;
  
} // end of service

    


export default {
  name: 'commonService',
  fn: commonService
};
