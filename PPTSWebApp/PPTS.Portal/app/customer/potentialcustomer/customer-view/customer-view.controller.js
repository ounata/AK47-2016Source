define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerViewController', [
            '$scope',
            '$state',
            '$stateParams',
            'customerDataService',
            function ($scope, $state, $stateParams, customerDataService) {
                var vm = this;

                // 页面初始化加载
                (function () {
                    customerDataService.getCustomerForUpdate($stateParams.id, function (result) {
                        vm.customer = result.customer;
                        $scope.$broadcast('dictionaryReady');
                    });
                })();

                // 切换子视图
                vm.loadView = function () {
                    var baseDir = ppts.config.webportalBaseUrl + 'app/customer/potentialcustomer/customer-view';
                    switch ($stateParams.page) {
                        case 'info':
                            return baseDir + '/customer-info.tpl.html';
                        case 'edit':
                            return baseDir + '/customer-edit.tpl.html';
                    }
                };

                // 保存数据
                vm.save = function () {
                    customerDataService.updateCustomer({
                        customer: vm.customer
                    }, function () {
                        vm.redirect('info');
                    });
                };

                // 切换页面(用于编辑和取消)
                vm.redirect = function (page) {
                    $state.go('ppts.customer-view', {
                        id: $stateParams.id,
                        page: page
                    });
                };
            }]);
    });