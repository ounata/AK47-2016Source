define([ppts.config.modules.custcenter,
        ppts.config.dataServiceConfig.custserviceDataService],
        function (custcenter) {
            custcenter.registerController('custserviceViewController', ['$scope', '$stateParams','$state',
                'custserviceDataService', 'custserviceDataViewService', 'custserviceAdvanceSearchItems','dataSyncService',
                function ($scope, $stateParams, $state, custserviceDataService, custserviceDataViewService, searchItems, dataSyncService) {
                    var vm = this;

                    vm.id = $stateParams.id;


                    custserviceDataViewService.configCustomerServiceItemListHeaders(vm);
                    //custserviceDataViewService.configCustomerServiceItemAllListHeaders(vm);

                    custserviceDataService.getCustomerServiceInfo(vm.id, function (result) {

                        vm.customerService = result.customerService;
                        if (vm.customerService.complaintTimes == '')
                        {
                            vm.customerService.complaintTimes = '0';
                        }
                        vm.customer = result.customer;
                        vm.pCustomer = result.pCustomer;
                        dataSyncService.injectPageDict(['ifElse']);

                        custserviceDataViewService.initCustomerServiceItemAllList(vm, function () {
                        });

                        //custserviceDataViewService.initCustomerServiceItemAllList(vm, function () {
                        //});
                        
                    });

                    

                    vm.overProcess = function () {

                        vm.customerService.serviceStatus = '2';

                        custserviceDataService.updateCustomerService({
                            customerService: vm.customerService
                        }, function () {
                            $state.go('ppts.custservice');
                        });
                    };

                }]);


});