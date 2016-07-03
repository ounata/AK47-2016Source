define([ppts.config.modules.task], function (task) {
    task.registerController('workflowController', ['$scope', 'mcsWorkflowService', 'mcsValidationService', function ($scope, mcsWorkflowService, mcsValidationService) {
        var vm = this;

        vm.startupFreeSteps = function () {
            if (!mcsValidationService.run($scope)) return;
            mcsWorkflowService.startupFreeSteps(vm.startupFreeStepsParams);
        };
    }]);
});