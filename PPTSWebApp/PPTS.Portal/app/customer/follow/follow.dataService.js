define(['angular', ppts.config.modules.customer], function (ng, customer) {

    customer.registerFactory('followDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customerfollows/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false, params: { customerId: '@customerId' }, }
            });

        resource.getAllFollows = function (criteria, success, error) {
            resource.post({ operation: 'getAllFollows' }, criteria, success, error);
        }

        resource.getPagedFollows = function (criteria, success, error) {
            resource.post({ operation: 'getPagedFollows' }, criteria, success, error);
        }

        resource.getFollowForCreate = function (customerId, success, error) {
            resource.query({ customerId: customerId }, { operation: 'createFollow' }, success, error);
        }

        resource.getFollowForUpdate = function (id, success, error) {
            resource.query({ operation: 'updateFollow', id: id }, success, error);
        }

        resource.createFollow = function (model, success, error) {
            resource.save({ operation: 'createFollow' }, model, success, error);
        };

        resource.updateFollow = function (model, success, error) {
            resource.save({ operation: 'updateFollow' }, model, success, error);
        };

        return resource;
    }]);

    customer.registerValue('followListDataHeader', {
        selection: 'checkbox',
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<a ui-sref="ppts.customer-view({id:row.customerID,page:\'info\'})">{{row.customerName}}</a>',
            // sortable: true
        }, {
            field: "customerCode",
            name: "学员编号",
            template: '<span>{{row.customerCode}}</span>',
        }, {
            field: "parentName",
            name: "家长姓名",
            template: '<span>{{row.parentName}}</span>',
        }, {
            field: "followTime",
            name: "跟进时间",
            template: '<a ui-sref="ppts.follow-view({id:row.followID,page:\'info\'})"><span>{{row.followTime | date:"yyyy-MM-dd"}}</span></a>',
        }, {
            field: "followType",
            name: "跟进方式",
            // template: '<span>{{row.followType | followType}}</span>',
        }, {
            field: "followObject",
            name: "跟进对象",
            //  template: '<span>{{row.followObject | followObject}}</span>',
        }, {
            field: "planVerifyTime",
            name: "预计上门时间",
            template: '<span>{{row.planVerifyTime | date:"yyyy-MM-dd"}}</span>'
        }, {
            field: "followStage",
            name: "跟进后阶段",
            template: '<span>{{row.followStage | followStage}}</span>'
        }, {
            field: "purchaseIntension",
            name: "购买意愿",
            template: '<span>{{row.purchaseIntension | purchaseIntension }}</span>'
        }, {
            field: "customerLevel",
            name: "客户级别",
            template: '<span>{{row.customerLevel | customerLevel}}</span>'
        }, {
            field: "intensionSubjects",
            name: "跟进情况备注",
            template: '<span>{{row.intensionSubjects}}</span>'
        }, {
            field: "followName",
            name: "记录人（所在部门）",
            template: '<span>{{row.FollowName}}({{row.FollowerName}})</span>'
        }],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'a.CreateTime', sortDirection: 1 }]
    });

    customer.registerValue('followInstutionItem',
      { template: '<td><ppts-select category="subject" style="width:120px" async="false" /></td><td><input class="mcs-readonly-input" ng-model="vm.customer.customerName" style="margin: 0 0 0 15px" placeholder="请填写机构名称"/></td><td><ppts-daterangepicker start-date="" end-date="" size="sm"/></td><td><button ng-click="" class="btn btn-link"><i class="ace-icon fa fa-plus bigger-110"></i></button></td>' });

    customer.registerFactory('followDataViewService', ['followDataService', 'dataSyncService', 'followListDataHeader',
    function (followDataService, dataSyncService, followListDataHeader) {
            var service = this;

            // 配置跟进列表表头
            service.configFollowListHeaders = function (vm) {
                vm.data = followListDataHeader;

                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    followDataService.getPagedFollows(vm.criteria, function (result) {
                        vm.data.rows = result.pagedData;
                    });
                }
            };

            // 初始化跟进列表数据
            service.initFollowList = function (vm, callback) {
                dataSyncService.initCriteria(vm);
                followDataService.getAllFollows(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.injectDictData();
                    dataSyncService.injectPageDict(['dateRange', 'people', 'ifElse']);
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };

            // 初始化日期范围
            service.initDateRange = function ($scope, vm, watchExps) {
                if (!watchExps && !watchExps.length) return;
                for (var index in watchExps) {
                    (function () {
                        var temp = index, exp = watchExps[index];
                        $scope.$watch(exp.watchExp, function () {
                            var selectedValue = exp.selectedValue;
                            var dateRange = dataSyncService.selectPageDict('dateRange', vm[selectedValue]);
                            if (dateRange) {
                                vm.criteria[exp.start] = dateRange.start;
                                vm.criteria[exp.end] = dateRange.end;
                            }
                        });
                    })();
                }
            };

            // 初始化新增跟进记录信息
            service.initCreateFollowInfo = function (customerId, vm, callback) {
                followDataService.getFollowForCreate(customerId, function (result) {
                    dataSyncService.injectPageDict(['ifElse']);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };

            service.addInstutionItem = function (vm) {
            };

            service.removeInstutionItem = function (vm) {
            };

            return service;
        }]);

});