define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountRefundDataService],
        function (account) {
            account.registerController('accountRefundPrintController', [
                '$scope', '$state', '$stateParams', '$uibModalInstance', 'accountRefundDataService', 'data',
                function ($scope, $state, $stateParams, $uibModalInstance, accountDataService, data) {
                    var vm = this;
                    vm.applyID = data.applyID;

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getRefundApplyByApplyID(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    //保存打印状态
                    vm.save = function () {
                        $uibModalInstance.close();
                    }

                    //关闭窗口
                    vm.close = function () {
                        $uibModalInstance.dismiss('Canceled');
                    }
                }]);
        });
