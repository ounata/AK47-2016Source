define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.confirmdoorDataService],
        function (customer) {
            customer.registerController('confirmdoorListController', ['$scope', 'dataSyncService', 'confirmdoorDataViewService',
                function ($scope, dataSyncService, confirmdoorDataViewService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    confirmdoorDataViewService.confirmdoorListDataHeader(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        confirmdoorDataViewService.initConfirmdoorList(vm, function () {
                          //  vm.searchItems = searchItems;
                            $scope.$broadcast('dictionaryReady');
                        });
                        confirmdoorDataViewService.initDateRange($scope, vm, [
                            { watchExp: 'vm.followTimeValue', selectedValue: 'followTimeValue', start: 'followStartTime', end: 'followEndTime' },
                            { watchExp: 'vm.nextFollowTimeValue', selectedValue: 'nextFollowTimeValue', start: 'nextTalkStartTime', end: 'nextTalkEndTime' },
                            { watchExp: 'vm.planSignDateValue', selectedValue: 'planSignDateValue', start: 'planSignStartTime', end: 'planSignEndTime' }
                        ]);
                    };
                    vm.search();
                }]);
        });