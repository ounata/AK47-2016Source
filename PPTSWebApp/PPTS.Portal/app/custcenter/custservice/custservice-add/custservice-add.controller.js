define([ppts.config.modules.custcenter,
        ppts.config.dataServiceConfig.custserviceDataService],
        function (customer) {
            customer.registerController('custserviceAddController', ['$scope', '$state', 'custserviceDataService', 'custserviceDataViewService', 'dataSyncService', 'mcsDialogService','mcsValidationService',

                function ($scope, $state, custserviceDataService, custserviceDataViewService, dataSyncService, mcsDialogService, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    // 页面初始化加载
                    (function () {
                        custserviceDataViewService.initCreateCustomerServiceInfo(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    // 保存数据
                    vm.save = function () {
                        if (mcsValidationService.run($scope)) {
                            custserviceDataService.createCustomerService({
                                customer: vm.customerService
                            }, function () {
                                $state.go('ppts.custservice');
                            });
                        }
                    };

                    vm.saveAndNext = function () {
                        custserviceDataService.createCustomerService({
                            customer: vm.customerService
                        }, function () {
                            $state.go('ppts.custservice-nextProcess', { id: vm.customer.serviceID });
                        });
                    };

                    vm.select = function (customer) {
                        vm.customerService.customerID = customer.customerID;
                        vm.customerService.consultantID = customer.consultant ? customer.consultant.staffID : '';
                        vm.customerService.consultantName = customer.consultant ? customer.consultant.staffName : '';
                        vm.customerService.consultantJobID = customer.consultant ? customer.consultant.staffJobID : '';
                        vm.customerService.consultantJobName = customer.consultant ? customer.consultant.staffJobName : '';
                        vm.customerService.educatorID = customer.market ? customer.market.staffID : '';
                        vm.customerService.educatorName = customer.market ? customer.market.staffName : '';
                        vm.customerService.educatorJobID = customer.market ? customer.market.staffJobID : '';
                        vm.customerService.educatorJobName = customer.market ? customer.market.staffJobName : '';
                    };

                    custserviceDataViewService.configCustomerListHeaders(vm);

                    vm.showTitle = function () {
                        if (vm.customerService.serviceType == 1) {
                            alert("投诉界面");
                            return "投诉界面";
                        }
                    };


                }]);
        });