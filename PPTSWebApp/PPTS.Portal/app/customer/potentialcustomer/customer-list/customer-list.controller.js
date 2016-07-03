define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
        function (customer) {
            customer.registerController('customerListController', [
                '$scope', '$state', 'dataSyncService', 'customerDataService', 'customerDataViewService', 'utilService', 'customerRelationType', 'customerListDataHeader', 'customerAdvanceSearchItems', 'exportExcelService',
                function ($scope, $state, dataSyncService, customerDataService, customerDataViewService, util, relationType, customerListDataHeader, searchItems, exportExcelService) {
                    var vm = this;

                    // 获取学员与员工关系
                    vm.relationType = relationType;

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, customerListDataHeader, customerDataService.getPagedCustomers);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, customerDataService.getAllCustomers, function () {
                            vm.searchItems = searchItems;

                            dataSyncService.injectDynamicDict([{ key: 0, value: '未分配' }, { key: 1, value: '已分配' }], { category: 'assignment' });
                            dataSyncService.injectDynamicDict([{ key: 9, value: '无效客户' }, { key: 1, value: '有效客户' }], { category: 'valid' });
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
                        });
                    };

                    vm.search();

                    //分配咨询师/学管师/坐席/市场专员
                    vm.assign = function (item) {
                        if (util.selectMultiRows(vm)) {
                            switch (item.text) {
                                case '分配咨询师':
                                    customerDataViewService.assignStaffRelation(vm.data.rowsSelected, vm.relationType.consultant, vm);
                                    break;
                                case '分配坐席':
                                    customerDataViewService.assignStaffRelation(vm.data.rowsSelected, vm.relationType.callcenter, vm);
                                    break;
                            }
                        }
                    };

                    vm.relations = [
                        { text: '分配咨询师', click: vm.assign, permission: '分配咨询师,分配咨询师-本校区,分配咨询师-本部门' },
                        { text: '分配坐席', click: vm.assign, permission: '分配坐席-本部门' }
                    ];

                    // 新增跟进记录
                    vm.addFollow = function () {
                        if (util.selectOneRow(vm)) {
                            $state.go('ppts.follow-add', {
                                customerId: vm.data.rowsSelected[0].customerID,
                                prev: 'ppts.customer'
                            });
                        }
                    };

                    // 划转资源
                    vm.transfer = function () {
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

                    // 充值
                    vm.pay = function () {
                        //当前操作人是潜客的咨询师才可以充值
                        if (util.selectOneRow(vm)) {

                            var customerID = vm.data.rowsSelected[0].customerID;

                            customerDataService.assertAccountCharge(customerID, function (result) {
                                if (result.ok) {
                                    $state.go('ppts.accountChargeEdit', { id: customerID, prev: 'ppts.customer' });
                                }
                                else {
                                    vm.errorMessage = result.message;
                                }
                            });
                        }
                    };

                    // 导出
                    vm.export = function () {
                        exportExcelService.export(ppts.config.customerApiBaseUrl + 'api/potentialcustomers/exportallcustomers', vm.criteria);
                    };

                    // 批量导入客户资源
                    vm.import = function () {
                        customerDataViewService.importCustomers();
                    };

                }]);
        });