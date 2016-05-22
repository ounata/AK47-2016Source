define([ppts.config.modules.infra,
        ppts.config.dataServiceConfig.extraGiftDataService
    ],
    function (schedule) {
        schedule.registerController('presentListController', [
            'extraGiftDataService',
            function (extraGiftDataService) {
                var vm = this;

                //列表数据
                vm.data = {
                    selection: 'radio',
                    rowsSelected: [],
                    keyFields: ['presentID'],
                    pager: {
                        pageIndex: 1,
                        pageSize: 10,
                        totalCount: -1,
                        pageChange: function () {
                            var deferred = $q.defer();
                            extraGiftDataService.getPageClasses(vm.criteria, function (result) {
                                vm.data.rows = result.pagedData;
                                deferred.resolve();
                            });
                            return deferred.promise;
                        }
                    },
                    orderBy: [{ dataField: 'classID', sortDirection: 1 }],
                    headers: [{
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
                
                //新增买赠表
                vm.addPresent = function () {

                }

                //停用买赠表
                vm.stopPresent = function () {

                }

                //导出买赠表
                vm.expPresent = function () {

                }

            }]);
    });