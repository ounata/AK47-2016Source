define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountDisplayDataService],
        function (account) {
            account.registerController('accountController', [
                '$scope', '$state', '$stateParams', 'accountDisplayDataService',
                function ($scope, $state, $stateParams, accountDisplayDataService) {
                    var vm = this;
                    vm.customerID = $stateParams.customerID;
                    
                    vm.data = {
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
                            field: "accountValue",
                            name: "账户价值",
                            template: '<span>{{row.accountValue|currency:"￥"}}</span>'
                        }, {
                            field: "assetMoney",
                            name: "订购余额",
                            template: '<span>{{row.assetMoney|currency:"￥"}}</span>'
                        }, {
                            field: "accountMoney",
                            name: "可用金额",
                            template: '<span>{{row.accountMoney|currency:"￥"}}</span>'
                        }],
                        pager: {
                            pagable:false
                        },
                        orderBy: [{ dataField: 'accountCode', sortDirection: 1 }]
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDisplayDataService.getAccountQueryResult(vm.customerID, function (result) {
                            vm.customer = result.customer;
                            vm.data.rows = result.items;                        
                        });
                    };
                    vm.init();
        }]);
});

