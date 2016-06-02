define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerTransferController', [
            '$scope',
            '$uibModalInstance',
            'customerDataService',
            'customerDataViewService',
            'data',
            'mcsValidationService',
            function ($scope, $uibModalInstance, customerDataService, customerDataViewService, data, mcsValidationService) {
                var vm = this;
                vm.customers = data.customers;
                mcsValidationService.init($scope);
                customerDataViewService.getTransferCustomerInfo(vm, data);

                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                vm.transfer = function () {
                    if (mcsValidationService.run($scope)) {
                        var transferResourcesList = [];
                        for (var index in vm.customers) {
                            var trCus = {
                                orgID: data.customers[index].orgID,
                                orgName: data.customers[index].orgName,
                                orgType: data.customers[index].orgType,
                                customerID: data.customers[index].customerID,
                                transferMemo: data.customers[index].transferMemo,
                                toOrgID: "" == vm.transferResources.campus ? vm.transferResources.branch : vm.transferResources.campus,
                                toOrgName: ""==vm.transferResources.campus ? vm.transferResources.selected.value : vm.transferResources.selected.value[1],
                                toOrgType: '暂未提供'
                            };
                            transferResourcesList.push(trCus);
                        }
                        customerDataService.transferCustomerTransferResources({ customerTransferResources: transferResourcesList }, function (result) {
                            $uibModalInstance.close(data.customers);
                        });
                    }
                };
            }]);
    });