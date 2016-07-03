define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerTeacherRelationController', [
            '$scope',
            '$uibModalInstance',
            'customerDataViewService',
            'data',
            'customerDataService',
            function ($scope, $uibModalInstance, customerDataViewService, data, customerDataService) {
                var vm = this;
                
                // init
                vm.init = function () {
                    vm.customerID = data.customerID;
                    customerDataViewService.configTeacherRelationsHeaders(vm);
                    customerDataViewService.getTeacherRelationsInfo(vm);
                };
                vm.init();

                // 关闭
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                
            }]);
    });