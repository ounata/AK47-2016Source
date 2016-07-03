define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
        	product.registerController('orderClassGroupListController', [
                '$scope', '$state', '$location', '$stateParams', 'dataSyncService', 'mcsDialogService', 'purchaseCourseDataService', 'productDataService',
                function ($scope, $state, $location, $stateParams, dataSyncService, mcsDialogService, purchaseCourseDataService, productDataService) {

                    var vm = this;
                    vm.customer = $location.$$search;

                    var campusId = $stateParams.campusId;
                    var customerId = $stateParams.customerId;

                	vm.data = {
                		//selection: 'checkbox',
                		//rowsSelected: [],
                		//keyFields: ['productID', 'categoryType', 'productStatus', 'endDate'],
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
                			//field: "productName",
                			name: "可插班班级数量",
                		    //template: '<span>{{row.classCount }}</span>'
                			template: '<a href="javascript:;" ng-click="vm.showClassList(row)" >{{row.classCount}}</a>'
                		}, {
                		    field: "catalogName",
                		    name: "产品类型",
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
                				productDataService.getPagedClassGroupProducts(vm.criteria, function (result) {
                					vm.data.rows = result.pagedData;
                				});
                			}
                		},
                		orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                	}

                	vm.showClassList = function (row) {

                	    if (vm.criteria.campusID == '') { mcsDialogService.error({ title: '错误', message: '没有选择校区不允许进行操作！' }); return; }

                	    var param = $.extend({ productId: row.productID, listType: 3, productCampusId: vm.criteria.campusID }, vm.customer);
                	    param = $.extend(param, $stateParams);

                	    $state.go('ppts.purchaseClassGroup', param);
                	};
                	vm.search = function () {

                	    productDataService.getClassGroupProducts(vm.criteria, function (result) {

                			console.log(result)

                			vm.data.rows = result.queryResult.pagedData;
                			dataSyncService.updateTotalCount(vm, result.queryResult);
                			$scope.$broadcast('dictionaryReady');

                			

                		});

                	}; 
                	vm.showShoppingCart = function () {

                	    var param = $.extend({ listType: 3 }, vm.customer);
                	    param = $.extend(param, $stateParams);

                	    $state.go('ppts.purchaseOrderList', param);
                	};

                	var init = (function () {

                	    dataSyncService.initCriteria(vm);
                	    vm.criteria.productStatus = 4;

                	    //权限指定
                	    vm.criteria.branch = ppts.user.branchId;
                	    vm.criteria.campusID = ppts.user.campusId;

                	    if (ppts.user.branchId) { vm.disabledlevel = 1; }
                	    if (ppts.user.campusId) { vm.disabledlevel = 2; }

                	    
                		vm.search();
                	})();


                }]);
        });

