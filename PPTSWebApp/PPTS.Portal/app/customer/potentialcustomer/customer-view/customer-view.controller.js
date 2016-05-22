define([ppts.config.modules.customer],
    function (customer) {
        customer.registerController('customerViewController', ['$state', '$location',
            function ($state, $location) {
                var vm = this;
                vm.page = $location.$$search.prev;

                vm.tabs = [{
                    title: '基本信息',
                    route: 'profiles({prev:vm.page})',
                    active: true
                }, {
                    title: '学员家长',
                    route: 'parents({prev:vm.page})',
                    active: false
                }, {
                    title: '跟进记录',
                    route: 'follows({prev:vm.page})',
                    active: false
                }];

                vm.switchTab = function (scope) {
                    angular.forEach(vm.tabs, function (tab) {
                        tab.active = false;
                    });
                    scope.tab.active = true;
                };
            }]);
    });