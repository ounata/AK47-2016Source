define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerVerifyDataService],
        function (customer) {
            customer.registerController('customerVerifyListController', ['$scope', 'dataSyncService', 'customerVerifyListDataHeader', 'customerVerifyDataService', 'customerVerifyAdvanceSearchItems',
                function ($scope, dataSyncService, customerVerifyListDataHeader, customerVerifyDataService, searchItems) {
                    var vm = this;

                    // 配置跟列表数据表头
                    dataSyncService.configDataHeader(vm, customerVerifyListDataHeader, customerVerifyDataService.getPagedCustomerVerifies);
                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.injectDynamicDict('dateRange,dept,ifElse,people,creation');
                        if (vm.createTimeValue && vm.createTimeValue != 0 && vm.createTimeValue != 5) {
                            var createDateRange = dataSyncService.selectPageDict('dateRange', vm.createTimeValue);
                            vm.criteria.createStartTime = createDateRange.start;
                            vm.criteria.createEndTime = createDateRange.end;
                        }
                        if (vm.followTimeValue && vm.followTimeValue != 0 && vm.followTimeValue != 5) {
                            var followDateRange = dataSyncService.selectPageDict('dateRange', vm.followTimeValue);
                            vm.criteria.planVerifyStartTime = followDateRange.start;
                            vm.criteria.planVerifyEndTime = followDateRange.end;
                        }
                        if (vm.customerCreateTimeValue && vm.customerCreateTimeValue != 0 && vm.customerCreateTimeValue != 5) {
                            var customerDateRange = dataSyncService.selectPageDict('dateRange', vm.customerCreateTimeValue);
                            vm.criteria.customerCreateStartTime = customerDateRange.start;
                            vm.criteria.customerCreateEndTime = customerDateRange.end;
                        }
                        dataSyncService.initDataList(vm, customerVerifyDataService.getAllCustomerVerifies, function () {
                            vm.searchItems = searchItems;
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();
                }]);
        });