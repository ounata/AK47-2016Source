define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeQueryController', [
                '$scope', '$state', 'mcsDialogService', 'dataSyncService', 'accountChargeDataService', 'chargeQueryTable', 'chargeQueryAdvanceSearchItems',
                function ($scope, $state, mcsDialogService, dataSyncService, accountDataService, chargeQueryTable, searchItems) {
                    var vm = this;
                    
                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, chargeQueryTable, accountDataService.queryPagedChargeApplyList);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, accountDataService.queryChargeApplyList, function () {
                            vm.searchItems = searchItems;
                            dataSyncService.injectDynamicDict('relation,creation,dept');
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

                    //编辑业绩比例
                    vm.edit = function () {

                        vm.errorMessage = null;
                        var currentRow = vm.getCurrentRow();
                        if (currentRow == null) {
                            vm.errorMessage = "请选择一条要编辑的记录";
                            return;
                        }
                        var applyID = currentRow.applyID;
                        $state.go('ppts.accountCharge-allot', {
                            applyID: applyID,
                            prev: 'ppts.accountCharge-query'
                        });
                    }

                    //缴费单审核
                    vm.audit = function () {

                        vm.errorMessage = null;
                        var applyIDs = [];
                        for (var i = 0; i < vm.data.rows.length; i++) {
                            for (var j = 0; j < vm.data.rowsSelected.length; j++) {
                                if (vm.data.rows[i].applyID == vm.data.rowsSelected[j].applyID && vm.data.rows[i].canAudit) {
                                    applyIDs.push(vm.data.rows[i].applyID);
                                }
                            }
                        }
                        if (applyIDs.length == 0) {
                            vm.errorMessage = "只有付款完成未审核的记录才可审核，请选择需要审核的记录";
                            return;
                        }

                        mcsDialogService.create('app/account/charge/charge-page/charge-audit.html', {
                            controller: 'accountChargeAuditController',
                            params: {
                                auditCount: applyIDs.length
                            },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function () {

                            accountDataService.auditChargeApply(applyIDs, function () {
                                vm.search();
                            });
                        });
                    }

                    vm.export = function () {
                        if (vm.criteria.pageParams.totalCount < 50000) {
                            var dlg = mcsDialogService.confirm({
                                title: '提示',
                                message: '您将导出共' + vm.criteria.pageParams.totalCount + '条记录，请确认是否要导出？'
                            });
                            dlg.result.then(function () {
                                var url = ppts.config.customerApiBaseUrl + 'api/accounts/ExportAllCharges';
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
