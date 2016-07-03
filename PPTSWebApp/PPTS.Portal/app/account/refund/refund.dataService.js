define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountRefundDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        //根据oa编码获取老师信息
        resource.getTeacher = function (campusID, oaCode, success, error) {
            resource.query({ operation: 'GetTeacher', campusID: campusID, oaCode: oaCode }, success, error);
        }

        //查询退款列表
        resource.queryRefundApplyList = function (criteria, success, error) {
            resource.post({ operation: 'QueryRefundApplyList' }, criteria, success, error);
        }

        //分页查询退费列表
        resource.queryPagedRefundApplyList = function (criteria, success, error) {
            resource.post({ operation: 'QueryPagedRefundApplyList' }, criteria, success, error);
            }

        //根据学员ID获取退费信息
        resource.getRefundApplyByCustomerID = function (id, success, error) {
            resource.query({ operation: 'GetRefundApplyByCustomerID', id: id }, success, error);
        }

        //根据申请ID获取退费信息
        resource.getRefundApplyByApplyID = function (id, success, error) {
            resource.query({ operation: 'GetRefundApplyByApplyID', id: id }, success, error);
        }

        //根据工作流参数获取退费信息
        resource.getRefundApplyByWorkflow = function (wfParams, success, error) {
            resource.post({ operation: 'GetRefundApplyByWorkflow' }, wfParams, success, error);
        }

        //获取折扣返还信息
        resource.getRefundReallowance = function (accountID, discountID, discountBase, reallowanceStartTime, success, error) {

            resource.query({ operation: 'GetRefundReallowance', accountID: accountID, discountID: discountID, discountBase: discountBase, reallowanceStartTime: reallowanceStartTime }, success, error);
        }

        //保存退费申请
        resource.saveRefundApply = function (apply, success, error) {
            resource.post({ operation: 'SaveRefundApply' }, apply, success, error);
        }
        
        //确认退费申请
        resource.verifyRefundApply = function (action, applyID, success, error) {

            var apply = { action: action, applyID: applyID };
            resource.post({ operation: 'VerifyRefundApply' }, apply, success, error);
        }

        //对账退费申请
        resource.checkRefundApply = function (applyIDs, success, error) {
            resource.post({ operation: 'CheckRefundApply' }, applyIDs, success, error);
        }
        return resource;
    }]);

    account.registerValue('refundQueryAdvanceSearchItems', [
        { name: '业务终审日期：', template: '<mcs-daterangepicker start-date="vm.criteria.approveTimeStart" end-date="vm.criteria.approveTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '财务终审日期：', template: '<mcs-daterangepicker start-date="vm.criteria.verifyTimeStart" end-date="vm.criteria.verifyTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '退款状态：', template: '<ppts-checkbox-group category="refundVerifyStatus" model="vm.criteria.verifyStatuses" width="150px" async="false"/>' },
        { name: '对账状态：', template: '<ppts-checkbox-group category="checkStatus" model="vm.criteria.checkStatuses" width="150px" async="false"/>' },
        { name: '退款操作人：', template: '<ppts-checkbox-group category="applierJobType" model="vm.criteria.applierJobTypes" width="150px" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="退款操作人姓名" model="vm.criteria.applierName" custom-style="width:28%"/>' },
    ]);
    
    account.registerValue('refundQueryTable', {

        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['applyID'],
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.accountRefund-query\'})">{{row.customerName}}</a>'
        }, {
            field: "customerCode",
            name: "学员编号",
            template: '<span>{{row.customerCode}}</span>'
        }, {
            field: "thatAccountMoney",
            name: "账户总额",
            template: '<span>{{row.thatAccountMoney | currency:"￥"}}</span>'
        }, {
            field: "oughtRefundMoney",
            name: "应退金额",
            template: '<span>{{row.oughtRefundMoney | currency:"￥"}}</span>'
        }, {
            field: "realRefundMoney",
            name: "实退金额",
            template: '<span>{{row.realRefundMoney | currency:"￥"}}</span>'
        }, {
            field: "drawer",
            name: "领款人",
            template: '<span>{{row.drawer}}</span>'
        }, {
            field: "applierName",
            name: "申请人",
            template: '<span>{{row.applierName}}</span>'
        }, {
            field: "applierJobName",
            name: "申请人岗位",
            template: '<span>{{row.applierJobName}}</span>'
        }, {
            field: "campusName",
            name: "申请校区",
            template: '<span>{{row.campusName}}</span>'
        }, {
            field: "approveTime",
            name: "业务终审日期",
            template: '<span>{{row.approveTime | date:"yyyy-MM-dd" | normalize }}</span>'
        }, {
            field: "verifyTime",
            name: "财务终审日期",
            template: '<span>{{row.verifyTime | date:"yyyy-MM-dd" | normalize }}</span>'
        }, {
            field: "checkStatus",
            name: "对账状态",
            template: '<span ng-class="{1: \'ppts-checked-color\', 0: \'ppts-unchecked-color\'}[{{row.checkStatus}}]">{{row.checkStatus | checkStatus}}</span>'
        }, {
            field: "verifyStatus",
            name: "退款状态",
            template: '{{row.verifyStatus | refundVerifyStatus}}</span>'
        }, {
            field: "applyNo",
            name: "退款申请详情",
            template: '<span><a ui-sref="ppts.accountRefund-info({id:row.applyID,prev:\'ppts.accountRefund-query\'})">退款申请详情</span>'
        }],
        orderBy: [{ dataField: 'applyTime', sortDirection: 1 }]
    });
});