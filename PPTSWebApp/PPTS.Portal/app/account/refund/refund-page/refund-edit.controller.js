﻿define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountRefundDataService],
        function (account) {
            account.registerController('accountRefundEditController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountRefundDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.customerID = $stateParams.id;

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['sortNo'],
                        headers: [{
                            field: "teacherOACode",
                            name: "OA账号",
                            headerCss: "col-sm-2",
                            template: '<span class="col-sm-2"><mcs-input model="row.teacherOACode" ng-blur="vm.fetchTeacher(row)" required="true" /></span>'
                        }, {
                            field: "teacherName",
                            name: "教师姓名",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.teacherName}}</span>'
                        }, {
                            field: "subject",
                            name: "科目",
                            headerCss: "col-sm-2",
                            template: '<span><ppts-select category="subject" model="row.subject" caption="科目" async="false"  required="true"/></span>'
                        }, {
                            field: "categoryType",
                            name: "产品类型",
                            headerCss: "col-sm-2",
                            template: '<span><ppts-select category="categoryType" model="row.categoryType" caption="产品类型"  async="false"  required="true"/></span>'
                        }, {
                            name: "岗位",
                            headerCss: "col-sm-1",
                            template: '<span>校教学教师</span>'
                        }, {
                            field: "teacherType",
                            name: "教师类型",
                            headerCss: "col-sm-1",
                            template: '<span><ppts-select category="teacherType" model="row.teacherType" caption="教师类型"  async="false"  required="true"/></span>'
                        }, {
                            field: "allotMoney",
                            name: "金额",
                            template: '<span><mcs-input model="row.allotMoney" type="number" ng-blur="vm.calcAllot()"  required="true"/></span>'
                        }, {
                            field: "allotAmount",
                            name: "课时",
                            template: '<span><mcs-input model="row.allotAmount" type="number" ng-blur="vm.calcAllot()"  required="true"/></span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'sortNo', sortDirection: 1 }]
                    }

                    //业绩分配表添加行
                    vm.addRow = function () {

                        var index = 0;
                        if (vm.data.rows.length != 0) {
                            index = vm.data.rows[vm.data.rows.length - 1].sortNo + 1;
                        }
                        vm.data.rows.push({
                            applyID: vm.apply.applyID,
                            sortNo: index,
                            teacherID: null,
                            teacherName: null,
                            teacherType: null,
                            teacherOACode: null,
                            subject: null,
                            categoryType: null,
                            allotAmount: null,
                            allotMoney: null
                        });
                    }

                    //业绩分配表删除行
                    vm.removeRow = function () {
                        mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected);
                        vm.calcAllot();
                    }

                    vm.fetchTeacher = function (row) {
                        if (row.teacherOACode && row.teacherOACode != '') {
                            accountDataService.getTeacher(row.teacherOACode, function (result) {
                                row.teacherID = null;
                                row.teacherName = null;
                                if (result) {
                                    row.teacherID = result.teacherID;
                                    row.teacherName = result.teacherName;
                                    row.teacherOACode = result.teacherOACode;
                                }
                            });
                        }
                    }

                    //计算业绩分配总额
                    vm.calcAllot = function () {
                        var totalMoney = 0;
                        var totalAmount = 0;

                        for (var i = 0; i < vm.apply.allot.items.length; i++) {

                            var item = vm.apply.allot.items[i];
                            if (item.allotMoney) {
                                totalMoney += item.allotMoney;
                            }
                            if (item.allotAmount) {
                                totalAmount += item.allotAmount;
                            }
                        }
                        vm.apply.allot.totalMoney = totalMoney;
                        vm.apply.allot.totalAmount = totalAmount;
                    }

                    vm.account = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['accountID'],
                        headers: [{
                            field: "accountCode",
                            name: "账户编码",
                            template: '<span>{{row.accountCode}}</span>'
                        }, {
                            field: "discountBase",
                            name: "折扣基数",
                            template: '<span>{{row.discountBase|currency:"￥"}}</span>'
                        }, {
                            field: "discountRate",
                            name: "折扣率",
                            template: '<span>{{row.discountRate|number:2}}</span>'
                        }, {
                            field: "accountValue",
                            name: "账户价值",
                            template: '<span>{{row.accountValue|currency:"￥"}}</span>'
                        }, {
                            field: "accountMoney",
                            name: "可退金额",
                            template: '<span>{{row.accountMoney|currency:"￥"}}</span>'
                        }, {
                            field: "assetMoney",
                            name: "订购资金余额",
                            template: '<span>{{row.assetMoney|currency:"￥"}}</span>'
                        }, {
                            field: "occurenceValue",
                            name: "当前课时价值",
                            template: '<span>{{row.occurenceValue|currency:"￥"}}</span>'
                        }, {
                            field: "consumptionValue",
                            name: "已消耗课时价值",
                            template: '<span>{{row.consumptionValue|currency:"￥"}}</span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'accountID', sortDirection: 1 }]
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getRefundApplyByCustomerID(vm.customerID, function (result) {
                            vm.apply = result.apply
                            vm.assert = result.assert;
                            vm.customer = result.customer;
                            vm.discount = result.discount;
                            vm.data.rows = result.apply.allot.items;
                            vm.account.rows = result.accounts;
                            vm.errorMessage = result.assert.message;
                            vm.canSave = result.assert.ok;

                            if (vm.account.rows.length == 1) {
                                vm.account.rows[0].selected = true;
                                vm.account.rowsSelected.push({ accountID: vm.account.rows[0].accountID })
                            }
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    vm.getCurrentRow = function () {

                        if (vm.account.rowsSelected.length == 1) {
                            for (var i = 0; i < vm.account.rows.length; i++) {
                                if (vm.account.rows[i].accountID == vm.account.rowsSelected[0].accountID) {
                                    return vm.account.rows[i];
                                }
                            }
                        }
                        return null;
                    }

                    vm.calcData = function() {
                        if (!vm.apply)
                            return;
                        if (vm.apply.applyRefundMoney == 'undefined')
                            return;
                        if (!vm.apply.compensateMoney || vm.apply.compensateMoney < 0)
                            vm.apply.compensateMoney = 0;
                        if (!vm.apply.extraRefundMoney || vm.apply.extraRefundMoney < 0)
                            vm.apply.extraRefundMoney = 0;

                        //新的账户余额=以前的账户余额-申退金额
                        vm.apply.thisAccountMoney = vm.apply.thatAccountMoney - vm.apply.applyRefundMoney;
                        //新的账户价值=以前的账户价值-申退金额
                        vm.apply.thisAccountValue = vm.apply.thatAccountValue - vm.apply.applyRefundMoney;

                        //当前发生的课时价值(confirmedValue>=thatDiscountBase)当前折扣基数
                        if (vm.apply.occurenceValue >= vm.apply.thatDiscountBase) {
                            vm.apply.consumptionValue = 0;
                            vm.apply.reallowanceMoney = 0;

                            vm.apply.thisDiscountID = vm.apply.thatDiscountID;
                            vm.apply.thisDiscountCode = vm.apply.thatDiscountCode;
                            vm.apply.thisDiscountBase = vm.apply.thatDiscountBase;
                            vm.apply.thisDiscountRate = vm.apply.thatDiscountRate;
                        }
                        else {
                            //计算折扣相关数值
                            //新的账户价值=以前的账户价值                            
                            vm.apply.thisDiscountBase = vm.apply.thatDiscountBase - vm.apply.applyRefundMoney;
                            //新的折扣率计算
                            vm.apply.thisDiscountRate = 1;
                            if (vm.discount && vm.discount.items.length != 0) {
                                vm.apply.thisDiscountID = vm.discount.discountID;
                                vm.apply.thisDiscountCode = vm.discount.discountCode;
                                vm.apply.thisDiscountRate = vm.discount.items[0].discountValue;
                                for (var i = 0; i < vm.discount.items.length; i++) {

                                    var item = vm.discount.items[i];
                                    if (vm.apply.thisDiscountBase >= item.discountStandard * 10000) {
                                        vm.apply.thisDiscountRate = item.discountValue;
                                        break;
                                    }
                                }
                            }

                            //已消耗课时价值不变
                            //折扣返还金额 = 已消耗课时价值/退费前折扣率*（退费后折扣率-退费前折扣率）
                            if (vm.apply.thatDiscountRate > 0) {
                                vm.apply.reallowanceMoney = vm.apply.consumptionValue / vm.apply.thatDiscountRate * (vm.apply.thisDiscountRate - vm.apply.thatDiscountRate);
                            }
                        }
                        //应退金额 = 申退金额-折扣返还
                        vm.apply.oughtRefundMoney = vm.apply.applyRefundMoney - vm.apply.reallowanceMoney;
                        //实退金额 = 应退金额-差价补偿+制度外退款
                        vm.apply.realRefundMoney = vm.apply.oughtRefundMoney - vm.apply.compensateMoney + vm.apply.extraRefundMoney;
                    }

                    //监控申退金额
                    $scope.$watch('vm.apply.applyRefundMoney', function () {
                        vm.calcData();
                    });

                    //监控差价补偿
                    $scope.$watch('vm.apply.compensateMoney', function () {
                        vm.calcData();
                    });

                    //监控制度外退费
                    $scope.$watch('vm.apply.extraRefundMoney', function () {
                        vm.calcData();
                    });

                    //监控账户的选择
                    $scope.$watch('vm.account.rowsSelected[0]', function () {

                        vm.errorMessage = null;
                        vm.canSave = true;
                        var row = vm.getCurrentRow();
                        if (row) {

                            vm.canSave = (row.assetMoney == 0);
                            if (!vm.canSave) {
                                vm.errorMessage = "该账户订购资金不为0，请先进行退订再进行退费";
                            }

                            vm.apply.accountID = row.accountID;
                            vm.apply.accountCode = row.accountCode;
                            vm.apply.thatDiscountID = row.discountID;
                            vm.apply.thatDiscountCode = row.discountCode;
                            vm.apply.thatDiscountBase = row.discountBase;
                            vm.apply.thatDiscountRate = row.discountRate;
                            vm.apply.thatAccountValue = row.accountValue;
                            vm.apply.thatAccountMoney = row.accountMoney;

                            vm.apply.assetMoney = row.assetMoney;  //订购资金余额
                            vm.apply.occurenceValue = row.occurenceValue;      //当前发生课时价值
                            vm.apply.consumptionValue = row.consumptionValue;  //已消耗课时价值
                            vm.apply.applyRefundMoney = row.accountMoney;      //申退金额
                        }
                    });
                    //新增转让单
                    vm.save = function () {

                        var currentRow = vm.getCurrentRow();
                        if (currentRow == null) {

                            mcsDialogService.error({ title: '警告', message: '请选择要退费的账户' });
                            return;
                        }
                        if (vm.apply.applyRefundMoney == 'undefined')
                        {
                            mcsDialogService.error({ title: '警告', message: '请输入合法的申退金额' });
                            return;
                        }
                        if (vm.apply.applyRefundMoney <= 0) {

                            mcsDialogService.error({ title: '警告', message: '申退金额必须大于零' });
                            return;
                        }
                        if (vm.apply.applyRefundMoney > currentRow.accountMoney) {

                            mcsDialogService.error({ title: '警告', message: '申退金额不能大于可退金额' });
                            return;
                        }
                        mcsDialogService.confirm({ title: '确认', message: '是否确认提交审批？' })
                           .result.then(function () {
                               accountDataService.saveRefundApply(vm.apply, function () {
                                   $state.go(vm.page);
                               });
                           });
                    }
                }]);
        });