define(['angular', ppts.config.modules.infra], function (ng,infra) {

    infra.registerFactory('servicefeeDataService', ['$resource',  function ($resource) {
        var resource = $resource(ppts.config.productApiBaseUrl + 'api/servicefees/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        resource.loadDictionaries = function (success, error) {
            resource.query({ operation: 'loadDictionaries' }, success, error);
        }
        
        //默认查询
        resource.getAllServiceFees = function (criteria, success, error) {
            resource.post({ operation: 'getAllServiceFees' }, criteria, success, error);
        }

        //分页查询
        resource.getPagedServiceFees = function (criteria, success, error) {
            resource.post({ operation: 'getPagedServiceFeeList' }, criteria, success, error);
        }
        //add/edit
        resource.createExpenses = function (expense, success, error) {
            resource.save({ operation: 'createExpenses' }, expense, success, error);
        }
        resource.getExpense = function (expenseID, success, error) {
            resource.query({ operation: 'getExpense', id: expenseID }, success, error);
        }
        //delete
        resource.delExpenses = function (expenseIDs, success, error) {
            resource.post({ operation: 'delExpenses'},expenseIDs, success, error);
        }
        return resource;
    }]);
    //教学服务会结果集列头
    infra.registerValue('servicefeeListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['expenseID'],
        headers: [{
            field: "branchName",
            name: "分公司"
        }, {
            field: "campusNames",
            name: "所属校区",
            template: '<span uib-tooltip="{{row.campusNames}}">{{row.campusNames | truncate}}</span>',
        }, {
            field: "expenseType",
            name: "服务费名称",
            template: '<span>{{ row.expenseType | serviceFeeType }}</span>'
        }, {
            field: "creatorName",
            name: "创建人姓名"
        }, {
            field: "createTime",
            name: "创建时间",
            template: '<span>{{ row.createTime | date:"yyyy-MM-dd HH:mm" | normalize }}</span>'
        }, {
            field: "modifyTime",
            name: "最后编辑时间",
            template: '<span ng-show="row.modifyTime.getFullYear() >1971 ">{{ row.modifyTime | date:"yyyy-MM-dd HH:mm" | normalize }}</span>'
        }, {
            field: "expenseValue",
            name: "综合服务费",
            template: '<span>{{ row.expenseValue | currency }}</span>'
        }],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'createTime', sortDirection: 1 }]
    });

    infra.registerFactory('servicefeeDataViewService', ['servicefeeDataService', 'dataSyncService', 'servicefeeListDataHeader',
        function (servicefeeDataService, dataSyncService, servicefeeListDataHeader) {
            var service = this;

            // 配置列表表头
            service.configServiceFeeListHeaders = function (vm) {
                vm.data = servicefeeListDataHeader;

                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    servicefeeDataService.getPagedServiceFees(vm.criteria, function (result) {
                        vm.data.rows = result.pagedData;
                    });
                }
            };

            // 初始化列表数据
            service.initServiceFeeList = function (vm, callback) {
                dataSyncService.initCriteria(vm);
                servicefeeDataService.getAllServiceFees(vm.criteria, function (result) {
                    //vm.dictionaries = result.dictionaries;
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.injectDictData();
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };
            return service;
        }]);

    infra.registerFactory("servicefeeAddViewService", ['servicefeeDataService', 'dataSyncService', 
        function (servicefeeDataService, dataSyncService) {
            var service = this;
            service.configServiceFee = function (vm,callback) {
                servicefeeDataService.loadDictionaries(function (result) {
                    dataSyncService.injectDictData();
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            }
            return service;
        }]);
    infra.registerFactory("servicefeeEditViewService", ['servicefeeDataService', 'dataSyncService',
       function (servicefeeDataService, dataSyncService) {
           var service = this;
           service.configServiceFee = function (vm, callback) {
               servicefeeDataService.loadDictionaries(function (result) {
                   dataSyncService.injectDictData();
                   servicefeeDataService.getExpense(vm.expense.expenseID, function (result) {
                       vm.expense = result.expense;
                       if (ng.isFunction(callback)) {
                           callback();
                       }
                   });
                   
               });
           }
           return service;
       }]);
});
