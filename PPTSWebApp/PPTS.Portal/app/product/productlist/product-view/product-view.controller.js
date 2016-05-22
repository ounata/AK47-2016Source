define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
function (helper) {
    helper.registerController('productViewController', [
        '$scope', '$state', 'dataSyncService', '$stateParams', 'productDataService',
        function ($scope, $state, dataSyncService, $stateParams, productDataService) {

            var vm = this;
            vm.id = $stateParams.id;



            var init = (function () {

                productDataService.getProduct(vm.id, function (result) {

                    console.log(result);

                    vm.product = result.product;
                    vm.productExOfCourse = result.productExOfCourse;
                    vm.salaryRules = result.salaryRules;

                    vm.showCampus = function () {
                        var campusIds = $(result.permissions).map(function (i, v) { return v.useOrgID; }).toArray();
                        return campusIds.join(",");
                    };

                });

                dataSyncService.injectPageDict(['ifElse']);

            })();


        }]);
});

