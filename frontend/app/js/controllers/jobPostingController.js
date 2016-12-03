function jobPostingController(jobService, commonService){
    'ngInject';
    const vm = this;
    vm.job = {
        Position: '',
        Industry: '',
        Insolvency: '',
        SalaryFrom: '',
        SalaryTo: '',
        Description: '',
        LocationList: [],
        SkillSet: [],
        IsFullTime: false,
        IsPartTime: false,
        IsTemporary: false,
        IsPermanent: false,
        IsLocal: false,
        IsRemote: false,
        YearOfExperience: '',
        isDeleted: false,
        EmployerID: '',
        EducationLevel: ''
    };


    vm.jobPositions = ['Software Developer', 'Lawer','Software Engineer',
        'Project Manager','Clerk', 'Nurse', 'Doctor', 'Math Teacher', 'Scientist'
    ];
    vm.salaryFrom = [15000, 25000, 35000, 45000, 55000, 65000];
    vm.salaryTo = [25000, 35000, 45000, 55000, 65000, 75000, 85000, 95000];
    vm.jobExperiences = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    vm.educationLevels = [
        'Doctorate', 'Master\'s degree', 'Bachelor\'s degree', 'Degree',
        'PG Diploma', 'Advanced Diploma', 'Diploma', 'Grade X', 
        'Grade IX', 'Grade VIII', 'Grade V'
    ];

    vm.user = {};

    vm.init = function(){
        commonService.getUser(localStorage.getItem('userName')).then(function(res){
            vm.user = res.data.Success;
        });
    }


    vm.createJob = function(){
        if(vm.jobPostingForm.$valid){
            jobService.createJob(vm.job).then(function(res){
                if(res.data.Success){
                    alert('Job Created!');
                }else{
                    alert(res.data.Error);
                }
            })
        }
        //console.log(vm.job);
    }

    vm.isValidJobType = function(){
        if((vm.job.IsFullTime || vm.job.IsPartTime) && (vm.job.IsPermanent || vm.job.IsTemporary)){
            return true;
        }
        return false;
    }

    vm.isValidSalary = function(){
        if(vm.job.SalaryTo >= vm.job.SalaryFrom){
            vm.jobPostingForm.salaryTo.$invalid = false;
            vm.jobPostingForm.salaryTo.$valid = true;
        }else{
            vm.jobPostingForm.salaryTo.$invalid = true;
            vm.jobPostingForm.salaryTo.$valid = false;
        }
    }

     vm.getLocations = function(viewValue){
       if(viewValue != null && viewValue != ''){
            return commonService.getLocations(viewValue).then(function(res){                
                return res.data.Success;                    
        });
       }
    }

    vm.addLocation = function(location){
        if(location){            
            let index = vm.job.LocationList.indexOf(location);
            if(index < 0){
                vm.job.LocationList.unshift(location);
                vm.location = '';
            }else{
                alert('Already added!');
            }
        }        
    }

    vm.removeLocation = function(index){
        vm.job.LocationList.splice(index, 1);
    }

    vm.getSkills = function(viewValue){
       if(viewValue != null && viewValue != ''){
            return commonService.getPositions(viewValue).then(function(res){                
                return res.data.Success;              
        });
       }
    }

    vm.addSkill = function(skill){
        if(skill){            
            let index = vm.job.SkillSet.indexOf(skill);
            if(index < 0){
                vm.job.SkillSet.unshift(skill);
                vm.skill = '';
            }else{
                alert('Already added!');
            }
        }        
    }

    vm.removeSkill = function(index){
        vm.job.SkillSet.splice(index, 1);
    }
        
    vm.resetJob = function(){
        vm.job = {
            LocationList: [],
            SkillSet: [],
        }
    }

    vm.resetForm = function(){
        vm.jobPostingForm.$setPristine();
        vm.jobPostingForm.$setUntouched();
        vm.resetJob();
    }
}

export default{
    name: 'jobPostingController',
    fn: jobPostingController
};