function signalRService($rootScope, AppSettings){
    'ngInject';    
    return {
        init: function () {
            var token =  localStorage.token;
                 
            if(token){
            var connection = $.hubConnection(AppSettings.apiUrl + 'signalr', {useDefaultPath: false, qs: 'Bearer_Token='+ token});
            var hub = connection.createHubProxy('notification');
            //var connection = $.hubConnection(serviceBase);            
            // hub.on('GetAllJob', function (i) {
            //     console.log(i);
            //  $rootScope.$emit('GetAllJob', i);
            // });

            hub.on('onNewJobApplication', function (i) {
                if(!$rootScope.notifications){
                $rootScope.notifications = [];
            }
            $rootScope.notifications.push(i);
            $rootScope.$apply();
            localStorage.setItem('notifications', JSON.stringify($rootScope.notifications));            
            console.log($rootScope.notifications);              
            return;
            //$rootScope.$emit('onNewJobApplication', i);
             
            });

            // hub.on('sayHello', function(i){
            //     $rootScope.$emit('sayHello', i);
            // });

            console.log('initializing hub connection...');
            console.log(connection);
            connection.start(function(){                
                hub.invoke('onNewJobApplication');
            });
            }
        }
    }
}

export default {
    name: 'signalRService', 
    fn: signalRService
};
