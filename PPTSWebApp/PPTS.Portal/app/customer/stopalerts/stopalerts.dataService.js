define(['angular', ppts.config.modules.customer], function (ng, customer) {
    customer.registerFactory('stopAlertdataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customerstopalerts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false, params: { customerId: '@customerId' }, }
            });

        resource.getAllStopAlerts = function (criteria, success, error) {
            resource.post({ operation: 'getAllStopAlerts' }, criteria, success, error);
        }

        resource.getPagedStopAlerts = function (criteria, success, error) {
            resource.post({ operation: 'getPagedStopAlerts' }, criteria, success, error);
        }

        resource.getStopAlertForCreate = function (success, error) {
            resource.query({ operation: 'createStopAlert' }, success, error);
        }

        resource.createStopAlert = function (model, success, error) {
            resource.save({ operation: 'createStopAlert' }, model, success, error);
        };

        return resource;
    }]);

    customer.registerValue('stopListDataHeader', {
        headers: [{
            field: "alertTime",
            name: "日期",
            template: '<span>{{row.alertTime | date:"yyyy-MM-dd"}}</span>',
        }, {
            field: "alertType",
            name: "类型",
            template: '<span>{{ row.alertType | stopAlertType }}</span>',
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
            template: '<span>{{ row.alertReason | stopAlertReason }}</span>',
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'alertTime', sortDirection: 1 }]
    });

    customer.registerFactory('stopAlertDataViewService', ['stopAlertdataService', 'dataSyncService',
    function (stopAlertdataService, dataSyncService) {
        var service = this;

        // 配置跟进列表表头
        service.configStopListHeaders = function (vm, header) {
            vm.data = header;
            vm.data.pager.pageChange = function () {
                dataSyncService.initCriteria(vm);
                stopAlertdataService.getPagedStopAlerts(vm.criteria, function (result) {
                    vm.data.rows = result.pagedData;
                });
            }
        };

        // 初始化跟进列表数据
        service.initStopAlertList = function (vm, callback) {
            dataSyncService.initCriteria(vm);
            stopAlertdataService.getAllStopAlerts(vm.criteria, function (result) {
                vm.data.rows = result.queryResult.pagedData;
                dataSyncService.injectDynamicDict('dateRange,dept,ifElse');
                dataSyncService.updateTotalCount(vm, result.queryResult);
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        // 初始化新增停课休学信息
        service.initCreateStopAlertInfo = function (vm, state, callback) {
            stopAlertdataService.getStopAlertForCreate(state.id, function (result) {
                vm.criteria = result.stopAlert;
                dataSyncService.injectDynamicDict('ifElse');
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        //添加停课休学信息
        service.createStopAlertInfo = function (vm, callback) {
            vm.stopAlert = [];
            vm.stopAlert.customerID = vm.criteria.customerID;
            vm.stopAlert.alertType = vm.criteria.stopAlertType;
            vm.criteria.alertReason = vm.criteria.stopAlertReason;
            vm.stopAlert.alertReasonName = vm.criteria.alertReasonName;
            vm.stopAlert = vm.criteria;
            stopAlertdataService.createStopAlert({
                stopAlert: vm.stopAlert
            }, function () {
                if (ng.isFunction(callback)) {
                    callback();
                }
            });

        };

        return service;
    }]);
});