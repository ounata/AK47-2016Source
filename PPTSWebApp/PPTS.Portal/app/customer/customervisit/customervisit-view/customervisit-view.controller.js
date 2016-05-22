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
            customer.registerController('customerVisitViewController', [
                '$state', '$scope', 'customerVisitDataService', 'customerVisitDataViewService', 'dataSyncService',
                function ($state, $scope, customerVisitDataService, customerVisitDataViewService, dataSyncService) {
                    var vm = this;

                    // 配置客户服务列表表头
                    customerVisitDataViewService.configCustomerVisitListSingleHeaders(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        customerVisitDataViewService.initCustomerVisitSingleList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();

                    vm.add = function () {
                        $state.go('ppts.student-view.customervisit-add', { prev: 'ppts.student' });
                    };

                }]);
        });