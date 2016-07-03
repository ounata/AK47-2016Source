define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeInvoiceAddController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'mcsValidationService', 'accountChargeDataService', '$uibModalInstance',
                function ($scope, $state, $stateParams, $location, mcsDialogService, mcsValidationService, accountDataService, $uibModalInstance) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.applyID;
                    vm.invoiceModel = {};

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.initAccountChargeInvoice(function (result) {
                            vm.invoiceModel = result.invoice;
                            vm.invoiceModel.applyID = vm.applyID;
                        });
                    };
                    vm.init();


                    //保存
                    vm.save = function () {
                        if (!mcsValidationService.run($scope)) {
                            return;
                        }
                        accountDataService.saveAccountChargeInvoice(vm.invoiceModel, function (data) {
                            if (data.msg != 'ok') {
                                vm.showMsg(data.msg);
                            }
                            $uibModalInstance.close('ok');

                        }, function (error) {

                        });
                    }

                    //取消
                    vm.cancel = function () {
                        $uibModalInstance.dismiss('canceled');
                    }
                }]);
        });