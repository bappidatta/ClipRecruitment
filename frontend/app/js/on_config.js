function OnConfig($stateProvider, $locationProvider, $urlRouterProvider, $compileProvider, $httpProvider) {
  'ngInject';    
  $httpProvider.interceptors.push('httpRequestInterceptor');
  $httpProvider.interceptors.push('authInterceptor');
  if (process.env.NODE_ENV === 'production') {
    $compileProvider.debugInfoEnabled(false);
  }

  $locationProvider.html5Mode({
    enabled: true,
    requireBase: false
  });

  $stateProvider   
  .state('Landing', {
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
    controller:'candidateProfileSearchController as candidateProfileSearch',
    templateUrl: 'search-candidate-profile.html',
    title: 'Dashboard'
  })
  .state('Create', {
    url: '/create',
    templateUrl: 'create.html',
    title: 'Create'
  })
  .state('Login', {
    url: '/SignIn',
    templateUrl: 'sign-in.html',
    controller: 'authController as auth',
    title : 'SignIn'
  })
  .state('Job Posting', {
    url: '/Job-Posting',
    controller: 'jobPostingController as jobPosting',
    templateUrl: 'job-posting.html',
    title: 'Job Posting'
  })
  .state('Candidate Signup', {
    url: '/Candidate-Signup',    
    controller: 'candidateSignupController as cSignup',
    templateUrl: 'candidate-signup.html',
    title: 'Sign Up'
  })
  .state('Employer Signup', {
    url: '/Employer-Signup',
    controller: 'employerSignupController as empSignup',
    templateUrl: 'employer-signup.html',
    title: 'Sign Up'
  }).state('Job Application', {
    url: '/Apply-Jobs',
    controller: 'jobApplicationController as applyJobs',
    templateUrl: 'job-application.html',
    title: 'Apply Jobs'
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
