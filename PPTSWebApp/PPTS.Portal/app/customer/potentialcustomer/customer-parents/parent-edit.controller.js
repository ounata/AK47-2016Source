define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService,
        ppts.config.dataServiceConfig.customerDataService,
        'app/customer/potentialcustomer/customer-add/ppts.customer.relation'],
    function (customer) {
        customer.registerController('parentEditController', [
            '$scope',
            '$state',
            '$stateParams',
            'customerService',
            'dataSyncService',
            'customerRelationService',
            'customerDataService',
            function ($scope, $state, $stateParams, customerService, dataSyncService, customerRelationService, customerDataService) {
                var vm = this, lastIndex = 0;

                // 页面初始化加载
                vm.init = function () {
                    customerService.handle('init-parent', function (result) {
                        vm.customer = result.customer;
                        vm.parent = result.parent;
                        vm.relation = result.customerParentRelation;
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

                // 亲属关系切换
                $scope.$watch('vm.parent.gender', function () {
                    if (!vm.parent || !vm.parent.gender) return;
                    updateParentRole(true);
                });

                $scope.$watch('vm.customer.gender', function () {
                    if (!vm.customer || !vm.customer.gender) return;
                    vm.customerRoles = mcs.util.mapping(customerRelationService.children(vm.customer.gender), { key: 'sid', value: 'sr' });
                    vm.customerRole = vm.customerRoles && vm.customerRoles.length > 0 && lastIndex > -1 ? vm.customerRoles[lastIndex].key : 0;
                    updateParentRole(true);
                });

                $scope.selectCustomerRole = function (item, model) {
                    vm.parentRoles = mcs.util.mapping(customerRelationService.myParents(model, vm.parent.gender), { key: 'pid', value: 'pr' });
                    lastIndex = mcs.util.indexOf(vm.customerRoles, 'key', vm.relation.customerRole);

                    updateParentRole(false);
                };

                var updateParentRole = function (needUpdateParentRoles) {
                    if (needUpdateParentRoles) {
                        vm.relation.parentRole = 0;
                        if (vm.relation.customerRole) {
                            vm.parentRoles = mcs.util.mapping(customerRelationService.myParents(vm.relation.customerRole, vm.parent.gender), { key: 'pid', value: 'pr' });
                        } else {
                            vm.parentRoles = mcs.util.mapping(customerRelationService.parents(vm.parent.gender), { key: 'pid', value: 'pr' });
                        }
                    }

                    if (vm.parentRoles.length == 1) {
                        vm.relation.parentRole = vm.parentRoles[0].key;
                    } else {
                        vm.relation.parentRole = 0;
                    }
                };

            }]);
    });