define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountRefundDataService],
        function (account) {
            account.registerController('accountRefundVerifyController', [
                '$scope', '$state', '$stateParams', '$uibModalInstance', 'accountRefundDataService', 'data',
                function ($scope, $state, $stateParams, $uibModalInstance, accountDataService, data) {
                    var vm = this;
                    vm.applyID = data.applyID;
                    vm.actionName = data.actionName;

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getRefundApplyByApplyID(vm.applyID, function (result) {
                            vm.apply = result.apply

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