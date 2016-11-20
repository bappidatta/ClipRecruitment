function jobSearchController(jobService)
{
    'ngInject';
    const vm = this;
    vm.jobList = [];
    vm.selectedJobs = [];
    vm.searchResult = [];

    vm.industryTypes = [{id: 1, name: 'Industry 1'},{id: 2, name: 'Industry 2'},{id: 3, name: 'Industry 3'},
                        {id: 4, name: 'Industry 4'},{id: 5, name: 'Industry 5'},{id: 6, name: 'Industry 6'}];

    vm.insolvencyTypes = [{id: 1, name: 'Insolvency 1'},{id: 2, name: 'Insolvency 2'},{id: 3, name: 'Insolvency 3'},
                        {id: 4, name: 'Insolvency 4'},{id: 5, name: 'Insolvency 5'},{id: 6, name: 'Insolvency 6'}];

    jobService.getAllJob(0).then(function(res){
        vm.searchResult = res.data.Success;
        console.log(res.data);
    });



    vm.searchCriteria = {
        IndustryID: 0,
        InsolvencyID: 4,
        Position: [],
        Experience: [],
        IsFullTime: false,
        IsPermanent: false,
        IsRemote: false,
        Location: [],
        SalaryFrom: 0,
        SalaryTo: 0
    }

    vm.onIndustryChange = function(){        
        searchJobs(vm.searchCriteria);     
    }

    vm.onSolvencyChange = function(){

    }

    vm.addPositionExperience = function(){

    }

    vm.addLocation = function(){

    }

    

    function searchJobs(searchCriteria){
        jobService.searchJobs(searchCriteria).then(function(res){
            console.log(res.data.Success);
             vm.searchResult = res.data.Success;
        })
    }

    vm.selectJobs = function(job){
        job.selected = !job.selected;
        var index = indexOfObjectInArray(vm.selectedJobs, '_id', job._id);

        if( index > -1){
            console.log('found!');
        }
        else{
            console.log('not found!');
        }
        
    }

    function indexOfObjectInArray(array, property, value) {
    for (var i = 0; i < array.length; i++) {
        if (array[i][property] == value) {
            return i;
        }
    }
    return -1;
}

}

export default{
    name: 'jobSearchController',
    fn: jobSearchController
};