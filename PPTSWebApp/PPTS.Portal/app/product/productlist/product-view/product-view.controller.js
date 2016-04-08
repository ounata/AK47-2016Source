define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
function (product) {
    product.registerController('productViewController', [
        '$scope', '$state', '$stateParams', 'blockUI', 'productDataService',
        function ($scope, $state, $stateParams, blockUI, productDataService) {
            var vm = this;

            vm.id = $stateParams.id;

            $scope.load = function () {
                //return 'app/product/productlist/product-view/product-view-onetoone.html';
                return 'app/product/productlist/product-view/product-view-other.html';
                //return 'app/product/productlist/product-view/product-view-classgroup.html';
            };


        }]);
});

