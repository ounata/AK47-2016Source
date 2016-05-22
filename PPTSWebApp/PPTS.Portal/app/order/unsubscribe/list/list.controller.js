define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.unsubscribeCourseDataService], function (unsubscribe) {
            unsubscribe.registerController('unsubscribeListController', [
                '$scope', '$state', 'dataSyncService', 'unsubscribeCourseDataService',
                function ($scope, $state, dataSyncService, unsubscribeCourseDataService) {
                    var vm = this;

                    function bindEvent() {

                        vm.search = function () {
                            if (vm.criteria.dateRange) {
                                var dateRange = dataSyncService.selectPageDict('dateRange', vm.criteria.dateRange);
                                vm.criteria.startDate = dateRange.start || vm.criteria.customStartDate;
                                vm.criteria.endDate = dateRange.end || vm.criteria.customEndDate;
                            }
                            console.log(vm.criteria);
                            unsubscribeCourseDataService.getAllDebookOrders(vm.criteria, function (result) {
                                vm.data.rows = result.queryResult.pagedData;
                                dataSyncService.updateTotalCount(vm, result.queryResult);
                                $scope.$broadcast('dictionaryReady');
                            });
                        };



                    };

                    function init() {
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
                                field: "debookNo",
                                name: "订单编号"
                            }, {
                                //field: "debookTime",
                                name: "退订日期",
                                template: '<span>{{row.debookTime | date:"yyyy-MM-dd"}}</span>',
                            }, {
                                field: "categoryTypeName",
                                name: "订单类型",
                                //template: '<span>{{row.categoryType | categoryType}}</span>'
                            }, {
                                field: "orderAmount",
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
                                field: "usedOrderAmount",
                                name: "已使用数量",
                            }, {
                                //field: "realPrice",
                                name: "已使用金额",
                                template: '<span>{{ row.realPrice * row.usedOrderAmount | currency }}</span>'
                            }, {
                                //field: "realPrice",
                                name: "退订数量（赠送）",
                                template: '<span>{{ row.debookAmount }}</span>'
                                        + '<span ng-if="row.presentID!=\'\'">'
                                        + '({{ row.orderAmount == row.usedOrderAmount ? row.debookAmount : (row.debookAmount - row.orderAmount - row.usedOrderAmount) }})'
                                        + '</span>'
                                        + '<span ng-if="row.presentID==\'\'">(0)</span>'
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
                        

                        //绑定
                        bindEvent();


                        vm.search();
                    };
                    init();
                }]);
        });