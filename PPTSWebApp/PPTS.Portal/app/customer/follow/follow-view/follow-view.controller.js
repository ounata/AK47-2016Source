define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService],
        function (customer) {
            customer.registerController('followListController', ['$scope', '$stateParams', 'followDataViewService', 'showfollowListDataHeader',
                function ($scope, $stateParams, followDataViewService, showfollowListDataHeader) {
                    var vm = this;

                    // 配置跟列表数据表头
                    followDataViewService.configFollowListHeaders(vm, showfollowListDataHeader);

                    (function () {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.customerID = $stateParams.id;
                        followDataViewService.initFollowList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();
                }]);
        });