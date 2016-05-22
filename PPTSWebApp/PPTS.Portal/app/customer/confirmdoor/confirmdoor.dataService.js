define(['angular', ppts.config.modules.customer], function (ng, customer) {

    customer.registerFactory('confirmdoorDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/confirmdoors/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false, params: { customerId: '@customerId' }, }
            });

        resource.getAllConfirmDoors = function (criteria, success, error) {
            resource.post({ operation: 'getAllConfirmDoors' }, criteria, success, error);
        }

        resource.getPagedConfirmDoors = function (criteria, success, error) {
            resource.post({ operation: 'getPagedConfirmDoors' }, criteria, success, error);
        }

        resource.getQueryStudentInfos = function (criteria, success, error) {
            resource.post({ operation: 'getQueryStudentInfos' }, criteria, success, error);
        }

        resource.getPagedStudentInfos = function (criteria, success, error) {
            resource.post({ operation: 'getPagedStudentInfos' }, criteria, success, error);
        }

        resource.getConfirmDoorForCreate = function (success, error) {
            resource.query({ operation: 'createConfirmDoor' }, success, error);
        }

        resource.createConfirmDoor = function (model, success, error) {
            resource.save({ operation: 'createConfirmDoor' }, model, success, error);
        };

        resource.getConfirmDoorForView = function (followId, isPotential, success, error) {
            resource.query({ followId: followId, isPotential: isPotential }, { operation: 'viewConfirmDoor' }, success, error);
        }

        return resource;
    }]);

    customer.registerValue('confirmdoorListDataHeader', {
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<span>{{row.customerName}}</span>',
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
            //template: '<a ui-sref="ppts.follow-view({id:row.followID,page:\'info\'})"><span>{{row.followTime | date:"yyyy-MM-dd"}}</span></a>',
            template: '<span>{{row.followTime | date:"yyyy-MM-dd"}}</span>'
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
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'a.CreateTime', sortDirection: 1 }]
    });
    customer.registerValue('confirmdoorInstutionItem',
      { template: '<td><ppts-select category="subject" style="width:120px" async="false" /></td><td><input class="mcs-readonly-input" ng-model="vm.customer.customerName" style="margin: 0 0 0 15px" placeholder="请填写机构名称"/></td><td><ppts-daterangepicker start-date="" end-date="" size="sm"/></td><td><button ng-click="" class="btn btn-link"><i class="ace-icon fa fa-plus bigger-110"></i></button></td>' });

    customer.registerFactory('confirmdoorDataViewService', ['confirmdoorDataService', 'dataSyncService', 'confirmdoorListDataHeader',
    function (confirmdoorDataService, dataSyncService, confirmdoorListDataHeader) {
        var service = this;

        // 配置上门列表表头
        service.confirmdoorListDataHeader = function (vm) {
            vm.data = confirmdoorListDataHeader;

            vm.data.pager.pageChange = function () {
                dataSyncService.initCriteria(vm);
                confirmdoorDataService.getPagedConfirmDoors(vm.criteria, function (result) {
                    vm.data.rows = result.pagedData;
                });
            }
        };

        // 初始化上门列表数据
        service.initConfirmdoorList = function (vm, callback) {
            dataSyncService.initCriteria(vm);
            confirmdoorDataService.getAllConfirmDoors(vm.criteria, function (result) {
                vm.data.rows = result.queryResult.pagedData;
                dataSyncService.injectDictData();
                dataSyncService.injectPageDict(['dateRange', 'people', 'ifElse']);
                dataSyncService.updateTotalCount(vm, result.queryResult);
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        // 初始化学员列表数据
        service.initCustomerConfirmdoorList = function (vm, callback) {
            dataSyncService.initCriteria(vm);
            confirmdoorDataService.getQueryStudentInfos(vm.criteria, function (result) {
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
        service.initCreateConfirmdoorInfo = function (state, vm, callback) {
            confirmdoorDataService.getConfirmDoorForCreate(function (result) {
                vm.confirmdoor = result.confirmDoor;
                // dataSyncService.injectPageDict(['ifElse']);
                //  vm.previousFollowStage = result.previousFollowStage;
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        return service;
    }]);
});