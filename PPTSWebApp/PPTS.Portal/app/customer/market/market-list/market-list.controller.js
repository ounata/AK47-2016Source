define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.marketDataService,
        ppts.config.dataServiceConfig.customerDataService,
        ppts.config.dataServiceConfig.studentDataService],
        function (customer) {
            customer.registerController('marketListController', [
                'dataSyncService', 'customerDataViewService', 'utilService', 'customerRelationType', 'marketListDataHeader', 'marketAdvanceSearchItems',
                function (dataSyncService, customerDataViewService, util, relationType, marketListDataHeader, searchItems) {
                    var vm = this;

                    // 配置数据表头
                    customerDataViewService.configCustomerListHeaders(vm, marketListDataHeader);

                    // 获取学员与员工关系
                    vm.relationType = relationType;

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        customerDataViewService.initCustomerList(vm, function () {
                            vm.searchItems = searchItems;
                        });
                    };
                    vm.search();
                     
                    // 分配咨询师/学管师/坐席/市场专员
                    vm.assign = function (relationType) {
                        if (util.selectMultiRows(vm)) {
                            customerDataViewService.assignStaffRelation(vm.data.rowsSelected, relationType);
                        }
                    };

                    // 划转资源
                    vm.transfer = function () {
                        if (util.selectMultiRows(vm)) {
                            customerDataViewService.transferCustomer(vm.data.rowsSelected);
                        }
                    };
                }]);
        });