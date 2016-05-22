define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.stopAlertDataService],
        function (customer) {
            customer.registerController('stopAlertsListController', ['$scope', '$stateParams', 'stopAlertDataViewService', 'stopListDataHeader', 'mcsDialogService',
                function ($scope, $stateParams, stopAlertDataViewService, stopListDataHeader, mcsDialogService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    stopAlertDataViewService.configStopListHeaders(vm, stopListDataHeader);

                    vm.init = function () {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.customerID = $stateParams.id;
                        stopAlertDataViewService.initStopAlertList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    vm.add = function () {
                        mcsDialogService.create('app/customer/stopalerts/stop-add/stop-add.html', {
                            controller: 'stopAlertAddController',
                            params: {
                                customerID: $stateParams.id
                            }
                        }).result.then(function () {
                            vm.init();
                        });
                    };

                }]);
        });