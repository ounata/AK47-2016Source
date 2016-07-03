define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountDisplayDataService],
        function (account) {
            account.registerController('accountController', [
                '$scope', '$state', '$location', '$stateParams', 'accountDisplayDataService',
                function ($scope, $state, $location, $stateParams, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;

                    if ($stateParams.id)
                        vm.customerID = $stateParams.id;
                    else
                        vm.customerID = $stateParams.customerID

                    vm.data = {
                        keyFields: ['accountID'],
                        headers: [{
                            field: "accountCode",
                            name: "账户编码",
                            template: '<span>{{row.accountCode}}</span>'
                        }, {
                            field: "accountType",
                            name: "账户类型",
                            template: '<span>{{row.accountType | accountType}}</span>'
                        }, {
                            field: "discountBase",
                            name: "折扣基数",
                            template: '<span>{{row.discountBase|currency:"￥"}}</span>'
                        }, {
                            field: "discountRate",
                            name: "折扣率",
                            template: '<span>{{row.discountRate|number:2}}</span>'
                        }, {
                            field: "accountValue",
                            name: "账户价值",
                            template: '<span>{{row.accountValue|currency:"￥"}}</span>'
                        }, {
                            field: "accountMoney",
                            name: "可用金额",
                            template: '<span>{{row.accountMoney|currency:"￥"}}</span>'
                        }, {
                            field: "assetMoney",
                            name: "订购资金余额",
                            template: '<span>{{row.assetMoney|currency:"￥"}}</span>'
                        }, {
                            field: "accountStatus",
                            name: "状态",
                            template: '<span>{{row.accountStatus | accountStatus}}</span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'accountCode', sortDirection: 1 }]
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getAccountList(vm.customerID, function (result) {
                            vm.customer = result.customer;
                            vm.data.rows = result.items;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();
                }]);
        });

