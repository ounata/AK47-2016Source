define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountRefundDataService],
        function (account) {
            account.registerController('accountRefundQueryController', [
                '$scope', '$state', 'mcsDialogService', 'dataSyncService', 'accountRefundDataService', 'refundQueryTable', 'refundQueryAdvanceSearchItems',
                function ($scope, $state, mcsDialogService, dataSyncService, accountDataService, refundQueryTable, searchItems) {
                    var vm = this;

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, refundQueryTable, accountDataService.queryPagedRefundApplyList);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, accountDataService.queryRefundApplyList, function () {
                            vm.searchItems = searchItems;
                            dataSyncService.injectDynamicDict('applierJobType');
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

                    vm.doVerify = function (item) {
                        switch (item.text) {
                            case '分出纳确认':
                                vm.cashierVerify();
                                break;
                            case '分财务确认':
                                vm.financeVerify();
                                break;
                            case '分区域确认':
                                vm.regionVerify();
                                break;
                            default:
                                break;
                        }
                    };

                    vm.verifyMenus = [
                        { text: '分出纳确认', click: vm.doVerify, permission: '待分出纳确认-本分公司' },
                        { text: '分财务确认', click: vm.doVerify, permission: '待分财务确认-本分公司' },
                        { text: '分区域确认', click: vm.doVerify, permission: '待分区域财务确认-本分公司' }
                    ];

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
                                            vm.data.rows[i].verifyStatus = result.verifyStatus;

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
                                    applyIDs.push(vm.data.rows[i].applyID);
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
                            vm.errorMessage = "请选择一条要打印的记录";
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

                    vm.export = function () {
                        if (vm.criteria.pageParams.totalCount < 50000) {
                            var dlg = mcsDialogService.confirm({
                                title: '提示',
                                message: '您将导出共' + vm.criteria.pageParams.totalCount + '条记录，请确认是否要导出？'
                            });
                            dlg.result.then(function () {
                                var url = ppts.config.customerApiBaseUrl + 'api/accounts/ExportAllRefundApply';
                                mcs.util.postMockForm(url, vm.criteria);
                            });
                        } else {
                            mcsDialogService.info({
                                title: '提示',
                                message: '内容超过5万条以上，无法正常导出，请缩小范围后再尝试!'
                            }
                            );
                        }
                    };
                }]);
        });
