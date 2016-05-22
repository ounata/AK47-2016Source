define([ppts.config.modules.customer,
    ppts.config.dataServiceConfig.customerService],
    function (customer) {
        customer.registerController('parentNewController', [
            '$scope',
            'dataSyncService',
            'customerService',
            'customerParentService',
            function ($scope, dataSyncService, customerService, customerParentService) {
                var vm = this, lastIndex = 0;

                // 页面初始化加载
                (function () {
                    customerService.handle('init-new-parent', function (result) {
                        vm.customer = result.customer;
                        vm.parent = result.parent;
                        dataSyncService.injectPageDict(['ifElse']);
                        $scope.$broadcast('dictionaryReady');
                    })
                })();

                vm.save = function () {
                    var data = {
                        customer: vm.customer,
                        parent: vm.parent,
                        customerRole: vm.customerRole,
                        parentRole: vm.parentRole
                    };
                    customerService.handle('new-parent', data);
                };

                // 亲属关系切换
                customerParentService.initCustomerParentRelation($scope, vm, lastIndex);

            }]);
    });