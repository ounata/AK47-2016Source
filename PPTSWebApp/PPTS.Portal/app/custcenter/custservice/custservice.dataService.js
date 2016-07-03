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

        resource.getCustomerServiceForCreate = function (success, error) {
            resource.query({ operation: 'createCustomerService' }, success, error);
        }

        resource.createCustomerService = function (model, success, error) {
            resource.save({ operation: 'createCustomerService' }, model, success, error);
        };

        resource.updateCustomerService = function (model, success, error) {
            resource.save({ operation: 'updateCustomerService' }, model, success, error);
        };

        resource.accessProcess = function (model, success, error) {
            resource.save({ operation: 'accessProcess' }, model, success, error);
        };

        resource.getCustomerServiceInfo = function (id, success, error) {
            resource.query({ operation: 'getCustomerServiceInfo', id: id }, success, error);
        }

        resource.getCustomerServicesItems = function (criteria, success, error) {
            resource.post({ operation: 'getCustomerServicesItems' }, criteria, success, error);
        }

        resource.getCustomerServicesItemsAll = function (criteria, success, error) {
            resource.post({ operation: 'getCustomerServicesItemsAll' }, criteria, success, error);
        }

        resource.getPagedCustomerServicesItem = function (criteria, success, error) {
            resource.post({ operation: 'getPagedCustomerServicesItem' }, criteria, success, error);
        }

        return resource;
    }]);


    custcenter.registerValue('custserviceListDataHeader', {
        headers: [
            {
                field: "orgName",
                name: "分公司",
                template: '<span ng-if="row.complaintLevel == 2 || row.complaintLevel == 3" class="important">*</span><span> {{row.orgName}}</span>'
            },
            {
                field: "campusName",
                name: "校区"
            }, {
            field: "parentName",
            name: "家长姓名"
            },
            {
                field: "customerName",
                name: "学员姓名",
                template: '<a ui-sref="ppts.customer-view.profiles({id:row.customerID,prev:\'ppts.customer\'})"> <span style="color:red" ng-if="row.serviceType != 3 || row.isUpgradeHandle == 1">{{row.customerName}}</span><span ng-if="row.serviceType == 3">{{row.customerName}}</span></a>'
            }
            //, {
            //    field: "grade",
            //    name: "当前年级",
            //    template: '<span>{{row.grade | grade}}</span>'
            //}
            , {
                field: "acceptTime",
                name: "受理时间",
                template: '<a ui-sref="ppts.custservice-view({id:row.serviceID})">{{row.acceptTime | date:"yyyy-MM-dd"}}</a>'
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
            }
            //,
            //{
            //    field: "schoolMemo",
            //    name: "校区反馈",
            //    template: '<span>{{row.schoolMemo }}</span>'
            //}
            , {
                field: 'isUpgradeHandle',
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
            orderBy: [{ dataField: 'cs.CreateTime', sortDirection: 1 }]
        });

    custcenter.registerValue('customerListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['customerID', 'customerName'],
        headers: [{
            field: "orgName",
            name: "校区",
            template: '{{row.orgName}}'
        },
        {
            field: "customerName",
            name: "学员姓名",
            template: '{{row.customerName}}'
        }, {
            field: "customerCode",
            name: "学员编号",
            template: '{{row.customerCode}}',
        }, {
            field: "parentName",
            name: "家长姓名"
        },
        {
            field: 'consultantStaff',
            name: "归属咨询师"
        },

        {
            field: "createTime",
            name: "建档日期",
            template: '<span>{{row.createTime | date:"yyyy-MM-dd"}}</span>'
        }, {
            field: "creatorName",
            name: "建档人"
        }
        ],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'pcc.CreateTime', sortDirection: 1 }]
    });

    custcenter.registerValue('customerServiceItemDataHeader', {
        headers: [{
            field: "handlerName",
            name: "操作人",
            template: '{{row.handlerName + " (" +row.handlerJobName+" )" }}'
        },
        {
            field: "handleTime",
            name: "受理时间（转下一个受理人）",
            template: '{{row.handleTime | date:"yyyy-MM-dd"}}'
        }, {
            field: "handleStatus",
            name: "当前受理状态",
            template: '{{row.handleStatus | serviceStatus}}',
        }, {
            field: "handleMemo",
            name: "受理详细描述"
        }
        ],
        pager: {
            pageIndex: 1,
            pageSize: 1000,
            totalCount: -1
        },
        orderBy: [{ dataField: 'ci.ItemID', sortDirection: 1 }]
    });

    custcenter.registerValue('customerServiceItemAllDataHeader', {
        headers: [{
            field: "handlerName",
            name: "操作人",
            template: '{{row.handlerName + " (" +row.handlerJobName+" )" }}'
        },
        {
            field: "handleTime",
            name: "受理时间（转下一个受理人）",
            template: '{{row.handleTime | date:"yyyy-MM-dd"}}'
        }, {
            field: "handleStatus",
            name: "当前受理状态",
            template: '{{row.handleStatus | serviceStatus}}',
        }, {
            field: "handleMemo",
            name: "受理详细描述"
        }
        ],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'ci.ItemID', sortDirection: 1 }]
    });


    custcenter.registerValue('custserviceAdvanceSearchItems', [
        { name: '当前年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.grades" async="false"/>' },
        { name: '受理状态：', template: '<ppts-checkbox-group category="serviceStatus" model="vm.criteria.serviceStatuses" async="false"/>' },
        { name: '来电日期：', template: '<mcs-daterangepicker start-date="vm.criteria.callTimeStart" end-date="vm.criteria.callTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '服务类型：', template: '<ppts-checkbox-group category="serviceType" model="vm.criteria.serviceTypes" async="false"/>' },
        { name: '投诉次数：', template: '<ppts-checkbox-group category="complaintTimes" model="vm.criteria.complaintTimes" async="false"/>' },
        { name: '客诉升级：', template: '<ppts-checkbox-group category="complaintUpgrade" model="vm.criteria.complaintUpgrades" async="false"/>' },
        { name: '是否升级：', template: '<ppts-radiobutton-group category="ifElse" model="vm.criteria.isUpgradeHandle" show-all="true" async="false" css="mcs-padding-left-10"/>' },
        { name: '严重程度：', template: '<ppts-checkbox-group category="complaintLevel" model="vm.criteria.complaintLevels" async="false"/>' },

    ]);

    custcenter.registerFactory('custserviceDataViewService', ['$state', '$stateParams', 'custserviceDataService', 'dataSyncService', 'custserviceListDataHeader', 'customerListDataHeader', 'customerServiceItemDataHeader','customerServiceItemAllDataHeader',
     function ($state, $stateParams, custserviceDataService, dataSyncService, custserviceListDataHeader, customerListDataHeader, customerServiceItemDataHeader, customerServiceItemAllDataHeader) {
         var service = this;

         //var syNow = new Date();
         //var year = syNow.getFullYear();        //年
         //var month = syNow.getMonth()+1;     //月
         //var day = syNow.getDate();
         //var startDate = new Date(new Date(year + "-" + month + "-" + 1).getTime());
         //var endDate = new Date(new Date(year + "-" + month + "-" + day).getTime());

         // 配置客户服务列表表头
         //service.configCustomerServiceListHeaders = function (vm) {
         //    vm.data = custserviceListDataHeader;

         //    vm.data.pager.pageChange = function () {
         //        dataSyncService.initCriteria(vm);
         //        custserviceDataService.getPagedCustomerServices(vm.criteria, function (result) {
         //            vm.data.rows = result.pagedData;
                     
         //        });
         //    }
         //}

         // 初始化客户服务列表
         //service.initCustomerServiceList = function (vm, callback) {
         //    dataSyncService.initCriteria(vm);
         //    custserviceDataService.getAllCustomerServices(vm.criteria, function (result) {
         //        vm.data.rows = result.queryResult.pagedData;
         //        vm.data.searching();
         //        dataSyncService.injectDynamicDict('ifElse');
         //        dataSyncService.updateTotalCount(vm, result.queryResult);
         //        if (ng.isFunction(callback)) {
         //            callback();
         //        }
         //    });
         //};

         // 配置客户服务明细列表表头
         service.configCustomerServiceItemListHeaders = function (vm) {
             vm.data = customerServiceItemDataHeader;

             vm.data.pager.pageChange = function () {
                 dataSyncService.initCriteria(vm);
                 custserviceDataService.getPagedCustomerServicesItem(vm.criteria, function (result) {
                     vm.data.rows = result.pagedData;
                 });
             }
         };

         

         service.initCustomerServiceItemAllList = function (vm, callback) {
             dataSyncService.initCriteria(vm);
             vm.criteria.customerID = vm.pCustomer.customerID;
             custserviceDataService.getCustomerServicesItemsAll(vm.criteria, function (result) {
                 vm.data.rows = result.queryResult.pagedData;
                 vm.data1 = {};
                 vm.data1.headers = vm.data.headers;
                 vm.data1.orderBy = vm.data.orderBy;
                 vm.data1.pager = vm.data.pager;
                 vm.data1.rows = [];
                 result.queryResult.pagedData.forEach(function (data) {
                     if (data.serviceID == $stateParams.id) {
                         vm.data1.rows.push(data);
                     }
                 });

                 vm.data2 = {};
                 vm.data2.headers = vm.data.headers;
                 vm.data2.headers = vm.data.headers;
                 vm.data2.orderBy = vm.data.orderBy;
                 vm.data2.rows = [];
                 result.queryResult.pagedData.forEach(function (data) {
                     if (data.serviceID != $stateParams.id) {
                         vm.data2.rows.push(data);
                     }
                 });

                 //vm.data.totalCount = result.queryResult.totalCount;
                 //dataSyncService.updateTotalCount(vm, result.queryResult);

                 

                 if (ng.isFunction(callback)) {
                     callback();
                 }
             });
         };

         //初始化添加页的潜在客户列表
         service.configCustomerListHeaders = function (vm) {
             vm.data = customerListDataHeader;

         };

         // 初始化新增客服信息
         service.initCreateCustomerServiceInfo = function (vm, callback) {
             custserviceDataService.getCustomerServiceForCreate(function (result) {
                 dataSyncService.injectDynamicDict('ifElse');
                 vm.title = '客服';
                 switch (vm.customerService.serviceType) {
                     case 1:
                         vm.title = '投诉';
                         break;
                     case 2:
                         vm.title = '退费';
                         break;
                     case 3:
                         vm.title = '咨询';
                         break;
                     case 4:
                         vm.title = '其他';
                         break;
                 }

                 if (ng.isFunction(callback)) {
                     callback();
                 }
             });
         };

         return service;
     }]);
});