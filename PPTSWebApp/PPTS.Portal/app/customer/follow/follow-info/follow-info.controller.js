define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService],
        function (customer) {
            customer.registerController('followInfoController', ['$scope', '$stateParams', 'followDataViewService', 'followInstutionItem',
                function ($scope, $stateParams, followDataViewService, followInstutionItem) {
                    var vm = this;

                    // 页面初始化加载
                    (function () {
                        followDataViewService.initFollowInfo(vm, $stateParams.followId, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    vm.instutionItems = [followInstutionItem.template];
                }]);
        });