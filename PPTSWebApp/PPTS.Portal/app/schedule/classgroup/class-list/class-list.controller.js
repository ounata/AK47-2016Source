define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.classgroupDataService
    ],
    function (schedule) {
        schedule.registerController('classListController', [
            '$scope',
            '$q',
            'classgroupDataService',
            'dataSyncService',
            function ($scope, $q, classgroupDataService, dataSyncService) {
                var vm = this;

                //高级查询
                vm.searchItems = [
                    { name: "上课科目", template: '<ppts-checkbox-group category="subject" model="vm.criteria.selectedSubjects" clear="vm.criteria.selectedSubjects=[]" />' },
                    { name: "上课年级", template: ' <ppts-checkbox-group category="grade" model="vm.criteria.selectedGrades" clear="vm.criteria.selectedGrades=[]" />' },
                    { name: "班级上课日期", template: "" },
                    { name: "班级状态", template: "" }
                ];

                //列表
                vm.data = {
                    selection: 'radio',
                    rowsSelected: [],
                    keyFields: ['classID'],
                    pager: {
                        pageIndex: 1,
                        pageSize: 10,
                        totalCount: -1,
                        pageChange: function () {
                            var deferred = $q.defer();
                            classgroupDataService.getPageClasses(vm.criteria, function (result) {
                                vm.data.rows = result.pagedData;
                                deferred.resolve();
                            });
                            return deferred.promise;
                        }
                    },
                    orderBy: [{ dataField: 'ClassID', sortDirection: 1 }],
                    headers: [ {
                                    field: "campusName",
                                    name: "校区",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "className",
                                    name: "班级名称",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "createTime",
                                    name: "创建时间",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "productCode",
                                    name: "产品编号",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "productName",
                                    name: "产品名称",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "classStatus",
                                    name: "班级状态",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "lessonCount",
                                    name: "总课次数",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "finishedLessons",
                                    name: "已上课次数",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "classPeoples",
                                    name: "上课人数",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "startTime",
                                    name: "班级开班时间",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "endTime",
                                    name: "班级结束时间",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }

                    ]
                }

                //查询
                vm.search = function () {
                    var deferred = $q.defer();

                    dataSyncService.initCriteria(vm);

                    classgroupDataService.getAllClasses(vm.criteria,
                        function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                            deferred.resolve();
                        },
                        function () {
                        })

                    return deferred.promise;
                };
            }]);
    });