define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountChargeDataService', ['$resource', function ($resource) {

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

        //查询缴费单列表
        resource.queryChargeApplyList = function (criteria, success, error) {
            resource.post({ operation: 'QueryChargeApplyList' }, criteria, success, error);
        }

        //分页查询缴费单列表
        resource.queryPagedChargeApplyList = function (criteria, success, error) {
            resource.post({ operation: 'QueryPagedChargeApplyList' }, criteria, success, error);
        }

        //查询收款列表
        resource.queryChargePaymentList = function (criteria, success, error) {
            resource.post({ operation: 'QueryChargePaymentList' }, criteria, success, error);
        }

        //分页查询收款列表
        resource.queryPagedChargePaymentList = function (criteria, success, error) {
            resource.post({ operation: 'QueryPagedChargePaymentList' }, criteria, success, error);
        }

        //根据学员ID获取充值记录
        resource.getChargeApplyList = function (customerID, success, error) {
            resource.query({ operation: 'GetChargeApplyList', customerID: customerID }, success, error);
        }

        //根据学员ID获取缴费单申请信息
        resource.getChargeApplyByApplyID = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByApplyID', id: id }, success, error);
        }

        //根据学员ID获取缴费单申请信息
        resource.getChargeApplyByCustomerID = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByCustomerID', id: id }, success, error);
        }

        //根据缴费申请单ID获取缴费单申请信息（针对支付）
        resource.getChargeApplyByApplyID4Payment = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByApplyID4Payment', id: id }, success, error);
        }

        //根据缴费申请单ID获取缴费单申请信息（针对业绩分配）
        resource.getChargeApplyByApplyID4Allot = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByApplyID4Allot', id: id }, success, error);
        }

        //根据缴费支付单ID获取缴费单申请信息
        resource.getChargeApplyByPayID = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByPayID', id: id }, success, error);
        }

        //保存缴费申请
        resource.saveChargeApply = function (apply, success, error) {
            resource.post({ operation: 'SaveChargeApply' }, apply, success, error);
        }

        //删除缴费申请
        resource.deleteChargeApply = function (applyID, success, error) {
            var apply = { applyID: applyID };
            resource.post({ operation: 'DeleteChargeApply' }, apply, success, error);
        }

        //保存缴费支付单
        resource.saveChargePayment = function (payment, success, error) {
            resource.post({ operation: 'SaveChargePayment' }, payment, success, error);
        }

        //对账缴费支付单
        resource.checkChargePayment = function (payIDs, success, error) {
            resource.post({ operation: 'CheckChargePayment' }, payIDs, success, error);
        }

        //保存缴费支付单打印结果
        resource.printChargePayment = function (payID, success, error) {
            var print = { payID: payID };
            resource.post({ operation: 'PrintChargePayment' }, print, success, error);
        }

        return resource;
    }]);

    account.registerValue('chargeQueryAdvanceSearchItems', [
        { name: '当前年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.grades" async="false"/>' },
        { name: '建档日期：', template: '<ppts-daterangepicker start-date="vm.criteria.createTimeStart" end-date="vm.criteria.createTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '充值日期：', template: '<ppts-daterangepicker start-date="vm.criteria.applyTimeStart" end-date="vm.criteria.applyTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '充值金额：', template: '<ppts-datarange min="vm.criteria.chargeMoneyStart" max="vm.criteria.chargeMoneyEnd" min-text="充值金额起" max-text="充值金额止" width="42%" css="mcs-padding-left-10"/>' },
        { name: '充值类型：', template: '<ppts-checkbox-group category="chargeType" model="vm.criteria.chargeTypes" width="150px" async="false"/>' },
        { name: '审核状态：', template: '<ppts-checkbox-group category="aduitStatus" model="vm.criteria.aduitStatuses" width="150px" async="false"/>' },
        { name: '建档人：', template: '<ppts-checkbox-group category="people" model="vm.criteria.creatorJobs" width="150px" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="建档人姓名" model="vm.criteria.creatorName" custom-style="width:28%"/>' },
    ]);

    account.registerValue('paymentQueryAdvanceSearchItems', [
        { name: '充值日期：', template: '<ppts-daterangepicker start-date="vm.criteria.applyTimeStart" end-date="vm.criteria.applyTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '充值类型：', template: '<ppts-checkbox-group category="chargeType" model="vm.criteria.chargeTypes" width="150px" async="false"/>' },
        { name: '支付类型：', template: '<ppts-checkbox-group category="payType" model="vm.criteria.payTypes" width="150px" async="false"/>' },
        { name: '对账状态：', template: '<ppts-checkbox-group category="checkStatus" model="vm.criteria.checkStatuses" width="150px" async="false"/>' },
    ]);
});