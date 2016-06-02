define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerVerifyDataService],
        function (customer) {
            customer.registerController('customerVerifyListController', ['$scope', 'dataSyncService', 'customerVerifyDataViewService',
                function ($scope, dataSyncService, customerVerifyDataViewService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    customerVerifyDataViewService.customerVerifyListDataHeader(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        customerVerifyDataViewService.initCustomerVerifyList(vm, function () {
                          //  vm.searchItems = searchItems;
                            $scope.$broadcast('dictionaryReady');
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