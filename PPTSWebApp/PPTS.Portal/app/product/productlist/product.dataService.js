define([ppts.config.modules.product], function (helper) {

    helper.registerFactory('productDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.productApiBaseUrl + 'api/products/:operation/:id',
            { operation: '@operation' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.addProductView = function (type, callback) {
            resource.query({ operation: 'createProduct', type: type }, function (product) { if (callback) { callback(product); } });
        }

        resource.copyProductView = function (id, callback) {
            resource.query({ operation: 'copyProduct', id: id }, function (product) { if (callback) { callback(product); } });
        }

        //resource.saveProduct = function (vmodel, callback) {
        //    //console.log(vmodel)
        //    resource.post({ operation: 'saveProduct' }, vmodel, function (product) { if (callback) { callback(product); } });
        //}

        resource.delProduct = function (ids, callback) {
            resource.post({ operation: 'delProduct' }, ids, function (entity) { if (callback) { callback(entity); } });
        }

        resource.stopProduct = function (ids, callback) {
            resource.post({ operation: 'stopSellProduct' }, { ids: ids }, function (entity) { if (callback) { callback(entity); } });
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

        /*
            resource.getProducts = function (ids, callback) {
                resource.post({ operation: 'GetProducts' }, ids, function (entity) { if (callback) { callback(entity); } });
            }
        */

        //------------------------------班组产品 插班 订购-----------------------------------
        resource.getClassGroupProducts = function (criteria, callback) {
            resource.post({ operation: 'GetClassGroupProducts' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }

        resource.getPagedClassGroupProducts = function (criteria, callback) {
            resource.post({ operation: 'GetPagedClassGroupProducts' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }
        //-----------------------------------------------------------------------------------

        resource.getProductByWorkflow = function (data, callback) {
            resource.post({ operation: 'GetProductByWorkflow' }, data, callback);
        }



        return resource;
    }]);

    helper.registerValue('productDictionary', {
        categories: {
            'onetoone': {
                index: 1, title: '一对一', active: false, templateUrl: 'product-add-onetoone.html', routeSuffix: 'onetoone', productCodePrefix: 'A'
                , description: ''
                , example: '高中三年级30分钟3A常规'
            },
            'classgroup': {
                index: 2, title: '班组', active: false, templateUrl: 'product-add-classgroup.html', routeSuffix: 'classgroup', productCodePrefix: 'B'
                , description: ''
                , example: '2016年高中三年级语文秋季高考保过长期小班'
            },
            'youxue': {
                index: 3, title: '游学', active: false, templateUrl: 'product-add-other.html', routeSuffix: 'youxue', productCodePrefix: 'C'
                , description: '常规国际游学：学大总部独立运营，收入完全归学大所有的游学产品<br/>合作国际游学：学大与合作商一起运营，收入与合作商按比例分账的游学产品<br/>常规国内游学：学大总部、各大区独立运营，收入完全归学大所有的游学产品<br/>合作国内游学：学大与合作商一起运营，收入与合作商按比例分账的游学产品'
                , example: ''
                , priceTip: '元/期'
            },
            'other': {
                index: 4, title: '其它', active: false, templateUrl: 'product-add-other.html', routeSuffix: 'other', productCodePrefix: 'Z'
                , description: '常规其它产品：学大总部独立运营，收入完全归学大所有的其它产品'
                , example: ''
                , priceTip: '元/份'
            },
            'dailizhaosheng': {
                index: 5, title: '代理招生', active: false, templateUrl: 'product-add-other.html', routeSuffix: 'other', productCodePrefix: 'D'
                , description: '代理招生产品：学大与合作商一起运营，收入与合作商按比例分账，不在系统里排课、不算课时收入的产品。如，合作方提供场地、师资，学大提供学员。'/*<br/>常规实物产品：学大独立运营，收入完全归学大所有的实物产品，如大区、分公司自主研发的教材、试卷、文具、学习工具（如PAD）等。*/
                , example: ''
                , priceTip: '元/期'
            },
            'shiwu': {
                index: 41, title: '实物', active: false, templateUrl: 'product-add-other.html', routeSuffix: 'other', productCodePrefix: 'E'
                , description: '常规实物产品：学大独立运营，收入完全归学大所有的实物产品，如大区、分公司自主研发的教材、试卷、文具、学习工具（如PAD）等。<br/>合作实物产品：学大与合作商一起运营，收入与合作商按比例分账的实物产品。'
                , example: ''
                , priceTip: '元/期'
            },
            'xuni': {
                index: 42, title: '虚拟', active: false, templateUrl: 'product-add-other.html', routeSuffix: 'other', productCodePrefix: 'F'
                , description: '常规虚拟产品：学大独立运营，收入完全归学大所有的非实物产品，如网络课程、电子书等。<br/>合作虚拟产品：学大与合作商一起运营，收入与合作商按比例分账的非实物产品。'
                , example: ''
                , priceTip: '元/期'
            },
            'feiyong': {
                index: 43, title: '费用', active: false, templateUrl: 'product-add-other.html', routeSuffix: 'other', productCodePrefix: 'G'
                , description: '常规费用产品：学大独立运营，学员缴纳金额完全归学大所有的各项杂费类产品，如全脱住宿费、伙食费等。<br/>合作费用产品：学大与合作商一起运营，学员缴纳金额需与合作商按比例分账的服务费用产品。如代收的报名费、服务费等。'
                , example: ''
                , priceTip: '元/期'
            },
            'liuxue': {
                index: 44, title: '留学', active: false, templateUrl: 'product-add-other.html', routeSuffix: 'other', productCodePrefix: 'H'
                , description: '常规其它产品：学大总部独立运营，收入完全归学大所有的其它产品'
                , example: ''
                , priceTip: '元/期'
            },
        }
    });

    helper.registerDirective("productpartnerDirective", function () {
        return {
            template: ''
+ '<div >'
+ '<label class="control-label">合作方</label>'
+ '<mcs-input model="vm.m[loadtype].product.partnerName" css="input-width-100" uib-popover="合作方名称" placeholder="合作方简称" maxlength="20" required />'
+ '</div>'
+ '<div ng-class="{\'has-error\':vm.m[loadtype].product.error}">'
+ '<label class="control-label">占订购金额的</label>'
+ '<mcs-input model="vm.m[loadtype].product.partnerRatio_watch" css="input-width-100" uib-popover="分成比例" placeholder="分成比例" type="number" validate="vm.floatValidate(vm.m[loadtype].product.partnerRatio_watch,vm.m[loadtype].product)" between="0,100" required /><label class="control-label">{{ vm.m[loadtype].product.error }}</label>'
+ '<label class="control-label">'
+ '%, 学大占订购金额的{{ (1- vm.m[loadtype].product.partnerRatio)*100 |number:2  }}% 则 合作单价 为{{ vm.m[loadtype].product.productPrice* (1- vm.m[loadtype].product.partnerRatio ) }}元/期。'
+ '</label>'
+ '</div>'
+ '<span class="text-left info">合作单价：用于确认课时收入的单价</span>'
        };
    });

    helper.registerDirective("productsalaryDirective", function () {
        return {
            template: ''
+ '<div class="form-control-static mcs-padding-0 ">'
+ '<label class="control-label label-100">咨询师提成：</label>'
+ '<br />'
+ '<ppts-radiobutton-group category="commission_custom" model="vm.zxs" />'
+ '<div ng-class="{\'has-error\':vm.m[loadtype].salaryRules[0].error}"><mcs-input model="vm.m[loadtype].salaryRules[0].moneyPerPeriod" css="input-width-120" ng-disabled="vm.zxs==0" ng-click="vm.salaryMonitor(vm.m[loadtype].salaryRules[0].moneyPerHour)" ng-change="vm.m[loadtype].salaryRules[0].moneyPerHour=\'\'" min="0" validate="vm.salaryValidate(vm.m[loadtype].salaryRules[0])" required  />'
+ '<label class="control-label">元，或每课时</label>'
+ '<mcs-input model="vm.m[loadtype].salaryRules[0].moneyPerHour" css="input-width-120" ng-disabled="vm.zxs==0" ng-click="vm.salaryMonitor(vm.m[loadtype].salaryRules[0].moneyPerPeriod)" ng-change="vm.m[loadtype].salaryRules[0].moneyPerPeriod=\'\'"  min="0" validate="vm.salaryValidate(vm.m[loadtype].salaryRules[0])" required /><label class="control-label">元核算。{{ vm.m[loadtype].salaryRules[0].error }} </label> '
+ '</div></div>'
+ '<div class="form-control-static mcs-padding-0">'
+ '<label class="control-label label-100">学管师提成：</label>'
+ '<br />'
+ '<ppts-radiobutton-group category="commission_custom" model="vm.xgs" />'
+ '<div ng-class="{\'has-error\':vm.m[loadtype].salaryRules[1].error}"><mcs-input model="vm.m[loadtype].salaryRules[1].moneyPerPeriod" css="input-width-120" ng-disabled="vm.xgs==0" ng-click="vm.salaryMonitor(vm.m[loadtype].salaryRules[1].moneyPerHour)" ng-change="vm.m[loadtype].salaryRules[1].moneyPerHour=\'\'"  min="0" validate="vm.salaryValidate(vm.m[loadtype].salaryRules[1])" required />'
+ '<label class="control-label">元，或每课时</label>'
+ '<mcs-input model="vm.m[loadtype].salaryRules[1].moneyPerHour" css="input-width-120" ng-disabled="vm.xgs==0" ng-click="vm.salaryMonitor(vm.m[loadtype].salaryRules[1].moneyPerPeriod)" ng-change="vm.m[loadtype].salaryRules[1].moneyPerPeriod=\'\'"  min="0" validate="vm.salaryValidate(vm.m[loadtype].salaryRules[1])" required /><label class="control-label">元核算。{{ vm.m[loadtype].salaryRules[1].error }} </label>'
+ '</div></div>'
+ '<div class="form-control-static mcs-padding-0">'
+ '<label class="control-label label-100">教师课时收入：</label>'
+ '<br />'
+ '<ppts-radiobutton-group category="commission_custom" model="vm.js" />'
+ '<div ng-class="{\'has-error\':vm.m[loadtype].salaryRules[2].error}"><mcs-input model="vm.m[loadtype].salaryRules[2].moneyPerPeriod" css="input-width-120" ng-disabled="vm.js==0" ng-click="vm.salaryMonitor(vm.m[loadtype].salaryRules[2].moneyPerHour)" ng-change="vm.m[loadtype].salaryRules[2].moneyPerHour=\'\'"  min="0" validate="vm.salaryValidate(vm.m[loadtype].salaryRules[2])" required />'
+ '<label class="control-label">元，或每课时</label>'
+ '<mcs-input model="vm.m[loadtype].salaryRules[2].moneyPerHour" css="input-width-120" ng-disabled="vm.js==0" ng-click="vm.salaryMonitor(vm.m[loadtype].salaryRules[2].moneyPerPeriod)" ng-change="vm.m[loadtype].salaryRules[2].moneyPerPeriod=\'\'"  min="0" validate="vm.salaryValidate(vm.m[loadtype].salaryRules[2])" required /><label class="control-label">元核算。{{ vm.m[loadtype].salaryRules[2].error }} </label>'
+ '</div></div>'

        };
    });


    helper.registerFactory('productEditHelper', ['dataSyncService', 'mcsDialogService', 'mcsValidationService', 'productDictionary', '$location',
        function (dataSyncService, mcsDialogService, mcsValidationService, productDictionary, $location) {
            //console.log(productDictionary)
            var handler = {};

            handler.init = function ($scope, vm, loadtype, productDataService, $state, loadHandler) {

                mcsValidationService.init($scope);

                $scope.loadtype = loadtype;
                $scope.tag = '新增';
                $scope.tabs = productDictionary.categories;

                $scope.tabs[loadtype].active = true;
                $scope.title = $scope.tabs[loadtype].title;
                $scope.description = $scope.tabs[loadtype].description;
                $scope.example = $scope.tabs[loadtype].example;

                vm.m = {};
                vm.m[loadtype] = {
                    product: {},
                    exOfCourse: {},
                    salaryRules: [],
                };

                vm.floatValidate = function (value, entity) {
                    if (value) {
                        if (!/^\d+(\.\d{1,2})?$/.test(value)) {
                            entity['error'] = '输入有误！';
                        } else {
                            entity['error'] = null;
                        }
                    }
                };

                vm.salaryValidate = function (salaryRule) {
                    var validateValue = salaryRule.moneyPerHour || salaryRule.moneyPerPeriod;
                    if (validateValue) {
                        if (!/^\d{1,7}(\.\d{1,2})?$/.test(validateValue)) {
                            salaryRule['error'] = '输入有误，金额最多允许输入7位数字，支持小数点后2位！';
                        } else {
                            salaryRule['error'] = null;
                        }
                    }
                    //console.log(salaryRule)
                };

                

                function getTextByTag(tag, key) {
                    var dataGroup = ppts.dict[ppts.config.dictMappingConfig[tag]] || [];
                    for (var index in dataGroup) {
                        if (dataGroup[index].key == key) { return dataGroup[index].value; }
                    }
                    return '';
                }

                vm.calcCharCount = function () {
                    var product = vm.m[loadtype].product;
                    return (product.productMemo && product.productMemo.length) || 0;
                };
                vm.submit = function () {
                    //console.log(vm.m[loadtype])
                    if (!mcsValidationService.run($scope)) { return; }

                    if (vm.m[loadtype].categoryType < 3) {
                        var productName = '';
                        switch (vm.m[loadtype].categoryType) {
                            case 1:
                                var grade = getTextByTag('grade', vm.m[loadtype].product.grade);
                                var subject = getTextByTag('subject', vm.m[loadtype].product.subject);
                                var duration = getTextByTag('duration', vm.m[loadtype].exOfCourse.periodDuration);
                                var courseLevel = getTextByTag('courseLevel', vm.m[loadtype].exOfCourse.courseLevel);
                                var coachType = getTextByTag('coachType', vm.m[loadtype].exOfCourse.coachType);
                                productName = grade + '-' + subject + '-' + duration + '-' + courseLevel + '-' + coachType;
                                break;
                            case 2:
                                var grade = getTextByTag('grade', vm.m[loadtype].product.grade);
                                var subject = getTextByTag('subject', vm.m[loadtype].product.subject);
                                var duration = getTextByTag('duration', vm.m[loadtype].exOfCourse.periodDuration);
                                var courseLevel = getTextByTag('courseLevel', vm.m[loadtype].exOfCourse.courseLevel);
                                var coachType = getTextByTag('coachType', vm.m[loadtype].exOfCourse.coachType);
                                var season = getTextByTag('season', vm.m[loadtype].product.season);
                                productName = grade + '-' + subject + '-' + duration + '-' + courseLevel + '-' + coachType + '-' + season;
                                break;
                        }
                        vm.m[loadtype].product.productName = productName;
                    }

                    vm.m[loadtype].productCodePrefix = productDictionary.categories[loadtype].productCodePrefix;

                    productDataService.submitProduct(vm.m[loadtype], function () { $state.go('ppts.product'); });
                };
                //vm.submitAndContinue = function () {
                //    productDataService.submitProduct(vm.m[loadtype], function () { $state.go('ppts.productAdd', { type: vm.product.nav }); });
                //};
                vm.isPartner = function () {
                    for (var c in vm.m[loadtype].catalogs) {
                        if (vm.m[loadtype].catalogs[c].catalog == vm.m[loadtype].product.catalog && vm.m[loadtype].catalogs[c].hasPartner == 1) { return true; }
                    }
                    return false;
                };
                vm.creator = function () { return ppts.user.name; };



                var init = (function () {

                    if (loadtype == "onetoone" || loadtype == "classgroup") {
                        vm.salaryMonitor = function (value) {
                            if (value) {
                                mcsDialogService.info({ title: '提示', message: '课时和小时只能选择一个，可删除已填金额后再填写。' });
                            }
                        };
                    }

                    if (loadtype == "classgroup") {
                        $scope.$watch("vm.m.classgroup.exOfCourse.lessonCount", function (o, n) {
                            if (o == 1) {
                                mcsDialogService.info({ title: '温馨提示', message: '总课次为1时，课时收入将一次性确认，请谨慎操作。', settings: { backdrop: 'static' } });
                            }
                        });
                    }

                    if (productDictionary.categories[loadtype].routeSuffix == "other") {
                        $scope.$watch('vm.rdOrg', function (n, o) {
                            if (n) {
                                vm.m[loadtype].product.rdOrgID = n.branchId;
                                vm.m[loadtype].product.rdOrgName = n.branchName;
                            }
                        });
                    }

                    $scope.$watch('vm.m[loadtype].product.partnerRatio_watch', function (n, o) {
                        if (n) {
                            vm.m[loadtype].product.partnerRatio = n / 100;
                        } else {
                            vm.m[loadtype].product.partnerRatio = 0;
                        }
                    });

                    var discountInfos = [{ key: '0', value: '不享受折扣' }, { key: '1', value: '享受折扣' }, ];
                    dataSyncService.injectDictData(mcs.util.mapping(discountInfos, { key: 'key', value: 'value' }, 'DiscountInfo'));
                    ppts.config.dictMappingConfig["discountInfo"] = "c_codE_ABBR_DiscountInfo";

                    var promotionInfos = [{ key: '0', value: '只享受客户折扣' }, { key: '1', value: '只享受订购时特殊优惠' }, ];
                    dataSyncService.injectDictData(mcs.util.mapping(promotionInfos, { key: 'key', value: 'value' }, 'PromotionInfo'));
                    ppts.config.dictMappingConfig["promotionInfo"] = "c_codE_ABBR_PromotionInfo";

                    var incomeBelongings = [{ key: '1', value: '课收归学员所在机构' }, { key: '0', value: '课收归授课教师岗位所在机构' }, ];
                    dataSyncService.injectDictData(mcs.util.mapping(incomeBelongings, { key: 'key', value: 'value' }, 'IncomeBelonging'));
                    ppts.config.dictMappingConfig["incomeBelonging"] = "c_codE_ABBR_IncomeBelonging";

                    var periodsOfLesson = [];
                    for (var i = 0; i < 9; i++) { periodsOfLesson[i] = { key: (i + 1) + '', value: (i + 1) + '' }; }
                    dataSyncService.injectDictData(mcs.util.mapping(periodsOfLesson, { key: 'key', value: 'value' }, 'PeriodsOfLesson'));
                    ppts.config.dictMappingConfig["periodsOfLesson"] = "c_codE_ABBR_PeriodsOfLesson";

                    var commission_custom = [{ key: '0', value: '遵循所属机构薪酬规则' }, { key: '1', value: '每小时' }, ];
                    dataSyncService.injectDictData(mcs.util.mapping(commission_custom, { key: 'key', value: 'value' }, 'CommissionCustom'));
                    ppts.config.dictMappingConfig["commission_custom"] = "c_codE_ABBR_CommissionCustom";


                    vm.zxs = vm.xgs = vm.js = '0';

                    dataSyncService.injectDynamicDict('ifElse');


                    if (loadHandler) {
                        loadHandler();
                    }



                })();

            };

            handler.callback = function ($scope, vm, loadtype, entity) {

                //console.warn(entity);

                vm.dictionaries = entity.dictionaries;

                vm.m[loadtype].categoryType = entity.categoryType;
                vm.m[loadtype].product = entity.product;
                vm.m[loadtype].exOfCourse = entity.exOfCourse || vm.m[loadtype].exOfCourse;
                vm.m[loadtype].salaryRules = entity.salaryRules || vm.m[loadtype].salaryRules;
                vm.m[loadtype].catalogs = entity.catalogs;


                if (loadtype == "classgroup") {
                    vm.m.classgroup.exOfCourse.groupType = 1;
                } else if (loadtype != "onetoone" && loadtype != "classgroup") {
                    vm.confirmStagings = [{ key: '1', value: '手工确认' }, { key: '2', value: '自动确认' }, ];
                    vm.m[loadtype].product.confirmMode = "1";
                }


                //复制不进行设置
                if (!$location.$$search.id) {
                    vm.m[loadtype].product.season = 1;
                    vm.m[loadtype].exOfCourse.coachType = 1;
                    vm.m[loadtype].exOfCourse.classType = 1;
                }

                dataSyncService.injectDictData(mcs.util.mapping(entity.catalogs, { key: 'catalog', value: 'catalogName' }, 'CategoryCustom'));
                ppts.config.dictMappingConfig["categoryCustom"] = "c_codE_ABBR_CategoryCustom";


                var filterParentKey = '';
                if (loadtype == 'onetoone') {
                    filterParentKey = '1';
                } else if (loadtype == 'classgroup') {
                    filterParentKey = '2';
                }
                ppts.dict['c_codE_ABBR_Product_CourseLevel'] = $(ppts.dict['c_codE_ABBR_Product_CourseLevel']).map(function (i, v) { if (v.parentKey == "0" || v.parentKey == filterParentKey) { return v; } });

                $scope.$broadcast('dictionaryReady');

                //console.warn(vm.m[loadtype]);
                //console.log(ppts.dict);

            };

            return handler;

        }]);


});

