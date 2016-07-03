/*
    名    称: customerVisitDataService
    功能概要: 客户回访
    作    者: 李辉
    创建时间: 
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerVisitDataService],
        function (customer) {
            customer.registerController('customerVisitAddBatchController', [
                '$state', '$scope', 'customerVisitDataService', 'customerVisitDataViewService', 'dataSyncService', 'visitAddBatchDataHeader', 'storage',
                function ($state, $scope, customerVisitDataService, customerVisitDataViewService, dataSyncService, visitAddBatchDataHeader, storage) {
                    var vm = this;

                    customerVisitDataViewService.initDate(vm);

                    customerVisitDataViewService.configVisitAddBatchHeaders(vm, visitAddBatchDataHeader);

                    (function () {
                        customerVisitDataViewService.initCreateCustomerVisitInfo(vm, function () {
                            dataSyncService.injectDynamicDict('messageType');
                            vm.data.rows = storage.get('selectedStudents');
                            //$scope.$on('selectedStudentsUpdated', function () {
                            //    vm.data.rows = customerService.selectedStudents;
                            //});
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    vm.close = function () {
                        storage.remove('selectedStudents');
                        $uibModalInstance.dismiss('Canceled');
                    };

                    // 保存
                    vm.save = function () {
                        var rows = vm.data.rows;
                        if (!rows || !rows.length) return;
                        // 只统计有输入得分项
                        var customerVisits = [];
                        for (var index in rows) {
                            var row = rows[index];
                            if (row.visitWay || row.visitType || row.satisficing || row.visitTime) {
                                customerVisits.push(row);
                            }
                        }
                        var data = {
                            customerVisits: customerVisits
                        };

                        customerVisitDataService.addVisitBatch(data, function (result) {
                            storage.remove('selectedStudents');
                            $state.go('ppts.student');
                        });
                    };

                    vm.cancel = function () {
                        storage.remove('selectedStudents');
                        $state.go('ppts.student');
                    };


                }]);
        });