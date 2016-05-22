define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (schedule) {
            schedule.registerController('stuAsgmtConditionController', [
                '$scope', '$state', 'dataSyncService', 'studentassignmentDataService', '$stateParams',
                function ($scope, $state, dataSyncService, studentassignmentDataService, $stateParams) {
                    var vm = this;
                    vm.customerID = $stateParams.id;


            }]);
        });