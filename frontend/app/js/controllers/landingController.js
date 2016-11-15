function landingController(){
    'ngInject';
    const vm = this;

    vm.currIndex = null;
    vm.prevIndex = null;

    vm.selectedJobs = [];

    vm.jobPostings = [
        {
            jobID: 1,
            title: 'Paralegal',
            shortDescription: 'A Commercial Prpoerty lawyer required to join a leading UK Law firm.',
            longDescription: 'dkdkd',
            salaryRange: {
                from: 15000,
                to: 20000
            }
        },
        {
            jobID: 2,
            title: 'Paralegal',
            shortDescription: 'A Commercial Prpoerty lawyer required to join a leading UK Law firm.',
            longDescription: 'Blake Morgan is one of the UK\'s leading full service law firms, with offices in London, Wales, Thames Valley and the South East. As a key client of Hays Lega, we are supporting Blake Morgan with this exciting opportunity that has arisen for a Senior Commercial Property Lawyer/Solicitor to be based in their Southampton offices in Hampshire.',
            salaryRange: {
                from: 15000,
                to: 20000
            }            
        },
         {
             jobID: 3,
            title: 'Paralegal',
            shortDescription: 'A Commercial Prpoerty lawyer required to join a leading UK Law firm.',
            longDescription: 'Blake Morgan is one of the UK\'s leading full service law firms, with offices in London, Wales, Thames Valley and the South East. As a key client of Hays Lega, we are supporting Blake Morgan with this exciting opportunity that has arisen for a Senior Commercial Property Lawyer/Solicitor to be based in their Southampton offices in Hampshire.',
            salaryRange: {
                from: 15000,
                to: 20000
            }
        },
         {
             jobID: 4,
            title: 'Paralegal',
            shortDescription: 'A Commercial Prpoerty lawyer required to join a leading UK Law firm.',
            longDescription: 'Blake Morgan is one of the UK\'s leading full service law firms, with offices in London, Wales, Thames Valley and the South East. As a key client of Hays Lega, we are supporting Blake Morgan with this exciting opportunity that has arisen for a Senior Commercial Property Lawyer/Solicitor to be based in their Southampton offices in Hampshire.',
            salaryRange: {
                from: 15000,
                to: 20000
            }
        },
         {
             jobID: 5,
            title: 'Paralegal',
            shortDescription: 'A Commercial Prpoerty lawyer required to join a leading UK Law firm.',
            longDescription: 'Blake Morgan is one of the UK\'s leading full service law firms, with offices in London, Wales, Thames Valley and the South East. As a key client of Hays Lega, we are supporting Blake Morgan with this exciting opportunity that has arisen for a Senior Commercial Property Lawyer/Solicitor to be based in their Southampton offices in Hampshire.',
            salaryRange: {
                from: 15000,
                to: 20000
            }
        },

    ];

    //select jobs and push it to a collection
    vm.selectJobs = function(job){
        job.selected = !job.selected;
        let tempIndex = indexOfObjectInArray(vm.selectedJobs, 'jobID', job.jobID);
        if(tempIndex > -1){
            vm.selectedJobs.splice(tempIndex, 1);
        }
        else{
            vm.selectedJobs.push(job);
        }
        console.log(vm.selectedJobs);
    }

    //apply to selected jobs if logged in 
    vm.applyToSelectedJobs = function(){
        if(vm.selectedJobs.length < 1){
            alertify.set('notifier','position', 'top-right'); 
            alertify.warning("No Job Selected");
        }
        console.log(vm.selectedJobs);
    }

    
    //returns index of given object if found in given array by comparing the given value for given perperty in that object, -1 otherwise
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
    name: 'landingController',
    fn: landingController
};