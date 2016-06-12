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
            customer.registerController('servicefeeListController', ['$scope', 'servicefeeDataViewService', '$state', 'utilService', 'servicefeeDataService','mcsDialogService',
                function ($scope, servicefeeDataViewService, $state, util, servicefeeDataService, mcsDialogService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    servicefeeDataViewService.configServiceFeeListHeaders(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        servicefeeDataViewService.initServiceFeeList(vm, function () {
                            //vm.searchItems = searchItems;
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.add = function () {
                        $state.go('ppts.servicefee-add', { prev: "ppts.servicefee" });
                    }
                    vm.edit = function () {
                        if (util.selectOneRow(vm)) {
                            $state.go('ppts.servicefee-edit', { expenseID: vm.data.rowsSelected[0].expenseID, prev: "ppts.servicefee" });
                        }
                    }
                    vm.delete = function () {
                        if (util.selectMultiRows(vm)) {
                            mcsDialogService.confirm({
                                title: 'confirm',
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