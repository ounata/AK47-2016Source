define(['angular', ppts.config.modules.customer], function (ng, customer) {
    customer.registerFactory('presentDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.productApiBaseUrl + 'api/present/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.getAllPresents = function (criteria, success, error) {
            resource.post({ operation: 'getAllPresents' }, criteria, success, error);
        }

        resource.getPagedPresents = function (criteria, success, error) {
            resource.post({ operation: 'getPagedPresents' }, criteria, success, error);
        }

        resource.getPresentForCreate = function (success, error) {
            resource.query({ operation: 'getCurrentBranchName' }, success, error);
        }

        resource.createPresent = function (model, success, error) {
            resource.save({ operation: 'createPresent' }, model, success, error);
        };

        resource.getPresentForView = function (presentId,success, error) {
            resource.query({ presentId: presentId }, { operation: 'getPresentDetial' }, success, error);
        }

        resource.getDeletePresent = function (criteria, success, error) {
            resource.post({ operation: 'deletePresent' }, criteria, success, error);
        }

        resource.getDisablePresent = function (criteria, success, error) {
            resource.post({ operation: 'disablePresent' }, criteria, success, error);
        }

        resource.getPresentWorkflowInfo = function (criteria, success, error) {
            resource.post({ operation: 'getPresentWorkflowInfo' }, criteria, success, error);
        }

        return resource;
    }]);

    customer.registerValue('presentMaximum', 8);

    customer.registerValue('presentListDataHeader', {
        selection: 'radio',
        rowsSelected: [],
        keyFields: ['presentID', 'presentStatus', "createTime"],
        headers: [{
            field: "presentCode",
            name: "买赠表编码",
            template: '<a ui-sref="ppts.present-view({presentId:row.presentID})">{{row.presentCode}}</a>',
        }, {
            field: "branchName",
            name: "分公司",
            template: '<span>{{row.branchName}}</span>',
        }, {
            field: "startDate",
            name: "启用时间",
            template: '<span>{{row.startDate  | date:"yyyy-MM-dd"  | normalize }}</span>'
        }, {
            field: "presentStatus",
            name: "状态",
            template: '<span>{{ row.presentStatus | presentStatus }}</span>',
        }, {
            field: "submitterName",
            name: "提交人",
            template: '<span uib-popover="{{row.submitterName | tooltip:4}}" popover-trigger="mouseenter">{{ row.submitterName | truncate:4 }}</span>',
        }, {
            field: "approverName",
            name: "终审人",
            template: '<span>{{ row.approverName  }}</span>',
        }, {
            field: "approveTime",
            name: "审批通过时间",
            template: '<span>{{ row.approveTime | date:"yyyy-MM-dd"  | normalize }}</span>',
        }, {
            field: "createTime",
            name: "创建日期",
            template: '<span>{{ row.createTime | date:"yyyy-MM-dd"  | normalize }}</span>',
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'createTime', sortDirection: 1 }]
    });

    customer.registerValue('presentInfoData', {
        customAction: 'edit',
        rowsSelected: [],
        keyFields: ['customerId'],
        headers: [{
            field: 'stall',
            name: '档位',
            template: '第{{row.stall}}档'
        },
        {
            field: 'presentStandard',
            name: '购买数量',
            template: '<label class="ppts-datatable-solidlineheight">{{row.presentStandard}}</label>'
        },
        {
            field: 'presentValue',
            name: '赠送数量',
            template: '<label class="ppts-datatable-solidlineheight">{{row.presentValue}}</label>'
        }],
        pager: {
            pagable: false,
        }
    });

    customer.registerValue('presentEditData', {
        customAction: 'edit',
        rowsSelected: [],
        keyFields: ['customerId'],
        headers: [{
            field: 'stall',
            name: '档位',
            template: '第{{row.stall}}档'
        },
        {
            field: 'presentStandard',
            name: '购买数量',
            headerCss: 'datatable-header',
            template: '<span ng-class=\'{"has-error":!row.validStandard}\'><mcs-input model="row.presentStandard" class="input-width-100" validate="vm.updatePresentStandardRank(row, $index)" self-validate="true" datatype="int" custom-style="width:40%"/><span class=\'help-inline\' ng-if="!row.validStandard">{{row.errorStandardMessage}}</span></span>'
        },
        {
            field: 'presentValue',
            name: '赠送数量',
            headerCss: 'datatable-header',
            template: '<span ng-class=\'{"has-error":!row.validValue}\'><mcs-input model="row.presentValue" class="input-width-100" validate="vm.updatePresentValueRank(row, $index)" self-validate="true" datatype="int" custom-style="width:40%"/><span class=\'help-inline\' ng-if="!row.validValue">{{row.errorValueMessage}}</span></span>'
        }],
        pager: {
            pagable: false,
        }
    });

    customer.registerValue('presentCampusData', {
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

    customer.registerFactory('presentDataViewService', ['presentDataService', 'dataSyncService', 'presentMaximum',
    function (presentDataService, dataSyncService, presentMaximum) {
        var service = this;

        service.configPresentAddDataTable = function (vm, data, campusData) {
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
                var relationDataLength = (vm.relationData.rows.length > presentMaximum ? vm.relationData.rows.length : presentMaximum);
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