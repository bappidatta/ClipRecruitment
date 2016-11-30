function locationService($location) {
  'ngInject';

  const service = {};
  service.redirectTo = function(path){
    //   window.location = path;
    $location.path(path);
  }

  return service;

}

export default {
  name: 'locationService',
  fn: locationService
};
