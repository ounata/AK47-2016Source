define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.classgroupDataService],
    function (schedule) {
        schedule.registerController('customerAddController', [
            '$q',
            'data',
            '$uibModalInstance',
            'classgroupDataService',
            'dataSyncService',
            function ($q,data, $uibModalInstance, classgroupDataService, dataSyncService) {
                var vm = this;

                //绑定页面tab数据
                vm.data = {
                    selection: 'checkbox',
                    rowsSelected: data.assets,
                    keyFields: ['customerID', 'assetID', 'assetCode', 'price', 'customerName'],
                    pager: {
                        pageIndex: 1,
                        pageSize: ppts.config.pageSizeItem,
                        totalCount: -1,
                        pageChange: function () {
                            dataSyncService.initCriteria(vm,false);
                            var deferred = $q.defer();

                            classgroupDataService.getPageAssets(vm.criteria, function (result) {
                                vm.data.rows = result.pagedData;

                                deferred.resolve();
                            });

                            return deferred.promise;

                        }
                    },
                    orderBy: [{ dataField: 'assetID', sortDirection: 1 }],
                    headers: [{
                        field: "customerName",
                        name: "学生姓名",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        description: 'customer name'
                    },
                        {
                            field: "customerCode",
                            name: "学生编号",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: 'customer code'
                        },
                        {
                            field: "customerGradeName",
                            name: "当前年级",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "submitTime",
                            name: "订购时间",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "AssetCode",
                            name: "订购编号",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "submitterName",
                            name: "订购人姓名",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "submitterJobName",
                            name: "订购人岗位",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "consultantName",
                            name: "当前咨询师",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        },
                        {
                            field: "educatorName",
                            name: "当前学管师",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }
                    ]
                }

                //查询条件
                vm.criteria = {
                    productID: data.productID
                }

                


                vm.init = function () {
                    var deferred = $q.defer();
                    dataSyncService.initCriteria(vm, false);

                    //获取学生订单信息
                    classgroupDataService.getAllAssets(

                        vm.criteria,
                        function (result) {
                        
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            deferred.resolve();
                        }, function () {

                        }
                    );

                    return deferred.promise;
                }

                //关闭窗口
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                    if (data.form)
                        data.form.customers.$setDirty();
                };

                //保存按周上课时间
                vm.save = function () {
                    $uibModalInstance.close(vm.data.rowsSelected);
                }



                
            }]);
    });