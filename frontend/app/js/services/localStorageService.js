function localStorageService() {
  'ngInject';

  const service = {};
  var authData  = {
        token: '',
        userName: ''
      };

  service.set = function(authData){
      console.log(authData);
      localStorage.setItem('token', authData.token);
      localStorage.setItem('userName', authData.userName);
  };

  service.get = function(){
    authData.token = localStorage.getItem('token');
    authData.userName = location.getItem('userName');  
    return authData;
  }

  return service;

}

export default {
  name: 'localStorageService',
  fn: localStorageService
};
