define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
function (helper) {
    helper.registerController('productApproveController', [
        '$scope', '$state', 'dataSyncService', '$stateParams', 'mcsDialogService', 'productDictionary', 'productDataService',
        function ($scope, $state, dataSyncService, $stateParams, mcsDialogService, productDictionary, productDataService) {

            var vm = this;
            vm.id = $stateParams.resourceID;

            vm.wfParams = {
                processID: $stateParams.processID,
                activityID: $stateParams.activityID,
                resourceID: $stateParams.resourceID
            };

            //function
            vm.approve = function (clientProcess) {

                mcsDialogService.info({ title: '提示', message: '审批通过！' })
                   .result.then(function () {
                       location.href = "/";
                       location.reload();
                   });
            };
            vm.reject = function (clientProcess) {

                mcsDialogService.info({ title: '提示', message: '审批拒绝！' })
                   .result.then(function () {
                       location.href = "/";
                       location.reload();
                   });
            };

            var init = (function () {

                productDataService.getProductByWorkflow(vm.wfParams, function (data) {

                    //console.log(result);

                    vm.clientProcess = data.clientProcess;

                    var result = data.model;
                    vm.product = result.product;
                    vm.productExOfCourse = result.productExOfCourse;
                    vm.salaryRules = result.salaryRules;

                    vm.showCampus = function () {
                        var campusIds = $(result.permissions).map(function (i, v) { return v.campusName; }).toArray();
                        return campusIds.join("\u3001");
                    };
                    vm.showCompanys = function () {
                        var campusIds = $(result.permissions).map(function (i, v) { return v.companyName; }).toArray();
                        return campusIds.join("\u3001");
                    };
                    vm.showRegions = function () {
                        var campusIds = $(result.permissions).map(function (i, v) { return v.regionName; }).toArray();
                        return campusIds.join("\u3001");
                    };
                });

                dataSyncService.injectDynamicDict('ifElse');

            })();


        }]);
});

