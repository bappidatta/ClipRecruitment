function commonService($http, AppSettings) {
  'ngInject';  
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
    return $http.get(AppSettings.apiUrl + 'api/Common/GetLocations/', {params: {inputString: val}});
  }

  service.getPositions = function(val){
    return $http.get(AppSettings.apiUrl + 'api/Common/GetPositions/', {params: {inputString: val}});
  }

   service.getSkills = function(val){
    return $http.get(AppSettings.apiUrl + 'api/Common/GetSkills/', {params: {inputString: val}});
  }

  service.getUser = function(userName){
    return $http.get(AppSettings.apiUrl + 'api/Common/GetUser/', {params: {userName}});
  }
  
  
    service.getUnreadNotifications = function(userName){
        return $http.get(AppSettings.apiUrl + 'api/Notification/GetNotificationListByUserId/', {params: {userId: userName}});
    }
    
  return service;
  
} // end of service

    


export default {
  name: 'commonService',
  fn: commonService
};
