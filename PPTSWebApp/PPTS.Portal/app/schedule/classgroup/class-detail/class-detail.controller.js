define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.classgroupDataService
],
    function (schedule) {
        schedule.registerController('classDetailController', [
            '$scope',
            '$state',
            '$stateParams',
            '$q',
            'classgroupDataService',
            'dataSyncService',
            'mcsDialogService',
            function ($scope, $state, $stateParams, $q, classgroupDataService, dataSyncService, mcsDialogService) {
                var vm = this;

                //列表
                vm.data = {
                    selection: 'radio',
                    rowsSelected: [],
                    keyFields: ['lessonID', 'index', 'classID', 'lessonCount', 'lessonStatus', 'startTime'],
                    pager: {
                        pageIndex: 1,
                        pageSize: ppts.config.pageSizeItem,
                        totalCount: -1,
                        pageChange: function () {
                            var deferred = $q.defer();
                            classgroupDataService.getPageClassLessons(vm.criteria, function (result) {
                                vm.data.rows = result.pagedData;
                                deferred.resolve();
                            });
                            return deferred.promise;
                        }
                    },
                    orderBy: [{ dataField: 'lessonID', sortDirection: 0 }],
                    headers: [{
                        field: "index",                        
                        name: "课次",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ''
                    }, {
                        field: "startTime | date:'yyyy-MM-dd' ",
                        name: "上课日期",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ''
                    }, {
                        field: "startTime | date:'EEEE' ",
                        name: "星期",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ''
                    }, {
                        field: "startTime | date:'hh:mm'",
                        name: "上课开始时间",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ''
                    }, {
                        field: "endTime | date:'hh:mm'",
                        name: "上课结束时间",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ''
                    }, {
                        field: "teacherName",
                        name: "上课教师",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ''
                    }, {
                        field: "lessonStatus | lessonStatus",
                        name: "课表状态",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ''
                    }, {
                        field: "lessonPeoples",
                        name: "上课人数",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ''
                    }, {
                        field: "confirmedMoney ",
                        name: "课时费",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ''
                    }

                    ]
                }

                //查询
                vm.search = function () {
                    dataSyncService.initCriteria(vm);
                    vm.criteria.classID = $stateParams.classID;
                    classgroupDataService.getClassDetail(vm.criteria,
                        function (result) {
                            for (var index in result.classLessones.queryResult.pagedData) {
                                result.classLessones.queryResult.pagedData[index].index = index / 1 + 1;
                                result.classLessones.queryResult.pagedData[index].classID = $stateParams.classID;
                                result.classLessones.queryResult.pagedData[index].lessonCount = result.classLessones.queryResult.totalCount;
                            }
                            vm.data.rows = result.classLessones.queryResult.pagedData;
                            vm.class = result.class;
                            dataSyncService.updateTotalCount(vm, result.classLessones.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        },
                        function () {
                        })
                };

                vm.search();

                //编辑课表
                vm.editClassLesson = function () {
                    if (vm.data.rowsSelected.length == 1) {
                        if (vm.data.rowsSelected[0].lessonStatus == 1) {
                            mcsDialogService.create('app/schedule/classgroup/lesson-edit/lesson-edit.html', {
                                controller: 'lessonEditController',
                                params: vm.data.rowsSelected[0],
                                settings: {
                                    size: 'lg'
                                }
                            }).result.then(function () {
                                vm.search();
                            }, function () {

                            });
                        } else {
                            mcsDialogService.info(
                              { title: '提示', message: "只能针对排定的课表进行编辑！" }
                          )
                        }
                    } else {
                        mcsDialogService.info(
                              { title: '提示', message: "请选中要修改的课次！" }
                          )
                    }
                }

                //更换教师
                vm.editLessonTeacher = function () {
                    if (vm.data.rowsSelected.length == 1) {
                        //更换教师检查
                        var dt = new Date();
                        var startDT = new Date(dt.getYear(),dt.getMonth(),1,0,0,0,0);//当月第一天
                        if (dt.getDate() == 1) {
                            startDT = getPreMonth(dt);
                        }
                        if (vm.data.rowsSelected[0].startTime >= startDT) {
                            mcsDialogService.create('app/schedule/classgroup/teacher-edit/teacher-edit.html', {
                                controller: 'teacherEditController',
                                params: vm.data.rowsSelected[0],
                                settings: {
                                    size: 'lg'
                                }
                            }).result.then(function () {
                                vm.search();
                            }, function () {

                            });
                        } else {
                            mcsDialogService.info(
                              { title: '提示', message: "该节课不能更换教师！" }
                          );
                        }
                    } else {
                        mcsDialogService.info(
                              { title: '提示', message: "请选择一节课！" }
                          )
                    }
                }

                //获取上月第一天
                vm.getPreMonth = function (dt) {
                    var year = dt.getYear();
                    var month = dt.getMonth();
                    if (month == 1)
                        return new Date(year - 1, 12, 1, 0, 0, 0, 0);
                    else
                        return new Date(year, month - 1, 1, 0, 0, 0, 0);
                }

                //添加上课学生
                vm.addCustomers = function () {
                    for (var i in vm.data.rows) {
                        var item = vm.data.rows[i];
                        if (item.lessonStatus != 1) {
                            mcsDialogService.info(
                              { title: '提示', message: "所有课次状态为排定才可操作！" }
                          );
                            return;
                        }
                    }
                    mcsDialogService.create('app/schedule/classgroup/customer-add/customer-add.html', {
                        controller: 'customerAddController',
                        params: {
                            productID: vm.class.productID,
                            assets: [],
                            form: false
                        },
                        settings: {
                            size: 'lg'
                        }
                    }).result.then(function (data) {
                        vm.class.assets = data;
                        var cus = [];
                        for (var i = 0; i < data.length; i++) {
                            cus.push(data[i].customerName);
                        }
                        classgroupDataService.addCustomer(vm.class, function () {

                        }, function () { });
                        
                    }, function () {

                    });
                }

                //查看学生
                vm.searchCustomers = function () {
                    if (vm.data.rowsSelected.length == 1) {
                        mcsDialogService.create('app/schedule/classgroup/customer-list/customer-list.html', {
                            controller: 'customerListController',
                            params: {
                                classID: vm.data.rowsSelected[0].classID,
                                lessonIDs: [vm.data.rowsSelected[0].lessonID]
                            },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function (data) {

                        }, function () {

                        });
                    } else {
                        mcsDialogService.info(
                             { title: '提示', message: "请选择一节课！" }
                         )
                    }
                }

            }]);
    });