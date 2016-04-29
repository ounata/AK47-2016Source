define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
            product.registerController('productListController', [
                '$scope', '$state', 'dataSyncService', 'mcsDialogService', 'productDataService',
                function ($scope, $state, dataSyncService, mcsDialogService, productDataService) {
                    var vm = this;

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
                            field: "catalogName",
                            name: "产品类型"
                        }, {
                            field: "categoryType",
                            name: "产品分类",
                            template: '<span>{{row.categoryType | categoryType}}</span>',
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
                            template: '<span>{{row.productStatus | productStatus }}</span>',
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
                            console.log(result)
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
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
                            case 2: goName = 'classgroup'; break;
                            case 3: goName = 'youxue'; break;
                            case 4: goName = 'wukeshou'; break;
                            case 5: goName = 'other'; break;
                        }
                        if (goName == '') { return; }
                        $state.go('ppts.productCopy.' + goName, param);
                    };

                    vm.delete = function () {
                        var failProduct = $.grep(vm.data.rowsSelected, function (obj, index) {
                            if (obj.productStatus != 2) return obj;
                        });
                        if (failProduct.length > 0) {
                            mcsDialogService.error({ title: '错误', message: '只允许删除 驳回状态 产品！' });
                            return;
                        }

                        mcsDialogService.confirm({ title: '危险确认', message: '删除产品无法恢复，是否确认删除？' })
                            .result.then(function () {
                                productDataService.delProduct(productIds, function (isSuccess) {
                                    mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected);
                                });
                            });
                    };


                    vm.stopProduct = function () {
                        var rowProduct = vm.data.rowsSelected[0];
                        if (rowProduct.productStatus == 1
                            && rowProduct.endDate >= new Date()
                            ) {
                            productDataService.stopProduct(rowProduct.productID, function (date) {
                                if (date.length > 0)
                                    vm.data.rows[getSelectedIndexs()].endDate = date;
                            })
                        }
                    };


                    vm.delayProduct = function () {
                        var rowProduct = vm.data.rowsSelected[0];
                        if (rowProduct.productStatus == 1
                            && rowProduct.endDate < new Date()
                            )
                        {
                            mcsDialogService.create('app/product/productlist/product-list/product-delay.html', { controller: 'delayController', params: rowProduct.endDate })
                                .result.then(function (data) {
                                    productDataService.delayProduct({ id: rowProduct.productID, endDate: data.endDate }, function () {
                                        var index = getSelectedIndexs(rowProduct.productID);
                                        vm.data.rows[index].endDate = data.endDate;
                                    });
                                }, function () { });
                        }

                    };

                    //创建班级
                    vm.createClass = function () {
                        var msg = createClassCheck();
                        msg = "";//测试  不检查
                        if (!msg) {
                            var rowProduct = vm.data.rowsSelected[0];
                            //1.获取产品信息  
                            $state.go('ppts.classAdd', { id: rowProduct.productID });
                        }
                        else {
                            mcsDialogService.error(
                                { title:'Error', message: msg }
                            );
                        }
                        
                    }

                    function createClassCheck() {
                        //1.选择数量判断
                        if (vm.data.rowsSelected.length != 1)
                            return "请选择1条班组产品！";

                        var rowPro = vm.data.rowsSelected[0];
                        //2.产品类型
                        if (rowPro.categoryType != 2)
                            return "只有班组产品才可以创建班级！";

                        //3.产品状态
                        if (rowPro.productStatus!=4  && rowPro.productStatus!=5)
                            return "只有产品状态是使用中、已停售的才可以创建班级！";

                        return "";
                    }



                    function getSelectedProductIds() {
                        var productIds = $.map(vm.data.rowsSelected, function (obj, index) {
                            return obj.productID;
                        });
                        return productIds;
                    }

                    function getSelectedIndexs(productId) {
                        var productIds = [productId] || getSelectedProductIds();
                        var selectedIndexs = $.map(vm.data.rows, function (obj, index) {
                            if ($.inArray(obj.productID, productIds) > -1) { return index; } return null;
                        });
                        return selectedIndexs;
                    }


                    vm.init = function () {
                        var timeTypes = [{ key: '1', value: '启售时间' }, { key: '2', value: '止售时间' }, { key: '3', value: '创建时间' }, ];

                        dataSyncService.injectDictData(mcs.util.mapping(timeTypes, { key: 'key', value: 'value' }, 'TimeType'));
                        ppts.config.dictMappingConfig["timeType"] = "c_codE_ABBR_TimeType";

                        vm.search();

                    };
                    vm.init();

                }]);


            product.registerController('delayController', [
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



            product.registerController('selectCategoryController', [
                '$scope', '$uibModalInstance', '$state', 'dataSyncService', 'data',
                function ($scope, $uibModalInstance, $state, dataSyncService, data) {

                    //console.log(data)

                    var vm = {};
                    $scope.vm = vm;



                    vm.nodeClick = function (event, treeId, treeNode) {
                        alert('you are selecting ' + treeNode.name);
                    }
                    vm.onCheck = function (event, treeId, treeNode) {
                        alert('you are selecting ' + treeNode.name);
                    }

                    vm.loadDataWaiting = function (treeId, treeNode) {

                    }


                    vm.treeSetting = {
                        data: {
                            key: {
                                children: 'children',
                                name: 'name',
                                title: 'name'
                            }
                        },
                        view: {
                            selectedMulti: true,
                            showIcon: true,
                            showLine: true,
                            nameIsHTML: false,
                            fontCss: {

                            }

                        },
                        check: {
                            enable: true,
                            chkStyle: 'checkbox'

                        },
                        async: {
                            enable: false,
                            autoParam: ["id"],
                            contentType: "application/json",
                            type: 'post',
                            dataType: "json",
                            url: 'api/users'
                        },
                        callback: {
                            onClick: vm.nodeClick,
                            onCheck: vm.nodeCheck,
                            beforeAsync: vm.loadDataWaiting
                        }
                    };





                    vm.treeData = [{
                        id: '0',
                        name: 'root',
                        open: true,
                        parent: true,

                        children: [{

                            id: '1',
                            name: 'company-1',
                            open: true,
                            children: [{
                                id: 11,
                                name: 'HR',
                                checked: true,
                                chkDisabled: false,
                                icon: '',
                                iconOpen: '',
                                iconClose: '',
                                iconSkin: '',
                                isHidden: false


                            }, {
                                id: 12,
                                name: 'IT'
                            }]
                        }, {
                            id: '2',
                            name: 'company-2',

                            children: [{
                                id: '21',
                                name: 'HR'

                            }]
                        }]
                    }];








                    vm.cancel = function () {
                        //alert(1)
                        $uibModalInstance.dismiss('Canceled');
                    };

                    vm.save = function () {
                        $uibModalInstance.close(vm.entity);
                    };

                    



                }]);


            //
        });

