define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerEditController', [
            '$scope',
            '$stateParams',
            'customerService',
            'customerDataViewService',
            'customerRelationType',
            function ($scope, $stateParams, customerService, customerDataViewService, relationType) {
                var vm = this;

                // 页面初始化加载
                customerService.handle('init', function (result) {
                    vm.customer = result.customer;
                    vm.parent = result.parent;
                    vm.customerStaffRelation = result.customerStaffRelation;

                    $scope.$broadcast('dictionaryReady');
                });

                // 获取学员与员工关系
                vm.relationType = relationType;

                // 查看咨询师/学管师/坐席/市场专员历史
                vm.viewHistory = function (relationType) {
                    customerDataViewService.viewStaffRelation($stateParams.id, relationType);
                };

                // 保存数据
                vm.save = function () {
                    customerService.handle('save', { customer: vm.customer });
                };

                // 取消
                vm.cancel = function () {
                    customerService.handle('cancel');
                }
            }]);
    });