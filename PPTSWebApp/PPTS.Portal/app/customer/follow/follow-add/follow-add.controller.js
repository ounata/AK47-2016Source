define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService],
        function (customer) {
            customer.registerController('followAddController', ['$scope', '$stateParams', 'followDataService', 'followDataViewService', 'followInstutionItem',
                function ($scope, $stateParams, followDataService, followDataViewService, followInstutionItem) {
                    var vm = this;

                    // 页面初始化加载
                    (function () {
                        followDataViewService.initCreateFollowInfo($stateParams.customerId, vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    // 保存数据
                    vm.save = function () {
                        followDataService.createFollow({
                            follow: vm.follow
                        }, function () {
                            $state.go('ppts.customer');
                        });
                    };

                    vm.instutionItems = [followInstutionItem.template];
  
                }]);
        });