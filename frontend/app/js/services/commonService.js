function commonService() {
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

   
  return service;
  
} // end of service

    


export default {
  name: 'commonService',
  fn: commonService
};
