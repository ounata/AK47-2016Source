define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
function (product) {
    product.registerController('productCopyController', [
        '$scope', '$state','$stateParams','blockUI', 'productDataService',
        function ($scope, $state,$stateParams, blockUI, productDataService) {
            var vm = this;
            var loadtype = $stateParams.type;

            $scope.tag='复制';

            $scope.load = function(){
                switch(loadtype){
                    case 'classgroup': return '/app/product/productlist/product-add/product-add-classgroup.html'; break;
                    case 'other': return '/app/product/productlist/product-add/product-add-other.html'; break;
                    case 'onetoone': 
                    default:
                    return '/app/product/productlist/product-add/product-add-onetoone.html'; break;
                }
            };


        }]);
    });

