define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
            product.registerController('productListController', [
                '$scope', '$state', 'dataSyncService', 'mcsDialogService', 'productDataService',
                function ($scope, $state, dataSyncService, mcsDialogService, productDataService) {
                    var vm = this;

                    //查询条件
                    vm.criteria = {
                        name: '',
                        productCode: '',
                        startDate: '',
                        endDate: '',
                        pagedParam: { "pageIndex": 1, "pageSize": 10, "totalCount": -1 }
                    };

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
                            template: '<a ui-sref="ppts.productView({id:row.productID})">{{row.productName}}</a>'
                        }, {
                            field: "parentName",
                            name: "销售大区"
                        }, {
                            field: "catalog",
                            name: "产品类型"
                        }, {
                            field: "categoryType",
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
                        }, {
                            name: "启售时间",
                            template: '<span>{{row.startDate | date:"yyyy-MM-dd"}}</span>',
                        }, {
                            name: "止售时间",
                            template: '<span>{{row.endDate | date:"yyyy-MM-dd"}}</span>',
                        }, {
                            field: "creatorName",
                            name: "创建人",
                        }, {
                            field: "productStatus",
                            name: "销售状态",
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

                        if ($.inArray('1', vm.criteria.priceTypes)>-1) {
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
                            vm.criteria.startStartDate = vm.criteria.begainTime;
                            vm.criteria.endStartDate = vm.criteria.endTime;
                        }
                        if (vm.criteria.timeType == 2) {
                            vm.criteria.starEndtDate = vm.criteria.begainTime;
                            vm.criteria.endEndtDate = vm.criteria.endTime;
                        }
                        if (vm.criteria.timeType == 3) {
                            vm.criteria.startCreateDate = vm.criteria.begainTime;
                            vm.criteria.endCreateDate = vm.criteria.endTime;
                        }
                        
                        
                        console.log(vm.criteria)
                        //return;

                        productDataService.getAllProducts(vm.criteria, function (result) {
                            console.warn('------------------------')
                            console.warn(result);
                            console.warn('------------------------')

                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);

                            dataSyncService.injectDictData(mcs.util.mapping(result.dictionaries["Product_ProductStatus"], { key: 'key', value: 'value' }, 'ProductStatus'));
                            ppts.config.dictMappingConfig["productStatus"] = "product_ProductStatus";

                            console.log(ppts)

                            $scope.$broadcast('dictionaryReady');
                        });

                    };
                    


                    //复制
                    vm.copyProduct = function () {

                        var selectedData = vm.data.rowsSelected[0]
                        var type = parseInt(selectedData.categoryType);
                        var param = { id: selectedData.productID };

                        var goName = '';
                        switch (type) {
                            case 1: goName = 'onetoone'; break;
                            case 2: goName='classgroup'; break;
                            case 3: goName = 'youxue'; break;
                            case 4: goName = 'wukeshou'; break;
                            case 5: goName = 'other'; break;
                        }
                        if (goName == '') { return; }
                        $state.go('ppts.productCopy.' + goName, param);
                    };

                    vm.delete = function () {
                        var failProduct = $.grep(vm.data.rowsSelected, function (obj, index) {
                            if (obj.productStatus != 2)return obj;
                        });
                        if (failProduct.length > 0) {
                            mcsDialogService.error('错误', '只允许删除 驳回状态 产品！');
                            return;
                        }

                        mcsDialogService.confirm('危险确认', '删除产品无法恢复，是否确认删除？')
                            .result.then(function () {
                                var productIds = getSelectedProductIds();
                                productDataService.delProduct(productIds, function (isSuccess) {
                                    if (isSuccess) {
                                        var selectedIndexs = getSelectedIndexs();
                                        $.each(selectedIndexs, function (i, v) { vm.data.rows.splice(v,1) });
                                    } else {
                                        mcsDialogService.error('错误', '操作失败！');
                                    }
                                });
                            }, function () { });
                    };


                    vm.stopProduct = function () {
                        var rowProduct = vm.data.rowsSelected[0];
                        if (rowProduct.productStatus == 1
                            && new Date(rowProduct.endDate) >= new Date()
                            )
                        {
                            productDataService.stopProduct(rowProduct.productID, function (date) {
                                if(date.length>0)
                                    vm.data.rows[getSelectedIndexs()].endDate = date;
                            })
                        }
                    };


                    vm.delayProduct = function () {
                        var rowProduct = vm.data.rowsSelected[0];
                        if (rowProduct.productStatus == 1
                            && new Date(rowProduct.endDate) <new Date()
                            )
                        {
                            //var controlArray = new Array();
                            //controlArray[controlArray.length] = '<p class="input-group">';
                            //controlArray[controlArray.length] = '<input type="text" class="form-control" uib-datepicker-popup="yyyy/MM/dd" ng-model="vm.delayTime" is-open="vm.popup3.opened" datepicker-options="vm.dateOptions" ng-required="true" close-text="Close" alt-input-formats="vm.altInputFormats" />';
                            //controlArray[controlArray.length] = '<span class="input-group-btn">';
                            //controlArray[controlArray.length] = '<button type="button" class="btn btn-default" ng-click="vm.open3()"><i class="glyphicon glyphicon-calendar"></i>';
                            //controlArray[controlArray.length] = '</button>';
                            //controlArray[controlArray.length] = '</span>';
                            //controlArray[controlArray.length] = '</p>';
                            ////angular.element()
                            //alert(controlArray.join(''))
                            //mcsDialogService.create('延期', '<div>延期产品:' + controlArray.join('') + '</div>')
                            mcsDialogService.create('app/product/productlist/product-list/startsell.html', 'productListController')
                        }

                    };



                    function getSelectedProductIds() {
                        var productIds = $.map(vm.data.rowsSelected, function (obj, index) {
                            return obj.productID;
                        });
                        return productIds;
                    }

                    function getSelectedIndexs() {
                        var productIds = getSelectedProductIds();
                        var selectedIndexs = $.map(vm.data.rows, function (obj, index) {
                            if ($.inArray(obj.productID, productIds) > -1) { return index; } return null;
                        });
                        return selectedIndexs;
                    }


                    vm.init = function () {
                        var priceTypes = [{ key: '1', value: '产品单价' }, { key: '2', value: '目标单价' }, { key: '3', value: '合作单价' }, ];
                        var timeTypes = [{ key: '1', value: '启售时间' }, { key: '2', value: '止售时间' }, { key: '3', value: '创建时间' }, ];
                        var isPartners = [{ key: '1', value: '全部' }, { key: '2', value: '无合作' }, { key: '3', value: '有合作' }, ];
                        var saleStatus = [{ key: '1', value: '待审批' }, { key: '2', value: '审批通过' }, { key: '3', value: '驳回' }, { key: '4', value: '销售中' }, { key: '5', value: '已停售' }, ];
                        var categoryTypes = [{ key: '1', value: '一对一' }, { key: '2', value: '班组' }, { key: '3', value: '游学' }, { key: '4', value: '无课收' }, { key: '5', value: '其他' }, ];



                        dataSyncService.injectDictData(mcs.util.mapping(priceTypes, { key: 'key', value: 'value' }, 'PriceType'));
                        ppts.config.dictMappingConfig["priceType"] = "c_codE_ABBR_PriceType";

                        dataSyncService.injectDictData(mcs.util.mapping(timeTypes, { key: 'key', value: 'value' }, 'TimeType'));
                        ppts.config.dictMappingConfig["timeType"] = "c_codE_ABBR_TimeType";

                        dataSyncService.injectDictData(mcs.util.mapping(isPartners, { key: 'key', value: 'value' }, 'HasPartner'));
                        ppts.config.dictMappingConfig["hasPartner"] = "c_codE_ABBR_HasPartner";

                        dataSyncService.injectDictData(mcs.util.mapping(saleStatus, { key: 'key', value: 'value' }, 'SaleStatus'));
                        ppts.config.dictMappingConfig["saleStatus"] = "c_codE_ABBR_SaleStatus";

                        dataSyncService.injectDictData(mcs.util.mapping(categoryTypes, { key: 'key', value: 'value' }, 'CategoryType'));
                        ppts.config.dictMappingConfig["categoryType"] = "c_codE_ABBR_CategoryType";

                        vm.search();

                    };
                    vm.init();
                    

                    vm.dateOptions = {
                        // dateDisabled: disabled,
                        formatYear: 'yy',
                        // maxDate: new Date(2020, 5, 22),
                        // minDate: new Date(),
                        startingDay: 1
                    };

                    vm.popup1 = { opened: false };
                    vm.open1 = function () { vm.popup1.opened = true; };

                    vm.popup2 = { opened: false };
                    vm.open2 = function () { vm.popup2.opened = true; };

                    vm.popup3 = { opened: false };
                    vm.open3 = function () { vm.popup3.opened = true; };



                    ////selected checkbox
                    //vm.selectedCheckbox = [];

                    ////修改
                    //vm.modifyProduct= function (id) {
                    //    $state.go('ppts.productEdit',{id:vm.selectedCheckbox[0] });
                    //};

                    

                    ////删除
                    //vm.deleteProduct = function () { 
                    //}

                    ////止售
                    //vm.stopSellProduct = function () {
                    //}

                    ////延期
                    //vm.postponeProduct = function () {
                    //}

                    //vm.close = function (category, dictionary) {
                    //    vm.criteria[category].length = 0;

                    //    vm.dictionaries[dictionary].forEach(function(item,value) {
                    //        item.checked = false;
                    //    });
                    //};



                }]);
            });

