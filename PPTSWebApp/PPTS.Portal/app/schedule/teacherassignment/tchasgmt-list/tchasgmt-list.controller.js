/*教师列表*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherAssignmentDataService],
        function (schedule) {
            schedule.registerController('tchAsgmtListController', [
                '$scope', 'dataSyncService', 'teacherAssignmentDataService', 'mcsDialogService', '$state',
                function ($scope, dataSyncService, teacherAssignmentDataService, mcsDialogService, $state) {
                    var vm = this;
                    vm.criteria = vm.criteria || {};
                    vm.criteria.gradeMemo = '';
                    vm.criteria.subjectMemo = '';
                    vm.criteria.isFullTime = '';
                    vm.criteria.gender = '';
                    vm.criteria.teacherName = '';

                    vm.age = '';

                    vm.ageCollection = new Array();
                    vm.ageCollection.push({ key: '30-', value: '30-' })
                    vm.ageCollection.push({ key: '30+', value: '30+' })
                    vm.ageCollection.push({ key: '50-', value: '50-' })
                    vm.ageCollection.push({ key: '50+', value: '50+' })


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
                    vm.search = function () {

                        dataSyncService.initCriteria(vm);
                        teacherAssignmentDataService.getTeacherList(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;

                            dataSyncService.updateTotalCount(vm, result.queryResult);

                            dataSyncService.injectDictData();
                            dataSyncService.injectDictData(mcs.util.mapping(vm.ageCollection, { key: 'key', value: 'value' }, 'AgeCollection'));
                         
                            $scope.$broadcast('dictionaryReady');
                        }, function (error) {
                        });
                    };
                    vm.search();

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


                    vm.calcAge = function (item) {
                        var curDate = new Date();
                        switch (item.key)
                        {
                            case '30-':
                                vm.criteria.moreBirthday = new Date(curDate.getFullYear() - 30, curDate.getMonth(), curDate.getDate(), 0, 0, 0);
                                vm.criteria.lessBirthday = '';
                                break;
                            case '30+':
                                vm.criteria.moreBirthday = ''
                                vm.criteria.lessBirthday = new Date(curDate.getFullYear() - 30, curDate.getMonth(), curDate.getDate(), 0, 0, 0);
                                break;
                            case '50-':
                                vm.criteria.moreBirthday = new Date(curDate.getFullYear() - 50, curDate.getMonth(), curDate.getDate(), 0, 0, 0);
                                vm.criteria.lessBirthday = '';
                                break;
                            case '50+':
                                vm.criteria.moreBirthday = ''
                                vm.criteria.lessBirthday = new Date(curDate.getFullYear() - 50, curDate.getMonth(), curDate.getDate(), 0, 0, 0);
                                break;
                            default:
                                vm.criteria.moreBirthday = '';
                                vm.criteria.lessBirthday = '';
                                break;
                        }
                    };


                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                }]);
        });