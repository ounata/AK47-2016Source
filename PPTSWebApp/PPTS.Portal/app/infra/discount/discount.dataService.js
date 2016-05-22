define(['angular', ppts.config.modules.customer], function (ng, customer) {
    customer.registerFactory('discountDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/infradiscount/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false, params: { customerId: '@infraId' }, }
            });

        resource.getAllDiscounts = function (criteria, success, error) {
            resource.post({ operation: 'getAlldiscounts' }, criteria, success, error);
        }

        //resource.getPageddiscounts = function (criteria, success, error) {
        //    resource.post({ operation: 'getPageddiscounts' }, criteria, success, error);
        //}

        //resource.getdiscountForCreate = function (success, error) {
        //    resource.query({ operation: 'creatediscountAlert' }, success, error);
        //}

        //resource.creatediscount = function (model, success, error) {
        //    resource.save({ operation: 'creatediscount' }, model, success, error);
        //};

        //resource.getdiscountForUpdate = function (id, success, error) {
        //    resource.query({ operation: 'updatediscount', id: id }, success, error);
        //};

        //resource.updatediscount = function (model, success, error) {
        //    resource.save({ operation: 'updatediscount' }, model, success, error);
        //};

        return resource;
    }]);

    customer.registerValue('discountListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['alertID', 'operatorName', "alertTime"],
        headers: [{
            field: "alertTime",
            name: "日期",
            template: '<span>{{row.alertTime | date:"yyyy-MM-dd"}}</span>',
        }, {
            field: "operatorName",
            name: "操作人姓名",
            template: '<span>{{row.operatorName}}</span>',
        }, {
            field: "operatorJobName",
            name: "操作人岗位",
            template: '<span>{{row.operatorJobName}}</span>'
        }, {
            field: "alertReason",
            name: "停课(休学)原因",
            template: '<span>{{ row.alertReason | refundAlertReason }}</span>',
        }, {
            field: "alertStatus",
            name: "当前状态",
            template: '<span>{{ row.alertStatus | refundAlertStatus }}</span>',
        }],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'alertTime', sortDirection: 1 }]
    });

    customer.registerFactory('discountDataViewService', ['discountDataService', 'dataSyncService',
    function (discountDataService, dataSyncService) {
        var service = this;

        // 配置跟进列表表头
        service.configDiscountListHeaders = function (vm, header) {
            vm.data = header;
            vm.data.pager.pageChange = function () {
                dataSyncService.initCriteria(vm);
                discountDataService.getPageddiscountAlerts(vm.criteria, function (result) {
                    vm.data.rows = result.pagedData;
                });
            }
        };

        // 初始化退费预警列表数据
        service.initDiscountList = function (vm, callback) {
            dataSyncService.initCriteria(vm);
            discountDataService.getAllDiscounts(vm.criteria, function (result) {
                vm.data.rows = result.queryResult.pagedData;
                dataSyncService.injectPageDict(['dateRange', 'people', 'ifElse']);
                dataSyncService.updateTotalCount(vm, result.queryResult);
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        return service;
    }]);
});