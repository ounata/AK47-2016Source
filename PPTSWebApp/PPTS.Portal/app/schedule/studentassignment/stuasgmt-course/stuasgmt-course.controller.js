define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService], function (schedule) {
            schedule.registerController("stuAsgmtCourseController", [
                 '$scope', '$state', '$stateParams', '$filter', 'dataSyncService', '$compile'
                 , '$uibModal', 'blockUI', '$http', 'studentassignmentDataService', 'mcsDialogService',
            function ($scope, $state, $stateParams, $filter, dataSyncService, $compile
                , $uibModal, blockUI, $http, studentassignmentDataService, mcsDialogService) {
                var vm = this;

                vm.CID = $stateParams.cID;
                vm.selectedEvents = []; 
                //设置查询模式, '', 'simple', 'advance'三选一
                vm.SearchMode = 'simple';
                vm.criteria = { Start: '2016-03-01', End: '2016-04-30' };
                vm.Teachers = [{ text: 'abc' }];
                //配置日程组件
                vm.uiConfig = {
                    calendar: {
                        lang: 'zh-cn',
                        allDaySlot: false,
                        height: 730,//1100,
                        slotLabelFormat: 'HH:mm',
                        minTime: '06:00:00',
                        maxTime: '22:00:00',
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
                                'uib-tooltip': event.startText + '-' + event.endText + '\r\n' + event.title,
                                'tooltip-append-to-body': true
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

                vm.events = [];
                vm.initEvent = function (event) {
                    event.className = "mcs-calendar-event";
                    if (event.duration < 0) event.color = '#a0a0a0';
                    else if (event.duration < 1) event.color = '#d6487e';
                    else event.color = '#82af6f';
                };
                vm.courseCollection = '';
                vm.schedules = function (start, end, timezone, callback) {
                    blockUI.start();
                    //简单查询
                    studentassignmentDataService.editStudentCourse(vm.criteria, function (data) {
                        if (data.code == 0) {
                            //var events = data.data.list;
                            vm.courseCollection = data.data.list;
                            angular.forEach(vm.courseCollection, function (event, index, array) {
                                vm.initEvent(event);
                            });
                            callback(vm.courseCollection);
                        } else {
                            bootbox.alert({ title: "错误", message: data.Message });
                        }
                        blockUI.stop();
                    });
                };
                vm.eventSources = [vm.events, vm.schedules];

                //简单查询按钮的事件
                vm.simpleSearch = function () {
                    alert(vm.selectedEvents.length);
                    alert(vm.courseCollection.length);
                };

                vm.cancelCourseClick = function () {
                    for (var i in vm.selectedEvents) {
                        for (var j in vm.courseCollection) {
                            var id1 = vm.selectedEvents[i].id;
                            var id2 = vm.courseCollection[j].id;
                            if (vm.selectedEvents[i].id == vm.courseCollection[j].id) {                               
                                vm.courseCollection.splice(j, 1);
     
                                break;
                            };
                        };
                    };


                };

                //新增课表
                vm.createSchedule = function () {
                    mcsDialogService.create('app/schedule/studentassignment/stuasgmt-add/stuasgmt-add.html', {
                        controller: 'stuAsgmtAddController',
                        settings: {
                            size:'lg'
                        }
                       
                    }).result.then(function (retValue) {
                        if (retValue == 'ok') {
                            vm.events.push({
                                id: 'test',
                                title: '高三生物 张庆民',
                                start: '2016-04-18 20:00',
                                startText: '20:00',
                                end: '2016-04-18 21:00',
                                endText: '21:00',
                               color: '#a0a0a0'
                               // textColor: '',
                               // allDay: false,
                               // status: '已上',
                               // duration: '-51.26',
                               
                            });
                        };
                    });
                };

                vm.copyCourseClick = function () {

                    mcsDialogService.create('app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-copy.html', {
                        controller: 'stuAsgmtCourseCopyController',
                    }).result.then(function (retValue) {
                        if (retValue == 'ok') {
                          
                        };
                    });
                };



                //排课按钮的事件
                //vm.addSchedule = function () {
                //    $uibModal.open({
                //        templateUrl: '../stuasgmt-add/stuasgmt-add.html',
                //        controller: 'stuAsgmtAddController',
                //        size: 'md',
                //        resolve: {
                //            item: function () {
                //                return 'mytest';
                //            }
                //        },
                //        backdrop: 'static'
                //    }).result.then(function (schedule) {
                //        vm.events.push({
                //            id: 'test',
                //            title: '高三生物 张庆民',
                //            start: '2016-03-18 20:00',
                //            end: '2016-03-18 21:00',
                //            stick: true
                //        });
                //    });
                //};

                vm.queryTeacherList = function (query) {
                    return courseService.queryTeacherList(query);
                };

            }]);


        });