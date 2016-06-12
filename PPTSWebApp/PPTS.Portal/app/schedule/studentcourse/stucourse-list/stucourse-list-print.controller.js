/*学员课表*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentCourseDataService],
        function (schedule) {
            schedule.registerController('stuCourseListPrintController', [
                '$scope', 'studentCourseDataService', 'dataSyncService', 'mcsDialogService', 'studentassignmentDataService', 'printService', 'data',
                function ($scope, studentCourseDataService, dataSyncService, mcsDialogService, studentassignmentDataService, printService, data) {
                    var vm = this;

                    vm.weekText = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");

                    vm.assignSource = '';
                    vm.assignStatus = '';

                    vm.criteria = {};
                    vm.criteria.campusID = '';
                    vm.criteria.subject = '';
                    vm.criteria.grade = '';
                    vm.criteria.assignStatus = '';
                    vm.criteria.assignSource = '';
                    vm.criteria.customerName = '';
                    vm.criteria.customerCode = '';
                    vm.criteria.teacherName = '';
                    vm.criteria.educatorName = '';
                    vm.criteria.consultantName = '';
                    vm.criteria.assetCode = '';
                    vm.criteria.isFullTimeTeacher = [];
                    vm.criteria.startTime = '';
                    vm.criteria.endTime = '';

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
                            field: "customerName",
                            name: "学员编号",
                            template: '<span>{{row.customerCode}}</span>'
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
                            field: "assignSource",
                            name: "课时类型",
                            template: '<span>{{row.assignSource | assignSource }}</span>'
                        }, {
                            field: "assetCode",
                            name: "订单编号",
                            template: '<span>{{ row.assetCode }}</span>'
                        }],
                        pager: {
                            pagable: false,
                        },
                        orderBy: [{ dataField: 'startTime', sortDirection: 1 }]
                    }

                    /*页面初始化加载或重新搜索时查询*/
                    vm.init = function () {
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
                        });
                    };
                    vm.init();


                    vm.print = function () {
                        printService.print();
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

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                }]);
        });