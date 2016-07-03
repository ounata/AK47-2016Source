define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService],
    function (customer) {
        customer.registerController('customerEditController', [
            '$scope',
            '$stateParams',
            'utilService',
            'dataSyncService',
            'customerService',
            'customerDataViewService',
            'customerRelationType',
            'mcsValidationService',
            function ($scope, $stateParams, util, dataSyncService, customerService, customerDataViewService, relationType, mcsValidationService) {
                var vm = this;

                // 页面初始化加载
                customerService.handle('init', function (result) {
                    vm.customer = result.customer;
                    vm.parent = result.parent;
                    vm.customerName = vm.customer.customerName;
                    vm.isCustomer = result.isCustomer;
                    customerDataViewService.getRelationInfo(vm, result.customerStaffRelations);

                    dataSyncService.injectDynamicDict('ifElse');
                    $scope.$broadcast('dictionaryReady');
                });

                // 获取学员与员工关系
                vm.relationType = relationType;

                // 查看咨询师/学管师/坐席/市场专员历史
                vm.viewHistory = function (relationType) {
                    customerDataViewService.viewStaffRelation($stateParams.id, relationType);
                };

                // 归属教师
                vm.viewHistoryTeachers = function (customerID) {
                    customerDataViewService.viewHistoryTeachers(customerID);
                };

                // 保存数据
                vm.save = function () {
                    if (mcsValidationService.run($scope)) { 
                        customerService.handle('save', { customer: vm.customer });
                    }
                };

                // 取消
                vm.cancel = function () {
                    customerService.handle('cancel');
                };

                // 学员姓名验证
                vm.checkCustomerName = function (target) {
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
                // 学员证件类型切换
                $scope.$watch('vm.customer.idType', function (idType) {
                    if (vm.customer && vm.customer.idNumber) {
                        vm.isIdCard(idType, vm.customer.idNumber, '#customerIdNumber');
                    }
                });
                // vip客户
                $scope.$watch('vm.customer.vipType', function (vipType) {
                    if (vipType == 3) {
                        vm.customer.vipLevel = '';
                        $scope.$broadcast('dictionaryReady');
                    }
                });
                // 家长、学员姓名只能输入英文和汉字
                $scope.$watch('vm.customer.customerName', function (customerName) {
                    if (!customerName) return;
                    vm.customer.customerName = customerName.replace(/[^\a-zA-Z\u4E00-\u9FA5]/g, '');
                });
            }]);
    });