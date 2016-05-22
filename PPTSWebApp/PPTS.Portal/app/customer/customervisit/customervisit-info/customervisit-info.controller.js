/*
    名    称: customerVisitDataService
    功能概要: 客户回访
    作    者: 李辉
    创建时间: 
    修正履历：
    修正时间:
*/

define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerVisitDataService],
        function (customer) {
            customer.registerController('customerVisitInfoController', [
                '$state', '$scope', '$stateParams', 'customerVisitDataService', 'customerVisitDataViewService', 'dataSyncService',
                function ($state, $scope,$stateParams, customerVisitDataService, customerVisitDataViewService, dataSyncService) {
                    var vm = this;

                    vm.id = $stateParams.visitId;

                    customerVisitDataService.getCustomerVisitInfo(vm.id, function (result) {

                        vm.customerVisit = result.customerVisit;
                        vm.customer = result.customer;
                        
                        console.log(result);

                    });

                    // 取消
                    vm.cancel = function () {
                        $state.go('ppts.student-view.visits', { prev: 'ppts.student' });
                    };

                }]);
        });