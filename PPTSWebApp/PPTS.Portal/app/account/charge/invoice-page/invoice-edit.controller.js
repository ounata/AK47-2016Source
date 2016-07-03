define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeInvoiceEditController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'mcsValidationService', 'accountChargeDataService', 'data', '$uibModalInstance',
                function ($scope, $state, $stateParams, $location, mcsDialogService, mcsValidationService, accountDataService, data, $uibModalInstance) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.invoiceID = data.para.invoiceID;
                    vm.showReturn = false;

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getAccountChargeInvoice(vm.invoiceID, function (data) {
                            vm.invoiceModel = data.invoice;
                            if (vm.invoiceModel.invoiceStatus == '1')
                                vm.showReturn = false;
                            else
                                vm.showReturn = true;
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                
                    vm.clickInvoiceStatus = function (item) {
                        if (item.key == '1')
                            vm.showReturn = false;
                        else
                            vm.showReturn = true;
                    };

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