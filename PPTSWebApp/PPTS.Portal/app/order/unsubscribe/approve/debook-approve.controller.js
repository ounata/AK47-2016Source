define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.unsubscribeCourseDataService], function (helper) {
            helper.registerController('orderApproveController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'mcsDialogService', 'unsubscribeCourseDataService',
                function ($scope, $state, $stateParams, dataSyncService, mcsDialogService, unsubscribeCourseDataService) {
                    var vm = this;

                    vm.data = {
                        headers: [{
                            field: "productCode",
                            name: "产品编号",
                        }, {
                            field: "productName",
                            name: "产品名称",
                        }, {
                            field: "debookAmount",
                            name: "退订数量",
                        }, {
                            name: "退订金额",
                            template: '<span>{{ row.debookAmount * row.realPrice}}</span>'
                        }, {
                            name: "退订时间",
                            template: '<span>{{ row.debookTime | date:"yyyy-MM-dd"}}</span>'
                        }]
                    }
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

                    vm.search = function () {
                        unsubscribeCourseDataService.getDebookOrderByWorkflow(vm.wfParams, function (result) {
                            console.log(result)

                            vm.entity = result.model;
                            vm.clientProcess = result.clientProcess;
                            vm.data.rows = [result.model];
                        })
                    }
                    var init = (function () {

                        vm.search();
                    })();

                    
                }]);
        });