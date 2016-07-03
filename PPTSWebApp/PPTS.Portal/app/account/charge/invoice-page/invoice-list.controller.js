define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeInvoiceListController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountChargeDataService', 'dataSyncService', 'utilService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService, dataSyncService, utilService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.applyID;

                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['invoiceID'],
                        headers: [{
                            field: "invoiceNo",
                            name: "发票号",
                            template: '<span>{{row.invoiceNo}}</span>'
                        }, {
                            field: "invoiceMoney",
                            name: "发票金额",
                            template: '<span>{{row.invoiceMoney}}</span>'
                        }, {
                            field: "invoiceClauses",
                            name: "发票条目",
                            template: '<span>{{ row.invoiceClauses }}</span>'
                        }, {
                            field: "invoiceHeader",
                            name: "发票抬头",
                            template: '<span>{{row.invoiceHeader }}</span>'
                        }, {
                            field: "invoiceTime",
                            name: "开票时间",
                            template: '<span>{{row.invoiceTime | date:"yyyy-MM-dd" | normalize }}</span>'
                        }, {
                            field: "invoiceStatus",
                            name: "发票状态",
                            template: '<span>{{row.invoiceStatus | invoiceStatus}}</span>'
                        }, {
                            field: "isDiscarded",
                            name: "发票记录状态",
                            template: '<span>{{row.isDiscarded | invoiceRecordStatus}}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 10,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                accountDataService.getPagedAccountChargeInvoiceCollection(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData.rows;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'invoiceNo', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        dataSyncService.initCriteria(vm);
                        vm.criteria.applyID = vm.applyID;
                        accountDataService.getAccountChargeInvoiceCollection(vm.criteria, function (result) {
                            vm.apply = result.chargeApply.apply
                            vm.customer = result.chargeApply.customer;
                            vm.data.rows = result.queryResult.pagedData;
                            vm.data.searching();
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                        });
                    };
                    vm.init();

                    vm.add = function () {
                        mcsDialogService.create('app/account/charge/invoice-page/invoice-add.html', {
                            controller: 'accountChargeInvoiceAddController',
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function (retValue) {
                            if (retValue != 'canceled') {
                                vm.init();
                            };
                        });
                    };
                    vm.edit = function () {
                        if (!utilService.selectOneRow(vm)) {
                            return;
                        }
                        var selectModel = { invoiceID: vm.data.rowsSelected[0].invoiceID };
                        mcsDialogService.create('app/account/charge/invoice-page/invoice-edit.html', {
                            controller: 'accountChargeInvoiceEditController',
                            params: { para: selectModel },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function (retValue) {
                            if (retValue != 'canceled') {
                                vm.init();
                            };
                        });
                    };
                    vm.look = function () {
                        if (!utilService.selectOneRow(vm)) {
                            return;
                        }
                        var selectModel = { invoiceID: vm.data.rowsSelected[0].invoiceID };
                        mcsDialogService.create('app/account/charge/invoice-page/invoice-info.html', {
                            controller: 'accountChargeInvoiceInfoController',
                            params: { para: selectModel },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function (retValue) {

                        });
                    };



                }]);
        });