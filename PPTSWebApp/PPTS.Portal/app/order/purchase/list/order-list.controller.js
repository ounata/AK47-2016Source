define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService,
        ppts.config.dataServiceConfig.purchaseCourseInitControl],
        function (purchase) {
            purchase.registerController('orderListController', [
                '$scope', '$state', 'dataSyncService', 'mcsDialogService', 'utilService', 'purchaseCourseDataService',
                function ($scope, $state, dataSyncService, mcsDialogService, utilService, purchaseCourseDataService) {
                    //console.log(this)
                    //console.log($scope)


                    var vm = this;

                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['itemID', 'orderID', 'categoryType', ''],
                        headers: [{
                            field: "customerName",
                            name: "学员姓名",
                            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.student\'})">{{row.customerName}}</a>',
                            //sortable: true
                        }, {
                            field: "customerCode",
                            name: "学员编号",
                        }, {
                            field: "parentName",
                            name: "家长姓名"
                        }, {
                            //field: "itemID",
                            name: "订单编号",
                            template: '<a ui-sref="ppts.purchaseOrderView({orderId:row.orderID})">{{ row.itemNo }}</a>',
                        }, {
                            field: "orderTime",
                            name: "订购日期",
                            template: '<span>{{row.orderTime | date:"yyyy-MM-dd"}}</span>',
                        }, {
                            field: "categoryType",
                            name: "订单类型",
                            template: '<span>{{row.categoryType | categoryType}}</span>'
                        }, {
                            field: "grade",
                            name: "年级",
                            template: '<span>{{row.grade | grade}}</span>'
                        }, {
                            field: "subject",
                            name: "科目",
                            template: '<span>{{row.subject | subject}}</span>'
                        }, {
                            field: "courseLevel",
                            name: "课程级别",
                            template: '<span>{{row.courseLevel | courseLevel}}</span>'
                        }, {
                            field: "orderAmount",
                            name: "订购数量",
                        }, {
                            //field: "realPrice",
                            name: "订购金额",
                            template: '<span>{{ row.realPrice * row.realAmount }}</span>'
                        }, {
                            field: "presentAmount",
                            name: "赠送数量",
                        }, {
                            field: "debookedAmount",
                            name: "已退数量",
                        }, {
                            field: "assignedAmount",
                            name: "已排数量",
                        }, {
                            field: "confirmedAmount",
                            name: "已上数量",
                        }, {
                            name: "剩余数量",
                            template: '{{ row.realAmount - row.debookedAmount }}'
                        }, {
                            field: "orderStatus",
                            name: "订单状态",
                            template: '<span>{{row.orderStatus | orderStatus }}</span>'
                        }, {
                            field: "submitterName",
                            name: "订购操作人",
                        }, {
                            field: "submitterJobName",
                            name: "操作人岗位",
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: ppts.config.pageSizeItem,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                purchaseCourseDataService.getPagedOrderItems(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }


                    vm.export = function () {

                        mcs.util.postMockForm(ppts.config.orderApiBaseUrl + '/api/Purchase/ExportOrderItemList', vm.criteria);
                    }
                    vm.search = function () {

                        if (vm.criteria.dateRange) {
                            var dateRange = dataSyncService.selectPageDict('dateRange', vm.criteria.dateRange);
                            vm.criteria.startDate = dateRange.start || vm.criteria.customStartDate;
                            vm.criteria.endDate = dateRange.end || vm.criteria.customEndDate;
                        }

                        //console.log(vm.criteria)
                        ////return;


                        purchaseCourseDataService.getAllOrderItems(vm.criteria, function (result) {

                            //console.warn(result)
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        });

                    };
                    vm.unsubscribe = function () {
                        if (!utilService.selectOneRow(vm)) { return; }
                        $state.go('ppts.unsubscribeProduct', { id: vm.data.rowsSelected[0].itemID });

                    };
                    vm.print = function () {
                        if (!utilService.selectOneRow(vm)) { return;}
                        if (vm.data.rowsSelected[0].categoryType != 1 && vm.data.rowsSelected[0].categoryType != 2) {
                            mcsDialogService.error({ title: '错误', message: '非一对一，班组产品不可打印！' })
                            return;
                        }
                        var id = vm.data.rowsSelected[0].itemID;
                        $state.go('ppts.purchasePrint', { id: id });
                    };
                    vm.editchargepay = function () {
                        if (!utilService.selectOneRow(vm)) { return; }
                        var orderId = vm.data.rowsSelected[0].orderID;
                        $state.go('ppts.purchaseEditpayment', { orderid: orderId });
                    };


                    var permisstionFilter = function () {
                        //权限指定
                        vm.criteria.branch = ppts.user.branchId;
                        vm.criteria.campusID = ppts.user.campusId;
                        if (ppts.user.branchId) { vm.disabledlevel = 1; }
                        if (ppts.user.campusId) { vm.disabledlevel = 2; }
                    };


                    var init = (function () {

                        dataSyncService.initCriteria(vm); 
                        dataSyncService.injectDynamicDict('dateRange');

                        permisstionFilter();

                        vm.search();

                    })();


                }]);
        });

