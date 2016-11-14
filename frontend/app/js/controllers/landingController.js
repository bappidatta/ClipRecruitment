function landingController(){
    'ngInject';
    const vm = this;


    vm.jobPostings = [
        {
            title: 'Paralegal',
            shortDescription: 'A Commercial Prpoerty lawyer required to join a leading UK Law firm.',
            longDescription: 'dkdkd',
            salaryRange: {
                from: 15000,
                to: 20000
            }
        },
        {
            title: 'Paralegal',
            shortDescription: 'A Commercial Prpoerty lawyer required to join a leading UK Law firm.',
            longDescription: 'dkdkd',
            salaryRange: {
                from: 15000,
                to: 20000
            }
        }
    ];
}

export default{
    name: 'landingController',
    fn: landingController
};