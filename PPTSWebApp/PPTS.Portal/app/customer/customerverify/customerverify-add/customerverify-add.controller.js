define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerVerifyDataService],
        function (customer) {
            customer.registerController('customerVerifyAddController', ['$scope', '$state', '$stateParams', 'mcsDialogService', 'utilService', 'customerVerifyDataService', 'customerVerifyDataViewService', 'mcsValidationService', 'customerVerifyInstutionItem',
            function ($scope, $state, $stateParams, mcsDialogService, util, customerVerifyDataService, customerVerifyDataViewService, mcsValidationService, customerVerifyInstutionItem) {
                var vm = this;
                mcsValidationService.init($scope);
                // 页面初始化加载
                (function () {
                    customerVerifyDataViewService.initCreateCustomerVerifyInfo($stateParams, vm, function () {
                        $scope.$broadcast('dictionaryReady');
                    });
                })();

                // 保存数据
                vm.save = function () {
                    if (mcsValidationService.run($scope)) {
                        vm.customerVerify.customerID = vm.customer.customerID;
                        vm.customerVerify.campusID = vm.customer.campusID;
                        vm.customerVerify.campusName = vm.customer.campusName;
                        customerVerifyDataService.createCustomerVerify(vm.customerVerify, function () {
                            $state.go('ppts.customerverify');
                        });
                    }
                };

                vm.cancel = function () {
                    $state.go('ppts.customerverify');
                }

                vm.error = function () {
                    mcsDialogService.error({
                        title: '提示',
                        message: '您没有输入任何搜索条件!'
                    })
                };

                vm.instutionItems = [customerVerifyInstutionItem.template];
            }]);
        });