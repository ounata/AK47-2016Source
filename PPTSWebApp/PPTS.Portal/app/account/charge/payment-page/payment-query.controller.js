define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargePaymentQueryController', [
                '$scope', '$state', 'mcsDialogService', 'dataSyncService', 'accountChargeDataService', 'paymentQueryAdvanceSearchItems',
                function ($scope, $state, mcsDialogService, dataSyncService, accountDataService, searchItems) {
                    var vm = this;

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['payID'],
                        headers: [{
                            field: "customerName",
                            name: "学员姓名",
                            template: '<span>{{row.customerName}}</span>'
                        }, {
                            field: "customerCode",
                            name: "学员编号",
                            template: '<span>{{row.customerCode}}</span>'
                        }, {
                            field: "applyNo",
                            name: "缴费单号",
                            template: '<span>{{row.applyNo}}</span>'
                        }, {
                            field: "chargeMoney",
                            name: "充值金额",
                            template: '<span>{{row.chargeMoney | currency:"￥"}}</span>'
                        }, {
                            field: "payeeName",
                            name: "收款人",
                            template: '<span>{{row.payeeName}}</span>'
                        }, {
                            field: "payMoney",
                            name: "收款金额",
                            template: '<span>{{row.payMoney | currency:"￥"}}</span>'
                        }, {
                            field: "payType",
                            name: "收款类型",
                            template: '<span>{{row.payType | payType}}</span>'
                        }, {
                            field: "payTime",
                            name: "收款时间",
                            template: '<span>{{row.payTime | date:"yyyy-MM-dd HH:mm"}}</span>'
                        }, {
                            field: "campusName",
                            name: "校区",
                            template: '<span>{{row.campusName}}</span>'
                        }, {
                            field: "checkStatus",
                            name: "对账状态",
                            template: '<span ng-class="{1: \'ppts-checked-color\', 0: \'ppts-unchecked-color\'}[{{row.checkStatus}}]">{{row.checkStatus | checkStatus}}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: ppts.config.pageSizeItem,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                customerDataService.queryPagedChargePaymentList(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'a.payTime', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {

                        dataSyncService.initCriteria(vm);
                        accountDataService.queryChargePaymentList(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            vm.searchItems = searchItems;
                            dataSyncService.injectDictData();
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();

                    //收款单对账
                    vm.check = function () {

                        vm.errorMessage = null;
                        var payIDs = [];
                        for (var i = 0; i < vm.data.rows.length; i++) {
                            for (var j = 0; j < vm.data.rowsSelected.length; j++) {
                                if (vm.data.rows[i].payID == vm.data.rowsSelected[j].payID && vm.data.rows[i].canCheck) {
                                    payIDs.push(vm.data.rows[i].payID);
                                }
                            }
                        }
                        if (payIDs.length == 0) {
                            vm.errorMessage = "请选择需要对账的记录";
                            return;
                        }
                        mcsDialogService.confirm({ title: '确认', message: '确认要对账这' + payIDs.length + '条记录？' })
                           .result.then(function () {
                               accountDataService.checkChargePayment(payIDs, function () {
                                   vm.search();
                               });
                           });
                    }

                }]);
        });
