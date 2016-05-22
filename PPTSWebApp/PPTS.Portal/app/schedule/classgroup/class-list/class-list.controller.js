define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.classgroupDataService
    ],
    function (schedule) {
        schedule.registerController('classListController', [
            '$scope',
            '$state',
            '$q',
            '$stateParams',
            'classgroupDataService',
            'dataSyncService',
            'mcsDialogService',
            function ($scope, $state, $q, $stateParams, classgroupDataService, dataSyncService, mcsDialogService) {
                var vm = this;               

                //高级查询
                vm.searchItems = [
                    { name: "上课科目", template: '<ppts-checkbox-group category="subject" model="vm.criteria.subjects" clear="vm.criteria.subjects=[]" />' },
                    { name: "上课年级", template: ' <ppts-checkbox-group category="grade" model="vm.criteria.grades" clear="vm.criteria.grades=[]" />' },
                    { name: '班级上课日期：', template: '<ppts-daterangepicker start-date="vm.criteria.startTime" end-date="vm.criteria.endTime"/>' },
                    { name: "班级状态", template: '<ppts-checkbox-group category="classStatus" model="vm.criteria.classStatuses" clear="vm.criteria.classStatuses=[]" />' }
                ];

                //列表
                vm.data = {
                    selection: 'radio',
                    rowsSelected: [],
                    keyFields: ['classID','classStatus'],
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
                    orderBy: [{ dataField: 'classID', sortDirection: 1 }],
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
                                    template: '<a ui-sref="ppts.classgroup-detail({classID:row.classID})">{{row.className}}</a>',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "createTime | date:'yyyy-MM-dd'",
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
                                    field: "classStatus | classStatus",
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
                                    field: "startTime | date:'yyyy-MM-dd'",
                                    name: "班级开班时间",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }, {
                                    field: "endTime | date:'yyyy-MM-dd'",
                                    name: "班级结束时间",
                                    headerCss: 'datatable-header-align-right',
                                    sortable: false,
                                    description: ''
                                }

                    ]
                }

                dataSyncService.initCriteria(vm);
                vm.criteria.productCode = $stateParams.productCode;

                //查询
                vm.search = function () {
                    var deferred = $q.defer();                    

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

                //删除班级
                vm.deleteClass = function () {
                    if (vm.data.rowsSelected.length == 1 && vm.data.rowsSelected[0].classStatus == 0) {
                        classgroupDataService.deleteClass(
                            vm.data.rowsSelected[0].classID, function () {
                                //刷新页面
                                vm.search();
                            }, function () {

                            })
                    }
                    else {
                        mcsDialogService.error(
                              { title: 'Error', message: "请选择一个状态为新建的班级！" }
                          )
                    }
                }

                //查看学生
                vm.searchCustomers = function () {
                    if (vm.data.rowsSelected.length == 1 ) {
                        mcsDialogService.create('app/schedule/classgroup/customer-list/customer-list.html', {
                            controller: 'customerListController',
                            params: { classID: vm.data.rowsSelected[0].classID },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function (data) {

                        }, function () {

                        });
                    }
                }
            }]);
    });