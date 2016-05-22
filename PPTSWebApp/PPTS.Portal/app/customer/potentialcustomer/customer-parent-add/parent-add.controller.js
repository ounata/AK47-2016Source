define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerParentAddController', [
            '$scope',
            'customerService',
            '$uibModalInstance',
            'customerDataService',
            'customerParentService',
            'utilService',
            'data',
            function ($scope, customerService, $uibModalInstance, customerDataService, customerParentService, util, data) {
                var vm = this, lastIndex = 0;;

                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                // 搜索
                vm.search = function () {
                    vm.parent = undefined;
                    customerParentService.getAllParents(vm, data);
                }

                // 添加
                vm.add = function () {
                    if (util.selectOneRow(vm)) {
                        var parentId = vm.data.rowsSelected[0].parentID;
                        var data = {
                            customer: vm.customer,
                            primaryParent: vm.parent,
                            customerRole: vm.customerRole,
                            parentRole: vm.parentRole
                        };
                        if (vm.type == "add") {
                            customerDataService.getParentInfo(parentId, function (result) {
                                $uibModalInstance.close(result.parent);
                            });
                        } else if (vm.type == "confirm") {
                            customerService.handle('confirm-parent', data, function () {
                                $uibModalInstance.dismiss('Canceled');
                            });
                        }
                    }
                }

                vm.initData = function () {
                    vm.type = data.type;
                    vm.customer = data.customer;
                }

                // 新建家长
                vm.new = function () {
                    customerService.handle('trans-new-parent', function () {
                        $uibModalInstance.dismiss('Canceled');
                    });
                };

                // 页面初始化加载
                vm.initData();
                customerParentService.configParentAddHeaders(vm);
                customerParentService.getAllParents(vm, data, function () {
                    $scope.$broadcast('dictionaryReady');
                });

                // 亲属关系切换
                customerParentService.initCustomerParentRelation($scope, vm, lastIndex);
            }]);
    });