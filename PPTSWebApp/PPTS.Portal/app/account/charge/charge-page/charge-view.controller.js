define([ppts.config.modules.account],
    function (account) {
        account.registerController('accountChargeViewController', ['$state', '$location',
            function ($state, $location) {
                var vm = this;
                vm.page = $location.$$search.prev;

                vm.tabs = [{
                    title: '充值信息',
                    route: 'info({prev:vm.page})',
                    active: $state.includes('ppts.accountCharge-view.info') || $state.includes('ppts.accountCharge-view.edit')
                }, {
                    title: '登记付款并打印收据',
                    route: 'payment-list({prev:vm.page})',
                    active: $state.includes('ppts.accountCharge-view.payment-list') || $state.includes('ppts.accountCharge-view.payment-edit')
                }, {
                    title: '缴费单凭证',
                    route: 'cert({prev:vm.page})',
                    active: $state.includes('ppts.accountCharge-view.cert')
                }];

                vm.switchTab = function (scope) {
                    angular.forEach(vm.tabs, function (tab) {
                        tab.active = false;
                    });
                    scope.tab.active = true;
                };
            }]);
    });