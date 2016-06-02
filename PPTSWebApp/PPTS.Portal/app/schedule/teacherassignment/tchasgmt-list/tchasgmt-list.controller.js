/*教师列表*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherAssignmentDataService],
        function (schedule) {
            schedule.registerController('tchAsgmtListController', [
                '$scope', 'dataSyncService', 'teacherAssignmentDataService', 'mcsDialogService', '$state', 'blockUI',
                function ($scope, dataSyncService, teacherAssignmentDataService, mcsDialogService, $state, blockUI) {
                    var vm = this;
                    vm.criteria = vm.criteria || {};
                    vm.criteria.gradeMemo = '';
                    vm.criteria.subjectMemo = '';
                    vm.criteria.isFullTime = '';
                    vm.criteria.gender = '';
                    vm.criteria.teacherName = '';
                   
                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['teacherID'],
                        headers: [{
                            field: "teacherName",
                            name: "教师姓名",
                            template: '<span>{{row.teacherName}}</span>'
                        }, {
                            field: "teacherCode",
                            name: "员工编号",
                            template: '<span>{{row.teacherCode}}</span>'
                        }, {
                            field: "jobOrgName",
                            name: "学科组",
                            template: '<span>{{row.jobOrgName }}</span>'
                        }, {
                            field: "isFullTime",
                            name: "岗位性质",
                            template: '<span>{{row.isFullTime | teacherType }}</span>'
                        }, {
                            field: "gender",
                            name: "性别",
                            template: '<span>{{row.gender | gender}}</span>'
                        }, {
                            field: "gradeMemo",
                            name: "授课年级",
                            template: '<span uib-tooltip="{{row.gradeMemo | grade_full}}">{{row.gradeMemo | grade}}</span>',
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }, {
                            field: "subjectMemo",
                            name: "授课科目",
                            template: '<span uib-tooltip="{{row.subjectMemo | subject_full}}">{{row.subjectMemo | subject}}</span>',
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 20,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                teacherAssignmentDataService.getTeacherListPaged(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'teacherCode', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        blockUI.start();
                        dataSyncService.initCriteria(vm);
                        teacherAssignmentDataService.getTeacherList(vm.criteria, function (result) {
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

                    vm.tchAssignClick = function () {
                        if (vm.data.rowsSelected[0] == undefined) {
                            vm.showMsg("请选择一个教师");
                            return;
                        }
                        var teacherName = '',teacherJobID = '';
                        for(var i in vm.data.rows){
                            if (vm.data.rows[i].teacherID == vm.data.rowsSelected[0].teacherID) {
                                teacherName = vm.data.rows[i].teacherName;
                                teacherJobID = vm.data.rows[i].jobID;
                                break;
                            };

                        };
                        $state.go('ppts.tchasgmt-course', { cID: vm.data.rowsSelected[0].teacherID, tn: teacherName, tji: teacherJobID });
                    };

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                }]);
        });