define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeQueryController', [
                '$scope', '$state', 'mcsDialogService', 'dataSyncService', 'accountChargeDataService', 'chargeQueryAdvanceSearchItems',
                function ($scope, $state, mcsDialogService, dataSyncService, accountDataService, searchItems) {
                    var vm = this;

                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['applyID'],
                        headers: [{
                            field: "customerName",
                            name: "学员姓名",
                            template: '<span>{{row.customerName}}</span>'
                        }, {
                            field: "customerCode",
                            name: "学员编号",
                            template: '<span>{{row.customerCode}}</span>'
                        }, {
                            field: "parentName",
                            name: "家长姓名",
                            template: '<span>{{row.parentName}}</span>'
                        }, {
                            field: "applyNo",
                            name: "缴费单号",
                            template: '<span><a ui-sref="ppts.accountCharge-view.info({applyID:row.applyID,prev:\'ppts.accountCharge-query\'})">{{row.applyNo}}</span>'
                        }, {
                            field: "chargeType",
                            name: "充值类型",
                            template: '<span>{{row.chargeType | chargeType}}</span>'
                        }, {
                            field: "payStatus",
                            name: "充值状态",
                            template: '<span>{{row.payStatus | payStatus}}</span>'
                        }, {
                            field: "auditStatus",
                            name: "审核状态",
                            template: '<span>{{row.auditStatus | chargeAuditStatus}}</span>'
                        }, {
                            field: "chargeMoney",
                            name: "充值金额",
                            template: '<span>{{row.chargeMoney | currency:"￥"}}</span>'
                        }, {
                            field: "thisDiscountBase",
                            name: "折扣基数",
                            template: '<span>{{row.thisDiscountBase | currency:"￥"}}</span>'
                        }, {
                            field: "thisDiscountRate",
                            name: "折扣率",
                            template: '<span>{{row.thisDiscountRate | number:"2"}}</span>'
                        }, {
                            field: "campusName",
                            name: "校区",
                            template: '<span>{{row.campusName}}</span>'
                        }, {
                            field: "applierName",
                            name: "申请人",
                            template: '<span>{{row.applierName}}</span>'
                        }, {
                            field: "applierJobName",
                            name: "申请人岗位",
                            template: '<span>{{row.applierJobName}}</span>'
                        }, {
                            field: "customerGrade",
                            name: "当时年级",
                            template: '<span>{{row.customerGrade | grade}}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: ppts.config.pageSizeItem,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                customerDataService.queryPagedChargeApplyList(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'applyTime', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {

                        dataSyncService.initCriteria(vm);
                        accountDataService.queryChargeApplyList(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            vm.searchItems = searchItems;
                            dataSyncService.injectDictData();
                            dataSyncService.injectPageDict(['people']);
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();

                    vm.getCurrentRow = function () {

                        if (vm.data.rowsSelected.length == 1) {
                            for (var i = 0; i < vm.data.rows.length; i++) {
                                if (vm.data.rows[i].applyID.toLowerCase() == vm.data.rowsSelected[0].applyID.toLowerCase()) {
                                    return vm.data.rows[i];
                                }
                            }
                        }
                        return null;
                    }

                    //编辑业绩比例
                    vm.edit = function () {

                        vm.errorMessage = null;
                        var currentRow = vm.getCurrentRow();
                        if (currentRow == null) {
                            vm.errorMessage = "请选择一条要编辑的记录";
                            return;
                        }
                        var applyID = currentRow.applyID;
                        $state.go('ppts.accountCharge-allot', {
                            applyID: applyID,
                            prev: 'ppts.accountCharge-query'
                        });
                    }

                    //缴费单审核
                    vm.audit = function () {

                        vm.errorMessage = null;
                        var currentRow = vm.getCurrentRow();
                        if (currentRow == null) {
                            vm.errorMessage = "请选择一条要审核的记录";
                            return;
                        }
                        if (!currentRow.canAudit) {
                            vm.errorMessage = "只有付款完成未审核的记录才可审核";
                            return;
                        }
                        var applyID = currentRow.applyID;
                        mcsDialogService.create('app/account/charge/charge-page/charge-audit.html', {
                            controller: 'accountChargeAuditController',
                            params: {
                                applyID: applyID
                            },
                            settings: {
                                size: 'lg'
                            }
                        }).result.then(function () {
                            vm.search();
                        });
                    }
                }]);
        });
