define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService],
        function (customer) {
            customer.registerController('followViewController', ['$scope', '$stateParams', 'dataSyncService', 'followDataService', 'followDataViewService', 'showfollowListDataHeader',
                function ($scope, $stateParams, dataSyncService, followDataService, followDataViewService, followListDataHeader) {
                    var vm = this;

                    // 配置跟列表数据表头
                    (function () {
                        // 配置跟列表数据表头
                        dataSyncService.configDataHeader(vm, followListDataHeader, followDataService.getPagedFollows);
                        vm.criteria = vm.criteria || {};
                        vm.search = function () {
                            vm.criteria.customerID = ($stateParams.id ? $stateParams.id : $stateParams.customerID);
                            dataSyncService.initDataList(vm, followDataService.getAllFollows, function () {
                                $scope.$broadcast('dictionaryReady');
                            });
                        };
                        vm.search();
                    })();

                }]);
        });