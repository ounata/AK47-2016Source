define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
function (helper) {
    helper.registerController('productViewController', [
        '$scope', '$state', 'dataSyncService', '$stateParams', 'productDictionary', 'productDataService',
        function ($scope, $state, dataSyncService, $stateParams, productDictionary, productDataService) {

            var vm = this;
            vm.id = $stateParams.id;

            

            var init = (function () {

                productDataService.getProduct(vm.id, function (result) {

                    //console.log(result);

                    vm.product = result.product;
                    vm.productExOfCourse = result.productExOfCourse;
                    vm.salaryRules = result.salaryRules;

                    vm.showCampus = function () {
                        var campusIds = $(result.permissions).map(function (i, v) { return v.campusName; }).toArray();
                        return campusIds.join("\u3001");
                    };
                    vm.showCompanys = function () {
                        var campusIds = $(result.permissions).map(function (i, v) { return v.companyName; }).toArray();
                        return campusIds.join("\u3001");
                    };
                    vm.showRegions = function () {
                        var campusIds = $(result.permissions).map(function (i, v) { return v.regionName; }).toArray();
                        return campusIds.join("\u3001");
                    };

                    vm.copyProduct = function () {

                        var type = parseInt(vm.product.categoryType);

                        var loadtype = '';
                        var routeSuffix = '';

                        for (var index in productDictionary.categories) {
                            if (productDictionary.categories[index].index == type) {
                                routeSuffix = productDictionary.categories[index].routeSuffix;
                                loadtype = index; break;
                            }
                        }

                        if (loadtype == '') { return; }
                        var param = { ltype: loadtype, id: vm.id };
                        $state.go('ppts.productCopy.' + routeSuffix, param);
                    };


                });

                dataSyncService.injectDynamicDict('ifElse');

            })();


        }]);
});

