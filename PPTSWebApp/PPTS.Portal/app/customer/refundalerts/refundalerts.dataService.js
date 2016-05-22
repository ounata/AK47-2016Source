define(['angular', ppts.config.modules.customer], function (ng, customer) {
    customer.registerFactory('refundAlertdataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customerrefundalerts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false, params: { customerId: '@customerId' }, }
            });

        resource.getAllRefundAlerts = function (criteria, success, error) {
            resource.post({ operation: 'getAllRefundAlerts' }, criteria, success, error);
        }

        resource.getPagedRefundAlerts = function (criteria, success, error) {
            resource.post({ operation: 'getPagedRefundAlerts' }, criteria, success, error);
        }

        resource.getRefundAlertForCreate = function (success, error) {
            resource.query({ operation: 'createRefundAlert' }, success, error);
        }

        resource.createRefundAlert = function (model, success, error) {
            resource.save({ operation: 'createRefundAlert' }, model, success, error);
        };

        resource.getRefundAlertForUpdate = function (id, success, error) {
            resource.query({ operation: 'updateRefundAlert', id: id }, success, error);
        };

        resource.updateRefundAlert = function (model, success, error) {
            resource.save({ operation: 'updateRefundAlert' }, model, success, error);
        };

        resource.getIsCurrentMonthAlert = function (id, success, error) {
            resource.query({ operation: 'isCurrentMonthAlert', id: id }, success, error);
        }

        return resource;
    }]);

    customer.registerValue('refundListDataHeader', {
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

    customer.registerFactory('refundAlertDataViewService', ['refundAlertdataService', 'dataSyncService',
    function (refundAlertdataService, dataSyncService) {
        var service = this;

        // 配置跟进列表表头
        service.configRefundListHeaders = function (vm, header) {
            vm.data = header;
            vm.data.pager.pageChange = function () {
                dataSyncService.initCriteria(vm);
                refundAlertdataService.getPagedRefundAlerts(vm.criteria, function (result) {
                    vm.data.rows = result.pagedData;
                });
            }
        };

        // 初始化退费预警列表数据
        service.initRefundAlertList = function (vm, callback) {
            dataSyncService.initCriteria(vm);
            refundAlertdataService.getAllRefundAlerts(vm.criteria, function (result) {
                vm.data.rows = result.queryResult.pagedData;
                dataSyncService.injectPageDict(['dateRange', 'people', 'ifElse']);
                dataSyncService.updateTotalCount(vm, result.queryResult);
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        // 初始化新增退费预警信息
        service.initCreateRefundAlertInfo = function (vm, state, callback) {
            refundAlertdataService.getRefundAlertForCreate(state.id, function (result) {
                vm.criteria = result.refundAlert;
                dataSyncService.injectPageDict(['ifElse']);
                if (ng.isFunction(callback)) {
                    callback();
                }
            });


        };

        //添加停课休学信息
        service.createRefundAlertInfo = function (vm, callback) {
            if (vm.alertID == undefined) {
                vm.refundAlert = [];
                vm.refundAlert.customerID = vm.criteria.customerID;
                vm.refundAlert.alertStatus = vm.criteria.alertStatus;
                vm.criteria.alertReason = vm.criteria.alertReason;
                vm.refundAlert.alertReasonName = vm.criteria.alertReasonName;
                vm.refundAlert = vm.criteria;
                refundAlertdataService.createRefundAlert({
                    refundAlert: vm.refundAlert
                }, function () {
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            }
            else {
                vm.refundAlert.alertID = vm.alertID;
                refundAlertdataService.updateRefundAlert({
                    refundAlert: vm.refundAlert
                }, function () {
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            }
        };

        //初始化编辑退费预警信息
        service.initEditRefundAlertInfo = function (vm, callback) {
            refundAlertdataService.getRefundAlertForUpdate(vm.alertID, function (result) {
                vm.refundAlert = result.refundAlert;
                dataSyncService.injectPageDict(['ifElse']);
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        service.isCurrentMonthData = function (vm, callback) {
            refundAlertdataService.getIsCurrentMonthAlert(vm.alertID, function (result) {
                vm.isOK = result.isEditor;
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        return service;
    }]);
});