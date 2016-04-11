define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
function (product) {
    product.registerController('productCopyController', [
        '$scope', '$location', '$state', '$stateParams', 'blockUI', 'dataSyncService', 'productDataService',
        function ($scope, $location, $state, $stateParams, blockUI, dataSyncService, productDataService) {
            var vm = this;
            var id = $stateParams.id;
            var loadtype = $location.url().replace('/ppts/product/copy/', '').replace(/\/.*$/, '');
            console.log(loadtype)

            $scope.tag='复制';

            vm.m = {
                onetoone: {
                    product: {},
                    productExOfCourse: {},
                    salaryRules: [],
                },
                classgroup: {
                    product: {},
                    productExOfCourse: {},
                    salaryRules: [],
                },
                youxue: {
                    product: {},
                    productExOfCourse: {},
                    salaryRules: [],
                },
                wukeshou: {
                    product: {},
                    productExOfCourse: {},
                    salaryRules: [],
                },
                other: {
                    product: {},
                    productExOfCourse: {},
                    salaryRules: [],
                },
            };

            


            //加载信息
            productDataService.copyProductView(id, function (entity) {

                console.log(entity);
               

                vm.dictionaries = entity.dictionaries;

                vm.m[loadtype].categoryType = entity.categoryType;
                vm.m[loadtype].product = entity.product;
                vm.m[loadtype].productExOfCourse = entity.productExOfCourse;
                vm.m[loadtype].salaryRules = entity.salaryRules;
                vm.m[loadtype].catalogs = entity.catalogs;

                vm.m[loadtype].product.startDate = new Date(vm.m[loadtype].product.startDate);
                vm.m[loadtype].product.endDate = new Date(vm.m[loadtype].product.endDate);

                dataSyncService.injectDictData(mcs.util.mapping(entity.catalogs, { key: 'catalog', value: 'catalogName' }, 'CategoryCustom'));
                ppts.config.dictMappingConfig["categoryCustom"] = "c_codE_ABBR_CategoryCustom";

                $scope.$broadcast('dictionaryReady');

            });






            vm.format = 'yyyy/MM/dd hh:mm:ss';
            //vm.date = new Date();
            vm.dateOptions = {
                // dateDisabled: disabled,
                formatYear: 'yy',
                // maxDate: new Date(2020, 5, 22),
                // minDate: new Date(),
                startingDay: 1
            };

            vm.open1 = function () {
                vm.popup1.opened = true;
            };

            vm.popup1 = {
                opened: false
            };

            vm.open2 = function () {
                vm.popup2.opened = true;
            };

            vm.popup2 = {
                opened: false
            };

            vm.open3 = function () {
                vm.popup3.opened = true;
            };

            vm.popup3 = {
                opened: false
            };

            vm.open4 = function () {
                vm.popup4.opened = true;
            };

            vm.popup4 = {
                opened: false
            };


        }]);
    });

