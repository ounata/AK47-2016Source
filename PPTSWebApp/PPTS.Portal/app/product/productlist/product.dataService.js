define([ppts.config.modules.product], function (product) {

    product.registerFactory('productDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.productApiBaseUrl + 'api/products/:operation/:id',
            { operation: '@operation' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.addProductView = function (type,callback) {
            resource.query({ operation: 'createProduct', type:type }, function (product) { if (callback) { callback(product); } });
        }

        resource.copyProductView = function (id, callback) {
            resource.query({ operation: 'copyProduct', id: id }, function (product) { if (callback) { callback(product); } });
        }

        //resource.saveProduct = function (vmodel, callback) {
        //    console.log(vmodel)
        //    resource.post({ operation: 'saveProduct' }, vmodel, function (product) { if (callback) { callback(product); } });
        //}

        resource.delProduct = function (ids, callback) {
            resource.post({ operation: 'delProduct' }, ids, function (entity) { if (callback) { callback(entity); } });
        }

        resource.stopProduct = function (id, callback) {
            resource.query({ operation: 'stopSellProduct', id: id }, function (entity) { if (callback) { callback(entity); } });
        }

        resource.delayProduct = function (data, callback) {
            resource.post({ operation: 'delayProduct' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        resource.submitProduct = function (vmodel, callback) {
            resource.post({ operation: 'submitProduct' }, vmodel, function (product) { if (callback) { callback(product); } });
        }


        resource.getProduct = function (id, callback) {
            resource.query({ operation: 'getProduct', id: id }, function (product) { if (callback) { callback(product); } });
        }



        resource.getAllProducts = function (criteria, success, error) {
            resource.post({ operation: 'getAllProducts' }, criteria, success, error);
        }

        resource.getPagedProducts = function (criteria, success, error) {
            resource.post({ operation: 'getPagedProducts' }, criteria, success, error);
        }


        resource.getProducts = function (ids, callback) {
            resource.post({ operation: 'GetProducts' }, ids, function (entity) { if (callback) { callback(entity); } });
        }

        //------------------------------班组产品 插班 订购-----------------------------------
        resource.getClassGroupProducts = function (criteria, callback) {
            resource.post({ operation: 'GetClassGroupProducts' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }

        resource.getPagedClassGroupProducts = function (criteria, callback) {
            resource.post({ operation: 'GetPagedClassGroupProducts' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }
        //-----------------------------------------------------------------------------------

        resource.test = function () {
            resource.post({ operation: 'TestPost' }, { TDate: '2015-01-01', Name: 'test' });
        }

        return resource;
    }]);



});


function addProduct($scope, $location, $state, uibDateParser, $stateParams, dataSyncService, productDataService) {
    var loadtype = $location.url().replace('/ppts/product/add/', '');
    var tag = "新增";
    addcopy($scope, $location, $state, uibDateParser, $stateParams, dataSyncService, productDataService, loadtype, tag);
}

function addcopyProduct($scope, $location, $state, uibDateParser, $stateParams, dataSyncService, productDataService, loadtype, tag) {
    var vm = this;
    var id = $stateParams.id;
    //var loadtype = $location.url().replace('/ppts/product/add/', '');
    function getTextByTag(tag, key) {
        var dataGroup = ppts.dict[ppts.config.dictMappingConfig[tag]] || [];
        for (var index in dataGroup) {
            if (dataGroup[index].key == key) { return dataGroup[index].value; }
        }
        return '';
    }

    $scope.loadtype = loadtype;
    $scope.tag = tag;
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
            index: 5, title: '代理招生', active: false, templateUrl: 'product-add-other.html'
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

    vm.calcCharCount = function () {
        var product = vm.m[loadtype].product;
        return (product.productMemo && product.productMemo.length) || 0;
    };

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

    //----------------------------------------------------

    //加载信息
    productDataService.addProductView($scope.tabs[loadtype].index, function (entity) {

        console.warn(entity);

        vm.dictionaries = entity.dictionaries;

        vm.m[loadtype].categoryType = entity.categoryType;
        vm.m[loadtype].product = entity.product;
        vm.m[loadtype].exOfCourse = entity.exOfCourse || vm.m[loadtype].exOfCourse;
        vm.m[loadtype].salaryRules = entity.salaryRules || vm.m[loadtype].salaryRules;
        vm.m[loadtype].catalogs = entity.catalogs;

        dataSyncService.injectDictData(mcs.util.mapping(entity.catalogs, { key: 'catalog', value: 'catalogName' }, 'CategoryCustom'));
        ppts.config.dictMappingConfig["categoryCustom"] = "c_codE_ABBR_CategoryCustom";

        dataSyncService.injectPageDict(['ifElse']);

        console.warn(vm.m[loadtype])

        console.log(ppts.dict);

        $scope.$broadcast('dictionaryReady');

    });


    vm.init = function () {
        var discountInfos = [{ key: '0', value: '不享受折扣' }, { key: '1', value: '享受折扣' }, ];
        dataSyncService.injectDictData(mcs.util.mapping(discountInfos, { key: 'key', value: 'value' }, 'DiscountInfo'));
        ppts.config.dictMappingConfig["discountInfo"] = "c_codE_ABBR_DiscountInfo";

        var promotionInfos = [{ key: '0', value: '只享受客户折扣' }, { key: '1', value: '只享受订购时特殊优惠' }, ];
        dataSyncService.injectDictData(mcs.util.mapping(promotionInfos, { key: 'key', value: 'value' }, 'PromotionInfo'));
        ppts.config.dictMappingConfig["promotionInfo"] = "c_codE_ABBR_PromotionInfo";

        var incomeBelongings = [{ key: '1', value: '课收归学员所在机构' }, { key: '0', value: '课收归授课教师岗位所在机构' }, ];
        dataSyncService.injectDictData(mcs.util.mapping(incomeBelongings, { key: 'key', value: 'value' }, 'IncomeBelonging'));
        ppts.config.dictMappingConfig["incomeBelonging"] = "c_codE_ABBR_IncomeBelonging";

    };
    vm.init();


    //----------------------------------------------------

}