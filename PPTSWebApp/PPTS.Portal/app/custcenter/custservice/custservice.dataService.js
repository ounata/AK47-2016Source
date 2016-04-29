define(['angular', ppts.config.modules.custcenter], function (ng, custcenter) {
    custcenter.registerFactory('custserviceDataService', ['$resource', function ($resource) {
        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customerservices/:operation/:id',
                { operation: '@operation', id: '@id' },
                {
                    'post': { method: 'POST' },
                    'query': { method: 'GET', isArray: false }
                });

        resource.getAllCustomerServices = function (criteria, success, error) {
            resource.post({ operation: 'getAllCustomerServices' }, criteria, success, error);
        }

        resource.getPagedCustomerServices = function (criteria, success, error) {
            resource.post({ operation: 'getPagedCustomerServices' }, criteria, success, error);
        }

        //resource.getCustomerForCreate = function (success, error) {
        //    resource.query({ operation: 'createCustomer' }, success, error);
        //}

        //resource.getCustomerForUpdate = function (id, success, error) {
        //    resource.query({ operation: 'updateCustomer', id: id }, success, error);
        //}

        //resource.createCustomer = function (model, success, error) {
        //    resource.save({ operation: 'createCustomer' }, model, success, error);
        //};

        //resource.updateCustomer = function (model, success, error) {
        //    resource.save({ operation: 'updateCustomer' }, model, success, error);
        //};
        return resource;
    }]);

    custcenter.registerValue('custserviceListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['serviceID'],
        headers: [{
            field: "parentName",
            name: "家长姓名",
            template: '<a ui-sref="ppts.customer-view.profiles({id:row.customerID,prev:vm.parentRoute})">{{row.parentName}}</a>',
            sortable: true
        },
        {
            field: "customerName",
            name: "学员姓名"
        }, {
            field: "grade",
            name: "当前年级",
            template: '<span>{{row.grade | grade}}</span>'
        }, {
            field: "acceptTime",
            name: "受理时间",
            template: '<span>{{row.acceptTime | date:"yyyy-MM-dd"}}</span>'
        }, {
            field: "serviceType",
            name: "服务类型",
            template: '<span>{{row.serviceType | serviceType}}</span>'
        }, {
            field: "accepterName",
            name: "客服受理人"
        }, {
            field: "serviceStatus",
            name: "当前受理状态",
            template: '<span>{{row.serviceStatus | serviceStatus}}</span>',
            description: 'customer handleStatus'
        }, {
            field: "handlerName",
            name: "当前受理人"
        }, {
            field: "complaintTimes",
            name: "投诉次数",
            template: '<span>{{row.complaintTimes | complaintTimes}}</span>'
        },
        {
            field: "schoolMemo",
            name: "校区反馈",
            template: '<span>{{row.schoolMemo }}</span>'
        }, {
            field:'isUpgradeHandle',
            name: "是否升级",
            template: '<span>{{row.isUpgradeHandle | ifElse}}</span>'
        }, {
            field: 'voiceID',
            name: "通话录音",
            template: '<span>{{row.voiceID}}</span>'
        }, {
            field: 'voiceStatus',
            name: "录音状态",
            template: '<span>{{row.voiceStatus}}</span>'
        }
        ],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'cs.CreateTime', sortDirection: 1 }]
    });

    custcenter.registerValue('custserviceAdvanceSearchItems', [
        { name: '当前年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.grades" clear="vm.criteria.grades=[]" async="false"/>' },
        { name: '受理状态：', template: '<ppts-checkbox-group category="serviceStatus" model="vm.criteria.serviceStatuses" clear="vm.criteria.serviceStatuses=[]" async="false"/>' },
        { name: '来电日期：', template: '<ppts-daterangepicker start-date="vm.criteria.callTimeStart" end-date="vm.criteria.callTimeEnd"/>' },
        { name: '服务类型：', template: '<ppts-checkbox-group category="serviceType" model="vm.criteria.serviceTypes" clear="vm.criteria.serviceTypes=[]" async="false"/>' },
        { name: '投诉次数：', template: '<ppts-checkbox-group category="complaintTimes" model="vm.criteria.complaintTimes" clear="vm.criteria.complaintTimes=[]" async="false"/>' },
        { name: '客服升级：', template: '<ppts-checkbox-group category="complaintUpgrade" model="vm.criteria.complaintUpgrades" clear="vm.criteria.complaintUpgrades=[]" async="false"/>' },
        { name: '是否升级：', template: '<ppts-radiobutton-group category="ifElse" model="vm.criteria.isUpgradeHandle" async="false"/>' },
        { name: '严重程度：', template: '<ppts-checkbox-group category="complaintLevel" model="vm.criteria.complaintLevels" clear="vm.criteria.complaintLevels=[]" async="false"/>' },
        
    ]);

    custcenter.registerFactory('custserviceDataViewService', ['$state', 'custserviceDataService', 'dataSyncService', 'custserviceListDataHeader',
     function ($state, custserviceDataService, dataSyncService, custserviceListDataHeader) {
         var service = this;

         // 配置客户服务列表表头
         service.configCustomerServiceListHeaders = function (vm) {
             vm.data = custserviceListDataHeader;

             vm.data.pager.pageChange = function () {
                 dataSyncService.initCriteria(vm);
                 custserviceDataService.getPagedCustomerServices(vm.criteria, function (result) {
                     vm.data.rows = result.pagedData;
                 });
             }
         };

         // 初始化客户服务列表
         service.initCustomerServiceList = function (vm, callback) {
             dataSyncService.initCriteria(vm);
             custserviceDataService.getAllCustomerServices(vm.criteria, function (result) {
                 vm.data.rows = result.queryResult.pagedData;
                 dataSyncService.injectPageDict(['ifElse']);
                 dataSyncService.updateTotalCount(vm, result.queryResult);
                 if (ng.isFunction(callback)) {
                     callback();
                 }
             });
         };

         return service;
     }]);
});