define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountTransferDataService],
        function (account) {
            account.registerController('accountTransferListController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountTransferDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.customerID = $stateParams.id;

                    vm.data = {
                        keyFields: ['applyID'],
                        headers: [{
                            field: "applyTime",
                            name: "转让日期",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.applyTime | date:"yyyy-MM-dd" | normalize}}</span>'
                        }, {
                            field: "transferType",
                            name: "转让类型",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.transferType | accountTransferType}}</span>'
                        }, {
                            field: "transferMoney",
                            name: "转让金额",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.transferMoney | currency:"￥"}}</span>'
                        }, {
                            field: "applyStatus",
                            name: "状态",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.applyStatus | applyStatus}}</span>'
                        }, {
                            field: "applierName",
                            name: "操作人",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.applierName}}</span>'
                        }, {
                            field: "applierJobName",
                            name: "操作人岗位",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.applierJobName}}</span>'
                        }, {
                            field: "bizCustomerName",
                            name: "对方学员",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.bizCustomerName}}</span>'
                        }, {
                            field: "bizCampusName",
                            name: "对方校区",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.bizCampusName}}</span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'applyID', sortDirection: 1 }]
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getTransferApplyList(vm.customerID, function (result) {
                            vm.customer = result.customer;
                            vm.data.rows = result.items;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                }]);
        });
