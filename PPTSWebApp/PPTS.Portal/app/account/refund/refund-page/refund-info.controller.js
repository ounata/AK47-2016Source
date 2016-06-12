define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountRefundDataService],
        function (account) {
            account.registerController('accountRefundInfoController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountRefundDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.id;

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getRefundApplyByApplyID(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    vm.cancel = function () {
                        $state.go(vm.page);
                    }
                }]);
        });