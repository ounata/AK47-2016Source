define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService],
        function (customer) {
            customer.registerController('followListController', [
                '$scope', '$state', 'dataSyncService', 'followDataService',
                function ($scope, $state, dataSyncService, followDataService) {
                    var vm = this;

                    vm.data = {
                        selection: 'checkbox',
                        headers: [{
                            field: "customerName",
                            name: "学员姓名",
                            //template: '<a ui-sref="ppts.customer-view({id:row.customerID,page:\'info\'})">{{row.customerID}}</a>',
                            sortable: true
                        }, {
                            field: "customerCode",
                            name: "学员编号"
                        }, {
                            field: "parentName",
                            name: "家长姓名"
                        }, {
                            field: "followTime",
                            name: "跟进时间",
                            template: '<a ui-sref="ppts.follow-view({id:row.followID,page:\'info\'})"><span>{{row.followTime | date:"yyyy-MM-dd"}}</span></a>',
                        }, {
                            field: "followType",
                            name: "跟进方式",
                            template: '<span>{{row.followType | followType}}</span>'
                        }, {
                            field: "followObject",
                            name: "跟进对象"
                        }, {
                            field: "planVerifyTime",
                            name: "预计上门时间",
                            template: '<span>{{row.planVerifyTime | date:"yyyy-MM-dd"}}</span>'
                        }, {
                            field: "followStage",
                            name: "跟进后阶段"
                        }, {
                            field: "purchaseIntension",
                            name: "购买意愿"
                        }, {
                            field: "customerLevel",
                            name: "客户级别"
                        }, {
                            field: "purchaseIntension",
                            name: "跟进情况备注",
                            template: '<span></span>'
                        }, {
                            name: "记录人（所在部门）",
                            template: '<span>{{row.FollowName}}({{row.FollowerJobName}})</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 10,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                followDataService.getPagedFollows(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        dataSyncService.initCriteria(vm);
                        followDataService.getAllFollows(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.injectDictData();
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();
                }]);
        });