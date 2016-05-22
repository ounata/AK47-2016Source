define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
        function (customer) {
            customer.registerController('customerSearchController', [
                '$scope', 'dataSyncService', 'customerDataViewService', 'utilService', '$uibModalInstance',
                function ($scope, dataSyncService, customerDataViewService, util, modal) {
                    var vm = this;

                    // 配置数据表头 
                    customerDataViewService.configCustomerSearchHeaders(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        customerDataViewService.initSearchCustomerList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();

                    vm.close = function () {
                        modal.dismiss('Canceled');
                    };

                    vm.select = function () {
                        if (util.selectOneRow(vm)) {
                            modal.close(vm.data.rowsSelected);
                        }
                    };
                }]);
        });