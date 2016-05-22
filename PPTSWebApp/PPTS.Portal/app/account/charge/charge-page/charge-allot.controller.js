define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeAllotController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.applyID;

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
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getChargeApplyByApplyID(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
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

                    //新增缴费单
                    vm.save = function () {
                        mcsDialogService.confirm({ title: '确认', message: '是否确认保存？' })
                           .result.then(function () {
                               accountDataService.saveChargeApply(vm.apply, function () {
                                   vm.cancel();
                               });
                           });
                    }

                    //取消
                    vm.cancel = function () {
                        $state.go(vm.page);
                    }

                }]);
        });