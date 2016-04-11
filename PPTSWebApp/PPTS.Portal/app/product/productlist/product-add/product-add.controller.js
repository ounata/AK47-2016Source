define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
            product.registerController('productAddController', [
                '$scope', '$location', 'uibDateParser', '$state', '$stateParams', 'dataSyncService', 'productDataService',
                function ($scope, $location, uibDateParser, $state, $stateParams, dataSyncService, productDataService) {
                    var vm = this;

                    var id = $stateParams.id;
                    //var loadtype = $stateParams.type || 'onetoone';
                    var loadtype = $location.url().replace('/ppts/product/add/', '');
                    
                    $scope.loadtype = loadtype;

                    $scope.tag = '新增';

                    $scope.tabs = {
                        'onetoone': {
                            index: 1, title: '一对一', active: false, templateUrl: 'product-add-onetoone.html'
                            , description: ''
                            , example: '高中三年级30分钟3A常规'
                        },
                        'classgroup': {
                            index: 2, title: '班组', active: false, templateUrl: 'product-add-classgroup.html'
                            , description: ''
                            , example: '2016年高中三年级语文秋季高考保过长期小班'
                        },
                        'youxue': {
                            index: 3, title: '游学', active: false, templateUrl: 'product-add-other.html'
                            , description: '常规国际游学：学大总部独立运营，收入完全归学大所有的游学产品<br/>合作国际游学：学大与合作商一起运营，收入与合作商按比例分账的游学产品<br/>常规国内游学：学大总部、各大区独立运营，收入完全归学大所有的游学产品<br/>合作国内游学：学大与合作商一起运营，收入与合作商按比例分账的游学产品'
                            , example: ''
                        },
                        'other': {
                            index: 4, title: '其他', active: false, templateUrl: 'product-add-other.html'
                            , description: '代理招生产品：学大与合作商一起运营，收入与合作商按比例分账，不在系统里排课、不算课时收入的产品。如，合作方提供场地、师资，学大提供学员。<br/>常规实物产品：学大独立运营，收入完全归学大所有的实物产品，如大区、分公司自主研发的教材、试卷、文具、学习工具（如PAD）等。<br/>合作实物产品：学大与合作商一起运营，收入与合作商按比例分账的实物产品。<br/>常规虚拟产品：学大独立运营，收入完全归学大所有的非实物产品，如网络课程、电子书等。<br/>合作虚拟产品：学大与合作商一起运营，收入与合作商按比例分账的非实物产品。<br/>常规费用产品：学大独立运营，学员缴纳金额完全归学大所有的各项杂费类产品，如全脱住宿费、伙食费等。<br/>合作费用产品：学大与合作商一起运营，学员缴纳金额需与合作商按比例分账的服务费用产品。如代收的报名费、服务费等。<br/>常规其它产品：学大总部独立运营，收入完全归学大所有的其它产品'
                            , example: ''
                        },
                        'wukeshou': {
                            index: 5, title: '无课收合作', active: false, templateUrl: 'product-add-other.html'
                            , description: ''
                            , example: ''
                        },
                    };
                    $scope.tabs[loadtype].active = true;
                    $scope.title = $scope.tabs[loadtype].title;
                    $scope.description = $scope.tabs[loadtype].description;
                    $scope.example = $scope.tabs[loadtype].example;
                    



                    vm.m = {
                        onetoone: {
                            product: {},
                            exOfCourse: {},
                            salaryRules:[],
                        },
                        classgroup: {
                            product: {},
                            exOfCourse: {},
                            salaryRules: [],
                        },
                        youxue: {
                            product: {},
                            exOfCourse: {},
                            salaryRules: [],
                        },
                        wukeshou: {
                            product: {},
                            exOfCourse: {},
                            salaryRules: [],
                        },
                        other: {
                            product: {},
                            exOfCourse: {},
                            salaryRules: [],
                        },
                    };

                    




                    //加载信息
                    productDataService.addProductView($scope.tabs[loadtype].index, function (entity) {

                        console.warn(entity);

                        vm.dictionaries = entity.dictionaries;

                        vm.m[loadtype].categoryType = entity.categoryType;
                        vm.m[loadtype].product = entity.product;
                        vm.m[loadtype].exOfCourse = entity.exOfCourse || vm.m[loadtype].exOfCourse;
                        vm.m[loadtype].salaryRules = entity.salaryRules || vm.m[loadtype].salaryRules;
                        vm.m[loadtype].catalogs = entity.catalogs;

                        vm.m[loadtype].product.startDate = new Date(vm.m[loadtype].product.startDate);
                        vm.m[loadtype].product.endDate = new Date(vm.m[loadtype].product.endDate);

                        vm.m[loadtype].exOfCourse.periodDuration = 5;

                        dataSyncService.injectDictData(mcs.util.mapping(entity.catalogs, { key: 'catalog', value: 'catalogName' }, 'CategoryCustom'));
                        ppts.config.dictMappingConfig["categoryCustom"] = "c_codE_ABBR_CategoryCustom";

                        console.warn(vm.m[loadtype])

                        console.log(ppts.dict);

                        $scope.$broadcast('dictionaryReady');

                        
                        //console.log($timeout);

                        //$timeout(function () { $scope.$broadcast("dictionaryReady"); }, 100);

                        //console.log(this);

                        

                        

                        

                        //vm.m[loadtype].product.productID = 12;

                        


                    });

                    //vm.save = function () {

                    //    //console.log(vm.m); return;


                    //    productDataService.saveProduct(vm.m[loadtype], function (entity) {
                    //        //vm.m[loadtype].product.productID = entity.product.productID;

                    //        console.log(entity);

                    //        vm.m[loadtype].product = entity.product;
                    //        vm.m[loadtype].exOfCourse = entity.exOfCourse;
                    //        vm.m[loadtype].salaryRules = entity.salaryRules;

                    //    });
                    //}

                    function getTextByTag(tag,key) {
                        var dataGroup = ppts.dict[ppts.config.dictMappingConfig[tag]]||[];
                        for (var index in dataGroup) {
                            if (dataGroup[index].key == key) { return dataGroup[index].value; }
                        }
                        return '';
                    }
                    

                    vm.submit = function () {
                        var productName = '';
                        switch (vm.m[loadtype].categoryType) {
                            case 1:
                                var grade = getTextByTag('grade', vm.m[loadtype].product.grade);
                                var subject = getTextByTag('subject', vm.m[loadtype].product.subject);
                                var duration = getTextByTag('duration', vm.m[loadtype].exOfCourse.periodDuration);
                                var courseLevel = getTextByTag('courseLevel', vm.m[loadtype].exOfCourse.courseLevel);
                                var coachType = getTextByTag('coachType', vm.m[loadtype].exOfCourse.coachType);
                                productName = grade + ' ' + subject + ' ' + duration + ' ' + courseLevel + ' ' + coachType;
                                break;
                            case 2:
                                var grade = getTextByTag('grade', vm.m[loadtype].product.grade);
                                var subject = getTextByTag('subject', vm.m[loadtype].product.subject);
                                var duration = getTextByTag('duration', vm.m[loadtype].exOfCourse.periodDuration);
                                var courseLevel = getTextByTag('courseLevel', vm.m[loadtype].exOfCourse.courseLevel);
                                var coachType = getTextByTag('coachType', vm.m[loadtype].exOfCourse.coachType);
                                var season = getTextByTag('season', vm.m[loadtype].product.season);
                                productName = grade + ' ' + subject + ' ' + duration + ' ' + courseLevel + ' ' + coachType + ' ' + season;
                                break;
                        }

                        vm.m[loadtype].product.productName = productName;

                        console.log(vm.m[loadtype])
                        //return;
                        productDataService.submitProduct(vm.m[loadtype], function () { $state.go('ppts.product'); });
                    }

                    vm.submitAndContinue = function () {
                        productDataService.submitProduct(vm.m[loadtype], function () { $state.go('ppts.productAdd', { type: vm.product.nav }); });
                    }



                    vm.isPartner = function () {
                        for (var c in vm.m[loadtype].catalogs) {
                            if (vm.m[loadtype].catalogs[c].catalog == vm.m[loadtype].product.catalog && vm.m[loadtype].catalogs[c].hasPartner == 1) { return true; }
                        }
                        return false;
                    }


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


                    ////查询条件
                    //vm.criteria = {
                    //    name: '',
                    //    productCode: '',
                    //    startDate: '',
                    //    endDate: '',
                    //    pagedParam: null
                    //};

                    //var successCallback = function (result) {
                    //    vm.pagedList = result.queryResult;
                    //    vm.dictionaries = vm.dictionaries || result.dictionaries;
                    //    vm.criteria.pagedParam = result.queryResult.pagedParam;
                    //    blockUI.stop();
                    //};

                    //var errorCallback = function (result) {

                    //};

                    //vm.search = (function () {
                    //    //blockUI.start();
                    //    productDataService.getAllCustomers(vm.criteria, successCallback, errorCallback);
                    //})//();

                    //vm.pageChanged = function () {
                    //    blockUI.start();
                    //    productDataService.getAllCustomers(vm.criteria, successCallback, errorCallback);
                    //};


                    //vm.dictionaries = {
                    //    productcategories: [
                    //        { key: 'G1', value: '1对1', checked: false, childcategories: [{ key: 'G11', value: '1对1child', checked: false }, { key: 'G12', value: '1对1child', checked: false }] },
                    //        { key: 'G2', value: '测试', checked: false, childcategories: [] },
                    //    ]
                    //    , cooperativerelations: [
                    //        { key: 'G1', value: '1对1', checked: true },
                    //    ]
                    //    , grades: [
                    //        { key: 'G1', value: '1对1', checked: false },
                    //    ]
                    //    , subjects: [
                    //        { key: 'G1', value: '1对1', checked: false },
                    //    ]
                    //    , courselevels: [
                    //        { key: 'G1', value: '1对1', checked: false },
                    //    ]
                    //    , tutoringtypes: [
                    //        { key: 'G1', value: '1对1', checked: false },
                    //    ]
                    //    , courseseasons: [
                    //        { key: 'G1', value: '1对1', checked: false },
                    //    ]
                    //    , classtypes: [
                    //        { key: 'G1', value: '1对1', checked: false },
                    //    ]
                    //    , unitpricetypes: [
                    //        { key: 'G1', value: '1对1', checked: false },
                    //    ]
                    //    , durations: [
                    //        { key: 'G1', value: '1对1', checked: false },
                    //    ]
                    //    , levels: [
                    //        { key: 'G1', value: '1对1', checked: false },
                    //    ]
                    //    , customerdiscountlevel: [
                    //        { key: 'G1', value: '1对1', checked: vm.product.onetoone.selectedCustomerDiscountLevel == 'G1' },
                    //        { key: 'G2', value: '1对1', checked: vm.product.onetoone.selectedCustomerDiscountLevel == 'G2' },
                    //    ]
                    //    , isusediscount: [
                    //        { key: 'G1', value: '1对1', checked: vm.product.onetoone.selectedIsUseDiscount == 'G1' },
                    //        { key: 'G2', value: '1对1', checked: vm.product.onetoone.selectedIsUseDiscount == 'G2' },
                    //    ]
                    //};



                    //vm.criteria = {
                    //    selectedProdutCategories: [],
                    //    selectedCooperativeRelations: [],
                    //    selectedGrades: [],
                    //    selectedSubjects: [],
                    //    selectedCourseLevels: [],
                    //    selectedTutoringTypes: [],
                    //    selectedCourseSeasons: [],
                    //    selectedClassTypes: [],
                    //    selectedUnitPriceTypes: [],
                    //};

                    //vm.select = function (category, item, eventargs) {
                    //    if (category == "selectedCustomerDiscountLevel") {
                    //        vm.product.onetoone.selectedCustomerDiscountLevel = item;
                    //    }

                    //    //mcs.util.setSelectedItems(vm.criteria[category],item, eventargs);
                    //};



                    //vm.createProduct = function () {
                    //    $state.go('ppts.productAdd');
                    //};



                    //vm.close = function (category, dictionary) {
                    //    vm.criteria[category].length = 0;

                    //    vm.dictionaries[dictionary].forEach(function (item, value) {
                    //        item.checked = false;
                    //    });
                    //};



                }]);
        });

