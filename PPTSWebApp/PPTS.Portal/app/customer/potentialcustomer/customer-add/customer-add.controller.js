define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService,
        ppts.config.dataServiceConfig.studentDataService],
    function (customer) {
        customer.registerController('customerAddController', [
            '$scope',
            '$state',
            'customerDataService',
            'studentDataService',
            'customerDataViewService',
            'customerParentService',
            function ($scope, $state, customerDataService, studentDataService, customerDataViewService, customerParentService) {
                var vm = this, orginalParent, lastIndex = 0;

                // 页面初始化加载
                (function () {
                    customerDataViewService.initCreateCustomerInfo(orginalParent, vm, function () {
                        // 仅针对parent
                        customerParentService.syncParentDict(vm);
                        $scope.$broadcast('dictionaryReady');
                    });
                })();

                // 清空家长信息
                vm.reset = function () {
                    vm.parent = mcs.util.clone(orginalParent);
                    customerParentService.syncParentDict(vm);
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

                // 添加已有家长
                vm.parentAdd = function (title) {
                    customerParentService.popupParentAdd(vm, title, 'add', function () {
                        customerParentService.syncParentDict(vm);
                        $scope.$broadcast('dictionaryReady');
                    });
                }

                // 获取转介绍员工信息
                vm.getCustomerByCode = function (customerCode) {
                    if (!customerCode) {
                        vm.customer.referralCustomerName = '';
                        return;
                    };
                    customerDataService.getCustomerByCode(customerCode, function (result) {
                        vm.customer.referralCustomerName = result.customerName;
                    });
                };

                // 获取当前浏览器
                vm.isIE = mcs.browser.get().msie;
                // 亲属关系切换
                customerParentService.initCustomerParentRelation($scope, vm, lastIndex);
            }]);
    });