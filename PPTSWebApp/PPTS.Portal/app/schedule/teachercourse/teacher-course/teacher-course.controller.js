/*教师课表*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherCourseDataService],
        function (schedule) {
            schedule.registerController('tchCourseListController', [
            '$scope', 'teacherCourseDataService', 'printService',
            function ($scope, teacherCourseDataService, printService) {
                var vm = this;

                vm.criteria = vm.criteria || {};
                vm.criteria.startTime = '';
                vm.criteria.endTime = '';
                vm.criteria.subject = '';
                vm.criteria.grade = '';
                vm.criteria.isFullTimeTeacher = '';
                vm.criteria.teacherName = '';
                vm.criteria.accountID = '';
                

                /*页面初始化加载或重新搜索时查询*/
                vm.init = function () {
                    teacherCourseDataService.initTchWeekCourse(vm.criteria, function (data) {
                        vm.result = data;
                        $scope.$broadcast('dictionaryReady');
                    });
                };
                vm.init();


                vm.search = function () {

                    teacherCourseDataService.getWeekCourse(vm.criteria, function (data) {
                        vm.teachersPrint = data.result;
                      
                    });
                };
             
                vm.print = function () {
                    printService.print();
                }

            }]);
        });