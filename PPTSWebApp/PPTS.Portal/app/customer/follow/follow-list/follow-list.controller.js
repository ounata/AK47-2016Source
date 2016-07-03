define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService],
        function (customer) {
            customer.registerController('followListController', ['$scope', 'dataSyncService', 'followDataService', 'followDataViewService', 'followListDataHeader', 'followAdvanceSearchItems',
                function ($scope, dataSyncService, followDataService, followDataViewService, followListDataHeader, searchItems) {
                    var vm = this;

                    // 配置跟列表数据表头
                    dataSyncService.configDataHeader(vm, followListDataHeader, followDataService.getPagedFollows);
                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.injectDynamicDict('dateRange,dept,ifElse,people,creation');
                        if (vm.followTimeValue && vm.followTimeValue != 5) {
                            var followDateRange = dataSyncService.selectPageDict('dateRange', vm.followTimeValue);
                            vm.criteria.followStartTime = followDateRange.start;
                            vm.criteria.followEndTime = followDateRange.end;
                        }
                        if (vm.nextFollowTimeValue && vm.nextFollowTimeValue != 5) {
                            var nextFollowDateRange = dataSyncService.selectPageDict('dateRange', vm.nextFollowTimeValue);
                            vm.criteria.nextTalkStartTime = nextFollowDateRange.start;
                            vm.criteria.nextTalkEndTime = nextFollowDateRange.end;
                        }
                        if (vm.planVerifyTimeValue && vm.planVerifyTimeValue != 5) {
                            var planVerifyDateRange = dataSyncService.selectPageDict('dateRange', vm.planVerifyTimeValue);
                            vm.criteria.planVerifyStartTime = planVerifyDateRange.start;
                            vm.criteria.planVerifyEndTime = planVerifyDateRange.end;
                        }
                        if (vm.planSignDateValue && vm.planSignDateValue != 5) {
                            var planSignDateRange = dataSyncService.selectPageDict('dateRange', vm.planSignDateValue);
                            vm.criteria.planSignStartTime = planSignDateRange.start;
                            vm.criteria.planSignEndTime = planSignDateRange.end;
                        }
                        dataSyncService.initDataList(vm, followDataService.getAllFollows, function () {
                            vm.searchItems = searchItems;
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();
                }]);
        });