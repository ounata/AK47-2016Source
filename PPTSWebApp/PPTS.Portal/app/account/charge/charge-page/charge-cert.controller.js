define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeCertController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.applyID;

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getChargeApplyByApplyID4Payment(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.items = result.apply.payment.items;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                }]);
        });