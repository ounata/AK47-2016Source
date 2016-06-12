/*
    名    称: servicefee-edit.controller
    功能概要: 综合服务费 edit Controller js
    作    者: Lucifer
    创建时间: 2016年5月20日 13:32:46
    修正履历：
    修正时间:
*/
define([ppts.config.modules.infra,
        ppts.config.dataServiceConfig.servicefeeDataService],
        function (customer) {
            customer.registerController('servicefeeEditController', ['$scope', 'servicefeeEditViewService', '$state', 'servicefeeDataService', '$stateParams', 'mcsValidationService',
                function ($scope, servicefeeEditViewService, $state, servicefeeDataService, $stateParams, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    if (!vm.expense) vm.expense = {};
                    vm.expense.expenseID = $stateParams.expenseID
                    // 初始化配置
                    servicefeeEditViewService.configServiceFee(vm, function () {
                        //vm.searchItems = searchItems;
                        $scope.$broadcast('dictionaryReady');
                    });

                    vm.save = function () {
                        if (mcsValidationService.run($scope)) {
                            servicefeeDataService.createExpenses(vm, function () {
                                //alert("添加成功!");
                                $state.go("ppts.servicefee", { prev: 'ppts' });
                            });
                        }
                    }
                    vm.cancel = function () {
                        $state.go('ppts.servicefee', { prev: "ppts" });
                    }
                }]);
        });