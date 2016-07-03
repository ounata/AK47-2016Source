define([ppts.config.modules.customer,
    ppts.config.dataServiceConfig.customerService],
    function (customer) {
        customer.registerController('parentNewController', [
            '$scope',
            'dataSyncService',
            'customerService',
            'customerParentService',
            'customerDataViewService',
            'mcsValidationService',
            function ($scope, dataSyncService, customerService, customerParentService, customerDataViewService, mcsValidationService) {
                var vm = this, lastIndex = 0;

                // 页面初始化加载
                (function () {
                    customerService.handle('init-new-parent', function (result) {
                        vm.isCustomer = result.isCustomer;
                        vm.customerName = result.customer.customerName;
                        vm.customer = result.customer;
                        vm.parent = result.parent;
                        vm.parent = result.parent;
                        vm.relation = result.customerParentRelation;

                        // 亲属关系切换
                        customerParentService.initCustomerParentRelation($scope, vm, lastIndex);

                        dataSyncService.injectDynamicDict('ifElse');
                        $scope.$broadcast('dictionaryReady');
                        $scope.$broadcast('dataReady');
                    })
                })();

                vm.save = function () {
                    if (mcsValidationService.run($scope)) {
                        vm.parent.addressDetail = customerDataViewService.getAddressDetail(vm);
                        vm.relation.isPrimary = vm.relation.isPrimary == 1 ? true : false;
                        vm.relation.customerRole = vm.customerRole;
                        vm.relation.parentRole = vm.parentRole;
                        var data = {
                            customer: vm.customer,
                            parent: vm.parent,
                            customerParentRelation: vm.relation,
                        };
                        customerService.handle('new-parent', data);
                    };
                }

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