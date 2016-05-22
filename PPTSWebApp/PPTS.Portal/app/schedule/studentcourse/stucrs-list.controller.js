define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentCourseDataService],
        function (schedule) {
            schedule.registerController('stucrsListController', [
                '$scope', '$state', 'dataSyncService', 'studentCourseDataService', '$stateParams', 'blockUI', 'mcsDialogService',
                function ($scope, $state, dataSyncService, studentCourseDataService, $stateParams, blockUI, mcsDialogService) {
                    var vm = this;
                    vm.weekText = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");

                   
                    vm.assignSource = '';
                    vm.assignStatus = '';

                    vm.criteria = vm.criteria || {};

                    vm.criteria.customerID = '446';// $stateParams.id;
                    vm.criteria.campusID = '';
                    vm.criteria.subject = '';
                    vm.criteria.grade = '';
                    vm.criteria.assignStatus = '';
                    vm.criteria.assignSource = '';              
                    vm.criteria.teacherName = '';
                    vm.criteria.educatorName = '';
                    vm.criteria.consultantName = '';
                    vm.criteria.orderNo = '';

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
                            template: '<span>{{  vm.getCourseHour(row.startTime,row.endTime) }}</span>'
                        }, {
                            field: "amount",
                            name: "课时",
                            template: '<span>{{row.amount}}</span>'
                        }, {
                            field: "realAmount",
                            name: "实际小时",
                            template: '<span>{{row.realAmount}}</span>'
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
                            field: "orderID",
                            name: "订单编号",
                            template: '<span></span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 10,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                studentCourseDataService.getStuCoursePaged(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'startTime', sortDirection: 1 }]
                    }

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

                        studentCourseDataService.getStuCourse(vm.criteria, function (result) {
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
                        mcsDialogService.create('app/schedule/studentcourse/stucrs-list-markup.html', {
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



                    vm.queryList = function () {
                        vm.init();
                    };




                }]);
        });