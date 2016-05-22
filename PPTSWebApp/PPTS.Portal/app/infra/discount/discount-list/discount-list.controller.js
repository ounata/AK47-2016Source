define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.discountDataService],
        function (customer) {
            customer.registerController('discountListController', ['$scope', '$stateParams', 'utilService', 'discountDataViewService', 'discountListDataHeader', 'mcsDialogService',
                function ($scope, $stateParams, util, discountDataViewService, discountListDataHeader, mcsDialogService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    discountDataViewService.configDiscountListHeaders(vm, discountListDataHeader);

                    vm.init = function () {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.customerID = $stateParams.id;
                        discountDataViewService.initDiscountList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    vm.add = function () {
                    };
                }]);
        });