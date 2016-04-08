define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
            product.registerController('productEditController', [
                '$scope', '$state','$stateParams','blockUI', 'productDataService',
                function ($scope, $state,$stateParams, blockUI, productDataService) {
                    var vm = this;

                    $scope.tag = '修改';

                    var loadtype = $stateParams.type;
                    var id = $stateParams.id;

                    if(undefined != id){ }
                    
                    $scope.load = function(){
                        switch(loadtype){
                            case 'classgroup': return '/app/product/productlist/product-add/product-add-classgroup.html'; break;
                            case 'other': return '/app/product/productlist/product-add/product-add-other.html'; break;
                            case 'onetoone': 
                            default:
                            return '/app/product/productlist/product-add/product-add-onetoone.html'; break;
                        }
                    };


                    vm.product= {
                        nav:'onetoone',
                        onetoone:{
                            selectedGrade:'年级',
                            selectedDuation:'时长',
                            selectedLevel:'级别',
                            description:'测试',
                            price:1,
                            selltime:new Date('2000-01-01'),
                            company:'1',
                            campus:'1',
                            selectedCustomerDiscountLevel:'G2',
                            selectedIsUseDiscount:'G2',
                        },
                        classgroup:{
                        },

                    };




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
                        ,durations:[
                            {key: 'G1', value: '1对1',checked: false },
                        ]
                        ,levels:[
                            {key: 'G1', value: '1对1',checked: false },
                        ]
                        ,customerdiscountlevel:[
                            {key: 'G1', value: '1对1',checked: vm.product.onetoone.selectedCustomerDiscountLevel == 'G1'},
                            {key: 'G2', value: '1对1',checked: vm.product.onetoone.selectedCustomerDiscountLevel == 'G2'},
                        ]
                        ,isusediscount:[
                            {key: 'G1', value: '1对1',checked: vm.product.onetoone.selectedIsUseDiscount== 'G1'},
                            {key: 'G2', value: '1对1',checked: vm.product.onetoone.selectedIsUseDiscount== 'G2'},
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
                        if(category == "selectedCustomerDiscountLevel"){
                            vm.product.onetoone.selectedCustomerDiscountLevel=item;
                        }

                        //mcs.util.setSelectedItems(vm.criteria[category],item, eventargs);
                    };


                    vm.createProduct= function () {
                        $state.go('ppts.productAdd');
                    };

                    vm.createTabProduct= function ($event) {
                        var currentBtn = $($event.target);

                        /*
                        $(".active").removeClass("active");
                        currentBtn.parent().addClass("active");
                        vm.product.nav=currentBtn.data('nav'); 
                        */

                        $state.go('ppts.productAdd',{type:currentBtn.data('nav')});
                    };

                    vm.close = function (category, dictionary) {
                        vm.criteria[category].length = 0;

                        vm.dictionaries[dictionary].forEach(function(item,value) {
                            item.checked = false;
                        });
                    };



                }]);
        });

