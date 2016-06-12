define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (helper) {
            helper.registerController('orderHistoryController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'mcsDialogService', 'purchaseCourseDataService',
                function ($scope, $state, $stateParams, dataSyncService, mcsDialogService, purchaseCourseDataService) {

                    var vm = this;
                    var stuCode = $stateParams.stuCode;


                    vm.dropdowns = [{
                        text: '跨年级补差价-选择产品', route: 'ppts.purchaseExchange', params: { type: "1" }
                    }, {
                        text: '跨年级不补差价-选择产品', route: 'ppts.purchaseExchange', params: { type: "2" }
                    }];
                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['orderID', 'itemID'],
                        headers: [{
                            name: "订单编号",
                            template: '<span ><a ui-sref="ppts.purchaseOrderView({orderNo:row.orderNo})">{{row.orderNo}}</a></span>',
                        }, {
                            field: "orderTime",
                            name: "订购日期",
                            template: '<span>{{row.orderTime | date:"yyyy-MM-dd"}}</span>',
                        }, {
                            field: "productName",
                            name: "产品名称",
                        }, {
                            field: "orderPrice",
                            name: "产品单价（元）",
                        }, {
                            field: "realPrice",
                            name: "实际单价（元）",
                        }, {
                            field: "orderAmount",
                            name: "订购数量",
                        }, {
                            name: "订购金额",
                            template: '<span>{{ row.realPrice * row.realAmount }}</span>'
                        }, {
                            field: "debookedAmount",
                            name: "已退数量",
                        }, {
                            //field: "confirmedAmount",
                            name: "已上数量",
                            template: '<span ng-if="row.confirmedAmount>0"><a href="javascript:;" ng-click="vm.showRecognizingIncomeList(row)">{{row.confirmedAmount}}</a></span>'
                                    + '<span ng-if="row.confirmedAmount<1">0</span>'
                        }, {
                            name: "剩余数量",
                            template: '{{ row.realAmount - row.debookedAmount }}'
                        }, {
                            field: "discountRate",
                            name: "折扣",
                        }, {
                            field: "discountRate",
                            name: "折扣",
                        }, {
                            field: "orderType",
                            name: "订购类型",
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


                    vm.search = function () {

                        if (vm.criteria.dateRange) {
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
                    };
                    vm.exchange = function (item) {
                        item.params["itemid"] = vm.data.rowsSelected[0].itemID;
                        $state.go(item.route, item.params);
                    };
                    vm.recognizingIncome = function () {

                        var selectedRow = vm.data.rowsSelected[0];
                        var index = mcs.util.indexOf(vm.data.rows, "itemID", selectedRow.itemID);
                        var data = vm.data.rows[index];

                        mcsDialogService.create('app/order/purchase/history/recognizingIncome.html', { controller: 'recognizingIncomeController', params: data })
                                .result.then(function (entity) {

                                    data.confirmedAmount = 1;

                                    console.warn(entity);

                                    //productDataService.delayProduct({ id: rowProduct.productID, endDate: data.endDate }, function () {
                                    //    var index = getSelectedIndexs(rowProduct.productID);
                                    //    vm.data.rows[index].endDate = data.endDate;
                                    //});
                                });

                    };
                    vm.showRecognizingIncomeList = function (row) {
                        console.log(row);

                        var data = row;
                        mcsDialogService.create('app/order/purchase/history/recognizingIncome-list.html', { controller: 'recognizingIncomeListController', params: data });

                    };

                    var init = (function () {

                        for (var index in vm.dropdowns) {
                            vm.dropdowns[index]['click'] = vm.exchange;
                        }

                        dataSyncService.initCriteria(vm);
                        vm.criteria.stuCode = stuCode;
                        dataSyncService.injectPageDict(['dateRange']);
                        vm.search();
                    })();


                }]);

            helper.registerController('recognizingIncomeController', [
                '$scope', '$uibModalInstance', 'mcsDialogService', 'data',
                function ($scope, $uibModalInstance, mcsDialogService, data) {
                    var vm = this;

                    vm.data = data;
                    vm.input = {};
                    

                    vm.close = function () { $uibModalInstance.dismiss('Canceled'); };
                    vm.cancel = function () { $uibModalInstance.dismiss('Canceled'); };
                    vm.save = function () {
                        mcsDialogService.confirm({
                            title: 'confirm',
                            message: '请确认是将该订购单的全部收入(' + vm.input.money + ')扣除，扣除后将计入“DASH-非授课产品收入”中，扣除后将不能返回操作，扣除请点“确定”，不扣除则点“取消”退出功能。'
                        }).result.then(function () {
                            $uibModalInstance.close(vm.input);
                        });
                    };

                    var init = (function () {

                    })();
                }
            ]);

            helper.registerController('recognizingIncomeListController', [
                '$scope', '$uibModalInstance', 'data',
                function ($scope, $uibModalInstance, data) {
                    var vm = this;

                    vm.data = {
                        headers: [ {
                            field: "orderTime",
                            name: "确认日期",
                        }, {
                            field: "productName",
                            name: "确认人",
                        }, {
                            field: "orderPrice",
                            name: "确认人岗位",
                        }, {
                            field: "realPrice",
                            name: "确认人归属机构",
                        }, {
                            name: "确认金额",
                            template: '<span>{{ row.realPrice * row.realAmount }}</span>'
                        }],
                        pager: {
                            pageable:false
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }

                    vm.close = function () { $uibModalInstance.dismiss('Canceled'); };

                    var init = (function () {

                    })();

                }]);

        });

