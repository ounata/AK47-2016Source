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

        resource.getDiscountWorkflowInfo = function (criteria, success, error) {
            resource.post({ operation: 'getDiscountWorkflowInfo' }, criteria, success, error);
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
            field: "branchName",
            name: "分公司",
            template: '<span>{{row.branchName}}</span>',
        }, {
            field: "startDate",
            name: "启用时间",
            template: '<span>{{row.startDate | date:"yyyy-MM-dd" | normalize}}</span>'
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
            template: '<span>{{ row.approveTime | date:"yyyy-MM-dd" | normalize}}</span>',
        }
        , {
            field: "approverName",
            name: "最终审批人",
            template: '<span>{{ row.approverName }}</span>',
        }, {
            field: "approverJobName",
            name: "审批人岗位",
            template: '<span>{{ row.approverJobName }}</span>',
        }, {
            field: "createTime",
            name: "创建日期",
            template: '<span>{{ row.createTime | date:"yyyy-MM-dd" | normalize }}</span>',
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
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
            headerCss: 'datatable-header',
            template: '<span ng-class=\'{"has-error":!row.validStandard}\'><mcs-input model="row.discountStandard" class="input-width-100" validate="vm.updateDiscountStandardRank(row, $index)" self-validate="true" datatype="number"/><span class=\'help-inline\' ng-if="!row.validStandard">{{row.errorStandardMessage}}</span></span>'
        },
        {
            field: 'discountValue',
            name: '折扣率(精确到0.01)',
            headerCss: 'datatable-header',
            template: '<span ng-class=\'{"has-error":!row.validValue}\'><mcs-input model="row.discountValue" class="input-width-100" validate="vm.updateDiscountValueRank(row, $index)" self-validate="true" datatype="number"/><span class=\'help-inline\' ng-if="!row.validValue">{{row.errorValueMessage}}</span></span>'
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
                var relationDataLength = (vm.relationData.rows.length > discountMaximum ? vm.relationData.rows.length : discountMaximum);
                if (rowCount < relationDataLength) {
                    for (var i = 0; i < relationDataLength - rowCount; i++) {
                        vm.campusData.rows.push({
                            campusName: '',
                            usedState: '-',
                            endTime: '-'
                        });
                    }
                } else {
                    for (var i = 0; i < rowCount - relationDataLength; i++) {
                        vm.relationData.rows.push({
                            stall: vm.relationData.rows.length + 1,
                            discountStandard: '',
                            discountValue: '',
                            validValue: true,
                            validStandard: true
                        });
                    }
                }
            }
        };

        return service;
    }]);
});