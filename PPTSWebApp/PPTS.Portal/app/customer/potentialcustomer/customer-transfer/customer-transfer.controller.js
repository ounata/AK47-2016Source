define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerTransferController', [
            '$scope',
            '$uibModalInstance',
            'customerDataService',
            'customerDataViewService',
            'data',
            function ($scope, $uibModalInstance, customerDataService, customerDataViewService, data) {
                var vm = this;

                customerDataViewService.getTransferCustomerInfo(vm, data);

                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                vm.transfer = function () {
                    customerDataService.transferCustomers(vm.transferResources, function () {
                        $uibModalInstance.close(data.customers);
                    });
                };
            }]);
    });