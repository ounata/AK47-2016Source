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

        resource.getFollowForCreate = function (customerId, isPotential, success, error) {
            resource.query({ customerId: customerId, isPotential: isPotential }, { operation: 'createFollow' }, success, error);
        }

        resource.getFollowForView = function (followId, success, error) {
            resource.query({ followId: followId }, { operation: 'viewFollow' }, success, error);
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
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<a ng-show="row.isStudent" ui-sref="ppts.student-view.follows({id:row.customerID,prev:\'ppts.follow\'})">{{row.customerName}}</a>' +
                      '<a ng-show="!row.isStudent" ui-sref="ppts.customer-view.follows({id:row.customerID,prev:\'ppts.follow\'})">{{row.customerName}}</a>',
        }, {
            field: "customerCode",
            name: "学员编号",
            template: '<a ng-show="row.isStudent" ui-sref="ppts.student-view.follows({id:row.customerID,prev:\'ppts.follow\'})">{{row.customerCode}}</a>' +
                      '<a ng-show="!row.isStudent" ui-sref="ppts.customer-view.follows({id:row.customerID,prev:\'ppts.follow\'})">{{row.customerCode}}</a>',
        }, {
            field: "parentName",
            name: "家长姓名",
            template: '<span>{{row.parentName}}</span>',
        }, {
            field: "followTime",
            name: "跟进时间",
            template: '<a ui-sref="ppts.follow-view({followId:row.followID})">{{row.followTime | date:"yyyy-MM-dd"}}</a>'
        }, {
            field: "followType",
            name: "跟进方式",
            template: '<span>{{row.followType | followType}}</span>',
        }, {
            field: "followObject",
            name: "跟进对象",
            template: '<span>{{row.followObject | followObject}}</span>',
        }, {
            field: "planVerifyTime",
            name: "预计上门时间",
            template: '<span>{{row.planVerifyTime | date:"yyyy-MM-dd"}}</span>'
        }, {
            field: "followStage",
            name: "跟进后阶段",
            template: '<span>{{row.followStage | followStage}}</span>'
        }, {
            field: "purchaseIntention",
            name: "购买意愿",
            template: '<span>{{row.purchaseIntention | purchaseIntention }}</span>'
        }, {
            field: "customerLevel",
            name: "客户级别",
            template: '<span>{{row.customerLevel | customerLevel}}</span>'
        }, {
            field: "followMemo",
            name: "跟进情况备注",
            template: '<span uib-popover="{{row.followMemo | tooltip:10}}" popover-trigger="mouseenter">{{row.followMemo| truncate:10}}</span>'
        }, {
            field: "followerAndJobName",
            name: "记录人（所在部门）",
            template: '<span>{{row.followerAndJobName}}</span>'
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'a.CreateTime', sortDirection: 1 }]
    });

    customer.registerValue('showfollowListDataHeader', {
        headers: [{
            field: "followTime",
            name: "跟进时间",
            template: '<a ui-sref="ppts.follow-view({followId:row.followID})">{{row.followTime | date:"yyyy-MM-dd"}}</a>'
        }, {
            field: "followType",
            name: "跟进方式",
            template: '<span>{{row.followType | followType}}</span>',
        }, {
            field: "followObject",
            name: "跟进对象",
            template: '<span>{{row.followObject | followObject}}</span>',
        }, {
            field: "planVerifyTime",
            name: "预计上门时间",
            template: '<span>{{row.planVerifyTime | date:"yyyy-MM-dd"}}</span>'
        }, {
            field: "followStage",
            name: "跟进后阶段",
            template: '<span>{{row.followStage | followStage}}</span>'
        }, {
            field: "purchaseIntention",
            name: "购买意愿",
            template: '<span>{{row.purchaseIntention | purchaseIntention }}</span>'
        }, {
            field: "customerLevel",
            name: "客户级别",
            template: '<span>{{row.customerLevel | customerLevel}}</span>'
        }, {
            field: "followMemo",
            name: "跟进情况备注",
            template: '<span uib-popover="{{row.followMemo|tooltip:10}}" popover-trigger="mouseenter">{{row.followMemo| truncate:10}}</span>'
        }, {
            field: "followerAndJobName",
            name: "记录人（所在部门）",
            template: '<span>{{row.followerAndJobName}}</span>'
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'a.CreateTime', sortDirection: 1 }]
    });


    customer.registerValue('followAdvanceSearchItems', [
        { name: '记录人：', template: '<ppts-checkbox-group category="people" model="vm.criteria.followerJobIDs" async="false" /><mcs-input placeholder="记录人姓名" model="vm.criteria.followerName" />' },
        { name: '建档关系：', template: '<ppts-checkbox-group category="creation" model="vm.criteria.creatorJobTypes" show-all="true" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="建档人姓名" model="vm.criteria.creatorName" custom-style="width:28%"/>' },
        { name: '建档时间：', template: '<mcs-daterangepicker start-date="vm.criteria.createTimeStart" end-date="vm.criteria.createTimeEnd" css="mcs-margin-left-10"/>' },
        { name: '跟进阶段：', template: '<ppts-checkbox-group category="followStage" model="vm.criteria.followStages" async="false"/>' },
        { name: '购买意愿：', template: '<ppts-checkbox-group category="purchaseIntention" model="vm.criteria.purchaseIntentions" clear="vm.criteria.purchaseIntentions=[]" async="false" />' },
        { name: '客户级别：', template: '<ppts-checkbox-group category="customerLevel" model="vm.criteria.customerLevels" clear="vm.criteria.customerLevels=[]" async="false" />' },
        { name: '跟进方式：', template: '<ppts-checkbox-group category="followType" model="vm.criteria.followTypes" clear="vm.criteria.followTypes=[]" async="false" />' },
        { name: '跟进时间：', template: '<ppts-radiobutton-group category="dateRange" model="vm.followTimeValue" show-all="true" async="false" css="mcs-padding-left-10" /><span ng-show="vm.followTimeValue == 5"><mcs-daterangepicker start-date="vm.criteria.followStartTime" end-date="vm.criteria.followEndTime"></mcs-daterangepicker></span>' },
        { name: '下次沟通时间：', template: '<ppts-radiobutton-group category="dateRange" model="vm.nextFollowTimeValue" show-all="true" async="false" css="mcs-padding-left-10" /><span ng-show="vm.nextFollowTimeValue == 5"><mcs-daterangepicker start-date="vm.criteria.nextTalkStartTime" end-date="vm.criteria.nextTalkEndTime"></mcs-daterangepicker></span>' },
        { name: '在其它机构辅导：', template: '<ppts-radiobutton-group category="ifElse" show-all="true" model="vm.criteria.isStudyThere" async="false" css="mcs-padding-left-10" />' },
        { name: '预计上门时间：', template: '<ppts-radiobutton-group category="dateRange" model="vm.planVerifyTimeValue" show-all="true" async="false" css="mcs-padding-left-10" /><span ng-show="vm.planVerifyTimeValue == 5"><mcs-daterangepicker start-date="vm.criteria.planVerifyStartTime" end-date="vm.criteria.planVerifyEndTime"></mcs-daterangepicker></span>' },
        { name: '预计签约时间：', template: '<ppts-radiobutton-group category="dateRange" model="vm.planSignDateValue" show-all="true" async="false" css="mcs-padding-left-10" /><span ng-show="vm.planSignDateValue == 5"><mcs-daterangepicker start-date="vm.criteria.planSignStartTime" end-date="vm.criteria.planSignEndTime"></mcs-daterangepicker></span>' },
        { name: '本部门：', template: '<ppts-checkbox-group category="dept" model="vm.criteria.queryDepts" width="150px" async="false" css="mcs-padding-left-10"/>' },
    ]);

    customer.registerValue('followInstutionItem',
      { template: '<td><mcs-select category="subject" style="width:120px" async="false" /></td><td><input class="mcs-readonly-input" ng-model="vm.customer.customerName" style="margin: 0 0 0 15px" placeholder="请填写机构名称"/></td><td><mcs-daterangepicker start-date="" end-date="" size="sm"/></td><td><button ng-click="" class="btn btn-link"><i class="ace-icon fa fa-plus bigger-110"></i></button></td>' });

    customer.registerFactory('followDataViewService', ['followDataService', 'dataSyncService',
        function (followDataService, dataSyncService) {
        var service = this;

        // 初始化日期范围
        //service.initDateRange = function ($scope, vm, watchExps) {
        //    if (!watchExps && !watchExps.length) return;
        //    for (var index in watchExps) {
        //        (function () {
        //            var temp = index, exp = watchExps[index];
        //            $scope.$watch(exp.watchExp, function () {
        //                var selectedValue = exp.selectedValue;
        //                var dateRange = dataSyncService.selectPageDict('dateRange', vm[selectedValue]);
        //                if (dateRange) {
        //                    vm.criteria[exp.start] = dateRange.start;
        //                    vm.criteria[exp.end] = dateRange.end;
        //                }
        //            });
        //        })();
        //    }
        //};

        // 初始化新增跟进记录信息
        service.initCreateFollowInfo = function (vm, result, callback) {
            vm.follow = result.follow;
            dataSyncService.injectDynamicDict('ifElse');
            vm.previousFollowStage = result.previousFollowStage;
            if (ng.isFunction(callback)) {
                callback();
            }
        };

        service.initFollowInfo = function (vm, id, callback) {
            followDataService.getFollowForView(id, function (result) {
                vm.follow = result.follow;
                vm.followItems = result.followItems;
                dataSyncService.injectDynamicDict('ifElse');
                vm.previousFollowStage = result.previousFollowStage;
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        return service;
    }]);
});