define(['angular', ppts.config.modules.customer], function (ng, customer) {
    customer.registerFactory('discountDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.productApiBaseUrl + 'api/discount/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.getAllDiscounts = function (criteria, success, error) {
            resource.post({ operation: 'getAllDiscounts' }, criteria, success, error);
        }

        resource.getPagedDiscounts = function (criteria, success, error) {
            resource.post({ operation: 'getPagedDiscounts' }, criteria, success, error);
        }

        resource.getDiscountForCreate = function (success, error) {
            resource.query({ operation: 'getCurrentBranchName' }, success, error);
        }

        resource.createDiscount = function (model, success, error) {
            resource.save({ operation: 'createDiscount' }, model, success, error);
        };

        resource.getDiscountForView = function (discountId, success, error) {
            resource.query({ discountId: discountId }, { operation: 'getDiscountDetial' }, success, error);
        }

        resource.getDeleteDiscount = function (criteria, success, error) {
            resource.post({ operation: 'deleteDiscount' }, criteria, success, error);
        }

        resource.getDisableDiscount = function (criteria, success, error) {
            resource.post({ operation: 'disableDiscount' }, criteria, success, error);
        }

        return resource;
    }]);

    customer.registerValue('discountMaximum', 8);

    customer.registerValue('discountListDataHeader', {
        selection: 'radio',
        rowsSelected: [],
        keyFields: ['discountID', 'discountStatus', "createTime"],
        headers: [{
            field: "discountCode",
            name: "折扣编码",
            template: '<a ui-sref="ppts.discount-view({discountId:row.discountID})">{{row.discountCode}}</a>',
        }, {
            field: "ownOrgName",
            name: "分公司",
            template: '<span>{{row.ownOrgName}}</span>',
        }, {
            field: "startDate",
            name: "启用时间",
            template: '<span>{{row.startDate}}</span>'
        }, {
            field: "discountStatus",
            name: "状态",
            template: '<span>{{ row.discountStatus | discountStatus }}</span>',
        }, {
            field: "submitterName",
            name: "提交人",
            template: '<span>{{ row.submitterName }}</span>',
        }, {
            field: "approveTime",
            name: "审批日期",
            template: '<span>{{ row.approveTime | date:"yyyy-MM-dd"}}</span>',
        }
        , {
            field: "approverName",
            name: "最终审批人",
            template: '<span>{{ row.alertStatus | refundAlertStatus }}</span>',
        }, {
            field: "approverJobName",
            name: "审批人岗位",
            template: '<span>{{ row.alertStatus | refundAlertStatus }}</span>',
        }, {
            field: "createTime",
            name: "创建日期",
            template: '<span>{{ row.createTime | date:"yyyy-MM-dd" }}</span>',
        }],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'createTime', sortDirection: 1 }]
    });

    customer.registerValue('discountInfoData', {
        customAction: 'edit',
        rowsSelected: [],
        keyFields: ['customerId'],
        headers: [{
            field: 'stall',
            name: '档位',
            template: '第{{row.stall}}档'
        },
        {
            field: 'discountStandard',
            name: '充值额(万元)',
            template: '<label class="ppts-datatable-solidlineheight">{{row.discountStandard}}</label>'
        },
        {
            field: 'discountValue',
            name: '折扣率(精确到0.01)',
            template: '<label class="ppts-datatable-solidlineheight">{{row.discountValue}}</label>'
        }],
        pager: {
            pagable: false,
        }
    });

    customer.registerValue('discountEditData', {
        customAction: 'edit',
        rowsSelected: [],
        keyFields: ['customerId'],
        headers: [{
            field: 'stall',
            name: '档位',
            template: '第{{row.stall}}档'
        },
        {
            field: 'discountStandard',
            name: '充值额(万元)',
            template: '<span ng-class=\'{"has-error":!row.validStandard}\'><mcs-input model="row.discountStandard" validate="vm.updateDiscountStandardRank(row, $index)" datatype="float" custom-style="width:40%"/><span class=\'help-inline\' ng-if="!row.validStandard">需大于上档小于下档!</span></span>'
        },
        {
            field: 'discountValue',
            name: '折扣率(精确到0.01)',
            template: '<span ng-class=\'{"has-error":!row.validValue}\'><mcs-input model="row.discountValue" validate="vm.updateDiscountValueRank(row, $index)" datatype="float" custom-style="width:40%"/><span class=\'help-inline\' ng-if="!row.validValue">{{vm.errorRowMessage}}</span></span>'
        }],
        pager: {
            pagable: false,
        }
    });

    customer.registerValue('discountCampusData', {
        customAction: 'add',
        rowsSelected: [],
        keyFields: ['customerId'],
        headers: [{
            field: 'campusName',
            name: '校区',
            headerCss: 'datatable-header-align-right',
            template: '<label class="ppts-datatable-solidlineheight">{{row.campusName}}<label>',
            sortable: false,
            description: 'campusName'
        },
        {
            field: 'usedState',
            name: '使用状态',
            headerCss: 'datatable-header-align-right',
            template: '<label class="ppts-datatable-solidlineheight">{{row.usedState}}<label>',
            sortable: false,
            description: 'usedState'
        },
        {
            field: 'endTime',
            name: '结束时间',
            headerCss: 'datatable-header-align-right',
            template: '<label class="ppts-datatable-solidlineheight">{{row.endTime}}<label>',
            sortable: false,
            description: 'endTime'
        }],
        pager: {
            pagable: false,
        }
    });

    customer.registerFactory('discountDataViewService', ['discountDataService', 'dataSyncService', 'discountMaximum',
    function (discountDataService, dataSyncService, discountMaximum) {
        var service = this;

        // 配置折扣表列表表头
        service.configDiscountListHeaders = function (vm, header) {
            vm.data = header;
            vm.data.pager.pageChange = function () {
                dataSyncService.initCriteria(vm);
                discountDataService.getPagedDiscounts(vm.criteria, function (result) {
                    vm.data.rows = result.pagedData;
                });
            }
        };

        service.configDiscountAddDataTable = function (vm, data, campusData) {
            vm.relationData = data;
            vm.campusData = campusData;
            vm.selectBranch = function (campuses) {
                vm.campusData.rows = [];
                if (campuses.names && campuses.names.length > 0)
                    campuses.campusNames = campuses.names;
                var rowCount = campuses.campusNames.length;
                for (var index in campuses.campusNames) {
                    vm.campusData.rows.push({
                        campusName: campuses.campusNames[index],
                        usedState: (campuses.usedStates ? campuses.usedStates[index] : '-'),
                        endTime: (campuses.endTimes ? campuses.endTimes[index] : '-')
                    });
                }
                if (rowCount < discountMaximum) {
                    for (var i = 0; i < discountMaximum - rowCount; i++) {
                        vm.campusData.rows.push({
                            campusName: '',
                            usedState: '-',
                            endTime: '-'
                        });
                    }
                } else {
                    for (var i = 0; i < rowCount - discountMaximum; i++) {
                        vm.relationData.rows.push({
                            stall: vm.relationData.rows.length + 1,
                            discountStandard: 0,
                            discountValue: 0.0
                        });
                    }
                }
            }
        };

        // 初始化折扣表列表数据
        service.initDiscountList = function (vm, callback) {
            dataSyncService.initCriteria(vm);
            discountDataService.getAllDiscounts(vm.criteria, function (result) {
                vm.data.rows = result.queryResult.pagedData;
                dataSyncService.injectPageDict(['dateRange', 'ifElse']);
                dataSyncService.updateTotalCount(vm, result.queryResult);
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        return service;
    }]);
});