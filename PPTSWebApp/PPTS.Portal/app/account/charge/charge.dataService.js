define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountChargeDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        
        //根据学员ID获取缴费界面需要的显示信息
        resource.getChargeDisplayResultByCustomerID = function (id, success, error) {
            resource.query({ operation: 'GetChargeDisplayResultByCustomerID',id:id }, success, error);
        }

        //根据缴费申请单ID获取缴费界面需要的显示信息
        resource.getChargeDisplayResultByApplyID4Payment = function (id, success, error) {
            resource.query({ operation: 'GetChargeDisplayResultByApplyID4Payment', id: id }, success, error);
        }

        //根据缴费申请单ID获取业绩分派界面需要的显示信息
        resource.getChargeDisplayResultByApplyID4Allot = function (id, success, error) {
            resource.query({ operation: 'GetChargeDisplayResultByApplyID4Allot', id: id }, success, error);
        }

        //根据缴费支付单ID获取收据打印界面需要的显示信息
        resource.getChargeDisplayResultByPayID = function (id, success, error) {
            resource.query({ operation: 'GetChargeDisplayResultByPayID', id: id }, success, error);
        }

        //保存缴费申请
        resource.saveChargeApply = function (apply, success, error) {
            resource.post({ operation: 'SaveChargeApply'}, apply, success, error);
        }

        //删除缴费申请
        resource.deleteChargeApply = function (applyID, success, error) {
            resource.post({ operation: 'DeleteChargeApply'}, applyID, success, error);
        }

        //保存缴费支付单
        resource.saveChargePayment = function (payment, success, error) {
            resource.post({ operation: 'SaveChargePayment' }, payment, success, error);
        }

        //保存缴费支付单
        resource.saveChargePaymentPrint = function (print, success, error) {
            resource.post({ operation: 'SaveChargePaymentPrint' }, print, success, error);
        }

        //根据oa编码获取老师信息
        resource.getTeacher = function (oaCode, success, error) {
            resource.query({ operation: 'GetTeacher', oaCode: oaCode }, success, error);
        }

        return resource;
    }]);
});