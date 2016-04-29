define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
        	product.registerController('orderProductController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'purchaseCourseDataService', 'productDataService',
                function ($scope, $state, $stateParams, dataSyncService, purchaseCourseDataService, productDataService) {
                	var vm = this;
                	var customerId = $stateParams.customerId || '657323';

                    //查询条件
                	vm.criteria = {
                	    name: '',
                	    pagedParam: { "pageIndex": 1, "pageSize": 10, "totalCount": -1 }
                	};

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
                	        pageSize: 10,
                	        totalCount: -1,
                	        pageChange: function () {
                	            dataSyncService.initCriteria(vm);
                	            productDataService.getPagedProducts(vm.criteria, function (result) {
                	                vm.data.rows = result.pagedData;
                	            });
                	        }
                	    },
                	    orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                	}


                	dataSyncService.initCriteria(vm);
                	vm.search = function () {

                	    productDataService.getAllProducts(vm.criteria, function (result) {
                	        
                	        vm.data.rows = result.queryResult.pagedData;
                	        dataSyncService.updateTotalCount(vm, result.queryResult);
                	        $scope.$broadcast('dictionaryReady');

                	        console.log(result)

                	    });

                	};

                	vm.showShoppingCart = function () {
                	    $state.go('ppts.purchaseOrderList', { listType: 1 });
                	};

                	vm.buy = function () {
                	    var data = $(vm.data.rowsSelected).map(function (i, v) { return { productID: v.productID, customerID: customerId }; }).toArray();
                	    purchaseCourseDataService.addShoppingCart(data, function (entity) { console.log(entity); });
                	};


                	vm.init = function () {
                	    vm.search();
                	};
                	vm.init();


                }]);
        });

