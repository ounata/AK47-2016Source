define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountRefundDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        //根据oa编码获取老师信息
        resource.getTeacher = function (oaCode, success, error) {
            resource.query({ operation: 'GetTeacher', oaCode: oaCode }, success, error);
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

        //获取折扣返还信息
        resource.getRefundReallowance = function (accountID, discountID, discountBase, reallowanceStartTime, success, error) {

            resource.query({ operation: 'GetRefundReallowance', accountID: accountID, discountID: discountID, discountBase: discountBase, reallowanceStartTime: reallowanceStartTime }, success, error);
        }

        //保存退费申请
        resource.saveRefundApply = function (apply, success, error) {
            resource.post({ operation: 'SaveRefundApply' }, apply, success, error);
        }

        //审批退费申请
        resource.approveRefundApply = function (applyID, opinion, success, error) {

            var apply = { applyID: applyID, opinion: opinion};
            resource.post({ operation: 'ApproveRefundApply' }, apply, success, error);
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
        { name: '业务终审日期：', template: '<ppts-daterangepicker start-date="vm.criteria.applyTimeStart" end-date="vm.criteria.applyTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '财务终审日期：', template: '<ppts-daterangepicker start-date="vm.criteria.verifyTimeStart" end-date="vm.criteria.verifyTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '退费确认状态：', template: '<ppts-checkbox-group category="refundVerifyStatus" model="vm.criteria.verifyStatuses" width="150px" async="false"/>' },
        { name: '对账状态：', template: '<ppts-checkbox-group category="checkStatus" model="vm.criteria.checkStatuses" width="150px" async="false"/>' },
        { name: '退款操作人：', template: '<ppts-checkbox-group category="people" model="vm.criteria.creatorJobs"  width="150px"  async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="退款操作人姓名" model="vm.criteria.applierName" custom-style="width:28%"/>' },
    ]);
});