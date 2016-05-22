define([ppts.config.modules.custcenter,
        ppts.config.dataServiceConfig.custserviceDataService],
        function (custcenter) {
            custcenter.registerController('custserviceEidtController', ['$scope', '$state', '$stateParams',
                'custserviceDataService', 'custserviceDataViewService', 'custserviceAdvanceSearchItems', 'dataSyncService',
                function ($scope, $state, $stateParams, custserviceDataService, custserviceDataViewService, searchItems, dataSyncService) {
                    var vm = this;

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

                        

                        dataSyncService.injectPageDict(['ifElse']);

                        console.log(result);
                    });

                    vm.edit = function () {

                        custserviceDataService.updateCustomerService({
                            customerService: vm.customerService
                        }, function () {
                            $state.go('ppts.custservice-view', {id:vm.id});
                        });
                    };

                }]);
        });