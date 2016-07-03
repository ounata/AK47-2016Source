define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeEditController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'mcsValidationService', 'dataSyncService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, mcsValidationService, dataSyncService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.path = $stateParams.path;
                    vm.showCancel = (vm.path != 'tabnew');

                    mcsValidationService.init($scope);

                    if ($stateParams.applyID)
                        vm.applyID = $stateParams.applyID;
                    else if ($stateParams.id)
                        vm.customerID = $stateParams.id;

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['sortNo'],
                        headers: [{
                            field: "teacherOACode",
                            name: "OA账号",
                            template: '<span><mcs-input model="row.teacherOACode" ng-blur="vm.fetchTeacher(row)" required ng-disabled="{{!row.canEdit}}" /></span>'
                        }, {
                            field: "teacherName",
                            name: "教师姓名",
                            template: '<span>{{row.teacherName}}</span>'
                        }, {
                            field: "subject",
                            name: "科目",
                            template: '<span><mcs-select category="subject" model="row.subject" caption="科目" async="false" required/></span>'
                        }, {
                            field: "categoryType",
                            name: "产品类型",
                            template: '<span><mcs-select category="categoryType" model="row.categoryType" caption="产品类型" async="false" required /></span>'
                        }, {
                            name: "岗位",
                            template: '<span><mcs-select category="teacherJobs_{{row.itemNo}}" model="row.teacherJobID" callback="vm.selectTeacherJob(item, model, row)" caption="岗位" ignore-async="true" required /></span>'
                        }, {
                            field: "teacherType",
                            name: "教师类型",
                            template: '<span>{{row.teacherType | teacherType}}</span>'
                        }, {
                            field: "allotMoney",
                            name: "金额",
                            template: '<span><mcs-input model="row.allotMoney" datatype="number" ng-blur="vm.calcAllot()" required currency less="0" great="999999" /></span>'
                        }, {
                            field: "allotAmount",
                            name: "课时",
                            template: '<span><mcs-input model="row.allotAmount" datatype="number" ng-blur="vm.calcAllot()" required positive less="0" great="999999" /></span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'sortNo', sortDirection: 1 }]
                    }

                    vm.callChargeApply = function (result) {

                        vm.apply = result.apply
                        vm.assert = result.assert;
                        vm.customer = result.customer;
                        vm.discount = result.discount;
                        vm.data.rows = result.apply.allot.items;
                        vm.errorMessage = result.assert.message;

                        for (var index in vm.data.rows) {
                            var item = vm.data.rows[index];
                            item.itemNo = mcs.util.newGuid();
                            item.canEdit = false;
                            dataSyncService.injectDynamicDict(item.teacherJobs, { key: 'jobID', value: 'jobName', category: 'teacherJobs', keyName: item.itemNo });
                        }

                        $scope.$broadcast('dictionaryReady');
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        if (vm.customerID) {
                            accountDataService.getChargeApplyByCustomerID(vm.customerID, function (result) {
                                vm.callChargeApply(result);
                            });
                        }
                        else {
                            accountDataService.getChargeApplyByApplyID(vm.applyID, function (result) {
                                vm.callChargeApply(result);
                            });
                        }
                    };
                    vm.init();

                    //业绩分配表添加行
                    vm.addRow = function () {

                        var index = 0;
                        if (vm.data.rows.length != 0) {
                            index = vm.data.rows[vm.data.rows.length - 1].sortNo + 1;
                        }

                        var row = {
                            itemNo: mcs.util.newGuid(),
                            applyID: vm.apply.applyID,
                            sortNo: index,
                            teacherID: null,
                            teacherName: null,
                            teacherType: null,
                            teacherOACode: null,
                            subject: null,
                            categoryType: null,
                            allotAmount: null,
                            allotMoney: null,
                            canEdit: true
                        };
                        vm.data.rows.push(row);
                    }

                    //业绩分配表删除行
                    vm.removeRow = function () {
                        mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected);
                        vm.calcAllot();
                    }

                    //监控金额变化
                    $scope.$watch('vm.apply.chargeMoney', function () {

                        if (!vm.apply)
                            return;
                        if (typeof (vm.apply.chargeMoney) == 'undefined' || isNaN(vm.apply.chargeMoney))
                            vm.apply.chargeMoney = null;
                        if (vm.apply.chargeMoney < 0)
                            vm.apply.chargeMoney = 0;

                        //新的账户价值=充值金额+以前的账户价值
                        vm.apply.thisAccountValue = vm.apply.thatAccountValue + vm.apply.chargeMoney;

                        //新的账户价值大于上次折扣基数则使用新的账户价值作为折扣基数
                        if (vm.apply.thisAccountValue > vm.apply.thatDiscountBase)
                            vm.apply.thisDiscountBase = vm.apply.thisAccountValue;
                        else
                            vm.apply.thisDiscountBase = vm.apply.thatDiscountBase;

                        //新的折扣率计算
                        vm.apply.thisDiscountRate = 1;
                        if (vm.apply.thisDiscountBase == vm.apply.thatDiscountBase)
                            vm.apply.thisDiscountRate = vm.apply.thatDiscountRate;
                        else if (vm.discount && vm.discount.items.length != 0) {
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
                        if (vm.apply.thisDiscountRate > vm.apply.thatDiscountRate)
                            vm.apply.thisDiscountRate = vm.apply.thatDiscountRate;
                    });

                    vm.selectTeacherJob = function (item, model, row) {

                        for (var i = 0; i < row.teacherJobs.length; i++) {
                            if (row.teacherJobs[i].jobID == model) {
                                row.teacherType = row.teacherJobs[i].teacherType;
                                break;
                            }
                        }
                    }
                    vm.fetchTeacher = function (row) {
                        if (row.teacherOACode && row.teacherOACode != '') {
                            var campusID = vm.customer.campusID;
                            accountDataService.getTeacher(campusID, row.teacherOACode, function (result) {
                                row.teacherID = null;
                                row.teacherName = null;
                                row.teacherJobs = [];
                                if (result) {
                                    row.teacherID = result.teacherID;
                                    row.teacherName = result.teacherName;
                                    row.teacherOACode = result.teacherOACode;
                                    row.teacherJobs = result.teacherJobs;
                                    row.teacherType = null;
                                    row.async = true;
                                    row.canEdit = false;
                                }

                                dataSyncService.injectDynamicDict(row.teacherJobs, { key: 'jobID', value: 'jobName', category: 'teacherJobs', keyName: row.itemNo });
                                $scope.$broadcast('dictionaryReady');
                            });
                        }
                    }

                    //计算业绩分配总额
                    vm.calcAllot = function () {
                        var totalMoney = 0;
                        var totalAmount = 0;

                        for (var i = 0; i < vm.apply.allot.items.length; i++) {

                            var item = vm.apply.allot.items[i];
                            if (item.allotMoney > 0) {
                                totalMoney += item.allotMoney;
                            }
                            if (item.allotAmount > 0) {
                                totalAmount += item.allotAmount;
                            }
                        }
                        vm.apply.allot.totalMoney = totalMoney;
                        vm.apply.allot.totalAmount = totalAmount;
                    }

                    //新增缴费单
                    vm.save = function () {

                        if (!vm.assert.ok) {

                            mcsDialogService.error({ title: '警告', message: vm.errorMessage });
                            return;
                        }
                        if (!mcsValidationService.run($scope))
                            return;
                        if (vm.apply.chargeMoney == 'undefined') {

                            mcsDialogService.error({ title: '警告', message: '请输入合法充值金额' });
                            return;
                        }
                        if (vm.apply.firstChargeMinMoney != 0
                            && vm.apply.chargeMoney < vm.apply.firstChargeMinMoney) {

                            mcsDialogService.error({ title: '警告', message: '首次充值金额要求不小于' + vm.apply.firstChargeMinMoney });
                            return;
                        }
                        if (vm.apply.chargeMoney <= 0) {

                            mcsDialogService.error({ title: '警告', message: '充值金额金额必须大于零' });
                            return;
                        }
                        mcsDialogService.confirm({ title: '确认', message: '是否确认保存？' })
                           .result.then(function () {
                               accountDataService.saveChargeApply(vm.apply, function () {
                                   vm.cancel();
                               });
                           });
                    }

                    //删除
                    vm.delete = function () {

                        mcsDialogService.confirm({ title: '确认', message: '是否确认删除该缴费单？' })
                           .result.then(function () {
                               accountDataService.deleteChargeApply(vm.apply.applyID, function () {
                                   if (vm.path == 'tabupd') {
                                       $state.go('ppts.student-view.accountChargeList', {
                                           id: vm.customer.customerID,
                                           prev: vm.page
                                       });
                                   }
                                   else {
                                       vm.init();
                                   }
                               });
                           });
                    }

                    //取消
                    vm.cancel = function () {
                        if (vm.path == 'tabupd') {
                            $state.go('ppts.accountCharge-view.info', {
                                applyID: vm.apply.applyID,
                                prev: vm.page
                            });
                        } else {
                            $state.go(vm.page);
                        }
                    }

                }]);
        });