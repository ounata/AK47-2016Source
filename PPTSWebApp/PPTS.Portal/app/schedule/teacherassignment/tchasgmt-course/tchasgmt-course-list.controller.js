define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherAssignmentDataService], function (schedule) {
            schedule.registerController("tchAsgmtCourseListController", [
              "$scope", '$state', 'dataSyncService', 'teacherAssignmentDataService', '$stateParams', 'mcsDialogService', 'utilService',
                function ($scope, $state, dataSyncService, teacherAssignmentDataService, $stateParams, mcsDialogService, utilService) {
                    var vm = this;
                    vm.weekText = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
                    vm.CID = $stateParams.cID;
                    vm.criteria = vm.criteria || {};
                    vm.criteria.teacherID = vm.CID;
                    vm.criteria.teacherJobID = $stateParams.tji;
                    vm.criteria.startTime = '';
                    vm.criteria.endTime = '';
                    vm.criteria.customerName = '';
                    vm.criteria.grade = '';
                    vm.criteria.subject = '';
                    vm.criteria.assignStatus = '';
                    vm.criteria.categoryType = '';
                    vm.criteria.assetCode = '';

                    vm.categoryType = '';
                    vm.assignStatus = '';

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
                            template: '<span> {{ row.realTime }}  </span>'
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
                            field: "categoryTypeName",
                            name: "课时类型",
                            template: '<span>{{ row.categoryTypeName }}</span>'
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
                                teacherAssignmentDataService.getPagedSCLV(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'StartTime', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initCriteria(vm);

                        vm.criteria.categoryType = new Array();
                        vm.criteria.assignStatus = new Array();
                        if (vm.categoryType != '' && vm.categoryType != null)
                            vm.criteria.categoryType[0] = vm.categoryType;
                        if (vm.assignStatus != '' && vm.assignStatus != null)
                            vm.criteria.assignStatus[0] = vm.assignStatus;

                        teacherAssignmentDataService.getSCLV(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();

                    /*新增课表*/
                    vm.createSchedule = function () {
                        mcsDialogService.create('app/schedule/teacherassignment/tchasgmt-add/tchasgmt-add.html', {
                            controller: 'tchAsgmtAddController',
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function (retValue) {
                            if (retValue != 'canceled') {
                                vm.search();

                            };
                        });
                    };

                    /*取消课表*/
                    vm.cancelCourseClick = function () {
                        if (!utilService.selectMultiRows(vm)) return;
                        var msg = '';
                        var model = [];
                        var selectedObj = vm.data.rowsSelected;
                        var objCollecton = vm.data.rows;
                        for (var i in selectedObj) {
                            for (var j in objCollecton) {
                                if (selectedObj[i].assignID == objCollecton[j].assignID
                                    && (objCollecton[j].assignStatus == 1 || objCollecton[j].assignStatus == 8)
                                    && objCollecton[j].categoryType == 1
                                    ) {
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
                        if (utilService.showMessage(vm, model.length == 0, '选择的记录不允许取消，请重新选择！')) {
                            return;
                        }
                        mcsDialogService.confirm({
                            title: '取消课表',
                            message: msg
                        }).result.then(function () {
                            teacherAssignmentDataService.cancelAssign(model, function (result) {
                                vm.search();
                                vm.showMsg("取消成功");
                            },
                            function (error) {

                            });
                        });
                    };

                    /*复制课表*/
                    vm.copyCourseClick = function () {
                        mcsDialogService.create('app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course-copy.html', {
                            controller: 'tchAsgmtCourseCopyController'
                        }).result.then(function (retValue) {
                            if (retValue == 'ok') {
                                vm.search();
                            };
                        });
                    };

                    /*调整课表*/
                    vm.resetCourseClick = function () {
                        if (!utilService.selectMultiRows(vm)) return;
                        /*将聂鑫航（ST00001）2016-05-05（星期二）13:00 - 15:00 的数学课调整为*/
                        var model = [], msg = '', tempdate2 = new Date();
                        var tempdate = new Date(tempdate2.getFullYear(), tempdate2.getMonth(), tempdate2.getDate(), 0, 0, 0, 0);
                        var selectedObj = vm.data.rowsSelected;
                        var objCollecton = vm.data.rows;
                        for (var j in selectedObj) {
                            for (var i in objCollecton) {
                                if (selectedObj[j].assignID == objCollecton[i].assignID
                                    && (objCollecton[i].assignStatus == 1 || objCollecton[i].status == 8)
                                    && objCollecton[j].categoryType == 1
                                    ) {
                                    var curDate = objCollecton[i].startTime.getFullYear() + '-' + vm.getDoubleStr((objCollecton[i].startTime.getMonth() + 1)) + '-' + vm.getDoubleStr(objCollecton[i].startTime.getDate());
                                    var curWeek = vm.weekText[objCollecton[i].startTime.getDay()];
                                    var startText = vm.getDoubleStr(objCollecton[i].startTime.getHours()) + ':' + vm.getDoubleStr(objCollecton[i].startTime.getMinutes());
                                    var endText = vm.getDoubleStr(objCollecton[i].endTime.getHours()) + ':' + vm.getDoubleStr(objCollecton[i].endTime.getMinutes());

                                    msg = "将" + objCollecton[i].customerName + "（" + objCollecton[i].customerCode + "）"
                                        + curDate + "（" + curWeek + "）" + startText + " - " + endText + "的" + objCollecton[i].subjectName + "课调整为";
                                    var tempM = {
                                        assignID: objCollecton[i].assignID,
                                        info: msg,
                                        reDate: new Date(),
                                        reHour: '',
                                        reMinute: '',
                                        startTime: objCollecton[i].startTime,
                                        endTime: objCollecton[i].endTime,
                                        allowResetDateTime: new Date(),
                                        customerID: objCollecton[i].customerID
                                    };
                                    tempM.reHour = vm.getDoubleStr(objCollecton[i].startTime.getHours());
                                    tempM.reMinute = vm.getDoubleStr(objCollecton[i].startTime.getMinutes());
                                    model.push(tempM);
                                };
                            }
                        };
                      
                        if (utilService.showMessage(vm, model.length == 0, '选择的记录不允许调课，请重新选择！')) {
                            return;
                        }

                        mcsDialogService.create('app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course-reset.html', {
                            controller: 'tchAsgmtCourseResetController',
                            params: { para: model },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function (retValue) {
                            if (retValue == 'ok') {
                                vm.search();
                            };
                        });
                    };

                    vm.getDoubleStr = function (curValue) {
                        if (parseInt(curValue) < 10)
                            return '0' + curValue;
                        return curValue;
                    };

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                    vm.gotoCourseWeek = function () {
                        $state.go('ppts.tchasgmt-course', { cID: vm.CID,tn:$stateParams.tn,tji:$stateParams.tji });
                    };

                    vm.getCourseDate = function (sDate) {
                        var startDate = new Date(sDate);
                        return startDate.getFullYear() + '-' + vm.getDoubleStr((startDate.getMonth() + 1)) + '-' + vm.getDoubleStr(startDate.getDate())
                         + '(' + vm.weekText[startDate.getDay()] + ')';
                    };

                    /*跳转教师排课列表*/
                    vm.gotoAssignList = function () {
                        $state.go('ppts.schedule-tchasgmt');
                    };

                }]);
        });