define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargePrintController', [
                '$scope', '$state', '$stateParams', '$uibModalInstance', 'accountChargeDataService', 'data',
                function ($scope, $state, $stateParams, $uibModalInstance, accountChargeDataService, data) {
                    var vm = this;
                    vm.id = data.applyID;

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountChargeDataService.getChargeDisplayResultByApplyID4Payment(vm.id, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.items = result.apply.payment.items;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();
                    
                    //关闭窗口
                    vm.close = function () {
                        $uibModalInstance.dismiss('Canceled');
                    }
                }]);
        });

1