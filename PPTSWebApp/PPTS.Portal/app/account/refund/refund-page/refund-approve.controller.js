define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountRefundDataService],
        function (account) {
            account.registerController('accountRefundApproveController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountRefundDataService',
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
                        accountDataService.getRefundApplyByWorkflow(vm.wfParams, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.clientProcess = result.clientProcess;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                }]);
        });