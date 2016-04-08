define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService,
        'app/customer/potentialcustomer/customer-add/ppts.customer.relation'],
    function (customer) {
        customer.registerController('customerAddController', [
            '$scope',
            '$state',
            'dataSyncService',
            'customerDataService',
            'customerRelationService',
            '$stateParams',
            function ($scope, $state, dataSyncService, customerDataService, customerRelationService, $stateParams) {
                var vm = this, orginalParent, lastIndex = 0;

                var syncParentDict = function () {
                    vm.parent.idTypes = ppts.dict[ppts.config.dictMappingConfig.idtype];
                    vm.parent.incomes = ppts.dict[ppts.config.dictMappingConfig.income];
                };

                // 页面初始化加载
                (function () {
                    customerDataService.getCustomerForCreate(function (result) {
                        orginalParent = result.primaryParent;
                        dataSyncService.injectDictData({ c_codE_ABBR_StudyAgain: [{ key: 1, value: '是' }, { key: 2, value: '否' }] });
                        dataSyncService.setDefaultValue(vm.customer, result.customer, ['idType', 'subjectType', 'vipType']);
                        dataSyncService.setDefaultValue(vm.parent, result.primaryParent, 'idType');
                        // 仅针对parent
                        syncParentDict();

                        $scope.$broadcast('dictionaryReady');
                    });
                })();


                // 清空家长信息
                vm.reset = function () {
                    vm.parent = mcs.util.clone(orginalParent);
                    syncParentDict();
                };

                // 保存数据
                vm.save = function () {
                    customerDataService.createCustomer({
                        customer: vm.customer,
                        primaryParent: vm.parent,
                        customerRole: vm.customerRole,
                        parentRole: vm.parentRole
                    }, function () {
                        $state.go('ppts.customer');
                    });
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

                vm.getCustomerInfo = function (code) {
                    if (!code) {
                        vm.customer.referralCustomerName = '';
                        return;
                    };
                    customerDataService.getCustomerInfo(code, function (result) {
                        vm.customer.referralCustomerName = result.customerName;
                    });
                };
            }]);
    });