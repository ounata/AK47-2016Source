define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.refundAlertDataService],
        function (customer) {
            customer.registerController('refundAlertAddController', [
                '$scope', '$stateParams', '$uibModalInstance', 'refundAlertDataViewService', 'mcsDialogService', 'mcsValidationService',
                function ($scope, $stateParams, $uibModalInstance, refundAlertDataViewService, mcsDialogService, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
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
                        if (mcsValidationService.run($scope)) {
                            vm.criteria.customerID = $stateParams.id;
                            refundAlertDataViewService.createRefundAlertInfo(vm, function () {
                                $uibModalInstance.close();
                            });
                        }
                    };
                }]);
        });