function OnConfig($stateProvider, $locationProvider, $urlRouterProvider, $compileProvider) {
  'ngInject';

  if (process.env.NODE_ENV === 'production') {
    $compileProvider.debugInfoEnabled(false);
  }

  $locationProvider.html5Mode({
    enabled: true,
    requireBase: false
  });

  $stateProvider
  .state('Home', {
    url: '/',
    controller: 'landingController as landing',
    templateUrl: 'landing.html',
    title: 'Home'
  })
  .state('Search Jobs', {
    url: '/Search-Jobs', 
    controller: 'jobSearchController as jobSearch',
    templateUrl: 'search-jobs.html',
    title: 'Jobs'
  })
  .state('Search Candidate Video', {
    url: '/Search-Candidate-Video',
    controller:'candidateVideoProfileSearchController as candidateVideoProfileSearch',
    templateUrl: 'search-candidate-video.html',
    title: 'Index'
  })
  .state('Search Candidate Profile', {
    url: '/Search-Candidate-Profile',
    templateUrl: 'search-candidate-profile.html',
    title: 'Dashboard'
  })
  .state('Create', {
    url: '/create',
    templateUrl: 'create.html',
    title: 'Title'
  })


  // .state('Test',{
  //   url: '/test',
  //   controller: 'TestCtrl as test',
  //   templateUrl: 'test.html',
  //   title: 'Test'
  // });

  $urlRouterProvider.otherwise('/');

}

export default OnConfig;
