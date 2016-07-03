define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.marketDataService],
        function (customer) {
            customer.registerController('marketListController', [
                '$scope', 'dataSyncService', 'marketDataService', 'customerDataViewService', 'utilService', 'customerRelationType', 'marketListDataHeader', 'marketAdvanceSearchItems',
                function ($scope, dataSyncService, marketDataService, customerDataViewService, util, relationType, marketListDataHeader, searchItems) {
                    var vm = this;
                    vm.criteria = vm.criteria || {};
                    vm.criteria.from = 'market';

                    // 获取学员与员工关系
                    vm.relationType = relationType;

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, marketListDataHeader, marketDataService.getPagedMarketCustomers);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, marketDataService.getAllMarketCustomers, function () {
                            vm.searchItems = searchItems;

                            dataSyncService.injectDynamicDict([{ key: 0, value: '未分配' }, { key: 1, value: '已分配' }], { category: 'assignment' });
                            dataSyncService.injectDynamicDict([{ key: 9, value: '无效客户' }, { key: 1, value: '有效客户' }], { category: 'valid' });
                            dataSyncService.injectDynamicDict([{ key: 0, value: '潜在客户' }, { key: 1, value: '学员' }], { category: 'customerType' });
                            dataSyncService.injectDynamicDict('dept,period,ifElse,relation,creation');

                            customerDataViewService.initWatchExps($scope, vm, [
                                  { watchExp: 'vm.criteria.isAssignCallcenter', selectedValue: 0, watch: 'callcenterName' },
                                  { watchExp: 'vm.criteria.isAssignConsultant', selectedValue: 0, watch: 'consultantName' },
                                  { watchExp: 'vm.criteria.isAssignMarket', selectedValue: 0, watch: 'marketName' },
                                  { watchExp: 'vm.criteria.followPeriodValue', selectedValue: 5, watch: 'followDays' }
                            ]);
                            customerDataViewService.initDatePeriod($scope, vm, [
                                  { watchExp: 'vm.criteria.followPeriodValue', selectedValue: 'followPeriodValue', end: 'followTime' },
                                  { watchExp: 'vm.followDays', selectedValue: 'followPeriodValue', end: 'followTime' }
                            ]);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };

                    vm.search();
                     
                    // 分配咨询师/学管师/坐席/市场专员
                    vm.assign = function (relationType) {
                        if (util.selectMultiRows(vm)) {
                            customerDataViewService.assignStaffRelation(vm.data.rowsSelected, relationType,vm);
                        }
                    };

                    // 划转资源
                    vm.transfer = function () {
                        //if (util.selectMultiRows(vm)) {
                        //    customerDataViewService.transferCustomer(vm.data.rowsSelected);
                        //}
                        if (util.selectMultiRows(vm)) {
                            var customers = [];
                            for (var index in vm.data.rows) {
                                if (vm.data.rows[index].selected) {
                                    customers.push(vm.data.rows[index]);
                                }
                            }
                            customerDataViewService.transferCustomers(vm, customers);
                        }
                    };

                    // 批量导入客户资源
                    vm.import = function () {
                        customerDataViewService.importCustomers();
                    };
                }]);
        });