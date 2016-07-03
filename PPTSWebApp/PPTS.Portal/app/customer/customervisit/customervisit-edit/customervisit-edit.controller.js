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
            customer.registerController('customerVisitEditController', [
                '$state', '$scope', '$stateParams', 'customerVisitDataService', 'customerVisitDataViewService', 'dataSyncService','mcsValidationService',
                function ($state, $scope, $stateParams, customerVisitDataService, customerVisitDataViewService, dataSyncService, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    vm.id = $stateParams.visitId;


                    (function () {
                        customerVisitDataViewService.initCreateCustomerVisitInfo(vm, function () {
                            dataSyncService.injectDynamicDict('messageType');
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    vm.error = function () {
                        mcsDialogService.error({
                            title: 'Error',
                            message: '提醒时间要小于回访时间'
                        }


                        );
                    }

                    customerVisitDataService.getCustomerVisitInfo(vm.id, function (result) {

                        vm.customerVisit = result.customerVisit;
                        vm.customer = result.customer;

                        //vm.tempVisitTime = result.customerVisit.visitTime;
                    });

                    // 取消
                    vm.cancel = function () {
                        $state.go('ppts.student-view.visits', { prev: 'ppts.student' });
                    };

                    vm.edit = function () {
                        if (vm.customerVisit.remindTime > vm.customerVisit.visitTime) {
                            vm.error();
                            return;
                        }
                        if (mcsValidationService.run($scope)) {
                            customerVisitDataService.editCustomerVisit({
                                customerVisit: vm.customerVisit,
                                selectType: vm.customerVisit.selectType
                            }, function () {
                                $state.go('ppts.student-view.visits', { prev: 'ppts.student' });
                            });
                        }
                    };

                }]);
        });