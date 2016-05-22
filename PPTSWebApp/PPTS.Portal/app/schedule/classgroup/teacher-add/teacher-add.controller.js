define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.productDataService,
        ppts.config.dataServiceConfig.classgroupDataService],
    function (schedule) {
        schedule.registerController('teacherAddController', [
             '$q',
            'data',
            '$scope',
            '$uibModalInstance',
            'dataSyncService',
            'classgroupDataService',
            function ($q,data, $scope, $uibModalInstance, dataSyncService, classgroupDataService) {
                var vm = this;

                //绑定页面tab数据
                vm.data = {
                    selection: 'radio',
                    rowsSelected: data.teacher,
                    keyFields: ['teacherID', 'teacherCode', 'teacherName'],
                    pager: {
                        pageIndex: 1,
                        pageSize: 10,
                        totalCount: -1,
                        pageChange: function () {
                            dataSyncService.initCriteria(vm);
                            var deferred = $q.defer();
                            classgroupDataService.getPageTeacherJobs(vm.criteria, function (result) {
                                vm.data.rows = result.pagedData;
                                deferred.resolve();
                            });
                            return deferred.promise;
                        }
                    },
                    orderBy: [{ dataField: 'teacherID', sortDirection: 1 }],
                    headers: [{
                        field: "teacherName",
                        name: "教师姓名",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: 'customer name'
                    },
                        {
                            field: "teacherCode",
                            name: "教师编号",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "campusName",
                            name: "校区",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "jobOrgName",
                            name: "所属学科组",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "gradeMemo",
                            name: "教师授课年级",
                            template: '<span uib-tooltip="{{row.gradeMemo | grade_full}}">{{row.gradeMemo | grade}}</span>',
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "subjectMemo",
                            name: "教师授课科目",
                            template: '<span uib-tooltip="{{row.subjectMemo | subject_full}}">{{row.subjectMemo | subject}}</span>',
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }

                    ]
                }

                dataSyncService.initCriteria(vm,false);

                

                //关闭窗口
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                    data.form.teacher.$setDirty();
                };

                //保存选定教师
                vm.save = function () {
                    $uibModalInstance.close(vm.data.rowsSelected);
                }

                //查询
                vm.search = function () {
                    var deferred = $q.defer();

                    //获取教师列表信息
                    classgroupDataService.getAllTeacherJobs(
                        vm.criteria,
                        function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                            deferred.resolve();
                        }, function () {

                        }
                    );

                    return deferred.promise;
                }
            }]);
    });