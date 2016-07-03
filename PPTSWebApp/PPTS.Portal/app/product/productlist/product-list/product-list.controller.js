define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService,
        ppts.config.dataServiceConfig.classgroupDataService],
        function (helper) {
            helper.registerController('productListController', [
                '$scope', '$state', 'dataSyncService', 'utilService', 'mcsDialogService', 'productDataService', 'productDictionary', 'classgroupDataService',
                function ($scope, $state, dataSyncService, utilService, mcsDialogService, productDataService, productDictionary, classgroupDataService) {

                    var vm = this;


                    vm.products = [
                          { text: '一对一', loadtype: 'onetoone', },
                          { text: '班组', loadtype: 'classgroup' },
                          { text: '游学', loadtype: 'youxue' },
                          { text: '代理招生', loadtype: 'dailizhaosheng' },
                          {
                              text: '其它', loadtype: 'other',
                              children: [
                                  { text: '实物产品', loadtype: 'shiwu' },
                                  { text: '虚拟产品', loadtype: 'xuni' },
                                  { text: '费用产品', loadtype: 'feiyong' },
                                  { text: '留学', loadtype: 'liuxue' },
                                  { text: '其它', loadtype: 'other' },
                              ]
                          }
                    ];

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['productID', 'categoryType', 'productStatus', 'endDate', 'category', 'categoryName', 'productName', 'productCode'],
                        headers: [{
                            field: "productCode",
                            name: "产品编码",
                            template: '<a ui-sref="ppts.productView({id:row.productID})">{{row.productCode}}</a>',
                            //sortable: true
                        }, {
                            field: "productName",
                            name: "产品名称",
                            template: '<a ui-sref="ppts.classgroup({productCode:row.productCode})">{{row.productName}}</a>'
                        }, {
                            field: "categoryType",
                            name: "产品类型",
                            template: '<span>{{row.categoryType | categoryType}}</span>',
                        }, {
                            field: "catalogName",
                            name: "产品分类",

                        }, {
                            field: "grade",
                            name: "年级",
                            template: '<span>{{row.grade | grade}}</span>'
                        }, {
                            field: "periodDuration",
                            name: "时长（分钟）",
                            template: '<span>{{row.periodDuration | period }}</span>'
                        }, {
                            field: "productPrice",
                            name: "产品单价（元）",
                        }, {
                            field: "targetPrice",
                            name: "目标单价（元）",
                        }, {
                            name: "合作单价（元）",
                            template: '<span ng-if="row.hasPartner==1">{{ row.productPrice * ( 1 - row.partnerRatio ) }}</span>'
                                    + '<span ng-if="row.hasPartner==0">--</span>'
                        }, {
                            field: "coachType",
                            name: "辅导类型",
                            template: '<span>{{ row.coachType | coachType }}</span>'
                        }, {
                            name: "启售时间",
                            template: '<span>{{row.startDate | date:"yyyy-MM-dd" | normalize}}</span>',
                        }, {
                            name: "止售时间",
                            template: '<span>{{row.endDate | date:"yyyy-MM-dd" | normalize}}</span>',
                        }, {
                            field: "creatorName",
                            name: "创建人",
                        }, {
                            field: "productStatus",
                            name: "销售状态",
                            template: '<span>{{row.productStatus | productStatus }}</span>',
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: ppts.config.pageSizeItem,
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

                    vm.create = function (item) {
                        var routeSuffix = productDictionary.categories[item.loadtype].routeSuffix;
                        $state.go('ppts.productAdd.' + routeSuffix, { ltype: item.loadtype });
                    };
                    vm.search = function () {

                        if ($.inArray('1', vm.criteria.priceTypes) > -1) {
                            vm.criteria.minProductPrice = vm.criteria.minPrice;
                            vm.criteria.maxProductPrice = vm.criteria.maxPrice;
                        }
                        if ($.inArray('2', vm.criteria.priceTypes) > -1) {
                            vm.criteria.minTargetPrice = vm.criteria.minPrice;
                            vm.criteria.maxTargetPrice = vm.criteria.maxPrice;
                        }
                        if ($.inArray('3', vm.criteria.priceTypes) > -1) {
                            vm.criteria.minCooperationPrice = vm.criteria.minPrice;
                            vm.criteria.maxCooperationPrice = vm.criteria.maxPrice;
                        }

                        if (vm.criteria.timeType == 1) {
                            vm.criteria.startStartDate = new Date(1461220356061);
                            vm.criteria.endStartDate = vm.criteria.endTime;
                        }
                        if (vm.criteria.timeType == 2) {
                            vm.criteria.starEndtDate = vm.criteria.beginTime;
                            vm.criteria.endEndtDate = vm.criteria.endTime;
                        }
                        if (vm.criteria.timeType == 3) {
                            vm.criteria.startCreateDate = vm.criteria.beginTime;
                            vm.criteria.endCreateDate = vm.criteria.endTime;
                        }
                        if (vm.criteria.timeType == 4) {
                            vm.criteria.startConfirmStartDate = vm.criteria.beginTime;
                            vm.criteria.endConfirmEndDate = vm.criteria.endTime;
                        }


                        ////console.log(vm.criteria)
                        //return;


                        productDataService.getAllProducts(vm.criteria, function (result) {

                            console.log(result)

                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);

                            $scope.$broadcast('dictionaryReady');
                        });

                    };
                    vm.copyProduct = function () {
                        if (utilService.selectOneRow(vm)) {

                            var selectedData = vm.data.rowsSelected[0]
                            var type = parseInt(selectedData.categoryType);

                            var loadtype = '';
                            var routeSuffix = '';

                            for (var index in productDictionary.categories) {
                                if (productDictionary.categories[index].index == type) {
                                    routeSuffix = productDictionary.categories[index].routeSuffix;
                                    loadtype = index; break;
                                }
                            }

                            if (loadtype == '') { return; }

                            var param = { ltype: loadtype, id: selectedData.productID };

                            $state.go('ppts.productCopy.' + routeSuffix, param);
                        }
                    };
                    vm.delete = function () {
                        if (utilService.selectMultiRows(vm)) {
                            var failProduct = $.grep(vm.data.rowsSelected, function (obj, index) {
                                if (obj.productStatus != 3) return obj;
                            });

                            if (utilService.showMessage(vm, failProduct.length > 0, '只允许删除驳回状态的产品！')) {
                                return;
                            }

                            var productIds = $(vm.data.rowsSelected).map(function (i, v) { return v.productID }).toArray();
                            mcsDialogService.confirm({ title: '危险确认', message: '删除产品无法恢复，是否确认删除？' })
                                .result.then(function () {
                                    productDataService.delProduct(productIds, function (isSuccess) {
                                        mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected);
                                    });
                                });
                        }
                    };
                    vm.stopProduct = function () {
                        if (utilService.selectMultiRows(vm)) {

                            var productIds = new Array();
                            var failProductIds = new Array();

                            $(vm.data.rowsSelected).each(function (i, v) {
                                if (v.productStatus != 4) {
                                    failProductIds.push(v.productID);
                                } else {
                                    productIds.push(v.productID);
                                }
                            });

                            if (utilService.showMessage(vm, failProductIds.length > 0, '只有 销售中 状态可止售！')) { return; }

                            mcsDialogService.confirm({ title: '操作确认', message: '产品 仍在销售中，是否确认止售？' })
                                .result.then(function () {
                                    productDataService.stopProduct(productIds, function (entity) {
                                        //var index = mcs.util.indexOf(vm.data.rows, "productID", rowProduct.productID);
                                        //vm.data.rows[index].endDate = entity.endDate;
                                        //vm.data.rows[index].productStatus = 5;
                                        vm.search();
                                    });
                                });
                        }
                    };
                    vm.delayProduct = function () {
                        if (utilService.selectMultiRows(vm)) {

                            var productIds = new Array();
                            var failProductIds = new Array();

                            $(vm.data.rowsSelected).each(function (i, v) {
                                if (v.productStatus == 4 || v.productStatus == 5) {
                                    productIds.push(v.productID);
                                } else {
                                    failProductIds.push(v.productID);
                                }
                            });

                            if (utilService.showMessage(vm, failProductIds.length > 0, '只有 销售中 或者 已停售 状态可延期！')) { return; }


                            mcsDialogService.create('app/product/productlist/product-list/product-delay.html', { controller: 'delayController', params: vm.data.rowsSelected })
                                    .result.then(function (data) {

                                        //console.log(data)

                                        var products = $(data).map(function (i, v) { return { productID: v.productID, endDate: v.modifyEndDate } }).toArray();
                                        var param = { model: products };

                                        productDataService.delayProduct(param, function () {
                                            vm.search();
                                        });

                                    });
                        }
                    };
                    vm.createClass = function () {
                        if (utilService.selectOneRow(vm)) {
                            classgroupDataService.checkCreateClass_Product(vm.data.rowsSelected[0].productID, function (result) {
                                if (result.sucess) {
                                    $state.go('ppts.classAdd', { id: vm.data.rowsSelected[0].productID });
                                } else {
                                    utilService.showMessage(vm, true, result.message)

                                }
                            }, function (result) {

                            });
                        }
                    }

                    var init = (function () {

                        for (var index in vm.products) {
                            if (vm.products[index].children) {
                                for (var cindex in vm.products[index].children) {
                                    vm.products[index].children[cindex].click = vm.create;
                                }
                            } else {
                                vm.products[index].click = vm.create;
                            }
                        }

                        var timeTypes = [{ key: '1', value: '启售时间' }, { key: '2', value: '止售时间' }, { key: '3', value: '创建时间' }, { key: '4', value: '收入确认时间' }];
                        dataSyncService.injectDictData(mcs.util.mapping(timeTypes, { key: 'key', value: 'value' }, 'TimeType'));
                        ppts.config.dictMappingConfig["timeType"] = "c_codE_ABBR_TimeType";

                        var priceTypes = [{ key: '1', value: '产品单价' }, { key: '2', value: '目标单价' }, { key: '3', value: '合作单价' }];
                        dataSyncService.injectDictData(mcs.util.mapping(priceTypes, { key: 'key', value: 'value' }, 'PriceType'));
                        ppts.config.dictMappingConfig["priceType"] = "c_codE_ABBR_PriceType";

                        dataSyncService.initCriteria(vm);

                        //默认销售中  c_codE_ABBR_Product_ProductStatus
                        vm.criteria.productStatus = "4";
                        vm.search();

                    })();

                }]);


            helper.registerController('delayController', [
                '$scope', '$uibModalInstance', 'data',
                function ($scope, $uibModalInstance, data) {
                    //console.log(data)
                    var vm = this;
                    //vm.entity = { endDate: data };
                    $scope.vm = vm;

                    vm.data = {
                        headers: [{
                            field: "productCode",
                            name: "产品编码",
                        }, {
                            field: "productName",
                            name: "产品名称",
                        }, {
                            name: "原止售时间",
                            template: '<span>{{row.endDate | date:"yyyy-MM-dd" | normalize}}</span>',
                        }, {
                            name: "修改后止售时间",
                            template: '<mcs-datepicker model="row.modifyEndDate" css="col-xs-10 col-sm-9" />',
                        }],
                        pager: {
                            pageable: false
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }

                    vm.close = vm.cancel = function () {
                        $uibModalInstance.dismiss('Canceled');
                    };

                    vm.save = function () {
                        $uibModalInstance.close(data);
                    };

                    vm.hitEnter = function (evt) {
                        //if (angular.equals(evt.keyCode, 13) && !(angular.equals(vm.entity.endDate, null) || angular.equals(vm.entity.endDate, '')))
                        if (angular.equals(evt.keyCode, 13)) {
                            vm.save();
                        }
                    };

                    var init = (function () {
                        $(data).each(function (i, v) { v.modifyEndDate = v.endDate; });
                        vm.data.rows = data;
                    })();

                }]);

        });

