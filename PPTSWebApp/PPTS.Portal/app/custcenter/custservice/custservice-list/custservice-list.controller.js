define([ppts.config.modules.custcenter,
        ppts.config.dataServiceConfig.custserviceDataService],
        function (customer) {
            customer.registerController('custserviceListController', [
                'custserviceDataService', 'custserviceDataViewService', 'custserviceAdvanceSearchItems',
                function (customerDataViewService, custserviceDataViewService, searchItems) {
                    var vm = this;

                    // 配置客户服务列表表头
                    custserviceDataViewService.configCustomerServiceListHeaders(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        custserviceDataViewService.initCustomerServiceList(vm, function () {
                            vm.searchItems = searchItems;
                        });
                    };
                    vm.export = function () {
                        mcs.util.postMockForm(ppts.config.customerApiBaseUrl + '/api/customerservices/exportCustomerService', vm.criteria);
                    }
                    vm.search();
                }]);
        });