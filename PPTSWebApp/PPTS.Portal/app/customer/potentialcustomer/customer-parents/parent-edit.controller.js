define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService,
        ppts.config.dataServiceConfig.customerDataService,
        'app/customer/potentialcustomer/customer-add/ppts.customer.relation'],
    function (customer) {
        customer.registerController('parentEditController', [
            '$scope',
            'customerService',
            'dataSyncService',
            'customerParentService',
            function ($scope, customerService, dataSyncService, customerParentService) {
                var vm = this, lastIndex = 0;

                // 页面初始化加载
                vm.init = function () {
                    customerService.handle('init-parent', function (result) {
                        vm.customer = result.customer;
                        vm.parent = result.parent;
                        vm.relation = result.customerParentRelation;

                        // 亲属关系切换
                        customerParentService.initCustomerParentRelation($scope, vm, lastIndex);
                        dataSyncService.injectPageDict(['ifElse']);
                        $scope.$broadcast('dictionaryReady');
                    });
                };
                vm.init();

                vm.save = function () {
                    var data = {
                        parent: vm.parent,
                        customerParentRelation: vm.relation
                    };
                    customerService.handle('update-parent', data);
                }

                vm.cancel = function () {
                    customerService.handle('update-parent-cancel');
                };
            }]);
    });