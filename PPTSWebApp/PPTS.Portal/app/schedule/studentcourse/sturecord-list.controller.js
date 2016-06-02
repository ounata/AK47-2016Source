/*学员上课记录*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentCourseDataService],
        function (schedule) {
            schedule.registerController('stucrsListController', [
                '$scope', '$state', 'dataSyncService', 'studentCourseDataService', '$stateParams', 'blockUI', 'mcsDialogService', 'printService',
                function ($scope, $state, dataSyncService, studentCourseDataService, $stateParams, blockUI, mcsDialogService, printService) {
                    var vm = this;
                    vm.weekText = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");

                    vm.assignSource = '';
                    vm.assignStatus = '';

                    vm.criteria = vm.criteria || {};

                    vm.criteria.customerID = $stateParams.id;
                    vm.criteria.campusID = '';
                    vm.criteria.subject = '';
                    vm.criteria.grade = '';
                    vm.criteria.assignStatus = '';
                    vm.criteria.assignSource = '';              
                    vm.criteria.teacherName = '';
                    vm.criteria.educatorName = '';
                    vm.criteria.consultantName = '';
                    vm.criteria.assetCode = '';

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['assignID'],
                        headers: [{
                            field: "teacherName",
                            name: "教师姓名",
                            template: '<span>{{row.teacherName}}</span>'
                        }, {
                            field: "customerName",
                            name: "学员姓名",
                            template: '<span>{{row.customerName}}</span>'
                        }, {
                            field: "startTime",
                            name: "上课日期",
                            template: '<span>{{ vm.getCourseDate(row.startTime) }}</span>'
                        }, {
                            field: "endTime",
                            name: "上课时段",
                            template: '<span>{{  row.courseSE }}</span>'
                        }, {
                            field: "amount",
                            name: "课时",
                            template: '<span>{{row.amount}}</span>'
                        }, {
                            field: "realAmount",
                            name: "实际小时",
                            template: '<span>{{ row.realTime }}</span>'
                        }, {
                            field: "subjectName",
                            name: "上课科目",
                            template: '<span>{{row.subjectName}}</span>'
                        }, {
                            field: "gradeName",
                            name: "上课年级",
                            template: '<span>{{row.gradeName}}</span>'
                        }, {
                            field: "educatorName",
                            name: "学管师",
                            template: '<span>{{row.educatorName}}</span>'
                        }, {
                            field: "consultantName",
                            name: "咨询师",
                            template: '<span>{{row.consultantName}}</span>'
                        }, {
                            field: "assignStatus",
                            name: "课时状态",
                            template: '<span>{{row.assignStatus | assignStatus }}</span>'
                        }, {
                            field: "assignSource",
                            name: "课时类型",
                            template: '<span>{{row.assignSource | assignSource }}</span>'
                        }, {
                            field: "assetCode",
                            name: "订单编号",
                            template: '<span>{{ row.assetCode }}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 20,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                studentCourseDataService.getStuClassRecordPaged(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'startTime', sortDirection: 1 }]
                    }


                  

                    vm.getCourseDate = function (sDate) {
                        var startDate = new Date(sDate);
                        return startDate.getFullYear() + '-' + vm.getDoubleStr((startDate.getMonth() + 1)) + '-' + vm.getDoubleStr(startDate.getDate())
                         + '(' + vm.weekText[startDate.getDay()] + ')';
                    };

                    vm.getDoubleStr = function (curValue) {
                        if (parseInt(curValue) < 10)
                            return '0' + curValue;
                        return curValue;
                    };

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        blockUI.start();
                        dataSyncService.initCriteria(vm);

                        vm.criteria.assignSource = new Array();
                        vm.criteria.assignStatus = new Array();
                        if (vm.assignSource != '' && vm.assignSource != null)
                            vm.criteria.assignSource[0] = vm.assignSource;
                        if (vm.assignStatus != '' && vm.assignStatus != null)
                            vm.criteria.assignStatus[0] = vm.assignStatus;

                        studentCourseDataService.getStuClassRecord(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.injectDictData();
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                            blockUI.stop();
                        });
                    };
                    vm.init();


                    /*补录课时*/
                    vm.markupSchedule = function () {
                        mcsDialogService.create('app/schedule/studentcourse/sturecord-list-markup.html', {
                            controller: 'stucrsMarkupController',
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function (retValue) {
                            if (retValue != 'canceled') {
                                vm.queryList();
                            };
                        });
                    };

                    /*删除课表*/
                    vm.deleteCourseClick = function () {
                        var msg = '';
                        var model = [];
                        var selectedObj = vm.data.rowsSelected;
                        if (selectedObj.length == 0) {
                            vm.showMsg("请选择要删除的课表");
                            return;
                        }
                        var objCollecton = vm.data.rows;
                        for (var i in selectedObj) {
                            for (var j in objCollecton) {
                                if (selectedObj[i].assignID == objCollecton[j].assignID) {
                                    ///已上状态的课表可以删除
                                    if (objCollecton[j].assignStatus == 3) {
                                        model.push({
                                            assetID: objCollecton[j].assetID,
                                            assignID: objCollecton[j].assignID,
                                            startTime: objCollecton[j].startTime,
                                            assignStatus: objCollecton[j].assignStatus,
                                        });
                                    }
                                    else {
                                        var curDate = objCollecton[j].startTime.getFullYear() + '-' + vm.getDoubleStr((objCollecton[j].startTime.getMonth() + 1)) + '-' + vm.getDoubleStr(objCollecton[j].startTime.getDate());
                                        var curWeek = vm.weekText[objCollecton[j].startTime.getDay()];
                                        var startText = vm.getDoubleStr(objCollecton[j].startTime.getHours()) + ':' + vm.getDoubleStr(objCollecton[j].startTime.getMinutes());
                                        var endText = vm.getDoubleStr(objCollecton[j].endTime.getHours()) + ':' + vm.getDoubleStr(objCollecton[j].endTime.getMinutes());
                                        msg += objCollecton[j].customerName + "学员" + curDate + "（" + curWeek + "）" + startText + "-" + endText + "的" + objCollecton[j].subjectName + "课表不是已上状态，不能删除<br>";
                                    };
                                    break;
                                }

                            };
                        };
                        if (msg != '') {
                            vm.showMsg(msg);
                            return;
                        }
                        msg = "确定删除？";
                        mcsDialogService.confirm({
                            title: '删除课表',
                            message: msg
                        }).result.then(function () {
                            studentCourseDataService.deleteAssign(model, function (result) {
                                vm.queryList();
                                vm.showMsg("删除成功");
                            },
                            function (error) {

                            });
                        });

                    };


                    /*确认课表*/
                    vm.confirmCourseClick = function () {
                        var msg = '';
                        var model = [];
                        var selectedObj = vm.data.rowsSelected;
                        if (selectedObj.length == 0) {
                            vm.showMsg("请选择要确认的课表");
                            return;
                        }
                        var objCollecton = vm.data.rows;
                        for (var i in selectedObj) {
                            for (var j in objCollecton) {
                                if (selectedObj[i].assignID == objCollecton[j].assignID) {
                                    ///异常状态的课表可以确认
                                    if (objCollecton[j].assignStatus == 8) {
                                        model.push({
                                            assetID: objCollecton[j].assetID,
                                            assignID: objCollecton[j].assignID,
                                            assignStatus: objCollecton[j].assignStatus,
                                            teacherName: objCollecton[j].teacherName,
                                            customerName: objCollecton[j].customerName,
                                            subjectName: objCollecton[j].subjectName,
                                            startTime: objCollecton[j].startTime,
                                            endTime: objCollecton[j].endTime,
                                            amount: objCollecton[j].amount
                                        });
                                    }
                                    else {
                                        var curDate = objCollecton[j].startTime.getFullYear() + '-' + vm.getDoubleStr((objCollecton[j].startTime.getMonth() + 1)) + '-' + vm.getDoubleStr(objCollecton[j].startTime.getDate());
                                        var curWeek = vm.weekText[objCollecton[j].startTime.getDay()];
                                        var startText = vm.getDoubleStr(objCollecton[j].startTime.getHours()) + ':' + vm.getDoubleStr(objCollecton[j].startTime.getMinutes());
                                        var endText = vm.getDoubleStr(objCollecton[j].endTime.getHours()) + ':' + vm.getDoubleStr(objCollecton[j].endTime.getMinutes());
                                        msg += objCollecton[j].customerName + "学员" + curDate + "（" + curWeek + "）" + startText + "-" + endText + "的" + objCollecton[j].subjectName + "课表不是异常状态，不能确认<br>";
                                    };
                                    break;
                                }

                            };
                        };
                        if (msg != '') {
                            vm.showMsg(msg);
                            return;
                        }
                        mcsDialogService.create('app/schedule/studentcourse/stucourse-list/stucourse-confirm.html', {
                            controller: 'stuCourseConfirmController',
                            params: { para: model },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function (retValue) {
                            if (retValue == 'ok') {
                                vm.queryList();
                            };
                        });
                    };

                    /*取消课表*/
                    vm.cancelCourseClick = function () {
                        var msg = '';
                        var model = [];
                        var selectedObj = vm.data.rowsSelected;
                        if (selectedObj.length == 0) {
                            vm.showMsg("请选择要取消的课表");
                            return;
                        }
                        var objCollecton = vm.data.rows;
                        for (var i in selectedObj) {
                            for (var j in objCollecton) {
                                ///异常，排定的课表可以取消
                                if (selectedObj[i].assignID == objCollecton[j].assignID && (objCollecton[j].assignStatus == 1 || objCollecton[j].assignStatus == 8)) {
                                    var curDate = objCollecton[j].startTime.getFullYear() + '-' + vm.getDoubleStr((objCollecton[j].startTime.getMonth() + 1)) + '-' + vm.getDoubleStr(objCollecton[j].startTime.getDate());
                                    var curWeek = vm.weekText[objCollecton[j].startTime.getDay()];
                                    var startText = vm.getDoubleStr(objCollecton[j].startTime.getHours()) + ':' + vm.getDoubleStr(objCollecton[j].startTime.getMinutes());
                                    var endText = vm.getDoubleStr(objCollecton[j].endTime.getHours()) + ':' + vm.getDoubleStr(objCollecton[j].endTime.getMinutes());
                                    msg += "确定将" + objCollecton[j].customerName + "学员" + curDate + "（" + curWeek + "）" + startText + "-" + endText + "的" + objCollecton[j].subjectName + "课取消？<br>";
                                    model.push({
                                        assetID: objCollecton[j].assetID,
                                        assignID: objCollecton[j].assignID
                                    });
                                    break;
                                };
                            };
                        };
                        if (msg == "")
                            return;
                        mcsDialogService.confirm({
                            title: '取消课表',
                            message: msg
                        }).result.then(function () {
                            studentassignmentDataService.cancelAssign(model, function (result) {
                                vm.queryList();
                                vm.showMsg("取消成功");
                            },
                            function (error) {

                            });
                        });
                    };

                    vm.queryList = function () {
                        vm.init();
                    };

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                    vm.print = function () {
                        printService.print();
                    }

                }]);
        });