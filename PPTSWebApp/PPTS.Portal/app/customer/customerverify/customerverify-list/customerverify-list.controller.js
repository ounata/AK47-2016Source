define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerVerifyDataService],
        function (customer) {
            customer.registerController('customerVerifyListController', ['$scope', 'dataSyncService', 'customerVerifyDataViewService', 'customerVerifyAdvanceSearchItems',
                function ($scope, dataSyncService, customerVerifyDataViewService, searchItems) {
                    var vm = this;

                    // 配置跟列表数据表头
                    customerVerifyDataViewService.customerVerifyListDataHeader(vm);
                    vm.createTimeValue = '0';
                    vm.followTimeValue = '0';
                    vm.customerCreateTimeValue = '0';
                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        customerVerifyDataViewService.initCustomerVerifyList(vm, function () {
                            vm.searchItems = searchItems;
                        });
                        customerVerifyDataViewService.initDateRange($scope, vm, [
                            { watchExp: 'vm.followTimeValue', selectedValue: 'followTimeValue', start: 'followStartTime', end: 'followEndTime' },
                            { watchExp: 'vm.nextFollowTimeValue', selectedValue: 'nextFollowTimeValue', start: 'nextTalkStartTime', end: 'nextTalkEndTime' },
                            { watchExp: 'vm.planSignDateValue', selectedValue: 'planSignDateValue', start: 'planSignStartTime', end: 'planSignEndTime' }
                        ]);
                    };
                    vm.search();
                }]);
        });