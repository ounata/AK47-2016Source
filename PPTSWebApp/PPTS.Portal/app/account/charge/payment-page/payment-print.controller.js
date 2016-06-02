define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargePaymentPrintController', [
                '$scope', '$state', '$stateParams', '$uibModalInstance','printService', 'accountChargeDataService', 'data',
                function ($scope, $state, $stateParams, $uibModalInstance, printService, accountDataService, data) {
                    var vm = this;
                    vm.payID = data.payID;

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getChargeApplyByPayID(vm.payID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            for (var i = 0; i < result.apply.payment.items.length; i++) {
                                if (result.apply.payment.items[i].payID.toLowerCase() == vm.payID.toLowerCase())
                                    vm.item = result.apply.payment.items[i];
                            }

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    //保存打印状态
                    vm.save = function () {
                        printService.print();
                        $uibModalInstance.close();
                    }

                    //关闭窗口
                    vm.close = function () {
                        $uibModalInstance.dismiss('Canceled');
                    }
                }]);
        });