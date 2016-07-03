define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountTransferDataService],
        function (account) {
            account.registerController('accountTransferApproveController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountTransferDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;

                    vm.wfParams = {
                        processID: $stateParams.processID,
                        activityID: $stateParams.activityID,
                        resourceID: $stateParams.resourceID
                    };

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getTransferApplyByWorkflow(vm.wfParams, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.bizCustomer = result.bizCustomer;
                            vm.clientProcess = result.clientProcess;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                }]);
        });