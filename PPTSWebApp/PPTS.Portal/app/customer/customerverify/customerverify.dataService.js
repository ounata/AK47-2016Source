define(['angular', ppts.config.modules.customer], function (ng, customer) {

    customer.registerFactory('customerVerifyDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customerverifies/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false, params: { customerId: '@customerId' }, }
            });

        resource.getAllCustomerVerifies = function (criteria, success, error) {
            resource.post({ operation: 'getAllcustomerVerifies' }, criteria, success, error);
        }

        resource.getPagedCustomerVerifies = function (criteria, success, error) {
            resource.post({ operation: 'getPagedcustomerVerifies' }, criteria, success, error);
        }

        resource.getQueryStudentInfos = function (criteria, success, error) {
            resource.post({ operation: 'getQueryStudentInfos' }, criteria, success, error);
        }

        resource.getPagedStudentInfos = function (criteria, success, error) {
            resource.post({ operation: 'getPagedStudentInfos' }, criteria, success, error);
        }

        resource.getCustomerVerifyForCreate = function (success, error) {
            resource.query({ operation: 'createCustomerVerify' }, success, error);
        }

        resource.createCustomerVerify = function (model, success, error) {
            resource.save({ operation: 'saveCustomerVerify' }, model, success, error);
        };

        resource.getCustomerVerifyForView = function (followId, isPotential, success, error) {
            resource.query({ followId: followId, isPotential: isPotential }, { operation: 'viewCustomerVerify' }, success, error);
        }

        return resource;
    }]);

    customer.registerValue('customerVerifyListDataHeader', {
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.customerverify\'})">{{row.customerName}}</a>',
        }, {
            field: "customerCode",
            name: "学员编号",
            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.customerverify\'})">{{row.customerCode}}</a>',
        }, {
            field: "parentName",
            name: "家长姓名",
            template: '<span>{{row.parentName}}</span>',
        }, {
            field: "followTime",
            name: "学员所在学校",
            template: '<span>{{row.campusName}}</span>'
        }, {
            field: "grade",
            name: "学员当前年级",
            template: '<span>{{row.grade | grade }}</span>',
        }, {
            field: "followObject",
            name: "邀约人",
            template: '<span>{{row.creatorName}}</span>',
        }, {
            field: "planVerifyTime",
            name: "预计上门时间",
            template: '<span>{{ row.planVerifyTime | date:"yyyy-MM-dd" | normalize }}</span>'
        }, {
            field: "createTime",
            name: "实际上门时间",
            template: '<span>{{ row.createTime | date:"yyyy-MM-dd" | normalize }}</span>'
        }, {
            field: "customerLevel",
            name: "上门人数",
            template: '<span>{{row.verifyPeoples | verifyPeople}}</span>'
        }, {
            field: "intensionSubjects",
            name: "上门人员关系",
            template: '<span>{{row.verifyRelations | verifyRelation}}</span>'
        }, {
            field: "creatorName",
            name: "咨询师",
            template: '<span>{{row.creatorName}}</span>'
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'a.CreateTime', sortDirection: 1 }]
    });

    customer.registerValue('customerVerifyAdvanceSearchItems', [
        { name: '建档关系：', template: '<ppts-checkbox-group category="creation" model="vm.criteria.creatorJobTypes" show-all="true" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="建档人姓名" model="vm.criteria.creatorName" custom-style="width:28%"/>' },
        { name: '实际上门时间：', template: '<ppts-radiobutton-group category="dateRange" model="vm.createTimeValue" show-all="true" async="false" css="mcs-padding-left-10" /><span ng-show="vm.createTimeValue == 5"><mcs-daterangepicker class="search" start-date="vm.criteria.createStartTime" end-date="vm.criteria.createEndTime"></mcs-daterangepicker></span>' },
        { name: '邀约情况：', template: '<ppts-radiobutton-group show-all="true" category="ifElse" model="vm.criteria.isInvited" async="false" css="mcs-padding-left-10" />' },
        { name: '预计上门时间：', template: '<ppts-radiobutton-group category="dateRange" model="vm.followTimeValue" show-all="true" async="false" css="mcs-padding-left-10" /><span ng-show="vm.followTimeValue == 5"><mcs-daterangepicker class="search" start-date="vm.criteria.planVerifyStartTime" end-date="vm.criteria.planVerifyEndTime"></mcs-daterangepicker></span>', hide: 'vm.criteria.isInvited != 1' },
        { name: '建档时间：', template: '<mcs-daterangepicker start-date="vm.criteria.createTimeStart" end-date="vm.criteria.createTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
    ]);

    customer.registerValue('customerVerifyInstutionItem',
        { template: '<td><mcs-select category="subject" style="width:120px" async="false" /></td><td><input class="mcs-readonly-input" ng-model="vm.customer.customerName" style="margin: 0 0 0 15px" placeholder="请填写机构名称"/></td><td><mcs-daterangepicker start-date="" end-date="" size="sm"/></td><td><button ng-click="" class="btn btn-link"><i class="ace-icon fa fa-plus bigger-110"></i></button></td>' });

    customer.registerFactory('customerVerifyDataViewService', ['customerVerifyDataService', 'dataSyncService', 'customerVerifyListDataHeader',
    function (customerVerifyDataService, dataSyncService, customerVerifyListDataHeader) {
        var service = this;

        // 初始化新增上门记录信息
        service.initCreateCustomerVerifyInfo = function (state, vm, callback) {
            customerVerifyDataService.getCustomerVerifyForCreate(function (result) {
                vm.customerVerify = result;
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        return service;
    }]);
});