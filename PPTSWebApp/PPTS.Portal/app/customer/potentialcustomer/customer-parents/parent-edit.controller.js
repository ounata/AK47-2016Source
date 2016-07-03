define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService,
        ppts.config.dataServiceConfig.customerDataService,
        'app/customer/potentialcustomer/customer-add/ppts.customer.relation'],
    function (customer) {
        customer.registerController('parentEditController', [
            '$scope',
            'customerService',
            'dataSyncService',
            'customerDataViewService',
            'customerParentService',
            'mcsValidationService',
            function ($scope, customerService, dataSyncService, customerDataViewService, customerParentService, mcsValidationService) {
                var vm = this, lastIndex = 0;

                // 页面初始化加载
                vm.init = function () {
                    customerService.handle('init-parent', function (result) {
                        vm.customer = result.customer;
                        vm.parent = result.parent;
                        vm.relation = result.customerParentRelation;
                        vm.isCustomer = result.isCustomer;
                        vm.customerName = vm.customer.customerName;

                        vm.customerRole = vm.relation.customerRole;
                        vm.parentRole = vm.relation.parentRole;

                        // 亲属关系切换
                        customerParentService.initCustomerParentRelation($scope, vm, lastIndex);
                        dataSyncService.injectDynamicDict('ifElse');
                        $scope.$broadcast('dictionaryReady');
                        $scope.$broadcast('dataReady');
                    });
                };
                vm.init();

                vm.save = function () {
                    if (mcsValidationService.run($scope)) {
                        vm.parent.addressDetail = customerDataViewService.getAddressDetail(vm);
                        vm.relation.isPrimary = vm.relation.isPrimary == 1 ? true : false;
                        vm.relation.customerRole = vm.customerRole;
                        vm.relation.parentRole = vm.parentRole;
                        var data = {
                            customer: vm.customer,
                            parent: vm.parent,
                            customerParentRelation: vm.relation
                        };
                        customerService.handle('update-parent', data);
                    }
                };

                vm.cancel = function () {
                    customerService.handle('update-parent-cancel');
                };

                // 家长姓名验证
                vm.checkParentName = function (target) {
                    var valid = !(vm.parent.parentName && vm.customer.customerName && vm.parent.parentName.trim() == vm.customer.customerName.trim());
                    mcsValidationService.check($(target), {
                        validate: valid
                    });
                    return valid;
                };
                // 身份证号码验证
                vm.isIdCard = function (idType, idNumber, targetID) {
                    return customerDataViewService.isIdCard(idType, idNumber, targetID);
                };
                // 家长证件类型切换
                $scope.$watch('vm.parent.idType', function (idType) {
                    if (vm.parent && vm.parent.idNumber) {
                        vm.isIdCard(idType, vm.parent.idNumber, '#parentIdNumber');
                    }
                });
                // 家长、学员姓名只能输入英文和汉字
                $scope.$watch('vm.parent.parentName', function (parentName) {
                    if (!parentName) return;
                    vm.parent.parentName = parentName.replace(/[^\a-zA-Z\u4E00-\u9FA5]/g, '');
                });
            }]);
    });