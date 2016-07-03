/*
    名    称: servicefee-list.controller
    功能概要: 综合服务费Controller js
    作    者: Lucifer
    创建时间: 2016年5月20日 13:32:46
    修正履历：
    修正时间:
*/
define([ppts.config.modules.infra,
        ppts.config.dataServiceConfig.servicefeeDataService],
        function (customer) {
            customer.registerController('servicefeeListController', 
                ['$scope', '$state', 'dataSyncService', 'utilService', 'servicefeeDataService', 'mcsDialogService', 'servicefeeListDataHeader',
                function ($scope, $state, dataSyncService, utilService, servicefeeDataService, mcsDialogService, servicefeeListDataHeader) {
                    var vm = this;

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, servicefeeListDataHeader, servicefeeDataService.getPagedServiceFees, function () {
                        $scope.$broadcast('dictionaryReady');
                    });

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, servicefeeDataService.getAllServiceFees, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };

                    vm.search();


                    vm.add = function () {
                        $state.go('ppts.servicefee-add', { prev: "ppts.servicefee" });
                    }
                    vm.edit = function () {
                        if (utilService.selectOneRow(vm)) {
                            $state.go('ppts.servicefee-edit', { expenseID: vm.data.rowsSelected[0].expenseID, prev: "ppts.servicefee" });
                        }
                    }
                    vm.delete = function () {
                        if (utilService.selectMultiRows(vm)) {
                            mcsDialogService.confirm({
                                title: '删除综合服务费',
                                message: '确认删除吗?'
                            }).result.then(function (reslut) {
                                if ("yes" == reslut) {
                                    var expenseIds = [];
                                    for(var index in vm.data.rowsSelected){
                                        expenseIds.push(vm.data.rowsSelected[index].expenseID);
                                    }
                                    servicefeeDataService.delExpenses(expenseIds, function () {
                                        vm.search();
                                    });
                                }
                            });
                           
                        }
                    }
                    
                    vm.search();
                }]);
        });