define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.refundAlertDataService],
        function (customer) {
            customer.registerController('refundAlertEditController', [
                '$scope', '$stateParams', '$uibModalInstance', 'refundAlertDataViewService', 'mcsDialogService','data',
                function ($scope, $stateParams, $uibModalInstance, refundAlertDataViewService, mcsDialogService, data) {
                    var vm = this;
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
                      //  vm.criteria.customerID = $stateParams.id;
                        refundAlertDataViewService.createRefundAlertInfo(vm, function () {
                            $uibModalInstance.close();
                        });
                    };
                }]);
        });