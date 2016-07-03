define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeAuditController', [
                '$scope', '$state', '$stateParams', '$uibModalInstance', 'accountChargeDataService', 'data',
                function ($scope, $state, $stateParams, $uibModalInstance, accountDataService, data) {
                    var vm = this;
                    vm.auditCount = data.auditCount;

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