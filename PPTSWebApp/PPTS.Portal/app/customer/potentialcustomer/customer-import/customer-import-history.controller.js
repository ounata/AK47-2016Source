define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerImportHistoryController', [
            '$scope',
            '$uibModalInstance',
            'excelImportService',
            'mcsValidationService',
            'customerDataViewService',
            function ($scope, $uibModalInstance, excelImportService, mcsValidationService, customerDataViewService) {
                var vm = this;

                vm.init = function () {
                    customerDataViewService.configImportHistoryHeaders(vm);
                    customerDataViewService.getImportHistory(vm);
                };
                vm.init();
                
                // 关闭
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

            }]);
    });