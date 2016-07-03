define([ppts.config.modules.account],
    function (account) {
        account.registerController('accountChargeViewController', ['$state', '$location',
            function ($state, $location) {
                var vm = this;
                vm.page = $location.$$search.prev;

                vm.tabs = [{
                    title: '充值信息',
                    route: 'info({prev:vm.page})',
                    active: $state.includes('ppts.accountCharge-view.info') || $state.includes('ppts.accountCharge-view.edit'),
                    permission: '按钮-充值/删除/编辑缴费单'
                }, {
                    title: '登记付款并打印收据',
                    route: 'payment-list({prev:vm.page})',
                    active: $state.includes('ppts.accountCharge-view.payment-list') || $state.includes('ppts.accountCharge-view.payment-edit'),
                    permission: '按钮-添加付款-本校区'
                }, {
                    title: '缴费单凭证',
                    route: 'cert({prev:vm.page})',
                    active: $state.includes('ppts.accountCharge-view.cert'),
                    permission: '按钮-打印缴费单凭证,按钮-打印缴费单凭证-本校区,按钮-打印服务协议,按钮-打印服务协议-本校区'
                }, {
                    title: '登记发票',
                    route: 'invoice-list({prev:vm.page})',
                    active: $state.includes('ppts.accountCharge-view.invoice-list') || $state.includes('ppts.accountCharge-view.invoice-edit'),
                    permission: '按钮-登记发票,按钮-登记发票-本校区'
                }];

                vm.switchTab = function (scope) {
                    angular.forEach(vm.tabs, function (tab) {
                        tab.active = false;
                    });
                    scope.tab.active = true;
                };
            }]);
    });