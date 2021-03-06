import angular from 'angular';

// angular modules
import constants from './constants';
import onConfig  from './on_config';
import onRun     from './on_run';
import 'signalr';
import 'angular-toastr';
import 'angular-animate';
import 'angular-sanitize';
import 'angular-ui-router';
import 'angular-ui-grid';
import 'angular-ui-bootstrap';
import 'angular-ui-bootstrap-datetimepicker';
import 'ng-flow';
import 'bootstrap';
import './templates';
import './filters';
import './controllers';
import './services';
import './directives';


// create and bootstrap application
const requires = [
  'ngAnimate',
  'ngSanitize',
  'ui.router',
  'ui.grid',
  'ui.bootstrap',
  'ui.bootstrap.datetimepicker',
  'flow',
  'toastr',  
  'templates',
  'app.filters',
  'app.controllers',
  'app.services',
  'app.directives'
];

// mount on window for testing
window.app = angular.module('app', requires);

angular.module('app').constant('AppSettings', constants);

angular.module('app').factory('httpRequestInterceptor', [
    '$q',
    function ($q) {      
        return {
            'responseError': function (rejection) {
                return $q.reject(rejection);
            }
        };
    }
]);


angular.module('app').factory('authInterceptor', [
    '$rootScope',
    '$q',
    '$window',
    '$location',
    function ($rootScope, $q, $window, $location) {
        return {
            request: function (config) {
                config.headers = config.headers || {};
                if ($window.localStorage.token) {                    
                    config.headers.Authorization = 'Bearer ' + $window.localStorage.token;
                    $rootScope.userName = $window.localStorage.userName;
                    if(localStorage.notifications){
                       $rootScope.notifications = JSON.parse(localStorage.notifications);
                    }
                    $rootScope.signOut = function(){
                        $window.localStorage.removeItem('token');
                        $rootScope.userName = null;
                        $rootScope.notifications = [];
                        localStorage.removeItem('notifications');
                        $location.path('/landing');                        
                    }
                } else {                  
                    if ($location.$$url == '/Candidate-Signup') {
                        $location.path('/Candidate-Signup');
                    }else if($location.$$url == '/Employer-Signup'){
                        $location.path('/Employer-Signup');
                    }else if($location.$$url == '/' || $location.$$url == '/landing'){
                        $location.path('/landing');
                    }else if($location.$$url == '/Search-Jobs'){
                        $location.path('/Search-Jobs');
                    }
                    else {
                        $location.path('/SignIn');
                    }
                }
                return config;
            },
            response: function (response) {
                if (response.status === 401) {
                    // handle the case where the user is not authenticated
                }
                return response || $q.when(response);
            }
        };
    }
]);


angular.module('app').config(onConfig);

angular.module('app').run(onRun);

angular.bootstrap(document, ['app'], {
  strictDi: true
});

