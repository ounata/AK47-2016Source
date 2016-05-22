define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountTransferDataService],
        function (account) {
            account.registerController('accountTransferEditController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountTransferDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.customerID = $stateParams.id;

                    vm.account = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['accountID'],
                        headers: [{
                            field: "accountCode",
                            name: "账户编码",
                            template: '<span>{{row.accountCode}}</span>'
                        }, {
                            field: "discountBase",
                            name: "折扣基数",
                            template: '<span>{{row.discountBase|currency:"￥"}}</span>'
                        }, {
                            field: "discountRate",
                            name: "折扣率",
                            template: '<span>{{row.discountRate|number:2}}</span>'
                        }, {
                            field: "accountMoney",
                            name: "可转金额",
                            template: '<span>{{row.accountMoney|currency:"￥"}}</span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'accountID', sortDirection: 1 }]
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getTransferApplyByCustomerID(vm.customerID, function (result) {
                            vm.apply = result.apply
                            vm.assert = result.assert;
                            vm.customer = result.customer;
                            vm.account.rows = result.accounts;
                            vm.errorMessage = result.assert.message;

                            if (vm.account.rows.length == 1) {
                                vm.account.rows[0].selected = true;
                                vm.account.rowsSelected.push({ accountID: vm.account.rows[0].accountID })
                            }

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    //获取转出地学员信息
                    vm.fetchCustomer = function (e) {
                        if (e && e.keyCode != 13) {
                            return;
                        }
                        if (vm.bizCustomerCode && vm.bizCustomerCode != '') {

                            vm.apply.bizCampusID = null;
                            vm.apply.bizCampusName = null;
                            vm.apply.bizCustomerID = null;
                            vm.apply.bizCustomerCode = null;
                            vm.apply.bizCustomerName = null;

                            accountDataService.getCustomer(vm.bizCustomerCode, function (result) {
                                if (result) {
                                    if (!result.customerID) {
                                        mcsDialogService.error({ title: '警告', message: '该学员并不存在' });
                                        return;
                                    }
                                    else if (result.customerID == vm.apply.customerID) {
                                        mcsDialogService.error({ title: '警告', message: '转至学员不能是自己' });
                                    }
                                    vm.apply.bizCampusID = result.campusID;
                                    vm.apply.bizCampusName = result.campusName;
                                    vm.apply.bizCustomerID = result.customerID;
                                    vm.apply.bizCustomerCode = result.customerCode;
                                    vm.apply.bizCustomerName = result.customerName;
                                }
                            });
                        }
                    }

                    vm.getCurrentRow = function () {

                        if (vm.account.rowsSelected.length == 1) {
                            for (var i = 0; i < vm.account.rows.length; i++) {
                                if (vm.account.rows[i].accountID == vm.account.rowsSelected[0].accountID) {
                                    return vm.account.rows[i];
                                }
                            }
                        }
                        return null;
                    }

                    $scope.$watch('vm.account.rowsSelected[0]', function () {
                        var row = vm.getCurrentRow();
                        if (row) {
                            vm.apply.accountID = row.accountID;
                            vm.apply.accountCode = row.accountCode;
                            vm.apply.accountType = row.accountType;
                            vm.apply.thatDiscountID = row.discountID;
                            vm.apply.thatDiscountCode = row.discountCode;
                            vm.apply.thatDiscountBase = row.discountBase;
                            vm.apply.thatDiscountRate = row.discountRate;
                            vm.apply.thatAccountValue = row.accountValue;
                            vm.apply.thatAccountMoney = row.accountMoney;

                            vm.apply.transferMoney = row.accountMoney;
                        }
                    });

                    //新增转让单
                    vm.save = function () {

                        var currentRow = vm.getCurrentRow();
                        if (currentRow == null) {

                            mcsDialogService.error({ title: '警告', message: '请选择要转让的账户' });
                            return;
                        }
                        if (vm.apply.transferMoney == 'undefined') {

                            mcsDialogService.error({ title: '警告', message: '请输入合法的转让金额' });
                            return;
                        }
                        if (vm.apply.transferMoney <= 0) {

                            mcsDialogService.error({ title: '警告', message: '转让金额金额必须大于零' });
                            return;
                        }
                        if (vm.apply.transferMoney > currentRow.accountMoney) {

                            mcsDialogService.error({ title: '警告', message: '转让金额不能大于可转金额' });
                            return;
                        }
                        mcsDialogService.confirm({ title: '确认', message: '是否确认提交审批？' })
                           .result.then(function () {
                               accountDataService.saveTransferApply(vm.apply, function () {
                                   $state.go(vm.page);
                               });
                           });
                    }
                }]);
        });