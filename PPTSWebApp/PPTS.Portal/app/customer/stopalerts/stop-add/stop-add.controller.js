define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.stopAlertDataService],
        function (customer) {
            customer.registerController('stopAlertAddController', [
                '$scope', '$stateParams', '$uibModalInstance', 'stopAlertDataViewService', 'mcsDialogService',
                function ($scope, $stateParams, $uibModalInstance, stopAlertDataViewService, mcsDialogService) {
                    var vm = this;

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
                        vm.criteria.customerID = $stateParams.id;
                        stopAlertDataViewService.createStopAlertInfo(vm, function () {
                            $uibModalInstance.close();
                        });
                    };
                }]);
        });