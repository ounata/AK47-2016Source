define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountRefundDataService],
        function (account) {
            account.registerController('accountRefundQueryController', [
                '$scope', '$state', 'mcsDialogService', 'dataSyncService', 'accountRefundDataService',
                function ($scope, $state, mcsDialogService, dataSyncService, accountDataService) {
                    var vm = this;

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['applyID'],
                        headers: [{
                            field: "customerName",
                            name: "学员姓名",
                            template: '<span>{{row.customerName}}</span>'
                        }, {
                            field: "customerCode",
                            name: "学员编号",
                            template: '<span>{{row.customerCode}}</span>'
                        }, {
                            field: "thatAccountMoney",
                            name: "账户总额",
                            template: '<span>{{row.thatAccountMoney | currency:"￥"}}</span>'
                        }, {
                            field: "oughtRefundMoney",
                            name: "应退金额",
                            template: '<span>{{row.oughtRefundMoney | currency:"￥"}}</span>'
                        }, {
                            field: "realRefundMoney",
                            name: "实退金额",
                            template: '<span>{{row.realRefundMoney | currency:"￥"}}</span>'
                        }, {
                            field: "drawer",
                            name: "领款人",
                            template: '<span>{{row.drawer}}</span>'
                        }, {
                            field: "applierName",
                            name: "申请人",
                            template: '<span>{{row.applierName}}</span>'
                        }, {
                            field: "applierJobName",
                            name: "申请人岗位",
                            template: '<span>{{row.applierJobName}}</span>'
                        }, {
                            field: "campusName",
                            name: "申请校区",
                            template: '<span>{{row.campusName}}</span>'
                        }, {
                            field: "approveTime",
                            name: "业务终审日期",
                            template: '<span>{{row.approveTime | date:"yyyy-MM-dd"}}</span>'
                        }, {
                            field: "verifyTime",
                            name: "财务终审日期",
                            template: '<span>{{row.verifyTime | date:"yyyy-MM-dd"}}</span>'
                        }, {
                            field: "checkStatus",
                            name: "对账状态",
                            template: '<span ng-class="{1: \'ppts-checked-color\', 0: \'\'}[{{row.checkStatus}}]">{{row.checkStatus | checkStatus}}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 10,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                customerDataService.queryPagedRefundApplyList(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'applyTime', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {

                        dataSyncService.initCriteria(vm);
                        accountDataService.queryRefundApplyList(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.injectDictData();
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();

                    vm.getCurrentRow = function () {

                        if (vm.data.rowsSelected.length == 1) {
                            for (var i = 0; i < vm.data.rows.length; i++) {
                                if (vm.data.rows[i].applyID.toLowerCase() == vm.data.rowsSelected[0].applyID.toLowerCase()) {
                                    return vm.data.rows[i];
                                }
                            }
                        }
                        return null;
                    }

                    //分出纳确认
                    vm.cashierVerify = function () {
                        vm.errorMessage = null;
                        var currentRow = vm.getCurrentRow();
                        if (currentRow != null && !currentRow.cashierCanVerify) {
                            vm.errorMessage = "当前记录不可进行分出纳确认";
                            return;
                        }
                        vm.verify('cashierVerify', '分出纳确认', currentRow);
                    }

                    //分财务确认
                    vm.financeVerify = function () {
                        vm.errorMessage = null;
                        var currentRow = vm.getCurrentRow();
                        if (currentRow != null &&!currentRow.financeCanVerify) {
                            vm.errorMessage = "当前记录不可进行分财务确认";
                            return;
                        }
                        vm.verify('financeVerify', '分财务确认', currentRow);
                    }

                    //分区域确认
                    vm.regionVerify = function () {
                        vm.errorMessage = null;
                        var currentRow = vm.getCurrentRow();
                        if (currentRow != null && !currentRow.regionCanVerify) {
                            vm.errorMessage = "当前记录不可进行分区域确认";
                            return;
                        }
                        vm.verify('regionVerify', '分区域确认', currentRow);
                    }
                    //退费单确认
                    vm.verify = function (action, actionName, currentRow) {
                        if (currentRow == null) {
                            vm.errorMessage = "请选择一条要确认的记录";
                            return;
                        }
                        var applyID = currentRow.applyID;
                        mcsDialogService.create('app/account/refund/refund-page/refund-verify.html', {
                            controller: 'accountRefundVerifyController',
                            params: { applyID: applyID, actionName : actionName },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function () {
                            accountDataService.verifyRefundApply(action, applyID, function (result) {
                                if (result) {
                                    for (var i = 0; i < vm.data.rows.length; i++) {
                                        if (vm.data.rows[i].applyID.toLowerCase() == result.applyID.toLowerCase()) {

                                            vm.data.rows[i].verifierJobName = result.verifierJobName;
                                            vm.data.rows[i].verifierName = result.verifierName;
                                            vm.data.rows[i].verifyTime = result.verifyTime;

                                            vm.data.rows[i].cashierCanVerify = result.cashierCanVerify;
                                            vm.data.rows[i].financeCanVerify = result.financeCanVerify;
                                            vm.data.rows[i].regionCanVerify = result.regionCanVerify;
                                            vm.data.rows[i].canCheck = result.canCheck;
                                            vm.data.rows[i].canPrint = result.canPrint;
                                        }
                                    }
                                }
                            });
                        });
                    }

                    //退费单对账
                    vm.check = function () {

                        vm.errorMessage = null;
                        var applyIDs = [];
                        for (var i = 0; i < vm.data.rows.length; i++) {
                            for (var j = 0; j < vm.data.rowsSelected.length; j++) {
                                if (vm.data.rows[i].applyID == vm.data.rowsSelected[j].applyID && vm.data.rows[i].canCheck) {
                                    applyIDs.push({ applyID: vm.data.rows[i].applyID });
                                }
                            }
                        }

                        if (applyIDs.length == 0) {
                            vm.errorMessage = "请勾选需要对账的记录";
                            return;
                        }
                        mcsDialogService.confirm({ title: '确认', message: '确认要对账这' + applyIDs.length + '条记录？' })
                           .result.then(function () {
                               accountDataService.checkRefundApply(applyIDs, function () {
                                   vm.search();
                               });
                           });
                    }

                    vm.showPrint = function () {

                        vm.errorMessage = null;
                        var currentRow = vm.getCurrentRow();
                        if (currentRow == null) {
                            return;
                        }
                        if (!currentRow.canPrint) {
                            vm.errorMessage = "该单据尚未最终确认，无法打印";
                            return;
                        }
                        var applyID = currentRow.applyID;
                        mcsDialogService.create('app/account/refund/refund-page/refund-print.html', {
                            controller: 'accountRefundPrintController',
                            params: { applyID: applyID },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function () {
                            //do nothing
                        });
                    }
                }]);
        });
