define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountTransferDataService],
        function (account) {
            account.registerController('accountTransferApproveController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountTransferDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.id;

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getTransferApplyByApplyID(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.bizCustomer = result.bizCustomer;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    //模拟保存审批结果
                    vm.approve = function () {

                        accountDataService.approveTransferApply(vm.apply.applyID, 'approve', function () {
                            vm.init();
                        });
                    }

                    //模拟保存审批结果
                    vm.refuse = function () {

                        accountDataService.approveTransferApply(vm.apply.applyID, 'refuse', function () {
                            vm.init();
                        });
                    }
                }]);
        });