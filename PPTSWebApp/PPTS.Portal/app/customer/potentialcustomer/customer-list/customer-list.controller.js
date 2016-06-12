define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
        function (customer) {
            customer.registerController('customerListController', [
                '$scope', '$state', 'dataSyncService', 'customerDataService', 'customerDataViewService', 'utilService', 'customerRelationType', 'customerListDataHeader', 'customerAdvanceSearchItems',
                function ($scope,$state, dataSyncService, customerDataService, customerDataViewService, util, relationType, customerListDataHeader, searchItems) {
                    var vm = this;


                    // 配置数据表头 
                    customerDataViewService.configCustomerListHeaders(vm, customerListDataHeader);

                    // 获取学员与员工关系
                    vm.relationType = relationType;

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        customerDataViewService.initCustomerList(vm, function () {
                            vm.searchItems = searchItems;
                            customerDataViewService.initWatchExps($scope, vm, [
                                  { watchExp: 'vm.criteria.isAssignCallcenter', selectedValue: 0, watch: 'callcenterName' },
                                  { watchExp: 'vm.criteria.isAssignConsultant', selectedValue: 0, watch: 'consultantName' },
                                  { watchExp: 'vm.criteria.isAssignMarket', selectedValue: 0, watch: 'marketName' },
                                  { watchExp: 'vm.followPeriodValue', selectedValue: 5, watch: 'followDays' }
                            ]);
                            customerDataViewService.initDatePeriod($scope, vm, [
                                  { watchExp: 'vm.followPeriodValue', selectedValue: 'followPeriodValue', end: 'followTime' },
                                  { watchExp: 'vm.followDays', selectedValue: 'followPeriodValue', end: 'followTime' }
                            ]);
                        });
                    };
                    vm.search();
                     
                    // 新增跟进记录
                    vm.addFollow = function () {
                        if (util.selectOneRow(vm)) {
                            $state.go('ppts.follow-add', {
                                customerId: vm.data.rowsSelected[0].customerID,
                                prev: 'ppts.customer'
                            });
                        }
                    };

                    // 分配咨询师/学管师/坐席/市场专员
                    vm.assign = function (relationType) {
                        if (util.selectMultiRows(vm)) {
                            customerDataViewService.assignStaffRelation(vm.data.rowsSelected, relationType, vm);
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
                }]);
        });