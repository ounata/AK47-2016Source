define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.confirmdoorDataService],
        function (customer) {
            customer.registerController('confirmdoorAddController', ['$scope', '$state', '$stateParams', 'mcsDialogService', 'utilService', 'confirmdoorDataService', 'confirmdoorDataViewService', 'confirmdoorInstutionItem',
            function ($scope, $state, $stateParams, mcsDialogService, util, confirmdoorDataService, confirmdoorDataViewService, confirmdoorInstutionItem) {
                var vm = this;

                // 页面初始化加载
                (function () {
                    confirmdoorDataViewService.initCreateConfirmdoorInfo($stateParams, vm, function () {
                        $scope.$broadcast('dictionaryReady');
                    });
                })();

                // 保存数据
                vm.save = function () {
                    vm.confirmdoor.customerID = vm.customer.customerID;
                    vm.confirmdoor.campusID = vm.customer.campusID;
                    vm.confirmdoor.campusName = vm.customer.campusName;
                    confirmdoorDataService.createConfirmDoor({
                        confirmdoor: vm.confirmdoor
                    }, function () {
                        $state.go('ppts.confirmdoor');
                    });
                };

                vm.cancel = function () {
                    $state.go('ppts.confirmdoor');
                }

                vm.error = function () {
                    mcsDialogService.error({
                        title: '提示',
                        message: '您没有输入任何搜索条件!'
                    })
                };

                vm.instutionItems = [confirmdoorInstutionItem.template];
            }]);
        });