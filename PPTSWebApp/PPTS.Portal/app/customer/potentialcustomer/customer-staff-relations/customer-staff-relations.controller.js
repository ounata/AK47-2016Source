define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerStaffRelationController', [
            '$scope',
            '$uibModalInstance',
            'customerDataViewService',
            'data',
            function ($scope, $uibModalInstance, customerDataViewService, data) {
                var vm = this;

                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                switch (data.type) {
                    case 'viewHistory':
                        // 页面初始化加载
                        customerDataViewService.configStaffRelationHeaders(vm);
                        customerDataViewService.getStaffRelationInfo(vm, data);
                        break;
                    case 'assignStaff':
                        customerDataViewService.getAssignStaffRelationInfo(vm, data);

                        vm.assign = function () {

                        };
                        break;
                }
            }]);
    });