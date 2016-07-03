define([ppts.config.modules.task], function (task) {
    task.registerController('workflowApproveController', ['$scope', '$stateParams', 'mcsWorkflowService', function ($scope, $stateParams, mcsWorkflowService) {
        var vm = this;

        // 页面初始化加载
        (function () {
            vm.searchParams = {
                processID: $stateParams.processID,
                activityID: $stateParams.activityID,
                resourceID: $stateParams.resourceID
            };
  
            mcsWorkflowService.getForm(vm.searchParams, function (result) {
                //...
                vm.form = result.form;
                vm.clientProcess = result.clientProcess;
                $scope.$broadcast('dictionaryReady');
            });

        })();

        vm.callbacks = {
            moveTo: function () {
                alert('下一步');
            },
            save: function () {
                alert('保存');
            },
            cancel: function () {
                alert('驳回');
            },
            withdraw: function () {
                alert('撤回');
            }
        };
    }]);
});