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
                '$state', '$scope', '$stateParams', 'customerVisitDataService', 'customerVisitDataViewService', 'dataSyncService','mcsValidationService',
                function ($state, $scope, $stateParams, customerVisitDataService, customerVisitDataViewService, dataSyncService, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    vm.id = $stateParams.id;

                    (function () {
                        customerVisitDataViewService.initCreateCustomerVisitInfoByStudentID(vm, function () {
                            dataSyncService.injectPageDict(['messageType']);
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    // 保存数据
                    vm.save = function () {
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