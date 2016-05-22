define([ppts.config.modules.schedule,
    ppts.config.dataServiceConfig.classgroupDataService ],
        function (account) {
            account.registerController('customerClassListController', [
                '$scope', '$state', '$stateParams', '$location', '$q', 'mcsDialogService', 'classgroupDataService', 'dataSyncService',
                function ($scope, $state, $stateParams, $location, $q, mcsDialogService,classgroupDataService,  dataSyncService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.customerID = $stateParams.id;

                    //列表
                    vm.data = {
                        pager: {
                            pageIndex: 0,
                            pageSize: 0,
                            totalCount: -1
                        },
                        orderBy: [{ dataField: 'c.createTime', sortDirection: 1 }],
                        headers: [{
                            field: "createTime | date:'yyyy-MM-dd'",
                            name: "选班日期",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }, {
                            field: "campusName",
                            name: "班级所在校区",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },  {
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
                            field: "className",
                            name: "班级名称",
                            headerCss: 'datatable-header-align-right',                            
                            sortable: false,
                            description: ''
                        }, {
                            field: "subjectName",
                            name: "科目",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }, {
                            field: "gradeName",
                            name: "年级",
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
                            field: "realAmount",
                            name: "订购课次",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }, {
                            field: "confirmedAmount",
                            name: "已上课次",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }, {
                            field: "lessonDurationValue",
                            name: "每课次时长",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }, {
                            field: "periodsOfLesson",
                            name: "每课次课时数",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }, {
                            field: "creatorName",
                            name: "选班级操作人",
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
                        vm.criteria.customerID = vm.customerID;

                        classgroupDataService.getCustomerAllClasses(vm.criteria,
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

1