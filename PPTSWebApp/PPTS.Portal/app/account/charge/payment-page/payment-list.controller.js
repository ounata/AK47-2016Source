define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargePaymentListController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.applyID;

                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['payID'],
                        headers: [{
                            field: "payMoney",
                            name: "本次收款金额",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.payMoney | currency:"￥"}}</span>'
                        }, {
                            field: "payer",
                            name: "付款人",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.payer}}</span>'
                        }, {
                            field: "subject",
                            name: "收款日期",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.payTime | date:"yyyy-MM-dd"}}</span>'
                        }, {
                            field: "payeeName",
                            name: "收款人",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.payeeName}}</span>'
                        }, {
                            field: "payType",
                            name: "收款类型",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.payType | payType}}</span>'
                        }, {
                            field: "payNo",
                            name: "付款单号",
                            headerCss: "col-sm-3",
                            template: '<span>{{row.payNo}}</span>'
                        }, {
                            field: "printStatus",
                            name: "收据状态",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.printStatus | printStatus}}</span>'
                        }, {
                            field: "payStatus",
                            name: "付款状态",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.payStatus | payStatus}}</span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'payID', sortDirection: 1 }]
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getChargeApplyByApplyID4Payment(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.data.rows = result.apply.payment.items;
                            vm.showAddButton = (vm.data.rows.length == 0);

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    vm.getCurrentRow = function () {

                        if (vm.data.rowsSelected.length == 1) {
                            for (var i = 0; i < vm.data.rows.length; i++) {
                                if (vm.data.rows[i].payID.toLowerCase() == vm.data.rowsSelected[0].payID.toLowerCase()) {
                                    return vm.data.rows[i];
                                }
                            }
                        }
                        return null;
                    }

                    //定向到编辑页面
                    vm.goEdit = function () {

                        $state.go('ppts.accountCharge-view.payment-edit', {
                            applyID: vm.apply.applyID,
                            prev: vm.page
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
                            vm.errorMessage = "该单据尚未支付，无法打印";
                            return;
                        }
                        var payID = currentRow.payID;
                        mcsDialogService.create('app/account/charge/payment-page/payment-print.html', {
                            controller: 'accountChargePaymentPrintController',
                            params: { payID: payID },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function () {
                            accountDataService.printChargePayment(payID, function (result) {
                                if (result) {
                                    for (var i = 0; i < vm.data.rows.length; i++) {
                                        if (vm.data.rows[i].payID.toLowerCase() == result.payID.toLowerCase())
                                            vm.data.rows[i] = result;
                                    }
                                }
                            });
                        });
                    }
                }]);
        });