function candidateVideoProfileSearchController(candidateVideoProfileSearchService, commonService, $sce, AppSettings) {
    'ngInject';
    
    const vm = this;

    vm.searchCriteria = {
        isVideoProfileSearch: true,
        Profile: '',
        PositionList: [],
        LocationList: [],
        IsFullTime: true,
        IsPermanent: true,
        Skills: []
    }

    vm.CandidateList = [];
    vm.searchResultFound=0;

    vm.trustSrc = function (src) {
        let base = AppSettings.apiUrl + 'api/Candidate/ClipStream?fileName=';        
        return $sce.trustAsResourceUrl(base+src);
    };

    vm.init = function () {
        candidateVideoProfileSearchService.getAllCandidates(0).then(function (res) {
            vm.CandidateList = res.data.Success;
            console.log(res.data);
            vm.searchResultFound = vm.CandidateList.length;
        });        
    }

    vm.video = false;
    vm.fetchVideo = function(fileName){
        console.log(fileName);
        candidateVideoProfileSearchService.fetchVideo(fileName).then(function(res){
            console.log(res);
        })
    }

/*
 * Search based on filtering Criteria 
 */
    vm.search = function () {
        candidateVideoProfileSearchService.searchCandidates(vm.searchCriteria).then(function (res) {
            vm.CandidateList = res.data.Success;
            vm.searchResultFound = vm.CandidateList.length;
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

    /**
     * Get Filtered Skills
     */
    vm.getSkills = function (viewValue) {
        if (viewValue != null && viewValue != '') {
            return commonService.getSkills(viewValue).then(function (res) {
                return res.data.Success;
            });
        }
    }

    /**
     * Add Location
     */
    vm.addSkill = function (skill) {
        if (skill) {
            let index = vm.searchCriteria.Skills.indexOf(skill);
            if (index < 0) {
                vm.searchCriteria.Skills.push(skill);
                vm.skill = '';
                vm.search();
            } else {
                alert('Already Added!');
                vm.skill = '';
            }
        }
    }

    /**
     * remove location from Search Criteria Location list
     */
    vm.removeSkill = function (index) {
        vm.searchCriteria.Skills.splice(index, 1);
        vm.search(); 
    }

    /**
     * On Check Uncheck Permanent Item
     */
    vm.onPermanentChange = function () {
        vm.search();
    }

    /**
     * On Check Uncheck Full Time
     */
    vm.onFullTimeChange = function () {
        vm.search();
    }

    vm.Reset = function(){
        vm.searchCriteria = {
        isVideoProfileSearch: true,
        Profile: '',
        PositionList: [],
        LocationList: [],
        IsFullTime: true,
        IsPermanent: true,
        Skills: []
    };
    vm.CandidateList = [];
    vm.init();
    }

}

export default {
    name: 'candidateVideoProfileSearchController',
    fn: candidateVideoProfileSearchController
};