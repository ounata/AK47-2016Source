define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherCourseDataService],
        function (schedule) {
            schedule.registerController('tchCoursePsnController', [
                '$scope', 'printService', 'teacherCourseDataService', '$state',
                function ($scope, printService, teacherCourseDataService, $state) {
                    var vm = this;

                    //vm.CID = $stateParams.cID;
                    //vm.tchJobID = $stateParams.tji;
                    //vm.tchName = $stateParams.tn;
                    vm.selectedEvents = [];
                    vm.events = [];

                    vm.weekText = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
                    vm.acQM = {
                        teacherID: vm.CID, startTime: '2016-04-18', endTime: '2016-05-09',
                        isUTCTime: false, grade: '', assignStatus: '', customerName: '', teacherJobID: vm.tchJobID
                    };

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
                            defaultView: 'agendaWeek',
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
                    vm.schedules = function (start, end, timezone, callback) {
                        vm.acQM.startTime = start;
                        vm.acQM.endTime = end;
                        teacherCourseDataService.getPsnTeacherWeekCourse(vm.acQM, function (data) {
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

                    vm.search = function () {
                        vm.events.splice(0, vm.events.length);
                        teacherCourseDataService.getPsnTeacherWeekCourse(vm.acQM, function (data) {
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

                    vm.getViewModel = function (event) {
                        var evt = {
                            id: event.assignID,
                            title: event.customerName + " " + event.subjectName + " " + event.gradeName,
                            tooltiptxt: event.customerName + " " + event.subjectName + " " + event.gradeName,
                            start: event.startTime.getFullYear() + '-' + vm.getDoubleStr((event.startTime.getMonth() + 1)) + '-' + vm.getDoubleStr(event.startTime.getDate()) + ' ' + vm.getDoubleStr(event.startTime.getHours()) + ':' + vm.getDoubleStr(event.startTime.getMinutes()),
                            startText: vm.getDoubleStr(event.startTime.getHours()) + ':' + vm.getDoubleStr(event.startTime.getMinutes()),
                            end: event.endTime.getFullYear() + '-' + vm.getDoubleStr((event.endTime.getMonth() + 1)) + '-' + vm.getDoubleStr(event.endTime.getDate()) + ' ' + vm.getDoubleStr(event.endTime.getHours()) + ':' + vm.getDoubleStr(event.endTime.getMinutes()),
                            endText: vm.getDoubleStr(event.endTime.getHours()) + ':' + vm.getDoubleStr(event.endTime.getMinutes()),
                            color: '#000000',
                            status: event.assignStatus,
                            coursessource: event.categoryType,
                            customerName: event.customerName,
                            subjectName: event.subjectName,
                            curDate: event.startTime.getFullYear() + '-' + vm.getDoubleStr((event.startTime.getMonth() + 1)) + '-' + vm.getDoubleStr(event.startTime.getDate()),
                            curWeek: vm.weekText[event.startTime.getDay()],
                            assetID: event.assetID,
                            customerCode: event.customerCode,
                            customerID: event.customerID,
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

                    vm.getDoubleStr = function (curValue) {
                        if (parseInt(curValue) < 10)
                            return '0' + curValue;
                        return curValue;
                    };
                    vm.cSCM = {};
                    vm.getPsnCurMonthStat = function () {
                        teacherCourseDataService.getPsnCurMonthStat(vm.acQM, function (data) {
                            vm.cSCM = data;
                        },
                       function (error) {
                       });
                    };
                    vm.getPsnCurMonthStat();

                    /*跳转列表视图*/
                    vm.gotoCourseList = function () {
                        $state.go('ppts.tchcoursepsn-list');
                    };


                    vm.print = function () {
                        printService.print(true);
                    }

                }]);
        });