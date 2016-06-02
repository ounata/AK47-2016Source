define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.unsubscribeCourseDataService], function (helper) {
            helper.registerController('unsubscribeListController', [
                '$scope', '$state', 'dataSyncService', 'unsubscribeCourseDataService',
                function ($scope, $state, dataSyncService, unsubscribeCourseDataService) {
                    var vm = this;

                    vm.search = function () {
                        if (vm.criteria.dateRange) {
                            var dateRange = dataSyncService.selectPageDict('dateRange', vm.criteria.dateRange);
                            vm.criteria.startDate = dateRange.start || vm.criteria.customStartDate;
                            vm.criteria.endDate = dateRange.end || vm.criteria.customEndDate;
                        }

                        console.log(vm.criteria);

                        unsubscribeCourseDataService.getAllDebookOrders(vm.criteria, function (result) {

                            console.log(result);

                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');

                        });
                    };
                    vm.export = function () {
                        mcs.util.postMockForm(ppts.config.orderApiBaseUrl + 'api/Unsubscribe/ExportDebookItemList', vm.criteria);
                    }


                    var init = (function () {

                        //定义
                        vm.data = {
                            selection: 'radio',
                            rowsSelected: [],
                            keyFields: ['orderID'],
                            headers: [{
                                field: "customerName",
                                name: "学生姓名",
                            }, {
                                field: "customerCode",
                                name: "学生编号",
                            }, {
                                field: "parentName",
                                name: "家长姓名"
                            }, {
                                field: "orderNo",
                                name: "订单编号"
                            }, {
                                //field: "debookTime",
                                name: "退订日期",
                                template: '<span>{{row.debookTime | date:"yyyy-MM-dd"}}</span>',
                            }, {
                                field: "categoryTypeName",
                                name: "订单类型",
                                template: '<span>{{row.orderType | orderType }}</span>'
                            }, {
                                field: "realAmount",//orderAmount
                                name: "订购数量",
                            }, {
                                //field: "realPrice",
                                name: "订购金额（元）",
                                template: '<span>{{row.realPrice * row.realAmount | currency}}</span>'
                            }, {
                                //field: "realPrice",
                                name: "实际单价（元）",
                                template: '<span>{{row.realPrice | currency}}</span>'
                            }, {
                                field: "confirmedAmount",
                                name: "已使用数量",
                            }, {
                                //field: "realPrice",
                                name: "已使用金额",
                                template: '<span>{{ row.confirmedMoney | currency }}</span>'
                            }, {
                                //field: "realPrice",
                                name: "退订数量（赠送）",
                                template: '<span>{{ row.debookAmount }}({{row.presentAmountOfDebook}})</span>'                                        
                            }, {
                                field: "debookMoney",
                                name: "退订金额",
                            }, {
                                field: "submitterName",
                                name: "退订申请人",
                            }],
                            pager: {
                                pageIndex: 1,
                                pageSize: 10,
                                totalCount: -1,
                                pageChange: function () {
                                    dataSyncService.initCriteria(vm);
                                    unsubscribeCourseDataService.getPagedDebookOrders(vm.criteria, function (result) {
                                        vm.data.rows = result.pagedData;
                                    });
                                }
                            },
                            orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                        };

                        //初始化数据
                        dataSyncService.initCriteria(vm);
                        vm.criteria.dateRange = '0';
                        dataSyncService.injectPageDict(['dateRange']);


                        vm.search();

                    })();


                }]);
        });