function appController(){
    'ngInject';

console.log("appController");
    const vm = this;
    vm.user = {
        isAuthenticated: false,
        userRole: 'anonymous',
        signIn: vm.signIn,
        signOut: vm.signOut,
        signUp: vm.signUp,

    };
    

// signup user 
    vm.signIn = function(){

    }

    vm.signUp = function(){

    }

    vm.signOut = function(){

    }
}

export default{
    name: 'appController',
    fn: appController
}