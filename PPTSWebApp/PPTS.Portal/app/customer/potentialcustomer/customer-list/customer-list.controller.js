define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
        function (customer) {
            customer.registerController('customerListController', [
                '$state', 'dataSyncService', 'customerDataService', 'customerDataViewService', 'utilService', 'customerRelationType', 'customerListDataHeader', 'customerAdvanceSearchItems',
                function ($state, dataSyncService, customerDataService, customerDataViewService, util, relationType, customerListDataHeader, searchItems) {
                    var vm = this;

                    // 配置数据表头 
                    customerDataViewService.configCustomerListHeaders(vm, customerListDataHeader);

                    // 获取学员与员工关系
                    vm.relationType = relationType;
                    
                    // 选择大区/公司/校区
                    vm.select = function () {
                        dataSyncService.popupTree(vm, {
                            title: '选择大区/分公司/校区'
                        });
                    };

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        customerDataViewService.initCustomerList(vm, function () {
                            vm.searchItems = searchItems;
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
                            customerDataViewService.assignStaffRelation(vm.data.rowsSelected, relationType);
                        }
                    };

                    // 充值
                    vm.pay = function () {
                        //当前操作人是潜客的咨询师才可以充值
                        if (util.selectOneRow(vm)) {

                            var id = vm.data.rowsSelected[0].customerID;

                            customerDataService.assertAccountCharge(id, function (result) {
                                if (result.ok) {
                                    $state.go('ppts.accountCharge-edit', { customerID: id, prev: 'ppts.customer' });
                                }
                                else {
                                    vm.errorMessage = result.message;
                                }
                            });
                        }
                    };
                }]);
        });