define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountTransferDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        //根据学员编号获取学员信息
        resource.getCustomer = function (customerCode, success, error) {
            resource.query({ operation: 'GetCustomer', customerCode: customerCode }, success, error);
        }
        //根据学员ID获取转让记录
        resource.getTransferApplyList = function (customerID, success, error) {
            resource.query({ operation: 'GetTransferApplyList', customerID: customerID }, success, error);
        }

        //根据学员ID获取转让信息
        resource.getTransferApplyByCustomerID = function (id, success, error) {
            resource.query({ operation: 'GetTransferApplyByCustomerID', id: id }, success, error);
        }

        //根据申请ID获取转让信息
        resource.getTransferApplyByApplyID = function (id, success, error) {
            resource.query({ operation: 'GetTransferApplyByApplyID', id: id }, success, error);
        }

        //根据工作流参数获取转让信息
        resource.getTransferApplyByWorkflow = function (wfParams, success, error) {
            resource.post({ operation: 'GetTransferApplyByWorkflow' }, wfParams, success, error);
        }

        //保存转让申请
        resource.saveTransferApply = function (apply, success, error) {
            resource.post({ operation: 'SaveTransferApply' }, apply, success, error);
        }

        //审批转让申请
        resource.approveTransferApply = function (applyID, opinion, success, error) {

            var apply = { applyID: applyID, opinion: opinion };
            resource.post({ operation: 'ApproveTransferApply' }, apply, success, error);
        }

        return resource;
    }]);
});