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
            customer.registerController('customerVisitSingleListController', [
                '$state', '$scope', '$stateParams', 'customerVisitDataService', 'customerVisitDataViewService', 'dataSyncService','mcsValidationService','mcsDialogService',
                function ($state, $scope, $stateParams, customerVisitDataService, customerVisitDataViewService, dataSyncService, mcsValidationService, mcsDialogService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    vm.id = $stateParams.id;

                    customerVisitDataViewService.initDate(vm);

                    (function () {
                        customerVisitDataViewService.initCreateCustomerVisitInfoByStudentID(vm, function () {
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

                    // 保存数据
                    vm.save = function () {
                        if (vm.customerVisit.remindTime > vm.customerVisit.visitTime)
                        {
                            vm.error();
                            return;
                        }
                        if (mcsValidationService.run($scope)) {
                            customerVisitDataService.createCustomerVisit({
                                customerVisit: vm.customerVisit
                            }, function () {
                                $state.go('ppts.student-view.visits', { prev: 'ppts.student' });
                            });
                        }
                    };

                    // 取消
                    vm.cancel = function () {
                        $state.go('ppts.student-view.visits', { prev: 'ppts.student' });
                    };

                }]);
        });