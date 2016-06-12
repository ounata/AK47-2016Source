define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.productDataService,
        ppts.config.dataServiceConfig.classgroupDataService],
    function (schedule) {
        schedule.registerController('customerListController', [
             '$q',
            'data',
            '$scope',
            '$uibModalInstance',
            'dataSyncService',
            'classgroupDataService',
            'mcsDialogService',
            function ($q, data, $scope, $uibModalInstance, dataSyncService, classgroupDataService, mcsDialogService) {
                var vm = this;

                //绑定页面tab数据
                vm.data = {
                    selection: 'checkbox',
                    rowsSelected: [],
                    keyFields: ['customerID'],
                    pager: {
                        pageIndex: 1,
                        pageSize: ppts.config.pageSizeItem,
                        totalCount: -1,
                        pageChange: function () {
                            dataSyncService.initCriteria(vm);
                            classgroupDataService.getPageCustomers(vm.criteria, function (result) {
                                vm.data.rows = result.pagedData;
                            });
                        }
                    },
                    orderBy: [{ dataField: 'customerID', sortDirection: 1 }],
                    headers: [{
                        field: "customerName",
                        name: "学生姓名",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ' '
                    }, {
                        field: "customerCode",
                        name: "学生编号",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ' '
                    }, {
                        field: "customerGradeName",
                        name: "当前年级",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ' '
                    }, {
                        field: "consultantName",
                        name: "当前咨询师",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ' '
                    }, {
                        field: "educatorName",
                        name: "当前学管师",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ' '
                    }, {
                        field: "isJoinClass | ifElse",
                        name: "是否插班",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ' '
                    }, {
                        field: "assetCode",
                        name: "订单编号",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: ' '
                    },
                        

                    ]
                }

                //查询条件
                vm.criteria = {
                    classID: data.classID,
                    lessonIDs:data.lessonIDs
                }

                dataSyncService.initCriteria(vm);

                dataSyncService.injectPageDict(['ifElse']);

                //关闭窗口
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');                    
                };

                //
                vm.save = function () {
                    $uibModalInstance.close();
                }

                //查询
                vm.search = function () {
                    //获取教师列表信息
                    classgroupDataService.getAllCustomers(
                        vm.criteria,
                        function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');                            
                        }, function () {

                        }
                    );
                }

                vm.search();

                //移除班级
                vm.deleteCustomer=function(){
                    if (vm.data.rowsSelected.length > 0) {
                        var customerIDs = [];
                        for (var i in vm.data.rowsSelected) {
                            customerIDs.push(vm.data.rowsSelected[i].customerID);
                        }
                        classgroupDataService.deleteCustomer({
                            customerIDs: customerIDs,
                            classID: vm.criteria.classID
                        }, function () {
                            vm.search();
                        }, function () {
                        });
                    }
                    else {
                        mcsDialogService.error(
                              { title: 'Error', message: "请选择一个学生！" }
                          )
                    }
                }
            }]);
    });