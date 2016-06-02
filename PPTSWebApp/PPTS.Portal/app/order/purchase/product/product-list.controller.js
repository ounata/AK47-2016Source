define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (helper) {
            helper.registerController('orderListController', [
                '$scope', '$state', '$stateParams', '$window', 'mcsDialogService', 'dataSyncService', 'purchaseCourseDataService',
                function ($scope, $state, $stateParams, $window, mcsDialogService, dataSyncService, purchaseCourseDataService) {
                    var vm = this;

                    var campusId = $stateParams.campusId;
                    var customerId = $stateParams.customerId;
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
                    }, ];

                    vm.account = {};


                    if (listType == 1) {
                        //常规清单

                        //实际单价
                        vm.actualPrice = function (row) {
                            if (row.tunlandAllowed == 0) { return row.productPrice; }
                            if (row.tunlandAllowed == 1) {
                                if (row.item.specialRate == 1) { return row.discount() * row.productPrice; }
                                if (row.item.specialRate < 1) { return row.item.specialRate * row.productPrice; }
                            }
                        };
                        //订购金额
                        vm.calcOrderPrice = function (row) {
                            if (row.tunlandAllowed == 0) {
                                row.orderPrice = vm.actualPrice(row) * row.item.orderAmount - (row.presetMoney || 0);
                            } else {
                                row.orderPrice = vm.actualPrice(row) * row.item.orderAmount;
                            }
                            return row.orderPrice;
                        };
                        //特殊折扣
                        vm.reCalcSpecialRate = function (row) {
                            row.item.specialRate = 1 - row.presetMoney / (row.item.orderAmount * row.productPrice);
                        };
                        //优惠金额
                        vm.reCalcPresetMoney = function (row) {
                            row.presetMoney = row.productPrice * row.item.orderAmount - vm.calcOrderPrice(row);
                        };
                        headers = headers.concat([{
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
                        }, {
                            name: "实际单价",
                            template: '{{ vm.actualPrice(row) | currency }}'

                        }, {
                            name: "订购数量",
                            template: '<span ng-if="row.canInput==1"><input type="text" ng-model="row.item.orderAmount" /></span>'
                                    + '<span ng-if="row.canInput==0">'
                                    + '<span ng-if="row.categoryType==2">{{ row.item.orderAmount }}</span>'
                                    + '<span ng-if="row.categoryType!=2">1</span>'
                                    + '</span>',

                        }, {
                            field: "ProductPrice",
                            name: "产品原价",
                            template: '{{ row.item.orderAmount * row.productPrice | currency}}'
                        }, {
                            name: "客户折扣",
                            template: '{{ row.discount() }}',
                        }, {
                            field: "item.specialRate",
                            name: "特殊折扣",
                            template: '<span ng-if="row.specialAllowed==1">'
                                    + '<span ng-if="row.categoryType==3" >'
                                    + '<span ng-if="row.tunlandAllowed == 1">--</span>'
                                    + '<span ng-if="row.tunlandAllowed == 0">{{ row.item.specialRate }}</span>'
                                    + '</span>'
                                    + '<span ng-if="row.categoryType!=3" >'
                                    + '<input type="number" ng-model="row.item.specialRate" ng-keyup="vm.reCalcPresetMoney(row)" />'
                                    + '</span>'
                                    + '</span>'
                                    + '<span ng-if="row.specialAllowed==0">--</span>'
                        }, {
                            name: "优惠金额",
                            template: '<span ng-if="row.categoryType==2 || row.categoryType ==3 ">'
                                    + '<span ng-show="row.lessonCount>1 || row.categoryType ==3"><input type="number" ng-model="row.presetMoney" ng-keyup="vm.reCalcSpecialRate(row)"/></span>'
                                    + '</span>'
                                    + '<span ng-if="row.categoryType!=2 && row.categoryType !=3 ">--</span>',
                        }, {
                            name: "订购金额",
                            template: '{{ vm.calcOrderPrice(row) | currency }}',
                        }]);

                    } else if (listType == 2) {
                        //买赠清单

                        //订购金额
                        vm.calcOrderPrice = function (row) {
                            row.orderPrice = row.item.orderAmount * row.productPrice;
                            return row.orderPrice;
                        };

                        headers = headers.concat([{
                            field: "productUnit",
                            name: "产品颗粒度",
                            template: '<span>{{ row.productUnit | unit }}</span>'
                        }, {
                            field: "productPrice",
                            name: "产品单价（元）",
                            template: '<span>{{row.productPrice | currency }}</span>',
                        }, {
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
                            template: '{{ vm.calcOrderPrice(row) | currency}}',
                        }]);

                    } else if (listType == 3) {
                        //插班清单

                        //实际单价
                        vm.actualPrice = function (row) {
                            if (row.tunlandAllowed == 0) { return row.productPrice; }
                            if (row.tunlandAllowed == 1) {
                                if (row.item.specialRate == 1) { return row.discount() * row.productPrice; }
                                if (row.item.specialRate < 1) { return row.item.specialRate * row.productPrice; }
                            }
                        };

                        //订购金额
                        vm.calcOrderPrice = function (row) {
                            row.orderPrice = row.item.orderAmount * row.productPrice;
                            return row.orderPrice;
                        };

                        headers = headers.concat([{
                            field: "lessonCount",
                            name: "总课次",
                            template: '<span>{{ row.categoryType==2 ? row.lessonCount : "--" }}</span>'
                        }, {
                            field: "noOpenClassCount",
                            name: "未上课次",
                        }, {
                            field: "remainLessonCount",
                            name: "可订购数量",
                        }, {
                            field: "productUnit",
                            name: "产品颗粒度",
                            template: '<span>{{ row.productUnit | unit }}</span>'
                        }, {
                            field: "productPrice",
                            name: "产品单价（元）",
                            template: '<span>{{row.productPrice | currency }}</span>',
                        }, {
                            name: "实际单价",
                            template: '{{ vm.actualPrice(row) | currency }}'

                        }, {
                            name: "订购数量",
                            template: '<input type="number" ng-model="row.item.orderAmount" ng-keyup="vm.reCalcPreset(row);" />',
                        }, {
                            field: "ProductPrice",
                            name: "产品原价",
                            template: '{{ row.item.orderAmount * row.productPrice | currency}}'
                        }, {
                            name: "客户折扣",
                            template: '{{ row.discount() }}',
                        }, {
                            name: "订购金额",
                            template: '{{ vm.calcOrderPrice(row) | currency}}',
                        }]);

                    }


                    vm.totalOriginalMoney = function () {
                        var total = 0;
                        $(vm.data.rows).each(function (i, v) {
                            if (v.selected) { total += v.item.orderAmount * v.productPrice; }
                        });
                        return total;
                    }
                    vm.totalMoney = function () {
                        var total = 0;
                        $(vm.data.rows).each(function (i, v) { if (v.selected) { total += v.orderPrice; } });
                        return total;
                    };

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
                        keyFields: ['cartID', 'item', 'categoryType'],
                        headers: headers,
                        pager: {
                            pageable: false
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }],
                    }


                    vm.showPursueReason = function () {
                        return $(vm.data.rowsSelected).map(function (i, v) { if (v.item.specialRate == 0) return v.cartID; }).length > 0;
                    };
                    vm.goBuy = function () {
                        $window.history.back();
                        //1 - 常规订购
                        //2 - 买赠订购
                        //3 - 插班订购
                        //4 - 补差兑换
                        //5 - 不补差兑换
                        //if (listType == 1 || listType == 2) {
                        //    $state.go('ppts.purchaseProduct', { type: listType, customerId: customerId });
                        //}  else if (listType == 3) {
                        //    $state.go('ppts.purchaseClassGroupList', { customerId: customerId });
                        //}

                    };
                    vm.delete = function () {
                        var cartIDs = $(vm.data.rowsSelected).map(function (i, v) { return v.cartID }).toArray();
                        purchaseCourseDataService.deleteShoppingCart(cartIDs, function (state) { mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected); });
                    };
                    vm.submit = function () {

                        var selectedRow = vm.account.data.rowsSelected[0];

                        purchaseCourseDataService.getServiceCharge({ customerId: customerId, campusId: campusId }, function (entity) {
                            var messageServiceMoney = '';
                            var totalMoney = vm.totalMoney();
                            if (entity.serviceCharge.length < 1) {
                                mcsDialogService.error({ title: 'Error', message: '没有创建综合服务费，请先创建综合服务费。' });
                                return;
                            } else {
                                var serviceMoney = 0;

                                var categoryTypes = $(vm.data.rowsSelected).map(function (i, v) { if (v.categoryType == 1 || v.categoryType == 2) { return v.categoryType }; }).toArray();
                                for (var index in entity.deductList) {
                                    var categoryType = entity.deductList[index].key;
                                    if ($.inArray(categoryType, [1, 2])) {
                                        if ($.inArray(categoryType, categoryTypes) > -1) {
                                            serviceMoney += entity.serviceCharge.expenseValue;
                                        }
                                    }
                                }

                                if (categoryTypes.length > 0 && serviceMoney>0) {
                                    totalMoney += serviceMoney;
                                    messageServiceMoney = '+¥' + serviceMoney + '(综合服务费)';
                                }
                            }

                            var message = '¥' + vm.totalMoney() + '(产品费用)' + messageServiceMoney + '=¥' + (totalMoney) + '(总金额)，您确认要提交审批吗？';
                            mcsDialogService.confirm({ title: 'Confirm', message: message }).result.then(function () {

                                if (selectedRow.accountMoney < vm.totalMoney()) {
                                    mcsDialogService.error({ title: 'Error', message: '该学员可用金额不足！' });
                                    return;
                                }

                                var items = $(vm.data.rowsSelected).map(function (i, v) { return v.item; }).toArray();
                                var param = {
                                    listType: listType,
                                    customerId: customerId,
                                    accountId: selectedRow.accountID,
                                    item: items,
                                    customerCampusID: campusId,
                                    chargeApplyId: vm.chargeApplyId,
                                    specialType: vm.specialType,
                                    specialMemo: vm.specialMemo,
                                    //productCampusID: '',
                                    //productCampusName:'',
                                };

                                console.debug(param)
                                //return;;
                                purchaseCourseDataService.submitShoppingCart(param, function (entity) { console.log(entity); });

                            });


                        });

                    };


                    purchaseCourseDataService.getShoppingCart({ customerId: customerId, listType: listType, campusId: campusId }, function (result) {
                        console.debug(result)

                        var rows = $(result.cart).map(function (i, v) {

                            if (v.noOpenClassCount) {
                                v.product['noOpenClassCount'] = v.noOpenClassCount;
                            }
                            if (v.remainLessonCount) {
                                v.product['remainLessonCount'] = v.remainLessonCount;
                            }
                            v.product['orderPrice'] = 0;
                            v.product['highlight'] = v.product.tunlandAllowed == 0;
                            v.product['cartID'] = v.cartID;
                            v.product['item'] = v.item;
                            v.product['discount'] = function () {
                                if (v.product.tunlandAllowed == 0) { return '--'; }
                                if (vm.account.data.rowsSelected.length < 1) return 1;
                                return vm.account.data.rowsSelected[0].discountRate;
                            };
                            return v.product;
                        });

                        vm.account.data.rows = result.account;
                        if (result.account.length > 0) {
                            vm.account.data.rowsSelected[0] = result.account[0];
                        }


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


                        //初始化缴费单下拉列表
                        var chargePays = $(result.chargePays).map(function (i, v) { return { applyID: v.applyID, payMemo: v.applyNo + ' 付款日期：' + mcs.date.format(v.payTime) + ' 缴费金额：' + v.paidMoney + '元' }; }).toArray();
                        dataSyncService.injectDictData(mcs.util.mapping(chargePays, { key: 'applyID', value: 'payMemo' }, 'ChargePayment'));
                        ppts.config.dictMappingConfig["chargePayment"] = "c_codE_ABBR_ChargePayment";


                        //选中0折
                        vm.specialType = "1";
                        $scope.$broadcast('dictionaryReady');
                    });

                }]);
        });

