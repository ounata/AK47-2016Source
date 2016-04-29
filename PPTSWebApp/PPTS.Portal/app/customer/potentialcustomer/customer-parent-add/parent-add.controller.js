define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerParentAddController', [
            '$scope',
            '$state',
            '$stateParams',
            'customerService',
            '$uibModalInstance',
            'customerDataService',
            'customerParentService',
            'customerRelationService',
            'utilService',
            'data',
            function ($scope, $state, $stateParams, customerService, $uibModalInstance, customerDataService, customerParentService, customerRelationService, util, data) {
                var vm = this, lastIndex = 0;;
                
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                // 搜索
                vm.search = function () {
                    vm.parent = undefined;
                    customerParentService.getAllParents(vm, data);
                }

                // 添加
                vm.add = function () {
                    if (util.selectOneRow(vm)) {
                        var parentId = vm.data.rowsSelected[0].parentID;
                        var data = {
                            customer: vm.customer,
                            primaryParent: vm.parent,
                            customerRole: vm.customerRole,
                            parentRole: vm.parentRole
                        };
                        if (vm.type == "add") {
                            customerDataService.getParentInfo(parentId, function (result) {
                                $uibModalInstance.close(result.parent);
                            });
                        } else if (vm.type == "confirm") {
                            customerService.handle('confirm-parent', data, function () {
                                $uibModalInstance.dismiss('Canceled');
                            });
                        }
                    }
                }

                vm.initData = function () {
                    vm.type = data.type;
                    vm.customer = data.customer;
                }

                // 页面初始化加载
                vm.initData();
                customerParentService.configParentAddHeaders(vm);
                customerParentService.getAllParents(vm, data, function () {
                    $scope.$broadcast('dictionaryReady');
                });

                // 亲属关系
                $scope.$watch('vm.data.rowsSelected[0].parentID', function () {
                    if (vm.type == 'add' || !vm.data.rowsSelected || !vm.data.rowsSelected.length) return;
                    vm.parent = $(vm.data.rowsSelected)[0];
                    vm.customerRoles = mcs.util.mapping(customerRelationService.children(vm.customer.gender), { key: 'sid', value: 'sr' });
                    vm.customerRole = vm.customerRoles && vm.customerRoles.length > 0 && lastIndex > -1 ? vm.customerRoles[lastIndex].key : 0;
                    updateParentRole(true);
                });

                $scope.selectCustomerRole = function (item, model) {
                    vm.parentRoles = mcs.util.mapping(customerRelationService.myParents(model, vm.parent.gender), { key: 'pid', value: 'pr' });
                    lastIndex = mcs.util.indexOf(vm.customerRoles, 'key', vm.relation.customerRole);

                    updateParentRole(false);
                };

                $scope.selectCustomerRole = function (item, model) {
                    vm.parentRoles = mcs.util.mapping(customerRelationService.myParents(model, vm.parent.gender), { key: 'pid', value: 'pr' });
                    lastIndex = mcs.util.indexOf(vm.customerRoles, 'key', vm.customerRole);

                    updateParentRole(false);
                };

                var updateParentRole = function (needUpdateParentRoles) {
                    if (needUpdateParentRoles) {
                        vm.parentRole = 0;
                        if (vm.customerRole) {
                            vm.parentRoles = mcs.util.mapping(customerRelationService.myParents(vm.customerRole, vm.parent.gender), { key: 'pid', value: 'pr' });
                        } else {
                            vm.parentRoles = mcs.util.mapping(customerRelationService.parents(vm.parent.gender), { key: 'pid', value: 'pr' });
                        }
                    }
                    if (vm.parentRoles.length == 1) {
                        vm.parentRole = vm.parentRoles[0].key;
                    } else {
                        vm.parentRole = 0;
                    }
                };

            }]);
    });