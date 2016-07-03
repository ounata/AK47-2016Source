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
                        var selected = mcs.util.toArray(vm.transferResources.selected.value);
                        for (var index in vm.customers) {
                            var trCus = {
                                orgID:  vm.transferResources.branch,//data.customers[index].orgID,
                                orgName: selected[0],//data.customers[index].orgName,
                                orgType: 3,//data.customers[index].orgType,
                                customerID: data.customers[index].customerID,
                                transferMemo: data.customers[index].transferMemo,
                                toOrgID: vm.transferResources.campus,
                                toOrgName: selected[1],//vm.transferResources.campus ? selected[1] : selected[0],
                                toOrgType: '3',
                                IsCampus: ""!=vm.transferResources.campus ? true : false
                            };
                            transferResourcesList.push(trCus);
                        }
                        customerDataService.transferCustomerTransferResources({ customerTransferResources: transferResourcesList, messageType: vm.selectTypes }, function (result) {
                            $uibModalInstance.close(data.customers);
                        });
                    }
                };
            }]);
    });