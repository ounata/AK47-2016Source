define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerParentAddController', [
            '$scope',
            'customerService',
            '$uibModalInstance',
            'dataSyncService',
            'customerDataService',
            'customerParentService',
            'parentSelectHeaders',
            'utilService',
            'data',
            function ($scope, customerService, $uibModalInstance, dataSyncService, customerDataService, customerParentService, parentSelectHeaders, util, data) {
                var vm = this, lastIndex = 0;

                // 配置数据表头 
                dataSyncService.configDataHeader(vm, parentSelectHeaders, customerDataService.getPagedParents);
                // 搜索
                vm.search = function () {
                    vm.parent = undefined;
                    vm.title = data.title;
                    vm.type = data.type;
                    vm.customer = data.customer;
                    if (data.customer.customerID) {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.customerID = data.customer.customerID;
                    }
                    dataSyncService.initDataList(vm, customerDataService.getAllParents, function () {
                        // 亲属关系切换
                        customerParentService.initCustomerParentRelation($scope, vm, lastIndex);
                        $scope.$broadcast('dataReady');
                    });
                };
                vm.search();

                // 添加
                vm.add = function () {
                    if (util.selectOneRow(vm)) {
                        var parentId = vm.data.rowsSelected[0].parentID;
                        var data = {
                            customer: vm.customer,
                            parent: vm.parent,
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
                };
                // 新建家长
                vm.new = function () {
                    customerService.handle('trans-new-parent', function () {
                        $uibModalInstance.dismiss('Canceled');
                    });
                };
                // 关闭
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };
            }]);
    });