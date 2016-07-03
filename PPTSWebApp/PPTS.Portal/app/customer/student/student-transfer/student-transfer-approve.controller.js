define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.studentDataService],
        function (account) {
            account.registerController('studentTransferApproveController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'studentDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, studentDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;

                    vm.wfParams = {
                        processID: $stateParams.processID,
                        activityID: $stateParams.activityID,
                        resourceID: $stateParams.resourceID
                    };

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        studentDataService.getStudentTransferApplyByWorkflow(vm.wfParams, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.clientProcess = result.clientProcess;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                }]);
        });