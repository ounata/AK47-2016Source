define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountDisplayDataService],
    function (customer) {
        customer.registerController('accountController', [
            '$scope',
            '$state',
            '$stateParams',
            'accountDataService',
            function ($scope, $state, $stateParams, accountDataService) {
                var vm = this;

                // 页面初始化加载
                (function () {
                    accountDataService.getCustomerForUpdate($stateParams.id, function (result) {
                        vm.customer = result.customer;
                        vm.parent = result.parent;
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
            }]);
    });