/*
    名    称: servicefee-add.controller
    功能概要: 综合服务费 添加Controller js
    作    者: Lucifer
    创建时间: 2016年5月20日 13:32:46
    修正履历：
    修正时间:
*/
define([ppts.config.modules.infra,
        ppts.config.dataServiceConfig.servicefeeDataService],
        function (customer) {
            customer.registerController('servicefeeAddController', ['$scope', 'servicefeeAddViewService', '$state', 'servicefeeDataService', 'mcsValidationService',
                function ($scope, servicefeeAddViewService, $state, servicefeeDataService, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    // 初始化配置
                    servicefeeAddViewService.configServiceFee(vm, function () {
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