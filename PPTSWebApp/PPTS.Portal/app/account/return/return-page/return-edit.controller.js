define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountReturnDataService],
        function (account) {
            account.registerController('accountReturnEditController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountReturnDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.customerID = $stateParams.id;

                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['expenseID'],
                        headers: [{
                            field: "expenseType",
                            name: "服务费类型",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.expenseType | expenseType}}</span>'
                        }, {
                            field: "expenseMoney",
                            name: "服务费金额",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.expenseMoney | currency:"￥"}}</span>'
                        }, {
                            field: "memo",
                            name: "描述",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.memo}}</span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'expenseID', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getCustomerExpenses(vm.customerID, function (result) {
                            vm.customer = result.customer;
                            vm.items = result.items
                            
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    vm.getCurrentRow = function () {

                        if (vm.data.rowsSelected.length == 1) {
                            for (var i = 0; i < vm.data.rows.length; i++) {
                                if (vm.data.rows[i].expenseID == vm.data.rowsSelected[0].expenseID) {
                                    return vm.data.rows[i];
                                }
                            }
                        }
                        return null;
                    }

                    //新增转让单
                    vm.save = function () {

                        var currentRow = vm.getCurrentRow();
                        if (currentRow == null) {

                            mcsDialogService.error({ title: '警告', message: '请选择要返还的项目' });
                            return;
                        }
                        if (!currentRow.canReturn) {

                            mcsDialogService.error({ title: '警告', message: '该项服务费不可返还' });
                            return;
                        }
                        mcsDialogService.confirm({ title: '确认', message: '是否确认要返还该服务费项目？' })
                           .result.then(function () {
                               accountDataService.returnCustomerExpense(vm.customerID, currentRow.expenseID, function () {
                                   $state.go(vm.page);
                               });
                           });
                    }
                }]);
        });