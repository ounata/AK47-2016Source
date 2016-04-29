define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (product) {
            product.registerController('orderListController', [
                '$scope', '$state', '$stateParams', '$window', 'mcsDialogService', 'dataSyncService', 'purchaseCourseDataService',
                function ($scope, $state, $stateParams, $window, mcsDialogService, dataSyncService, purchaseCourseDataService) {
                    var vm = this;

                    var customerId = $stateParams.customerId || '657323';
                    var listType = $stateParams.listType || 1; // 1:常规清单  2:买赠清单

                    var headers = [{
                        field: "productCode",
                        name: "产品编码",
                    }, {
                        field: "productName",
                        name: "产品名称",
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
                        field: "productPrice",
                        name: "产品单价（元）",
                        template: '<span>{{row.productPrice | currency }}</span>',
                    }];

                    //查看买赠清单
                    if (listType == 2) {

                        headers = headers.concat([{
                            name: "订购数量",
                            template: '<input type="number" ng-model="row.item.orderAmount" ng-keyup="vm.reCalcPreset(row);" />',
                        }, {
                            name: "赠送数量",
                            template: '<input type="number" ng-model="row.item.presentAmount" />',
                        }, {
                            name: "总订购数量",
                            template: '{{ row.item.orderAmount + row.item.presentAmount }}',
                        }, {
                            name: "订购金额",
                            template: '{{ row.item.orderAmount * row.productPrice | currency}}',
                        }]);

                    } else {
                        headers = headers.concat([{
                            field: "productPrice",
                            name: "实际单价",
                            template: '<span>{{row.productPrice | currency}}</span>',
                        }, {
                            name: "订购数量",
                            template: '<span ng-if="row.canInput==1"><input type="text" ng-model="row.item.orderAmount" /></span><span ng-if="row.canInput==0">1</span>',
                        }, {
                            field: "ProductPrice",
                            name: "产品原价",
                            template: '<span>{{ row.item.orderAmount * row.productPrice | currency}}</span>',
                        }, {
                            //field: "row.item.tunlandRate",
                            name: "客户折扣",
                            template: '<span ng-if="row.tunlandAllowed==1">{{ row.discount() }}</span><span ng-if="row.tunlandAllowed==0">--</span>',
                        }, {
                            field: "item.specialRate",
                            name: "特殊折扣",
                            template: '<span ng-if="row.categoryType==3" ><span ng-if="row.tunlandAllowed == 1">--</span><span ng-if="row.tunlandAllowed == 0">{{ row.item.realPrice/row.productPrice | number:2 }}</span></span><span ng-if="row.categoryType!=3" ><input type="text" ng-model="row.item.specialRate" /></span>',
                        }, {
                            field: "productPrice",
                            name: "订购金额",
                            template: '<span ng-if="row.categoryType==3" ><input type="text" ng-model="row.item.realPrice" /></span><span ng-if="row.categoryType!=3">{{ vm.calcRealPrice(row) }}</span>',
                        }]);
                    }


                    vm.account = {};
                    vm.calcRealPrice = function (row) {
                        var discount = row.tunlandAllowed == 0 ? 1 : row.discount();
                        row.item.realPrice = (row.item.specialRate < discount ? row.item.specialRate : discount) * row.productPrice * row.item.orderAmount;
                        return row.item.realPrice;
                    }
                    vm.totalMoney = function () {
                        var total = 0;
                        $(vm.data.rowsSelected).each(function (i, v) { total += v.item.realPrice; });
                        return total;
                    }

                    vm.account.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['accountID', 'discountRate', 'accountMoney'],
                        headers: [{
                            field: "accountCode",
                            name: "账户编号",
                        }, {
                            field: "accountMoney",
                            name: "可用金额",
                        }, {
                            field: "discountRate",
                            name: "折扣率",
                        }, ],
                        pager: {
                            pageable: false
                        },
                    };

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['cartID', 'item'],
                        headers: headers,
                        pager: {
                            pageable: false
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }],
                    }

                    vm.goBuy = function () {
                        $window.history.back();
                    };

                    vm.delete = function () {
                        var cartIDs = $(vm.data.rowsSelected).map(function (i, v) { return v.cartID }).toArray();
                        purchaseCourseDataService.deleteShoppingCart(cartIDs, function (state) {
                            if (state) {
                                var indexs = $(vm.data.rows).map(function (i, v) { if ($.inArray(v.cartID, cartIDs) > -1) { return i; } }).toArray().reverse();
                                $(indexs).each(function (i, v) { vm.data.rows.shift(v, 1); });
                            }
                        });
                    };

                    vm.submit = function () {

                        var selectedRow = vm.account.data.rowsSelected[0];
                        //if (selectedRow.accountMoney < vm.totalMoney()) {
                        //    mcsDialogService.error({ title: 'Error', message: '该学员可用金额不足！' });
                        //    return;
                        //}

                        //


                        if (false) {
                            mcsDialogService.error({ title: 'Error', message: '没有创建综合服务费，请先创建综合服务费。' });
                            return;
                        }

                        //获取要扣取的服务费
                        //purchaseCourseDataService.getServiceMoney(customerId, function (entity) {

                        var messageServiceMoney = '';
                        if (true) {
                            messageServiceMoney = '+¥(综合服务费)';
                        }

                        var message = '¥' + vm.totalMoney() + '(产品费用)' + messageServiceMoney + '=¥(总金额)，您确认要提交审批吗？';
                        mcsDialogService.confirm({ title: 'Confirm', message: message }).result.then(function () {

                            var items = $(vm.data.rowsSelected).map(function (i, v) { return v.item; }).toArray();
                            var param = { listType: listType, customerId: customerId, accountId: selectedRow.accountID, item: items, chargeApplyId: 1 };

                            console.debug(param)
                            //return;;

                            purchaseCourseDataService.submitShoppingCart(param, function (entity) { console.log(entity); });

                        }, function () { });

                        //});
                    };






                    purchaseCourseDataService.getShoppingCart({ customerId: customerId, listType: listType }, function (result) {
                        console.debug(result)

                        var rows = $(result.cart).map(function (i, v) {
                            v.product['cartID'] = v.cartID;
                            v.product['item'] = v.item;
                            v.product['discount'] = function () { return vm.account.data.rowsSelected[0].discountRate; };
                            return v.product;
                        });

                        vm.account.data.rows = result.account;
                        vm.account.data.rowsSelected[0] = result.account[0];

                        vm.data.rows = rows.toArray();

                        //查看买赠清单
                        if (listType == 2) {
                            vm.reCalcPreset = function (row) {
                                if (result.present.items.length > 0) {
                                    for (var index in result.present.items) {
                                        if (row.item.orderAmount >= result.present.items[index].presentStandard) {
                                            return row.item.presentAmount = result.present.items[index].presentValue;
                                        }
                                    }
                                }
                            };
                        }

                    });



                }]);
        });

