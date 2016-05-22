define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService],
        function (customer) {
            customer.registerController('followListController', ['$scope', 'dataSyncService', 'followDataViewService', 'followListDataHeader',
                function ($scope, dataSyncService, followDataViewService, followListDataHeader) {
                    var vm = this;

                    // 配置跟列表数据表头
                    followDataViewService.configFollowListHeaders(vm, followListDataHeader);



                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        followDataViewService.initFollowList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                        followDataViewService.initDateRange($scope, vm, [
                            { watchExp: 'vm.followTimeValue', selectedValue: 'followTimeValue', start: 'followStartTime', end: 'followEndTime' },
                            { watchExp: 'vm.nextFollowTimeValue', selectedValue: 'nextFollowTimeValue', start: 'nextTalkStartTime', end: 'nextTalkEndTime' },
                            { watchExp: 'vm.planSignDateValue', selectedValue: 'planSignDateValue', start: 'planSignStartTime', end: 'planSignEndTime' }
                        ]);
                    };
                    vm.search();
                }]);
        });