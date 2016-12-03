function localStorageService() {
  'ngInject';

  const service = {};
  var authData  = {        
        userName: ''
      };

  service.set = function(authData){            
      localStorage.setItem('userName', authData.userName);
  };

  service.get = function(){    
    authData.userName = localStorage.getItem('userName');  
    return authData;
  }

  return service;

}

export default {
  name: 'localStorageService',
  fn: localStorageService
};
