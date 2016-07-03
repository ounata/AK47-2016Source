define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
            product.registerController('orderProductController', [
                '$scope', '$state', '$location', '$stateParams', 'dataSyncService', 'mcsDialogService', 'utilService', 'purchaseCourseDataService', 'productDataService',
                function ($scope, $state, $location, $stateParams, dataSyncService, mcsDialogService, utilService, purchaseCourseDataService, productDataService) {

                    var vm = this;
                    vm.customer = $location.$$search;
                    vm.criteria = { selectedGrades: packageArray($stateParams.grade), productStatus: 4 };

                    var campusId = $stateParams.campusId;
                    var customerId = $stateParams.customerId;

                    //1-常规订购 2-买赠订购
                    var type = $scope.type = $stateParams.type;
                    if (type == 2) { vm.criteria.categoryType = 1; }


                    function packageArray(val) { return val instanceof Array ? val : [val]; }
                    function buildQueryEntity(vm) {
                        if (vm.criteria.selectedSubjects) { vm.criteria.selectedSubjects = packageArray(vm.criteria.selectedSubjects); }
                        vm.criteria.selectedGrades = packageArray(vm.criteria.selectedGrades);
                    }


                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['productID', 'categoryType', 'productStatus', 'endDate'],
                        headers: [{
                            field: "productCode",
                            name: "产品编码",
                            //template: '<a ui-sref="ppts.productView({id:row.productID})">{{row.productCode}}</a>',
                            //sortable: true
                        }, {
                            field: "productName",
                            name: "产品名称",
                            //template: '<a ui-sref="ppts.productView({id:row.productID})">{{row.productName}}</a>'
                        }, {
                            field: "catalogName",
                            name: "产品类型"
                        }, {
                            field: "categoryType",
                            name: "产品分类",
                            template: '<span>{{row.categoryType | categoryType}}</span>',
                        }, {
                            field: "subject",
                            name: "科目",
                            template: '<span>{{row.subject | subject}}</span>'
                        }, {
                            field: "grade",
                            name: "年级",
                            template: '<span>{{row.grade | grade}}</span>'
                        }, {
                            field: "courseLevel",
                            name: "课程级别",
                            template: '<span>{{row.courseLevel | courseLevel}}</span>'
                        }, {
                            field: "periodDuration",
                            name: "时长（分钟）",
                            template: '<span>{{row.periodDuration | period }}</span>'
                        }, {
                            field: "lessonCount",
                            name: "总课次",
                            template: '<span>{{ row.categoryType==2 ? row.lessonCount : "--" }}</span>'
                        }, {
                            field: "productUnit",
                            name: "产品颗粒度",
                            template: '<span>{{ row.productUnit | unit }}</span>'
                        }, {
                            field: "ProductPrice",
                            name: "产品单价（元）",
                            template: '<span>{{row.productPrice}}</span>',
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: ppts.config.pageSizeItem,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                buildQueryEntity(vm);
                                productDataService.getPagedProducts(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }


                    vm.search = function () {
                        buildQueryEntity(vm);
                        productDataService.getAllProducts(vm.criteria, function (result) {
                            vm.data.rowsSelected = [];

                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');

                            console.log(result)

                        });

                    };
                    vm.showShoppingCart = function () {

                        var param = $.extend({ listType: type }, vm.customer);
                        param = $.extend(param, $stateParams);

                        //1-常规订购 2-买赠订购 3-插班订购 4-补差兑换 5-不补差兑换
                        $state.go('ppts.purchaseOrderList', param);
                    };

                    vm.buy = function () {
                        //if (vm.criteria.campusID == '') { mcsDialogService.error({ title: '错误', message: '没有选择校区不允许进行购买！' }); return; }
                        if (utilService.showMessage(vm, !vm.criteria.campusID, '没有选择校区不允许进行购买！')) { return; }

                        if (!utilService.selectMultiRows(vm)) { return; }
                        var data = $(vm.data.rowsSelected).map(function (i, v) { return { productID: v.productID, customerID: customerId, orderType: type, productCampusID: vm.criteria.campusID }; }).toArray();
                        //console.log(data)
                        //return;
                        purchaseCourseDataService.addShoppingCart(data, function (entity) { console.log(entity); });
                    };

                    var permisstionFilter = function () {
                        //权限指定
                        vm.criteria.branch = ppts.user.branchId;
                        vm.criteria.campusID = ppts.user.campusId;
                        if (ppts.user.branchId) { vm.disabledlevel = 1; }
                        if (ppts.user.campusId) { vm.disabledlevel = 2; }
                    };

                    var init = (function () {

                        dataSyncService.initCriteria(vm);
                        permisstionFilter();


                        vm.search();
                    })();


                }]);
        });

