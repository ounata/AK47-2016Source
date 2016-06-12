define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService,
        ppts.config.dataServiceConfig.classgroupDataService],
        function (helper) {
            helper.registerController('productListController', [
                '$scope', '$state', 'dataSyncService', 'utilService', 'mcsDialogService', 'productDataService', 'classgroupDataService',
                function ($scope, $state, dataSyncService, utilService, mcsDialogService, productDataService, classgroupDataService) {

                    var vm = this;
                    var iswatch_categories_extend = false;


                    vm.products = [
                          { text: '一对一', route: 'ppts.productAdd.onetoone', loadtype: 'onetoone', typename: 'all' },
                          { text: '班组', route: 'ppts.productAdd.classgroup', loadtype: 'classgroup', typename: 'all' },
                          { text: '游学', route: 'ppts.productAdd.youxue', loadtype: 'youxue', typename: 'all' },
                          {
                              text: '更多', route: 'ppts.productAdd.other', loadtype: 'other', typename: 'all',
                              children: [
                                  { text: '实物产品', route: 'ppts.productAdd.other', loadtype: 'other', typename: '实物产品' },
                                  { text: '虚拟产品', route: 'ppts.productAdd.other', loadtype: 'other', typename: '虚拟产品' },
                                  { text: '费用产品', route: 'ppts.productAdd.other', loadtype: 'other', typename: '费用产品' },
                                  { text: '留学', route: 'ppts.productAdd.other', loadtype: 'other', typename: '留学' },
                                  { text: '其它', route: 'ppts.productAdd.other', loadtype: 'other', typename: '其它产品' },
                              ]
                          },
                          { text: '代理招生', route: 'ppts.productAdd.wukeshou', loadtype: 'wukeshou', typename: 'all' }
                    ];
                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['productID', 'categoryType', 'productStatus', 'endDate'],
                        headers: [{
                            field: "productCode",
                            name: "产品编码",
                            template: '<a ui-sref="ppts.productView({id:row.productID})">{{row.productCode}}</a>',
                            sortable: true
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
                            field: "ProductPrice",
                            name: "产品单价（元）",
                            template: '<span>{{row.productPrice}}</span>',
                        }, {
                            name: "合作单价（元）",
                            template: '<span>{{ row.productPrice * ( 1 - row.partnerRatio ) }}</span>'
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
                        $state.go(item.route, { ltype: item.loadtype, lname: item.typename });
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


                        console.log(vm.criteria)
                        //return;


                        productDataService.getAllProducts(vm.criteria, function (result) {

                            console.log(result)

                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);




                            var categories = [];
                            var iscontinue = true;
                            $(result.categories).each(function (i, v) {
                                if (v.categoryType == "3") {
                                    if (iscontinue) {
                                        iscontinue = false;
                                        categories.push({ category: '30', categoryName: '游学' });
                                    }
                                } else {
                                    categories.push(v);
                                }
                            });

                            if (!iswatch_categories_extend) {
                                $scope.$watch('vm.criteria.categories_extend', function (n, o) {
                                    var selectedCategories = [n];
                                    if (n == 30) { selectedCategories = $(result.categories).map(function (i, v) { if (v.categoryType == "3") { return v.category; } }).toArray(); }
                                    vm.criteria.categories = selectedCategories;
                                });
                                iswatch_categories_extend = true;
                            }

                            dataSyncService.injectDictData(mcs.util.mapping(categories, { key: 'category', value: 'categoryName' }, 'Categories'));
                            ppts.config.dictMappingConfig["categories"] = "c_codE_ABBR_Categories";




                            $scope.$broadcast('dictionaryReady');
                        });

                    };
                    vm.copyProduct = function () {
                        if (utilService.selectOneRow(vm)) {
                            var selectedData = vm.data.rowsSelected[0]
                            var type = parseInt(selectedData.categoryType);
                            var param = { id: selectedData.productID };

                            var goName = '';
                            switch (type) {
                                case 1: goName = 'onetoone'; break;
                                case 2: goName = 'classgroup'; break;
                                case 3: goName = 'youxue'; break;
                                case 4: goName = 'wukeshou'; break;
                                case 5: goName = 'other'; break;
                            }
                            if (goName == '') { return; }
                            $state.go('ppts.productCopy.' + goName, param);
                        }
                    };
                    vm.delete = function () {
                        if (utilService.selectMultiRows(vm)) {
                            var failProduct = $.grep(vm.data.rowsSelected, function (obj, index) {
                                if (obj.productStatus != 2) return obj;
                            });

                            if (utilService.showMessage(vm, failProduct.length > 0, '只允许删除驳回状态的产品！')) {
                                return;
                            }

                            mcsDialogService.confirm({ title: '危险确认', message: '删除产品无法恢复，是否确认删除？' })
                                .result.then(function () {
                                    productDataService.delProduct(productIds, function (isSuccess) {
                                        mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected);
                                    });
                                });
                        }
                    };
                    vm.stopProduct = function () {
                        if (utilService.selectOneRow(vm)) {
                            var rowProduct = vm.data.rowsSelected[0];
                            if (rowProduct.productStatus == 1
                                && rowProduct.endDate >= new Date()
                                ) {
                                productDataService.stopProduct(rowProduct.productID, function (entity) {
                                    var index = mcs.util.indexOf(vm.data.rows, "productID", rowProduct.productID);
                                    vm.data.rows[index].endDate = entity.endDate;
                                });
                            }
                        }
                    };
                    vm.delayProduct = function () {
                        if (utilService.selectOneRow(vm)) {
                            var rowProduct = vm.data.rowsSelected[0];
                            if (rowProduct.productStatus == 1
                                && rowProduct.endDate < new Date()
                                ) {
                                mcsDialogService.create('app/product/productlist/product-list/product-delay.html', { controller: 'delayController', params: rowProduct.endDate })
                                    .result.then(function (data) {
                                        productDataService.delayProduct({ id: rowProduct.productID, endDate: data.endDate }, function () {
                                            var index = mcs.util.indexOf(vm.data.rows, "productID", rowProduct.productID);
                                            vm.data.rows[index].endDate = data.endDate;
                                        });
                                    });
                            }
                        }
                    };
                    vm.createClass = function () {
                        if (utilService.selectOneRow(vm)) {
                            classgroupDataService.checkCreateClass_Product(vm.data.rowsSelected[0].productID, function (result) {
                                if (result.sucess) {
                                    $state.go('ppts.classAdd', { id: vm.data.rowsSelected[0].productID });
                                } else
                                    mcsDialogService.error(
                                   { title: 'Error', message: result.message }
                               );
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


                        dataSyncService.initCriteria(vm);

                        //默认销售中  c_codE_ABBR_Product_ProductStatus
                        vm.criteria.productStatus = "4";
                        vm.search();

                    })();

                }]);


            helper.registerController('delayController', [
                '$scope', '$uibModalInstance', 'data',
                function ($scope, $uibModalInstance, data) {

                    var vm = this;
                    vm.entity = { endDate: data };
                    $scope.vm = vm;

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('Canceled');
                    };

                    vm.save = function () {
                        $uibModalInstance.close(vm.entity);
                    };

                    vm.hitEnter = function (evt) {
                        if (angular.equals(evt.keyCode, 13) && !(angular.equals(vm.entity.endDate, null) || angular.equals(vm.entity.endDate, '')))
                            vm.save();
                    };

                }]);

        });

