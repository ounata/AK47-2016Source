define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (helper) {
            helper.registerController('orderHistoryController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'utilService', 'mcsDialogService', 'purchaseCourseDataService',
                function ($scope, $state, $stateParams, dataSyncService, utilService, mcsDialogService, purchaseCourseDataService) {

                    var vm = this;
                    var stuId = $stateParams.stuId;


                    vm.dropdowns = [{
                        text: '跨年级补差价-选择产品', route: 'ppts.purchaseExchange-one', params: { type: "one" }
                    }, {
                        text: '跨年级不补差价-选择产品', route: 'ppts.purchaseExchange-two', params: { type: "two" }
                    }];

                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['orderID', 'itemID','categoryType'],
                        headers: [{
                            name: "订单编号",
                            template: '<span ><a ui-sref="ppts.purchaseOrderView({orderId:row.orderID})" ng-class="{true:\'red\',false:\'blue\'}[row.orderType==2]" >{{row.itemNo}}</a></span>',
                        }, {
                            field: "orderTime",
                            name: "订购日期",
                            template: '<span>{{row.orderTime | date:"yyyy-MM-dd"}}</span>',
                        }, {
                            name: "产品名称",
                            template: '<span uib-popover=" {{ row.productName | tooltip:6 }}" popover-trigger="mouseenter">{{ row.productName | truncate:6  }}</span>',
                        }, {
                            field: "orderPrice",
                            name: "产品单价（元）",
                        }, {
                            field: "realPrice",
                            name: "实际单价（元）",
                        }, {
                            //field: "orderAmount",
                            name: "订购数量",
                            template: '<span ng-if="row.orderType!=2">{{ row.orderAmount }}</span>'
                                + '<span ng-if="row.orderType==2"><span uib-popover=" 购买:{{row.orderAmount}} 赠送:{{ row.presentAmount}}" popover-trigger="mouseenter">{{ row.orderAmount  }}</span></span>'
                        }, {
                            name: "订购金额",
                            template: '<span ng-show="row.confirmedMoney==0"><a href="javascript:;" >{{ row.realPrice * row.realAmount | currency}}</a></span>'
                                    + '<span ng-show="row.confirmedMoney>0"><a href="javascript:;" ng-click="vm.showRecognizingIncomeList(row)">{{ row.realPrice * row.realAmount | currency}}</a></span>'
                            //template: '<span>{{ row.realPrice * row.realAmount }}</span>'
                        }, {
                            //field: "debookedAmount",
                            name: "已退数量",
                            template: '<span ng-if="row.orderType!=2">{{ row.debookedAmount }}</span>'
                                + '<span ng-if="row.orderType==2"><span uib-popover=" 购买:{{row.orderAmount}} 赠送:{{ row.presentAmount}}" popover-trigger="mouseenter">{{ row.debookedAmount  }}</span></span>'
                        }, {
                            //field: "confirmedAmount",
                            name: "已上数量",
                            template: '<span ng-if="row.orderType!=2">{{ row.confirmedAmount }}</span>'
                                + '<span ng-if="row.orderType==2"><span uib-popover=" 购买:{{row.orderAmount}} 赠送:{{ row.presentAmount}}" popover-trigger="mouseenter">{{ row.confirmedAmount  }}</span></span>'
                            
                        },
                        {
                            name: "剩余数量",
                            //template: '{{ row.realAmount - row.debookedAmount }}'
                            template: '<span ng-if="row.orderType!=2">{{ row.realAmount - row.debookedAmount }}</span>'
                                + '<span ng-if="row.orderType==2"><span uib-popover=" 购买:{{row.orderAmount}} 赠送:{{ row.presentAmount}}" popover-trigger="mouseenter">{{ row.realAmount - row.debookedAmount  }}</span></span>'
                        }, {
                            field: "discountRate",
                            name: "折扣",
                        }, {
                            //field: "orderType",
                            name: "订购类型",
                            template: '<span>{{row.categoryType | categoryType}}</span>'
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

                        //console.log(vm.criteria)
                        //return;

                        purchaseCourseDataService.getAllOrderItems(vm.criteria, function (result) {

                            console.warn(result)

                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);

                            $scope.$broadcast('dictionaryReady');
                        });

                    };
                    vm.unsubscribe = function () {
                        if (!utilService.selectOneRow(vm)) { return; }
                        $state.go('ppts.unsubscribeProduct', { id: vm.data.rowsSelected[0].itemID });
                    };
                    vm.exchange = function (item) {
                        if (!utilService.selectOneRow(vm)) { return; }
                        var selectedRow = vm.data.rowsSelected[0]; 
                        if (utilService.showMessage(vm, selectedRow.categoryType != 1, '只有1对1类型的订购单可以进行资产兑换！')) { return; }

                        item.params["itemid"] = selectedRow.itemID;
                        $state.go(item.route, item.params);
                    };
                    vm.recognizingIncome = function () {

                        if (!utilService.selectOneRow(vm)) { return; }
                        var selectedRow = vm.data.rowsSelected[0];

                        var index = mcs.util.indexOf(vm.data.rows, "itemID", selectedRow.itemID);
                        var data = vm.data.rows[index];

                        purchaseCourseDataService.getProduct(data.productID, function (result) {

                            if (result.hasCourse != 0) {
                                mcsDialogService.confirm({ title: '提示', message: '此订购单不能进行手工确认！' });
                                return;
                            }

                            if (result.confirmMode==2) {
                                mcsDialogService.confirm({ title: '提示', message: '此订购单不能进行手工确认，系统会在指定时间自动确认！' });
                                return;
                            }

                            data.confirmStartDate = result.confirmStartDate;
                            data.confirmEndDate = result.confirmEndDate;

                            mcsDialogService.create('app/order/purchase/history/recognizingIncome.html', { controller: 'recognizingIncomeController', params: data })
                                .result.then(function (entity) {

                                    data.confirmedMoney = entity;

                                });

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
                        vm.criteria.stuId = stuId;
                        dataSyncService.injectDynamicDict('dateRange');
                        vm.search();
                    })();


                }]);

            helper.registerController('recognizingIncomeController', [
                '$scope', '$uibModalInstance', 'mcsDialogService', 'mcsValidationService', 'purchaseCourseDataService', 'data',
                function ($scope, $uibModalInstance, mcsDialogService, mcsValidationService, purchaseCourseDataService, data) {

                    mcsValidationService.init($scope);

                    var vm = this;
                    vm.data = data;
                    vm.input = {};
                    


                    vm.close = function () { $uibModalInstance.dismiss('Canceled'); };
                    vm.cancel = function () { $uibModalInstance.dismiss('Canceled'); };
                    vm.save = function () {
                        if (!mcsValidationService.run($scope)) { return; }

                        if (vm.data.realPrice * vm.data.realAmount - vm.data.confirmedMoney < vm.input.money) {
                            mcsDialogService.error({ title: 'Error', message: '提交数据有误！' });
                            return;
                        }

                        //var currentTime = new Date();
                        //if (vm.data.confirmStartDate > currentTime || currentTime > vm.data.confirmEndDate) {
                        //    mcsDialogService.error({ title: 'Error', message: '确认时间已过，系统将自动确认！' });
                        //    return;
                        //}

                        mcsDialogService.confirm({
                            title: 'confirm',
                            message: '请确认是将该订购单的全部收入(' + vm.input.money + ')扣除，扣除后将计入“DASH-非授课产品收入”中，扣除后将不能返回操作，扣除请点“确定”，不扣除则点“取消”退出功能。'
                        }).result.then(function () {
                            var param = { customerID: data.customerID, orderID: data.orderID, itemID: data.itemID, productID: data.productID, confirmedMoney: vm.input.money };
                            purchaseCourseDataService.submitRecognizingIncome(param, function (result) {
                                data.confirmedMoney +=parseFloat( vm.input.money);
                                $uibModalInstance.close(data.confirmedMoney);
                            });
                        });

                    };

                    var init = (function () {
                        
                    })();
                }

            ]);

            helper.registerController('recognizingIncomeListController', [
                '$scope', '$uibModalInstance', 'purchaseCourseDataService', 'data',
                function ($scope, $uibModalInstance, purchaseCourseDataService, data) {
                    var vm = this;

                    vm.data = {
                        headers: [ {
                            //field: "confirmTime",
                            name: "确认日期",
                            template: '<span>{{ row.confirmTime | date:"yyyy-MM-dd" }}</span>'
                        }, {
                            field: "confirmerName",
                            name: "确认人",
                        }, {
                            field: "confirmerJobName",
                            name: "确认人岗位",
                        }, {
                            field: "confirmerJobName",
                            name: "确认人归属机构",
                        }, {
                            name: "确认金额",
                            template: '<span>{{ row.confirmMoney | currency }}</span>'
                        }],
                        pager: {
                            pageable:false
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }

                    vm.close = function () { $uibModalInstance.dismiss('Canceled'); };

                    var init = (function () {
                        purchaseCourseDataService.getRecognizingIncome(data.itemID, function (result) {
                            vm.data.rows = result.list;
                        });
                    })();

                }]);

        });

