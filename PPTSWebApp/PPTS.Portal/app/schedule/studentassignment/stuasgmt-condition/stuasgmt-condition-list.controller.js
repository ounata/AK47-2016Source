define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (schedule) {
            schedule.registerController('stuAsgmtConditionController', [
                '$scope', '$state', 'dataSyncService', 'studentassignmentDataService', '$stateParams', 'blockUI', 'mcsDialogService',
                function ($scope, $state, dataSyncService, studentassignmentDataService, $stateParams, blockUI, mcsDialogService) {
                    var vm = this;
                    vm.customerID = $stateParams.id;
                    vm.criteria = vm.criteria || {};
                    vm.criteria.customerID = vm.customerID;

                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['conditionID'],
                        headers: [{
                            field: "orderID",
                            name: "订单编号",
                            template: '<span>{{   row.assetCode }}</span>',
                        }, {
                            field: "customerCode",
                            name: "产品名称",
                            template: '<span>{{row.productName}}</span>'
                        }, {
                            field: "grade",
                            name: "上课年级",
                            template: '<span>{{row.gradeName }}</span>'
                        }, {
                            field: "gender",
                            name: "上课科目",
                            template: '<span>{{row.subjectName}}</span>'
                        }, {
                            field: "birthday",
                            name: "编辑日期",
                            template: '<span>{{row.modifyTime | date:"yyyy-MM-dd"}}</span>'
                        }, {
                            field: "schoolName",
                            name: "所属学科组",
                            template: '<span>{{row.teacherJobOrgName}}</span>'
                        }, {
                            field: "educatorName",
                            name: "教师姓名",
                            template: '<span>{{row.teacherName}}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 20,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                studentAssignmentDataService.getACCPaged(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'ModifyTime', sortDirection: 1 }]
                    }

                    /*页面初始化加载或重新搜索时查询*/
                    vm.init = function () {
                        blockUI.start();
                        dataSyncService.initCriteria(vm);
                        studentassignmentDataService.getACC(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.injectDictData();
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                            blockUI.stop();
                        }, function (error) {
                            blockUI.stop();
                        });
                    };
                    vm.init();

                    vm.editAcc = function () {
                        if (vm.data.rowsSelected[0] == undefined) {
                            vm.showMsg("请选择一个排课条件");
                            return;
                        }
                        $state.go('ppts.accedit', { accid: vm.data.rowsSelected[0].conditionID,cid:vm.customerID });
                    };

                    vm.deleteAcc = function () {
                        if (vm.data.rowsSelected[0] == undefined) {
                            vm.showMsg("请选择一个排课条件");
                            return;
                        }

                        studentassignmentDataService.deleteAssignCondition(vm.data.rowsSelected, function (result) {
                            if (result.msg != 'ok') {
                                vm.showMsg(result.msg);
                                return;
                            }
                            vm.init();
                        }, function (error) {
                        });


                    };

                    //vm.getOrderNo = function (str) {
                    //    return str.substring(0, str.indexOf('-'));
                    //};

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };


                }]);
        });