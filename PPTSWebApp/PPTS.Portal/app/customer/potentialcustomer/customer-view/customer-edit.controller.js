define(['customer', mcs.app.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerEditController', [
            '$scope',
            '$state',
            '$stateParams',
            'customerDataService',
            function ($scope, $state, $stateParams, customerDataService) {
                var vm = this;

                vm.editable = false;

                var init = function (editable) {

                    customerDataService.editCustomer({
                        customerID: $stateParams.id,
                        success: function (result) {
                            vm.editable = editable;
                            vm.customer = result.customer;
                            mcs.app.dict.merge(result.dictionaries);
                        },
                        error: function (error) {
                            vm.editable = editable;
                            alert(error);
                        }
                    });
                };

                init();

                vm.edit = function () {
                    init(true);
                };

                vm.save = function () {

                    customerDataService.editCustomer({
                        model: {
                            customer: vm.customer
                        },
                        success: function (result) {
                            var result = result;
                            vm.editable = false;
                        },
                        error: function (error) {
                            vm.editable = false;
                        }
                    });
                };

                vm.cancel = function () {
                    vm.editable = false;
                };
            }]);
    });