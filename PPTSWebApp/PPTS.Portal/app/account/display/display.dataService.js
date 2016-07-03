define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountDisplayDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        
        //获取账户列表
        resource.getAccountList = function (customerID, success, error) {
            resource.query({ operation: 'GetAccountList',customerID:customerID }, success, error);
        }

        //查询账户日志列表
        resource.queryAccountRecordList = function (criteria, success, error) {
            resource.post({ operation: 'QueryAccountRecordList' }, criteria, success, error);
        }

        //分页查询账户日志列表
        resource.queryPagedAccountRecordList = function (criteria, success, error) {
            resource.post({ operation: 'QueryPagedAccountRecordList' }, criteria, success, error);
        }

        return resource;
    }]);
    
    account.registerValue('accountRecordTable', {

        keyFields: ['recordID'],
        headers: [{
            field: "billTime",
            name: "日期",
            template: '<span>{{row.billTime|date:"yyyy-MM-dd"}}</span>'
        }, {
            field: "recordType",
            name: "类型",
            template: '<span>{{row.recordType|recordType}}</span>'
        }, {
            field: "billNo",
            name: "编号",
            template: '<span>{{row.billNo}}</span>'
        }, {
            field: "incomeMoney",
            name: "收入",
            template: '<span>{{row.incomeMoney | currency:"￥" | normalize}}</span>'
        }, {
            field: "expendMoney",
            name: "支出",
            template: '<span>{{row.expendMoney|currency:"￥" | normalize}}</span>'
        }, {
            field: "assetMoney",
            name: "冻结",
            template: '<a class="btn btn-link" ng-click="vm.assetConsumeView(row)">{{row.frozeMoney|currency:"￥" | normalize}}</a>'
        }, {
            field: "releaseMoney",
            name: "解冻",
            template: '<a class="btn btn-link" ng-click="vm.debookOrderItem(row)">{{row.releaseMoney|currency:\'￥\'|normalize}}</a>',
        }, {
            field: "billerName",
            name: "操作人",
            template: '<span>{{row.billerName}}</span>'
        }, {
            field: "billerJobName",
            name: "操作人岗位",
            template: '<span>{{row.billerJobName}}</span>'
        }],
        orderBy: [{ dataField: 'billTime', sortDirection: 1 }]
    });
});