define([ppts.config.modules.custcenter,
        ppts.config.dataServiceConfig.custserviceDataService],
        function (customer) {
            customer.registerController('custserviceListController', [
                '$state', 'dataSyncService', 'custserviceDataService', 'custserviceListDataHeader', 'custserviceAdvanceSearchItems',
                function ($state, dataSyncService, custserviceDataService, custserviceListDataHeader, searchItems) {
                    var vm = this;
                    

                    var syNow = new Date();
                    var year = syNow.getFullYear();        //年
                    var month = syNow.getMonth() + 1;     //月
                    var day = syNow.getDate();
                    vm.criteria = vm.criteria || {};
                    vm.criteria.callTimeStart = new Date(new Date(year + "-" + month + "-" + 1).getTime());
                    vm.criteria.callTimeEnd = new Date(new Date(year + "-" + month + "-" + day).getTime());

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, custserviceListDataHeader, custserviceDataService.getPagedCustomerServices);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, custserviceDataService.getAllCustomerServices, function () {
                            vm.searchItems = searchItems;
                            dataSyncService.injectDynamicDict('ifElse');
                        });
                    };

                    vm.search();

                    vm.add = function () {
                        $state.go('ppts.custservice-add');
                    }

                    vm.export = function () {
                        mcs.util.postMockForm(ppts.config.customerApiBaseUrl + '/api/customerservices/exportCustomerService', vm.criteria);
                    }
                }]);
        });