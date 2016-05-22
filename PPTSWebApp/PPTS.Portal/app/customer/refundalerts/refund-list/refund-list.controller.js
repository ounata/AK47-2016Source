define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.refundAlertDataService],
        function (customer) {
            customer.registerController('refundAlertsListController', ['$scope', '$stateParams', 'utilService', 'refundAlertDataViewService', 'refundListDataHeader', 'mcsDialogService',
                function ($scope, $stateParams, util, refundAlertDataViewService, refundListDataHeader, mcsDialogService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    refundAlertDataViewService.configRefundListHeaders(vm, refundListDataHeader);

                    vm.init = function () {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.customerID = $stateParams.id;
                        refundAlertDataViewService.initRefundAlertList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    vm.add = function () {
                        mcsDialogService.create('app/customer/refundalerts/refund-add/refund-add.html', {
                            controller: 'refundAlertAddController',
                            params: {
                                customerID: $stateParams.id
                            }
                        }).result.then(function () {
                            vm.init();
                        });
                    };

                    vm.edit = function () {
                        if (util.selectOneRow(vm)) {
                            vm.alertID = vm.data.rowsSelected[0].alertID;
                            refundAlertDataViewService.isCurrentMonthData(vm, function () {
                                if (vm.isOK) {
                                    mcsDialogService.create('app/customer/refundalerts/refund-edit/refund-edit.html', {
                                        controller: 'refundAlertEditController',
                                        params: {
                                            alertID: vm.data.rowsSelected[0].alertID,
                                        }
                                    }).result.then(function () {
                                        vm.init();
                                    });
                                }
                                else {
                                    vm.errorMessage = '不是当月的信息不可以编辑！';
                                }
                            });
                        }
                    };

                }]);
        });