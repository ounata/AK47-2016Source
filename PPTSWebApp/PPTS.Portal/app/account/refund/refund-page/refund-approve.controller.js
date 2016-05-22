define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountRefundDataService],
        function (account) {
            account.registerController('accountRefundApproveController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountRefundDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.id;

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getRefundApplyByApplyID(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    //模拟保存审批结果
                    vm.approve = function () {
                        
                        accountDataService.approveRefundApply(vm.apply.applyID, 'approve', function () {
                            vm.init();
                        });
                    }

                    //模拟保存审批结果
                    vm.refuse = function () {

                        accountDataService.approveRefundApply(vm.apply.applyID, 'refuse', function () {
                            vm.init();
                        });
                    }
                }]);
        });