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
            name: "学员姓名",
            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.student\'})">{{row.customerName}}</a>'
        }, {
            field: "customerCode",
            name: "学员编号"
        }, {
            field: "parentName",
            name: "家长姓名"
        },  {
            field: "c.visitTime",
            name: "回访时间",
            template: '<a ui-sref="ppts.student-view.visit-info({visitId:row.visitID,prev:\'ppts.student\'})">{{row.visitTime | date:"yyyy-MM-dd"}}</a>',
            sortable: true
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
            template: '<span uib-popover="{{row.visitContent|tooltip:30}}" popover-trigger="mouseenter">{{row.visitContent | truncate:30}}</span>'
        }
        ],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
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
            template: '<span uib-popover="{{row.visitContent|tooltip:30}}" popover-trigger="mouseenter">{{row.visitContent | truncate:30}}</span>'
        }
        ],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
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
            template: '<mcs-select category="visitWay" model="row.visitWay" async="false"  required/>'
        }, {
            field: 'visitType',
            name: '回访类型',
            template: '<mcs-select category="visitType" model="row.visitType" async="false"  required/>'
        }, {
            field: 'visitTime',
            name: '回访时间',
            template: '<mcs-datetimepicker model="row.visitTime" start-date="vm.startDate" end-date="vm.endDate" required/>'
        }, {
            field: 'satisficing',
            name: '家长满意度',
            template: '<mcs-select category="satisficing" model="row.satisficing" async="false"  required/>'
        }, {
            field: 'visitContent',
            name: '回访内容',
            template: '<textarea id="textArea" ng-model="row.visitContent" cols="20"  required></textarea>'
        }, {
            field: 'nextVisitTime',
            name: '预计下次回访时间',
            template: '<mcs-datetimepicker model="row.nextVisitTime" required/>'
        }, {
            field: 'remainTime',
            name: '设置提醒时间',
            template: '<mcs-datetimepicker model="row.remindTime" required />'
        }, {
            field: 'selectTypes',
            name: '提醒类型',
            template: '<ppts-radiobutton-group category="messageType" model="row.selectType" async="false" required/>'
        }],
        rows: [],
        pager: {
            pagable: false
        }
    });


    customerVisit.registerFactory('customerVisitDataViewService', ['$state', '$stateParams', 'customerVisitDataService', 'dataSyncService', 'customerVisitListDataHeader', 'customerVisitListDataSingleHeader',
     function ($state,$stateParams, customerVisitDataService, dataSyncService, customerVisitListDataHeader, customerVisitListDataSingleHeader) {
         var service = this;

         //初始化回访时间范围
         service.initDate = function (vm) {
             var syNow = new Date();
             var year = syNow.getFullYear();        //年
             var month = syNow.getMonth() + 1;     //月
             var day = syNow.getDate();            //天
             vm.startDate = new Date(new Date(year + "-" + month + "-" + day).getTime() - 3 * 24 * 60 * 60 * 1000);
             vm.endDate = new Date(new Date(year + "-" + month + "-" + day + " 23:59").getTime());

         }

         service.initDateEdit = function (vm) {
             var year = vm.customerVisit.visitTime.getFullYear();        //年
             var month = vm.customerVisit.visitTime.getMonth() + 1;     //月
             var day = vm.customerVisit.visitTime.getDate();            //天
             vm.startDate = new Date(new Date(year + "-" + month + "-" + day).getTime() - 3 * 24 * 60 * 60 * 1000);
             vm.endDate = new Date(new Date(year + "-" + month + "-" + day).getTime() + 4 * 24 * 60 * 60 * 1000);
         }

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
                 vm.data.searching();
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
                 vm.data.searching();
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