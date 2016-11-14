function TestCtrl(ExampleService) {
    'ngInject';
    // ViewModel
    const vm = this;

    vm.title = 'qwqw';
    vm.desc = 'asdfasdfasdf';
    vm.serviceText = ExampleService.test();
    vm.serviceText2 = ExampleService.test2();

    vm.myData = [
        {
            'firstName': 'Cox',
            'lastName': 'Carney',
            'company': 'Enormo',
            'employed': true
        },
        {
            'firstName': 'Lorraine',
            'lastName': 'Wise',
            'company': 'Comveyer',
            'employed': false
        },
        {
            'firstName': 'Nancy',
            'lastName': 'Waters',
            'company': 'Fuelton',
            'employed': false
        }
    ];

}

export default {
    name: 'TestCtrl',
    fn: TestCtrl
};