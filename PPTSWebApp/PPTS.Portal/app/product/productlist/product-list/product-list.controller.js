define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
            product.registerController('productListController', [
                '$scope', '$state', 'blockUI', 'productDataService',
                function ($scope, $state, blockUI, productDataService) {
                    var vm = this;

                    //查询条件
                    vm.criteria = {
                        name: '',
                        productCode: '',
                        startDate: '',
                        endDate: '',
                        pagedParam: null
                    };

                    var successCallback = function (result) {
                        vm.pagedList = result.queryResult;
                        vm.dictionaries = vm.dictionaries || result.dictionaries;
                        vm.criteria.pagedParam = result.queryResult.pagedParam;
                        blockUI.stop();
                    };

                    var errorCallback = function (result) {

                    };

                    vm.search = (function () {
                        //blockUI.start();
                        productDataService.getAllCustomers(vm.criteria, successCallback, errorCallback);
                    })//();

                    vm.pageChanged = function () {
                        blockUI.start();
                        productDataService.getAllCustomers(vm.criteria, successCallback, errorCallback);
                    };


                    vm.dictionaries={
                        productcategories:[
                            {key: 'G1', value: '1对1',checked: false ,childcategories:[{key: 'G11', value: '1对1child',checked: false },{key: 'G12', value: '1对1child',checked: false }]},
                            {key: 'G2', value: '测试',checked: false ,childcategories:[]},
                        ]
                        ,cooperativerelations:[
                            {key: 'G1', value: '1对1',checked: true},
                        ]
                        ,grades:[
                            {key: 'G1', value: '1对1',checked: false },
                        ]
                        ,subjects:[
                            {key: 'G1', value: '1对1',checked: false },
                        ]
                        ,courselevels:[
                            {key: 'G1', value: '1对1',checked: false },
                        ]
                        ,tutoringtypes:[
                            {key: 'G1', value: '1对1',checked: false },
                        ]
                        ,courseseasons:[
                            {key: 'G1', value: '1对1',checked: false },
                        ]
                        ,classtypes:[
                            {key: 'G1', value: '1对1',checked: false },
                        ]
                        ,unitpricetypes:[
                            {key: 'G1', value: '1对1',checked: false },
                        ]
                    };

                    vm.criteria= {
                        selectedProdutCategories:[],
                        selectedCooperativeRelations:[],
                        selectedGrades:[],
                        selectedSubjects:[],
                        selectedCourseLevels:[],
                        selectedTutoringTypes:[],
                        selectedCourseSeasons:[],
                        selectedClassTypes:[],
                        selectedUnitPriceTypes:[],
                    };

                    vm.select = function (category,item, eventargs) {
                        mcs.util.setSelectedItems(vm.criteria[category],item, eventargs);
                    };


                    vm.createProduct= function () {
                        $state.go('ppts.productAdd');
                    };

                    vm.modifyProduct= function (id) {
                        $state.go('ppts.productEdit',{id:id});
                    };

                    vm.copyProduct= function (id) {
                        $state.go('ppts.productCopy',{id:id});
                    };

                    vm.close = function (category, dictionary) {
                        vm.criteria[category].length = 0;

                        vm.dictionaries[dictionary].forEach(function(item,value) {
                            item.checked = false;
                        });
                    };



                }]);
        });

