define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargePaymentEditController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'mcsValidationService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, mcsValidationService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.applyID;

                    mcsValidationService.init($scope);

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['sortNo'],
                        headers: [{
                            field: "payMoney",
                            name: "本次收款金额",
                            headerCss: "col-sm-1",
                            template: '<span class="col-sm-2"><mcs-input model="row.payMoney" datatype="number" ng-blur="vm.calcPayMoney()" required currency less="0" great="9999999" ng-disabled="row.isVerified" /></span>'
                        }, {
                            field: "payer",
                            name: "付款人",
                            headerCss: "col-sm-2",
                            template: '<span><mcs-input model="row.payer" placeholder="请输入付款人" required maxlength="10"/></span>'
                        }, {
                            field: "payTime",
                            name: "收款日期",
                            headerCss: "col-sm-2",
                            template: '<span><mcs-datepicker model="row.inputTime" disabled="row.isVerified" start-date="vm.apply.payment.payStartTime" end-date="vm.apply.payment.payEndTime" required z-index="1" /></span>'
                        }, {
                            field: "payeeName",
                            name: "收款人",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.payeeName}}</span>'
                        }, {
                            field: "payType",
                            name: "收款类型",
                            headerCss: "col-sm-2",
                            template: '<span><mcs-select category="payType" model="row.payType" caption="收款类型" async="false" required disabled="row.isVerified" callback="vm.selectPayType(item,model,row)"/></span>'
                        }, {
                            field: "payTicket",
                            name: "流水号",
                            headerCss: "col-sm-3",
                            template: '<span><mcs-input model="row.payTicket" placeholder="请输入流水号" ng-show="row.showPayTicketTextBox" required ng-disabled="row.isVerified" /><mcs-button category="ok" size="medium" text="验证流水号" click="vm.verifyPayTicket(row)" ng-show="row.showPayTicketVerifyButton" /><div class="help-block" ng-show="row.showPayTicketTextBox"></div></span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'sortNo', sortDirection: 1 }]
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getChargeApplyByApplyID4Payment(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.data.rows = result.apply.payment.items;
                            if (vm.data.rows.length == 0) {
                                vm.addRow();
                            }
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    //选择收款类型
                    vm.selectPayType = function (item, model, row) {
                        //payType 是1表示现金，2表示电汇，4表示支票，其它都是刷卡
                        row.showPayTicketTextBox = false;
                        row.showPayTicketVerifyButton = false;
                        row.needVerify = false;
                        row.limitInput = false;
                        if (model == 2 || model == 4) {
                            row.showPayTicketTextBox = true;
                        }
                        if (model != 1 && model != 2 && model != 4) {
                            row.showPayTicketTextBox = true;
                            row.showPayTicketVerifyButton = true;
                            row.needVerify = true;
                            row.limitInput = true;
                        }
                    }

                    //验证流水号
                    vm.verifyPayTicket = function (row) {
                        if (row.payType == null) {
                            mcsDialogService.error({ title: '警告', message: '请选择收款类型' });
                            return;
                        }
                        if (row.payTicket == null || row.payTicket == '') {
                            mcsDialogService.error({ title: '警告', message: "请输入交易流水号" });
                            return;
                        }
                        accountDataService.getPosRecord(vm.apply.campusID, row.payTicket, row.payType, function (result) {
                            if (!result.ok) {
                                mcsDialogService.error({ title: '警告', message: result.message });
                                return;
                            }
                            var m = result.record;
                            row.payMoney = m.payMoney;
                            row.inputTime = m.payTime;
                            row.swipeTime = m.swipeTime;
                            row.isVerified = true;
                        });
                    }
                    
                    //收款记录删除
                    vm.addRow = function () {

                        var index = 0;
                        if (vm.data.rows.length != 0) {
                            index = vm.data.rows[vm.data.rows.length - 1].sortNo + 1;
                        }
                        vm.data.rows.push({
                            applyID: vm.apply.applyID,
                            sortNo: index,
                            payer: vm.apply.payment.payer,
                            inputTime: vm.apply.payment.nowTime,
                            swipeTime: vm.apply.payment.nowTime,
                            payeeID: vm.apply.payment.payeeID,
                            payeeName: vm.apply.payment.payeeName,
                            payeeJobID: vm.apply.payment.payeeJobID,
                            payeeJobName: vm.apply.payment.payeeJobName,
                            payMoney : null,
                            payType: null,
                            payTicket: '',
                            isVerified: false,
                            needVerify: false,
                            limitInput: false,
                            showPayTicketTextBox: false,
                            showPayTicketVerifyButton: false
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

                        if (!mcsValidationService.run($scope))
                            return;

                        vm.apply.payTime = vm.apply.payment.nowTime;
                        vm.apply.swipeTime = vm.apply.payment.nowTime;
                        for (var i = 0; i < vm.data.rows.length; i++) {
                            var itm = vm.data.rows[i];
                            if (!itm.limitInput)
                                itm.swipeTime = itm.inputTime;

                            if (vm.apply.payTime > itm.inputTime) {
                                vm.apply.payTime = itm.inputTime;
                            }
                            if (vm.apply.swipeTime > itm.swipeTime) {
                                vm.apply.swipeTime = itm.swipeTime;
                            }
                            if (itm.needVerify && !itm.isVerified) {

                                mcsDialogService.error({ title: '警告', message: '存在没有验证的流水号' });
                                return;
                            }
                        }

                        if (vm.apply.chargeMoney != vm.apply.payment.paidMoney) {

                            mcsDialogService.error({ title: '警告', message: '付款总额与应付金额不一致' });
                            return;
                        }

                        var payTimeStr = moment(vm.apply.payTime).format('YYYY-MM-DD');
                        var message = '您的收款日期为“' +  payTimeStr + '”\n';
                        message += '确认保存此付款记录吗？保存后，将不能注销或删除此付款记录！';
                        mcsDialogService.confirm({ title: '确认', message: message })
                           .result.then(function () {
                               accountDataService.saveChargePayment(vm.apply, function () {
                                   $state.go('ppts.accountCharge-view.payment-list', {
                                       id: vm.customer.customerID,
                                       prev: vm.page
                                   });
                               });
                           });
                    }

                    //支付取消
                    vm.cancel = function () {
                        $state.go('ppts.accountCharge-view.payment-list', {
                            applyID: vm.apply.applyID,
                            prev: vm.page
                        });
                    }

                }]);
        });