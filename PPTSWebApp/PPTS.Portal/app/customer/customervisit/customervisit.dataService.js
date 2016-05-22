/*
    名    称: customerVisitDataService
    功能概要: 客户回访
    作    者: 李辉
    创建时间: 2016年5月10日 14:45:06
    修正履历：
    修正时间:
*/
define(['angular', ppts.config.modules.customer], function (ng, customerVisit) {

    //客户反馈后端交互
    customerVisit.registerFactory('customerVisitDataService', ['$resource', '$stateParams', function ($resource, $stateParams) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customervisits/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        //默认查询
        resource.getAllCustomerVisits = function (criteria, success, error) {
            resource.post({ operation: 'getAllCustomerVisits' }, criteria, success, error);
        }

        //分页查询
        resource.getPagedCustomerVisits = function (criteria, success, error) {
            resource.post({ operation: 'getPagedCustomerVisits' }, criteria, success, error);
        }

        resource.getCustomerVisitForCreate = function (success, error) {
            resource.query({ operation: 'createCustomerVisit' }, success, error);
        }

        //传入学生ID
        resource.getCustomerVisitForCreateByStudentID = function (studentID,success, error) {
            resource.query({ operation: 'createCustomerVisitByStudnetID', studentID: studentID }, success, error);
        }

        resource.createCustomerVisit = function (model, success, error) {
            model.customerVisit.customerID = $stateParams.id;
            resource.save({ operation: 'createCustomerVisit' }, model, success, error);
        };

        resource.getCustomerVisitInfo = function (id, success, error) {
            resource.query({ operation: 'getCustomerVisitInfo', id: id }, success, error);
        }

        resource.editCustomerVisit = function (model, success, error) {
            resource.save({ operation: 'updateCustomerVisit' }, model, success, error);
        };

        resource.addVisitBatch = function (model, success, error) {
            resource.save({ operation: 'addVisitBatch' }, model, success, error);
        }

        return resource;
    }]);


    customerVisit.registerValue('customerVisitListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['VisitID'],
        headers: [
        {
            field: "customerName",
            name: "学员姓名"
        }, {
            field: "customerCode",
            name: "学员编号"
        }, {
            field: "parentName",
            name: "家长姓名",
            sortable: true
        },  {
            field: "visitTime",
            name: "回访时间",
            template: '<span>{{row.visitTime | date:"yyyy-MM-dd"}}</span>'
        }, {
            field: "visitWay",
            name: "回访方式",
            template: '<span>{{row.visitWay | visitWay}}</span>'
        },  {
            field: "visitType",
            name: "回访类型",
            template: '<span>{{row.visitType | visitType}}</span>'
        }, {
            field: "visitorName",
            name: "回访人"
        }, {
            field: "satisficing",
            name: "家长满意度",
            template: '<span>{{row.satisficing | satisficing}}</span>'
        },
        {
            field: "visitContent",
            name: "回访内容",
            template: '<span>{{row.visitContent }}</span>'
        }
        ],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'c.CreateTime', sortDirection: 1 }]
    });

    customerVisit.registerValue('customerVisitListDataSingleHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['VisitID'],
        headers: [
        {
            field: "visitTime",
            name: "回访时间",
            template: '<a ui-sref="ppts.student-view.visit-info({visitId:row.visitID,prev:\'ppts.student\'})">{{row.visitTime | date:"yyyy-MM-dd"}}</a>'
        }, {
            field: "visitWay",
            name: "回访方式",
            template: '<span>{{row.visitWay | visitWay}}</span>'
        }, {
            field: "visitType",
            name: "回访类型",
            template: '<span>{{row.visitType | visitType}}</span>'
        }, {
            field: "visitorName",
            name: "回访人"
        }, {
            field: "satisficing",
            name: "家长满意度",
            template: '<span>{{row.satisficing | satisficing}}</span>'
        },
        {
            field: "visitContent",
            name: "回访内容",
            template: '<span>{{row.visitContent }}</span>'
        }
        ],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'c.CreateTime', sortDirection: 1 }]
    });

    customerVisit.registerValue('visitAddBatchDataHeader', {
        keyFields: ['customerID'],
        headers: [{
            field: "customerName",
            name: "学生姓名",
            template: '<span>{{ row.customerName }}</span>',
        }, {
            field: "customerCode",
            name: "学员编号",
            template: '<span>{{ row.customerCode }}</span>'
        }, {
            field: 'visitWay',
            name: '回访方式',
            template: '<ppts-select category="visitWay" model="row.visitWay" async="false" style="width:90px;"/>',
        }, {
            field: 'visitType',
            name: '回访类型',
            template: '<ppts-select category="visitType" model="row.visitType" async="false" style="width:90px;"/>',
        }, {
            field: 'visitTime',
            name: '回访时间',
            template: '<ppts-datetimepicker model="row.visitTime" style="width:90px;" />'
        }, {
            field: 'satisficing',
            name: '家长满意度',
            template: '<ppts-select category="satisficing" model="row.satisficing" async="false" style="width:90px;"/>',
        }, {
            field: 'visitContent',
            name: '回访内容',
            template: '<textarea class="col-sm-8" id="textArea" ng-model="row.visitContent" css="mcs-padding-0" style="width:200px;"></textarea>',
        }, {
            field: 'nextVisitTime',
            name: '预计下次回访时间',
            template: '<ppts-datetimepicker model="row.nextVisitTime" style="width:90px;" />'
        }, {
            field: 'remainTime',
            name: '设置提醒时间',
            template: ''
        }, {
            field: 'remainType',
            name: '提醒类型',
            template: ''
        }],
        rows: [],
        pager: {
            pagable: false
        }
    });


    customerVisit.registerFactory('customerVisitDataViewService', ['$state', '$stateParams', 'customerVisitDataService', 'dataSyncService', 'customerVisitListDataHeader', 'customerVisitListDataSingleHeader',
     function ($state,$stateParams, customerVisitDataService, dataSyncService, customerVisitListDataHeader, customerVisitListDataSingleHeader) {
         var service = this;

         // 配置客户回访列表表头
         service.configCustomerVisitListHeaders = function (vm) {
             vm.data = customerVisitListDataHeader;

             vm.data.pager.pageChange = function () {
                 dataSyncService.initCriteria(vm);
                 customerVisitDataService.getPagedCustomerVisits(vm.criteria, function (result) {
                     vm.data.rows = result.pagedData;
                 });
             }
         };

         // 初始化客户回访列表
         service.initCustomerVisitList = function (vm, callback) {
             dataSyncService.initCriteria(vm);
             if (vm.timeType != '2')
             {
                 vm.criteria.visitTimeStart = vm.timeStart;
                 vm.criteria.visitTimeEnd = vm.timeEnd;
             }
             if (vm.timeType == '2') {
                 vm.criteria.nextVisitTimeStart = vm.timeStart;
                 vm.criteria.nextVisitTimeEnd = vm.timeEnd;
             }

             customerVisitDataService.getAllCustomerVisits(vm.criteria, function (result) {
                 vm.data.rows = result.queryResult.pagedData;
                 dataSyncService.updateTotalCount(vm, result.queryResult);
                 if (ng.isFunction(callback)) {
                     callback();
                 }
             });
         };

         // 配置客户回访列表表头(单个学生)
         service.configCustomerVisitListSingleHeaders = function (vm) {
             vm.data = customerVisitListDataSingleHeader;
             vm.data.pager.pageChange = function () {
                 dataSyncService.initCriteria(vm);
                 customerVisitDataService.getPagedCustomerVisits(vm.criteria, function (result) {
                     vm.data.rows = result.pagedData;
                 });
             }
         };

         // 初始化客户回访列表
         service.initCustomerVisitSingleList = function (vm, callback) {
             dataSyncService.initCriteria(vm);
             vm.criteria.customerID = $stateParams.id;
             customerVisitDataService.getAllCustomerVisits(vm.criteria, function (result) {
                 vm.data.rows = result.queryResult.pagedData;
                 dataSyncService.updateTotalCount(vm, result.queryResult);
                 if (ng.isFunction(callback)) {
                     callback();
                 }
             });
         };

         // 初始化新增回访信息
         service.initCreateCustomerVisitInfo = function (vm, callback) {
             customerVisitDataService.getCustomerVisitForCreate(function (result) {
                 vm.currName = result.currentName;
                 if (ng.isFunction(callback)) {
                     callback();
                 }
             });
         };

         

         service.initCreateCustomerVisitInfoByStudentID = function (vm, callback) {
             customerVisitDataService.getCustomerVisitForCreateByStudentID(vm.id, function (result) {
                 vm.currName = result.currentName;
                 vm.customer = result.customer;
                 if (ng.isFunction(callback)) {
                     callback();
                 }
             });
         };


         // 批量录入回访表头
         service.configVisitAddBatchHeaders = function (vm, header) {
             vm.data = header;
         };

         return service;
     }]);
});