﻿define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService], function (schedule) {
            schedule.registerController("stuAsgmtCourseController", [
                 '$scope', '$state', '$stateParams', '$filter', 'dataSyncService', 'utilService', '$compile',
                 '$uibModal', '$http', 'studentassignmentDataService', 'mcsDialogService', 'uiCalendarConfig', 'printService','$location',
            function ($scope, $state, $stateParams, $filter, dataSyncService, utilService, $compile,
                $uibModal, $http, studentassignmentDataService, mcsDialogService, uiCalendarConfig, printService, $location) {

                var vm = this;
                vm.page = $location.$$search.prev;
                vm.CID = $stateParams.id;
                //vm.stuName = $stateParams.tn;
                vm.selectedEvents = [];
                vm.events = [];

                vm.weekText = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
                vm.acQM = { customerID: vm.CID, startTime: '2016-04-18', endTime: '2016-05-09', isUTCTime: false, grade: '', assignStatus: '', teacherName: '', categoryType: '' };

                /*配置日程组件*/
                vm.uiConfig = {
                    calendar: {
                        lang: 'zh-cn',
                        allDaySlot: false,
                        height: 800,
                        slotLabelFormat: 'HH:mm',
                        minTime: '06:00:00',
                        maxTime: '24:00:00',
                        timeFormat: 'HH:mm',
                        editable: false,
                        selectable: true,
                        selected: true,
                        multipleSelected: false,
                        defaultView: 'agendaWeek',//'agendaWeek',
                        header: {
                            left: 'prev,next today',
                            center: 'title',
                            right: ''
                        },
                        eventRender: function (event, element, view) {
                            element.attr({
                                'uib-popover': event.startText + '-' + event.endText + '\r\n' + event.tooltiptxt,
                                'tooltip-append-to-body': true,
                                'popover-trigger': 'mouseenter'
                            });
                            element.context.innerText = event.title;
                            $compile(element)($scope);
                        },
                        eventClick: function (event, jsEvent, view) {
                            var existEvent = false;
                            for (var i in vm.selectedEvents) {
                                if (vm.selectedEvents[i].id == event.id) {
                                    existEvent = true;
                                    $(vm.selectedEvents[i].element).removeClass('mcs-calendar-event-selected');
                                    $(vm.selectedEvents[i].element).find('div.fc-title i.ace-icon').remove();
                                    vm.selectedEvents.splice(i, 1);
                                    break;
                                }
                            }
                            if (!existEvent) {
                                $(this).addClass('mcs-calendar-event-selected');
                                $(this).find('div.fc-title').prepend($('<i class="ace-icon fa fa-check"></i>'));
                                event.element = this;
                                vm.selectedEvents.push(event);
                            }
                        },
                        viewRender: function (view, element) {
                            vm.selectedEvents = [];
                        }
                    }
                };

                var firstExec = true;
                vm.result = {};
                /*日程组件回调方法，加载周课表数据*/
                vm.schedules = function (start, end, timezone, callback) {
                    vm.acQM.startTime = start;
                    vm.acQM.endTime = end;
                    studentassignmentDataService.getStudentWeekCourse(vm.acQM, function (data) {
                        vm.result = data;
                        var assignCollection = data.result;
                        angular.forEach(assignCollection, function (event, index, array) {
                            var evt = vm.getViewModel(event);
                            vm.events.push(evt);
                        });
                        if (firstExec) {
                            firstExec = false;
                            $scope.$broadcast('dictionaryReady');
                        };
                    },
                    function (error) {
                    });
                };
                vm.eventSources = [vm.events, vm.schedules];

                /*刷新周视图数据*/
                vm.search = function () {
                    vm.events.splice(0, vm.events.length);
                    studentassignmentDataService.getStudentWeekCourse(vm.acQM, function (data) {
                        var assignCollection = data.result;
                        angular.forEach(assignCollection, function (event, index, array) {
                            var evt = vm.getViewModel(event);
                            vm.events.push(evt);
                        });
                        uiCalendarConfig.calendars.courseCalendar.fullCalendar('rerenderEvents');
                    },
                    function (error) {
                    });
                };

                /*将排课对象转为视图对象*/
                vm.getViewModel = function (event) {
                    var evt = {
                        id: event.assignID,
                        title: event.teacherName + " " + event.subjectName + " " + event.gradeName,
                        tooltiptxt: event.teacherName + " " + event.subjectName + " " + event.gradeName,
                        start: event.startTime.getFullYear() + '-' + vm.getDoubleStr((event.startTime.getMonth() + 1)) + '-' + vm.getDoubleStr(event.startTime.getDate()) + ' ' + vm.getDoubleStr(event.startTime.getHours()) + ':' + vm.getDoubleStr(event.startTime.getMinutes()),
                        startText: vm.getDoubleStr(event.startTime.getHours()) + ':' + vm.getDoubleStr(event.startTime.getMinutes()),
                        end: event.endTime.getFullYear() + '-' + vm.getDoubleStr((event.endTime.getMonth() + 1)) + '-' + vm.getDoubleStr(event.endTime.getDate()) + ' ' + vm.getDoubleStr(event.endTime.getHours()) + ':' + vm.getDoubleStr(event.endTime.getMinutes()),
                        endText: vm.getDoubleStr(event.endTime.getHours()) + ':' + vm.getDoubleStr(event.endTime.getMinutes()),
                        color: '#000000',
                        status: event.assignStatus,
                        coursessource: event.categoryType,
                        customerName: event.customerName,
                        customerID: event.customerID,
                        subjectName: event.subjectName,
                        curDate: event.startTime.getFullYear() + '-' + vm.getDoubleStr((event.startTime.getMonth() + 1)) + '-' + vm.getDoubleStr(event.startTime.getDate()),
                        curWeek: vm.weekText[event.startTime.getDay()],
                        assetID: event.assetID,
                        customerCode: event.customerCode,
                        sTime: event.startTime,
                        eTime: event.endTime
                    };
                    evt.className = "mcs-calendar-event";
                    switch (event.assignStatus) {
                        case 1:
                            evt.color = '#0000FF'; evt.title += '排定'; evt.tooltiptxt += '排定'; /*排定*/
                            break;
                        case 8:
                            evt.color = '#FF0000'; evt.title += '异常'; evt.tooltiptxt += '异常'; /*异常*/
                            break;
                        case 3:
                            evt.color = '#C0C0C0'; evt.title += '已上'; evt.tooltiptxt += '已上'; /*已上*/
                            break;
                    };
                    switch (event.categoryType) {
                        case 1: evt.tooltiptxt += "一对一"; break;
                        case 2: evt.tooltiptxt += "班组"; break;
                    }
                    return evt;
                };

              

                /*取消课表*/
                vm.cancelCourseClick = function () {
                    if (utilService.showMessage(vm, !vm.selectedEvents.length, '请选择需要取消的课表！')) {
                        return;
                    }
                    var msg = '';
                    var model = [];
                    for (var i in vm.selectedEvents) {
                        ///只能取消异常，排定的课表 并且是一对一
                        if ((vm.selectedEvents[i].status == 1 || vm.selectedEvents[i].status == 8) && vm.selectedEvents[i].coursessource == 1) {
                            msg += "确定将学员" + vm.selectedEvents[i].customerName + " " + vm.selectedEvents[i].curDate + "（" + vm.selectedEvents[i].curWeek + "）"
                                + vm.selectedEvents[i].startText + "-" + vm.selectedEvents[i].endText + "的" + vm.selectedEvents[i].subjectName + "课取消？<br>";
                            model.push({
                                assetID: vm.selectedEvents[i].assetID,
                                assignID: vm.selectedEvents[i].id
                            });
                        };
                    };

                    if (utilService.showMessage(vm, model.length == 0, '选择的记录不允许取消，请重新选择！')) {
                        return;
                    }

                    mcsDialogService.confirm({
                        title: '取消课表',
                        message: msg
                    }).result.then(function () {
                        studentassignmentDataService.cancelAssign(model, function (result) {
                            for (var i in model) {
                                for (var j in vm.events) {
                                    if (vm.events[j].id == model[i].assignID) {
                                        vm.events.splice(j, 1);
                                        break;
                                    };
                                };
                            };
                            uiCalendarConfig.calendars.courseCalendar.fullCalendar('rerenderEvents');
                            vm.selectedEvents.splice(0, vm.selectedEvents.length);
                            vm.showMsg("取消成功");
                            vm.getCustomerStatCurMonth();
                        },
                        function (error) {

                        });
                    });
                };

                /*新增课表*/
                vm.createSchedule = function () {
                    mcsDialogService.create('app/schedule/studentassignment/stuasgmt-add/stuasgmt-add.html', {
                        controller: 'stuAsgmtAddController',
                        settings: {
                            size: 'lg',
                            backdrop: 'static'
                        }
                    }).result.then(function (retValue) {
                        if (retValue != 'canceled') {
                            retValue.assignStatus = 1;
                            var evt = vm.getViewModel(retValue);
                            vm.events.push(evt);
                            uiCalendarConfig.calendars.courseCalendar.fullCalendar('rerenderEvents');
                            vm.getCustomerStatCurMonth();
                        };
                    });
                };

                /*复制课表*/
                vm.copyCourseClick = function () {
                    mcsDialogService.create('app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-copy.html', {
                        controller: 'stuAsgmtCourseCopyController',
                        settings: {
                            backdrop: 'static'
                        }
                    }).result.then(function (retValue) {
                        if (retValue == 'ok') {
                            vm.search();
                            vm.getCustomerStatCurMonth();
                        };
                    });
                };

                /*调整课表*/
                vm.resetCourseClick = function () {
                    if (utilService.showMessage(vm, !vm.selectedEvents.length, '请选择需要调整的课表！')) {
                        return;
                    }
                    /*将聂鑫航（ST00001）2016-05-05（星期二）13:00 - 15:00 的数学课调整为*/
                    var model = [], msg = '', tempdate2 = new Date();
                    var tempdate = new Date(tempdate2.getFullYear(), tempdate2.getMonth(), tempdate2.getDate(), 0, 0, 0, 0);
                    for (var i in vm.selectedEvents) {
                        if ((vm.selectedEvents[i].status == 1 || vm.selectedEvents[i].status == 8) && vm.selectedEvents[i].coursessource == 1) {
                            msg = "将" + vm.selectedEvents[i].customerName + "（" + vm.selectedEvents[i].customerCode + "）"
                                + vm.selectedEvents[i].curDate + "（" + vm.selectedEvents[i].curWeek + "）" + vm.selectedEvents[i].startText
                                + " - " + vm.selectedEvents[i].endText + "的" + vm.selectedEvents[i].subjectName + "课调整为";
                            var tempM = {
                                assignID: vm.selectedEvents[i].id,
                                info: msg,
                                reDate: '',
                                reHour: '',
                                reMinute: '',
                                startTime: vm.selectedEvents[i].sTime,
                                endTime: vm.selectedEvents[i].eTime,
                                allowResetDateTime: new Date(),
                                customerID: vm.selectedEvents[i].customerID
                            };
                            var sdate = vm.selectedEvents[i].startText.split(':');
                            if (sdate.length == 2) {
                                tempM.reHour = sdate[0];
                                tempM.reMinute = sdate[1];
                            }
                            model.push(tempM);
                        };
                    };

                    if (utilService.showMessage(vm, model.length == 0, '选择的记录不允许调课，请重新选择！')) {
                        return;
                    }

                    mcsDialogService.create('app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-reset.html', {
                        controller: 'stuAsgmtCourseResetController',
                        params: { para: model },
                        settings: {
                            size: 'lg',
                            backdrop: 'static'
                        }
                    }).result.then(function (retValue) {
                        if (retValue == 'ok') {
                            vm.search();
                            vm.getCustomerStatCurMonth();
                        };
                    });
                };

                vm.getDoubleStr = function (curValue) {
                    if (parseInt(curValue) < 10)
                        return '0' + curValue;
                    return curValue;
                };

                vm.cSCM = {};
                vm.getCustomerStatCurMonth = function () {
                    studentassignmentDataService.getCurMonthStat(vm.acQM, function (data) {
                        vm.cSCM = data;
                    },
                   function (error) {
                   });
                };
                vm.getCustomerStatCurMonth();

                /*跳转列表视图*/
                vm.gotoCourseList = function () {
                    $state.go('ppts.stuasgmt-course-list', { id: vm.CID, prev: vm.page });
                };

                /*跳转学生排课列表*/
                vm.gotoAssignList = function () {
                    $state.go('ppts.schedule');
                };

                vm.showMsg = function (msg) {
                    mcsDialogService.error({ title: '提示信息', message: msg });
                };

                vm.print = function () {
                    printService.print(true);
                }


            }]);
        });