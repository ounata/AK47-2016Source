define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (purchase) {
            purchase.registerController('orderListController', [
                '$scope', '$state', 'dataSyncService', 'purchaseCourseDataService',
                function ($scope, $state, dataSyncService, purchaseCourseDataService) {
                    var vm = this;
                    
                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['itemID', 'orderID', 'categoryType', ''],
                        headers: [{
                            field: "customerName",
                            name: "学生姓名",
                            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.purchase\'})">{{row.customerName}}</a>',
                            //sortable: true
                        }, {
                            field: "customerCode",
                            name: "学生编号",
                        }, {
                            field: "parentName",
                            name: "家长姓名"
                        }, {
                            //field: "itemID",
                            name: "订单编号",
                            template: '<a ui-sref="ppts.purchaseOrderView({orderId:row.orderID})">{{row.orderNo}}</a>',
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

                        if (vm.criteria.dateRange ) {
                            var dateRange = dataSyncService.selectPageDict('dateRange', vm.criteria.dateRange);
                            vm.criteria.startDate = dateRange.start || vm.criteria.customStartDate;
                            vm.criteria.endDate = dateRange.end || vm.criteria.customEndDate;
                        }

                        console.log(vm.criteria)
                        //return;



                        purchaseCourseDataService.getAllOrderItems(vm.criteria, function (result) {

                            console.warn(result)

                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);

                            $scope.$broadcast('dictionaryReady');
                        });

                    };
                    vm.unsubscribe = function () {
                        $state.go('ppts.unsubscribeProduct', { id: vm.data.rowsSelected[0].itemID });
                        //console.log(vm.data.rowsSelected);
                    };
                    vm.print = function () {
                        var id = vm.data.rowsSelected[0].itemID;
                        $state.go('ppts.purchasePrint', { id: id });
                    };
                    vm.editchargepay = function () {
                        var orderId = vm.data.rowsSelected[0].orderID;
                        $state.go('ppts.purchaseEditpayment', { orderid: orderId });
                    };


                    var init = (function () {
                        dataSyncService.initCriteria(vm);

                        //vm.criteria.dateRange = "-1";
                        dataSyncService.injectPageDict(['dateRange']);

                        vm.search();

                    })();


                }]);
        });

