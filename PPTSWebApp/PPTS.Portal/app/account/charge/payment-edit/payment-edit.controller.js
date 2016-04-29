define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargePaymentEditController', [
                '$scope', '$state', '$stateParams', 'mcsDialogService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, mcsDialogService, accountChargeDataService) {
                    var vm = this;
                    vm.id = $stateParams.applyID;

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['sortNo'],
                        headers: [{
                            field: "payMoney",                            
                            name: "本次收款金额",
                            headerCss: "col-sm-1",
                            template: '<span class="col-sm-2"><mcs-input model="row.payMoney" type="number" ng-blur="vm.calcPayMoney()" required="true" /></span>'
                        }, {
                            field: "payer",
                            name: "付款人",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.payer}}</span>'
                        }, {
                            field: "payTime",
                            name: "收款日期",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.payTime | date:"yyyy-MM-dd"}}</span>'
                        }, {
                            field: "payeeName",
                            name: "收款人",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.payeeName}}</span>'
                        }, {
                            field: "payType",
                            name: "收款类型",
                            headerCss: "col-sm-2",
                            template: '<span><ppts-select category="payType" model="row.payType" caption="付款类型"  async="false"  required="true"/></span>'
                        },  {
                            field: "serialNo",
                            name: "流水号",
                            headerCss: "col-sm-3",
                            template: '<span><mcs-input model="row.serialNo" required="true" /></span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'sortNo', sortDirection: 1 }]
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountChargeDataService.getChargeDisplayResultByApplyID4Payment(vm.id, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.data.rows = result.apply.payment.items;
                            if (vm.data.rows.length == 0) {
                                vm.addRow();
                                vm.addRow();
                                vm.addRow();
                            }
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();
                    

                    //收款记录删除
                    vm.addRow = function () {

                        var index = 0;
                        if (vm.data.rows.length != 0) {
                            index = vm.data.rows[vm.data.rows.length - 1].sortNo + 1;
                        }
                        vm.data.rows.push({
                            applyID : vm.apply.applyID,
                            sortNo: index,
                            payer: vm.apply.payment.payer,
                            payTime: vm.apply.payment.payTime,
                            payeeID: vm.apply.payment.payeeID,
                            payeeName: vm.apply.payment.payeeName,
                            payeeJobID: vm.apply.payment.payeeJobID,
                            payeeJobName: vm.apply.payment.payeeJobName,
                            payType: null,
                            serialNo: ''
                        });
                    }

                    //收款记录删除
                    vm.removeRow = function () {

                        mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected);
                        vm.calcPayMoney();
                    }


                    vm.calcPayMoney = function () {

                        var paidMoney = 0;
                        for (var i = 0; i < vm.apply.payment.items.length; i++) {

                            var item = vm.apply.payment.items[i];
                            if (item.payMoney) {
                                paidMoney += item.payMoney;
                            }
                        }
                        vm.apply.payment.paidMoney = paidMoney;
                    }

                    //支付保存
                    vm.save = function () {

                        if (vm.apply.chargeMoney != vm.apply.payment.paidMoney){

                            mcsDialogService.error({ title: '警告', message: '付款总额与应付金额不一致' });
                            return;
                        }
                        mcsDialogService.confirm({ title: '确认', message: '确认保存此付款记录吗？保存后，将不能注销或删除此付款记录！' })
                           .result.then(function () {
                               accountChargeDataService.saveChargePayment(vm.apply, function () {
                                   $state.go('ppts.accountCharge-view', { id: vm.customer.customerID });
                               });
                           });
                    }

                    //支付取消
                    vm.cancel = function() {

                    }

                }]);
        });

1