define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargePaymentQueryController', [
                '$scope', '$state', 'mcsDialogService', 'dataSyncService', 'accountChargeDataService', 'chargePaymentQueryTable', 'paymentQueryAdvanceSearchItems',
                function ($scope, $state, mcsDialogService, dataSyncService, accountDataService, chargePaymentQueryTable, searchItems) {
                    var vm = this;

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, chargePaymentQueryTable, accountDataService.queryPagedChargePaymentList);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, accountDataService.queryChargePaymentList, function () {
                            vm.searchItems = searchItems;
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();

                    //收款单对账
                    vm.check = function () {

                        vm.errorMessage = null;
                        var payIDs = [];
                        for (var i = 0; i < vm.data.rows.length; i++) {
                            for (var j = 0; j < vm.data.rowsSelected.length; j++) {
                                if (vm.data.rows[i].payID == vm.data.rowsSelected[j].payID && vm.data.rows[i].canCheck) {
                                    payIDs.push(vm.data.rows[i].payID);
                                }
                            }
                        }
                        if (payIDs.length == 0) {
                            vm.errorMessage = "请选择需要对账的记录";
                            return;
                        }
                        mcsDialogService.confirm({ title: '确认', message: '确认要对账这' + payIDs.length + '条记录？' })
                           .result.then(function () {
                               accountDataService.checkChargePayment(payIDs, function () {
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
                                var url = ppts.config.customerApiBaseUrl + 'api/accounts/ExportAllChargePayments';
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
