define([ppts.config.modules.custcenter,
        ppts.config.dataServiceConfig.custserviceDataService],
        function (custcenter) {
            custcenter.registerController('custserviceEidtController', ['$scope', '$state', '$stateParams',
                'custserviceDataService', 'custserviceDataViewService', 'custserviceAdvanceSearchItems', 'dataSyncService','mcsValidationService',
                function ($scope, $state, $stateParams, custserviceDataService, custserviceDataViewService, searchItems, dataSyncService, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    (function () {
                        custserviceDataViewService.initCreateCustomerServiceInfo(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    vm.id = $stateParams.id;
                    custserviceDataService.getCustomerServiceInfo(vm.id, function (result) {

                        vm.customerService = result.customerService;
                        if (vm.customerService.complaintTimes == '') {
                            vm.customerService.complaintTimes = '0';
                        }
                        vm.customer = result.customer;
                        vm.pCustomer = result.pCustomer;

                        dataSyncService.injectPageDict(['ifElse']);

                        $scope.$broadcast('dictionaryReady');
                        console.log(result);
                    });

                    vm.edit = function () {
                        if (mcsValidationService.run($scope)) {
                            custserviceDataService.updateCustomerService({
                                customerService: vm.customerService
                            }, function () {
                                $state.go('ppts.custservice-view', { id: vm.id });
                            });
                        }
                    };

                }]);
        });