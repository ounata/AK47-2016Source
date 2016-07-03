define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeAllotController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'mcsValidationService', 'dataSyncService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, mcsValidationService, dataSyncService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.applyID;

                    mcsValidationService.init($scope);

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
                            template: '<span><mcs-select category="subject" model="row.subject" caption="科目" async="false" required /></span>'
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
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getChargeApplyByApplyID(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;
                            vm.data.rows = result.apply.allot.items;

                            for (var index in vm.data.rows) {
                                var item = vm.data.rows[index];
                                item.itemNo = mcs.util.newGuid();
                                item.canEdit = false;
                                dataSyncService.injectDynamicDict(item.teacherJobs, { key: 'jobID', value: 'jobName', category: 'teacherJobs', keyName: item.itemNo });
                            }

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
                        });
                    }

                    //业绩分配表删除行
                    vm.removeRow = function () {
                        mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected);
                        vm.calcAllot();
                    }

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

                    //保存业绩分配
                    vm.save = function () {
                        if (!mcsValidationService.run($scope))
                            return;

                        mcsDialogService.confirm({ title: '确认', message: '是否确认保存？' })
                           .result.then(function () {
                               accountDataService.saveChargeAllot(vm.apply.allot, function () {
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