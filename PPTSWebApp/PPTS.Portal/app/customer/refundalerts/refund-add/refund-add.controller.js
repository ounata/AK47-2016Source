define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.refundAlertDataService],
        function (customer) {
            customer.registerController('refundAlertAddController', [
                '$scope', '$stateParams', '$uibModalInstance', 'refundAlertDataViewService', 'mcsDialogService',
                function ($scope, $stateParams, $uibModalInstance, refundAlertDataViewService, mcsDialogService) {
                    var vm = this;

                    // 页面初始化加载
                    (function () {
                        refundAlertDataViewService.initCreateRefundAlertInfo(vm, $stateParams, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('Canceled');
                    };

                    vm.save = function () {
                        vm.criteria.customerID = $stateParams.id;
                        refundAlertDataViewService.createRefundAlertInfo(vm, function () {
                            $uibModalInstance.close();
                        });
                    };
                }]);
        });