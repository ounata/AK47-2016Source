define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.studentDataService],
    function (customer) {
        customer.registerController('studentTransferController', [
            '$scope',
            '$uibModalInstance',
            'studentDataViewService',
            'data',
            function ($scope, $uibModalInstance, studentDataViewService, data) {
                var vm = this;

                studentDataViewService.getTransferStudentInfo(vm, data);

                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                vm.transfer = function () {
                    //...
                };
            }]);
    });