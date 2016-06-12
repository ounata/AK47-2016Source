define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService],
        function (customer) {
            customer.registerController('followViewController', ['$scope', '$stateParams', 'followDataViewService',
                function ($scope, $stateParams, followDataViewService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    (function () {
                        followDataViewService.configShowFollowListHeaders(vm);
                        vm.criteria = vm.criteria || {};
                        vm.search = function () {
                            vm.criteria.customerID = ($stateParams.id ? $stateParams.id : $stateParams.customerID);
                            followDataViewService.initFollowList(vm, function () {
                                $scope.$broadcast('dictionaryReady');
                            });
                        };
                        vm.search();
                    })();

                }]);
        });