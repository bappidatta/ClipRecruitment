function ExampleService($http) {
  'ngInject';

  const service = {};

  service.get = function() {
    return new Promise((resolve, reject) => {
      $http.get('apiPath').success((data) => {
        resolve(data);
      }).error((err, status) => {
        reject(err, status);
      });
    });
  };

  service.test = function(){
    return 'testing service';
  };

  service.test2 = function(){
    return 'testing service 2';
  };

  return service;

}

export default {
  name: 'ExampleService',
  fn: ExampleService
};
