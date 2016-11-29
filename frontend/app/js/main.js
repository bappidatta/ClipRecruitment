import angular from 'angular';

// angular modules
import constants from './constants';
import onConfig  from './on_config';
import onRun     from './on_run';
import 'angular-animate';
import 'angular-sanitize';
import 'angular-ui-router';
import 'angular-ui-grid';
import 'angular-ui-bootstrap';
import 'angular-ui-bootstrap-datetimepicker';
import 'ng-alertify';
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
  'Alertify',
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
                if ($window.sessionStorage.token) {
                    config.headers.Authorization = 'Bearer ' + $window.sessionStorage.token;
                } else {
                  console.log($location.$$url);
                    if ($location.$$url == '/signup') {
                        $location.path('/signup');
                    }else if($location.$$url == '/' || $location.$$url == '/landing'){
                        $location.path('/landing');
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

