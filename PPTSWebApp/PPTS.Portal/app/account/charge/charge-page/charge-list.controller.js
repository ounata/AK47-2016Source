define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeListController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.customerID = $stateParams.id;

                    vm.data = {
                        keyFields: ['applyID'],
                        headers: [{
                            field: "applyNo",                            
                            name: "缴费单编号",
                            headerCss: "col-sm-1",
                            template: '<a ui-sref="ppts.accountCharge-view.info({applyID:row.applyID,prev:vm.page})">{{row.applyNo}}</a>'
                        }, {
                            field: "chargeType",
                            name: "充值类型",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.chargeType | chargeType}}</span>'
                        }, {
                            field: "applyTime",
                            name: "充值日期",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.payTime | date:"yyyy-MM-dd" | normalize}}</span>'
                        }, {
                            field: "payStatus",
                            name: "充值状态",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.payStatus | payStatus}}</span>'
                        }, {
                            field: "chargeMoney",
                            name: "本次充值金额",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.chargeMoney | currency:"￥"}}</span>'
                        }, {
                            field: "thisDiscountBase",
                            name: "本次折扣基数",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.thisDiscountBase | currency:"￥"}}</span>'
                        }, {
                            field: "thisDiscountRate",
                            name: "本次折扣率",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.thisDiscountRate | number:2}}</span>'
                        }, {
                            field: "auditStatus",
                            name: "审核状态",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.auditStatus | chargeAuditStatus}}</span>'
                        }, {
                            field: "applierName",
                            name: "申请人",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.applierName}}</span>'
                        }, {
                            field: "applierJobName",
                            name: "申请人岗位",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.applierJobName}}</span>'
                        }, {
                            field: "campusName",
                            name: "校区",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.campusName}}</span>'
                        }],
                        pager: {
                            pagable: false
                        }
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getChargeApplyList(vm.customerID, function (result) {
                            vm.customer = result.customer;
                            vm.data.rows = result.items;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                }]);
        });