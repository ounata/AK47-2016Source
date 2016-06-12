define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.refundAlertDataService],
        function (customer) {
            customer.registerController('refundAlertEditController', [
                '$scope', '$stateParams', '$uibModalInstance', 'refundAlertDataViewService', 'mcsDialogService', 'data', 'mcsValidationService',
                function ($scope, $stateParams, $uibModalInstance, refundAlertDataViewService, mcsDialogService, data, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    vm.alertID = data.alertID;
                    // 页面初始化加载
                    (function () {
                        refundAlertDataViewService.initEditRefundAlertInfo(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('Canceled');
                    };

                    vm.save = function () {
                        if (mcsValidationService.run($scope)) {
                            refundAlertDataViewService.createRefundAlertInfo(vm, function () {
                                $uibModalInstance.close();
                            });
                        }

                    };
                }]);
        });