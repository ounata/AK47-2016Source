define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherCourseDataService],
        function (schedule) {
            schedule.registerController('tchCourseListController', [
            '$scope', 'teacherCourseDataService',
            function ($scope, teacherCourseDataService) {
                var vm = this;



            }]);
        });