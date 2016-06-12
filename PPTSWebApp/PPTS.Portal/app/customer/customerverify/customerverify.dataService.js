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
            template: '<span>{{row.customerCode}}</span>',
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
            template: '<span>{{row.grade | gradeType }}</span>',
        }, {
            field: "followObject",
            name: "邀约人",
            template: '<span>{{row.creatorName}}</span>',
        }, {
            field: "planTime",
            name: "预计上门时间",
            template: '<span>{{ row.planTime | date:"yyyy-MM-dd" }}</span>'
        }, {
            field: "createTime",
            name: "实际上门时间",
            template: '<span>{{ row.createTime | date:"yyyy-MM-dd" }}</span>'
        }, {
            field: "customerLevel",
            name: "上门人数",
            template: '<span>{{row.verifyPeoples | verifyPeople}}</span>'
        }, {
            field: "intensionSubjects",
            name: "上门人员关系",
            template: '<span>{{row.verifyRelations | verifyRelation}}</span>'
        }, {
            field: "staffName",
            name: "咨询师",
            template: '<span>{{row.staffName}}</span>'
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'a.CreateTime', sortDirection: 1 }]
    });

    customer.registerValue('customerVerifyAdvanceSearchItems', [
    { name: '建档人：', template: '<ppts-checkbox-group category="people" model="vm.criteria.creatorJobs" clear="vm.criteria.creatorJobs=[]" async="false" /><mcs-input placeholder="建档人姓名" model="vm.criteria.creatorName" />' },
    { name: '实际上门时间：', template: '<ppts-radiobutton-group category="dateRange" model="vm.createTimeValue" async="false" css="mcs-padding-left-10" /><span ng-show="vm.createTimeValue == 5"><ppts-daterangepicker class="search" start-date="vm.criteria.createStartTime" end-date="vm.criteria.createEndTime"></ppts-daterangepicker></span>' },
    { name: '邀约情况：', template: '<ppts-radiobutton-group show-all="true" category="ifElse" model="vm.isInvitedValue" async="false" css="mcs-padding-left-10" />' },
    { name: '预计上门时间：', template: '<ppts-radiobutton-group category="dateRange" model="vm.followTimeValue" async="false" css="mcs-padding-left-10" /><span ng-show="vm.followTimeValue == 5"><ppts-daterangepicker class="search" start-date="vm.criteria.planVerifyStartTime" end-date="vm.criteria.planVerifyEndTime"></ppts-daterangepicker></span>', hide: 'vm.isInvitedValue != 1' },
    { name: '建档时间：', template: '<ppts-radiobutton-group category="dateRange" model="vm.customerCreateTimeValue" async="false" css="mcs-padding-left-10" /><span ng-show="vm.customerCreateTimeValue == 5"><ppts-daterangepicker class="search" start-date="vm.criteria.customerCreateStartTime" end-date="vm.criteria.customerCreateEndTime"></ppts-daterangepicker></span>' },
    ]);

    customer.registerValue('customerVerifyInstutionItem',
      { template: '<td><ppts-select category="subject" style="width:120px" async="false" /></td><td><input class="mcs-readonly-input" ng-model="vm.customer.customerName" style="margin: 0 0 0 15px" placeholder="请填写机构名称"/></td><td><ppts-daterangepicker start-date="" end-date="" size="sm"/></td><td><button ng-click="" class="btn btn-link"><i class="ace-icon fa fa-plus bigger-110"></i></button></td>' });

    customer.registerFactory('customerVerifyDataViewService', ['customerVerifyDataService', 'dataSyncService', 'customerVerifyListDataHeader',
    function (customerVerifyDataService, dataSyncService, customerVerifyListDataHeader) {
        var service = this;

        // 配置上门列表表头
        service.customerVerifyListDataHeader = function (vm) {
            vm.data = customerVerifyListDataHeader;

            vm.data.pager.pageChange = function () {
                dataSyncService.initCriteria(vm);
                customerVerifyDataService.getPagedCustomerVerifies(vm.criteria, function (result) {
                    vm.data.rows = result.pagedData;
                });
            }
        };

        // 初始化上门列表数据
        service.initCustomerVerifyList = function (vm, callback) {
            dataSyncService.initCriteria(vm);
            if (vm.createTimeValue != 5) {
                var createDateRange = dataSyncService.selectPageDict('dateRange', vm.createTimeValue);
                vm.criteria.createStartTime = createDateRange.start;
                vm.criteria.createEndTime = createDateRange.end;
            }
            if (vm.followTimeValue != 5) {
                var followDateRange = dataSyncService.selectPageDict('dateRange', vm.followTimeValue);
                vm.criteria.planVerifyStartTime = followDateRange.start;
                vm.criteria.planVerifyEndTime = followDateRange.end;
            }
            if (vm.customerCreateTimeValue != 5) {
                var customerDateRange = dataSyncService.selectPageDict('dateRange', vm.customerCreateTimeValue);
                vm.criteria.customerCreateStartTime = customerDateRange.start;
                vm.criteria.customerCreateEndTime = customerDateRange.end;
            }
            dataSyncService.injectPageDict(['dateRange', 'people', 'ifElse']);
            customerVerifyDataService.getAllCustomerVerifies(vm.criteria, function (result) {
                vm.data.rows = result.queryResult.pagedData;
                dataSyncService.updateTotalCount(vm, result.queryResult);
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        // 初始化学员列表数据
        service.initCustomerCustomerVerifyList = function (vm, callback) {
            dataSyncService.initCriteria(vm);
            customerVerifyDataService.getQueryStudentInfos(vm.criteria, function (result) {
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