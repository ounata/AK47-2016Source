define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.stopAlertDataService],
        function (customer) {
            customer.registerController('stopAlertAddController', [
                '$scope', '$stateParams', '$uibModalInstance', 'stopAlertDataViewService', 'mcsDialogService', 'mcsValidationService',
                function ($scope, $stateParams, $uibModalInstance, stopAlertDataViewService, mcsDialogService, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    // 页面初始化加载
                    (function () {
                        stopAlertDataViewService.initCreateStopAlertInfo(vm, $stateParams, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('Canceled');
                    };

                    vm.save = function () {
                        if (mcsValidationService.run($scope)) {
                            vm.criteria.customerID = $stateParams.id;
                            stopAlertDataViewService.createStopAlertInfo(vm, function () {
                                $uibModalInstance.close();
                            });
                        }
                    };
                }]);
        });