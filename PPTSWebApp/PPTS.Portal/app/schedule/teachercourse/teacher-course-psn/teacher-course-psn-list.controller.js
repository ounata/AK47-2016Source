define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherCourseDataService],
        function (schedule) {
            schedule.registerController('tchCoursePsnListController', [
                '$scope', 'printService', 'teacherCourseDataService', 'dataSyncService', '$state',
                function ($scope, printService, teacherCourseDataService, dataSyncService, $state) {
                    var vm = this;
                    vm.weekText = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
         
                    vm.criteria = vm.criteria || {};
                    vm.criteria.teacherID = vm.CID;
                 
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
                                teacherCourseDataService.getPagedSCLV(vm.criteria, function (result) {
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

                        teacherCourseDataService.getSCLV(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();

                   
                    vm.getDoubleStr = function (curValue) {
                        if (parseInt(curValue) < 10)
                            return '0' + curValue;
                        return curValue;
                    };

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                    vm.gotoCourseWeek = function () {
                        $state.go('ppts.tchcoursepsn');
                    };

                    vm.getCourseDate = function (sDate) {
                        var startDate = new Date(sDate);
                        return startDate.getFullYear() + '-' + vm.getDoubleStr((startDate.getMonth() + 1)) + '-' + vm.getDoubleStr(startDate.getDate())
                         + '(' + vm.weekText[startDate.getDay()] + ')';
                    };


                }]);
        });