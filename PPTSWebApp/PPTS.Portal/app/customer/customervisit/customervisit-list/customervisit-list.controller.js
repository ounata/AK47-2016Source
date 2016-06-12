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
            customer.registerController('customerVisitListController', [
                '$state', '$scope', 'customerVisitDataService', 'customerVisitDataViewService', 'dataSyncService',
                function ($state,$scope, customerVisitDataService, customerVisitDataViewService, dataSyncService) {
                    var vm = this;

                    // 配置客户服务列表表头
                    customerVisitDataViewService.configCustomerVisitListHeaders(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        customerVisitDataViewService.initCustomerVisitList(vm, function () {

                            dataSyncService.injectDictData({
                                c_codE_ABBR_TimeType_Service: [{ key: '1', value: '回访时间' }, { key: '2', value: '下次回访时间' }]
                            });
                            $scope.$broadcast('dictionaryReady');

                        });
                    };
                    vm.export = function () {
                        mcs.util.postMockForm(ppts.config.customerApiBaseUrl + '/api/customervisits/exportCustomerVisit', vm.criteria);
                    }
                    vm.search();
                   
                }]);
        });