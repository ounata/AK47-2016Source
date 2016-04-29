define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeEditController', [
                '$scope', '$state', '$stateParams', 'mcsDialogService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, mcsDialogService, accountChargeDataService) {
                    var vm = this;
                    vm.id = $stateParams.customerID;

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
                            field: "tearcherType",
                            name: "教师类型",
                            headerCss: "col-sm-1",
                            template: '<span><ppts-select category="teacherType" model="row.tearcherType" caption="教师类型"  async="false"  required="true"/></span>'
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
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountChargeDataService.getChargeDisplayResultByCustomerID(vm.id, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.discount = result.discount;
                            vm.data.rows = result.apply.allot.items;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();
                    
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

                    //计算本次账户价值
                    vm.calcAccountValue = function () {
                        if (!vm.apply)
                            return 0;
                        //新的账户价值=充值金额+以前的账户价值
                        var chargeMoney = 0;
                        if (vm.apply.chargeMoney)
                            chargeMoney = vm.apply.chargeMoney;
                        vm.apply.thisAccountValue = chargeMoney + vm.apply.thatAccountValue;
                        return vm.apply.thisAccountValue;
                    }

                    //计算本次折扣基数
                    vm.calcDiscountBase = function () {
                        if (!vm.apply)
                            return 0;
                        //新的账户价值大于上次折扣基数则使用新的账户价值作为折扣基数
                        var thisAccountValue = vm.calcAccountValue();
                        if (thisAccountValue > vm.apply.thatDiscountBase)
                            vm.apply.thisDiscountBase = thisAccountValue;
                        else
                            vm.apply.thisDiscountBase = vm.apply.thatDiscountBase;
                        return vm.apply.thisDiscountBase;
                    }

                    //计算本次折扣率
                    vm.calcDiscountRate = function () {
                        if (!vm.apply)
                            return 0;
                        if (vm.discount.items.length == 0)
                            return 1;

                        //新的折扣基数
                        var thatDiscountBase = vm.apply.thatDiscountBase;
                        var thatDiscountRate = vm.apply.thatDiscountRate;
                        var thisDiscountBase = vm.calcDiscountBase();
                        var thisDiscountRate = vm.discount.items[0].discountValue;
                        if (thisDiscountBase == thatDiscountBase)
                            thisDiscountRate = thatDiscountRate;
                        else {
                            for (var i = 0; i < vm.discount.items.length; i++) {

                                var item = vm.discount.items[i];
                                if (thisDiscountBase >= item.discountStandard * 10000) {
                                    thisDiscountRate = item.discountValue;
                                    break;
                                }
                            }
                        }
                        if (thisDiscountRate > thatDiscountRate)
                            thisDiscountRate = thatDiscountRate;
                        vm.apply.thisDiscountRate = thisDiscountRate;
                        return vm.apply.thisDiscountRate;
                    }

                    vm.fetchTeacher = function (row) {
                        if (row.teacherOACode && row.teacherOACode != '') {
                            accountChargeDataService.getTeacher(row.teacherOACode, function (result) {
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
                        vm.apply.totalAllotMoney = totalMoney;
                        vm.apply.totalAllotAmount = totalAmount;
                    }

                    //新增缴费单
                    vm.save = function () {
                        if (!vm.apply.chargeMoney || vm.apply.chargeMoney <= 0) {

                            mcsDialogService.error({ title: '警告', message: '请输入充值金额' });
                            return;
                        }
                        if (vm.apply.chargeMoney != 0
                            && vm.apply.chargeMoney < vm.apply.firstChargeMinMoney) {

                            mcsDialogService.error({ title: '警告', message: '首次充值金额要求不小于' + vm.apply.firstChargeMinMoney });
                            return;
                        }
                        mcsDialogService.confirm({ title: '确认', message: '是否确认保存？' })
                           .result.then(function () {
                               accountChargeDataService.saveChargeApply(vm.apply, function () {
                                   $state.go($stateParams.prev);
                               });
                           });
                    }

                    //取消
                    vm.cancel = function () {
                        $state.go($stateParams.prev);
                    }

                }]);
        });

1