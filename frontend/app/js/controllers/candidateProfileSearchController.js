function candidateProfileSearchController(candidateProfileSearchService, commonService, $sce) {
    'ngInject';    
    const vm = this;
    vm.searchCriteria = {
        isVideoProfileSearch: false,
        Profile: '',
        PositionList: [],
        LocationList: [],
        IsFullTime: true,
        IsPermanent: true,
        IsPartTime: true,
        IsTemporary: true,
        IsRemote: true,
        IsLocum: true,
        ExpectedSalaryFrom: 0,
        ExpectedSalaryTo: 0,
        Skills: []
    }

    vm.CandidateList = [];
    vm.searchResultFound = 0;

    vm.trustSrc = function (src) {
        return $sce.trustAsResourceUrl(src);
    };

    vm.init = function () {
        candidateProfileSearchService.getAllCandidates(0).then(function (res) {
            console.log(res.data);
            
            vm.CandidateList = res.data.Success;
            vm.searchResultFound = vm.CandidateList.length;
        });
    }

    /**
     * Search Function
     */
    vm.search = function () {
        candidateProfileSearchService.searchCandidates(vm.searchCriteria).then(function (res) {
            vm.CandidateList = res.data.Success;            
            vm.searchResultFound = vm.CandidateList.length;
            console.log(vm.CandidateList);
        });
    }
    /**
     * Get All Positions
     */
    vm.getPositions = function (viewValue) {
        if (viewValue != null && viewValue != '') {
            return commonService.getPositions(viewValue).then(function (res) {
                return res.data.Success;
            });
        }
    }

    /**
     * Add Position
     */
    vm.addPosition = function (position) {
        if (position) {
            let index = vm.searchCriteria.PositionList.indexOf(position);
            if (index < 0) {
                vm.searchCriteria.PositionList.push(position);
                vm.position = '';
                vm.search();
            } else {
                alert('Already Added!');
                vm.position = '';
            }
        }
    }

    /**
     * remove position from position list
     */
    vm.removePosition = function (index) {
        vm.searchCriteria.PositionList.splice(index, 1);
        vm.search();
    }

    /**
     * Get Filtered Locations
    */
    vm.getLocations = function (viewValue) {
        if (viewValue != null && viewValue != '') {
            return commonService.getLocations(viewValue).then(function (res) {
                return res.data.Success;
            });
        }
    }

    /**
     * Add Location
     */
    vm.addLocation = function (location) {
        if (location) {
            let index = vm.searchCriteria.LocationList.indexOf(location);
            if (index < 0) {
                vm.searchCriteria.LocationList.push(location);
                vm.location = '';
                vm.search();
            } else {
                alert('Already Added!');
                vm.location = '';
            }
        }
    }

    /**
     * remove location from Search Criteria Location list
     */
    vm.removeLocation = function (index) {
        vm.searchCriteria.LocationList.splice(index, 1);
        vm.search();
    }


    vm.onCheckBoxSelectionChange = function () {
        vm.search();
    }

    vm.onFromSlarayChange = function () {
        //call search function
        if (!isNaN(vm.searchCriteria.ExpectedSalaryFrom)) {
            var salaryFrom = parseFloat(vm.searchCriteria.ExpectedSalaryFrom);
            var salaryTo = parseFloat(vm.searchCriteria.ExpectedSalaryTo);
            if (salaryTo != 0 && salaryFrom > salaryTo) {
                vm.searchCriteria.ExpectedSalaryFrom = 0;
            } else {
                vm.search();
            }
        } else {
            vm.searchCriteria.ExpectedSalaryFrom = 0;
        }
    }

    vm.onToSlarayChange = function () {
        //call search function
        if (!isNaN(vm.searchCriteria.ExpectedSalaryTo)) {
            var salaryTo = parseFloat(vm.searchCriteria.ExpectedSalaryTo);
            var salaryFrom = parseFloat(vm.searchCriteria.ExpectedSalaryFrom);
            if (salaryFrom != 0 && salaryTo < salaryFrom) {
                vm.searchCriteria.ExpectedSalaryTo = 0;
            } else {
                vm.search();
            }
        } else {
            vm.searchCriteria.ExpectedSalaryTo = 0;
        }
    }


}

export default {
    name: 'candidateProfileSearchController',
    fn: candidateProfileSearchController
};