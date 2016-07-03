define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (helper) {
            helper.registerController('orderItemListController', [
                '$scope', '$state', '$location', '$stateParams', '$window', 'mcsDialogService', 'mcsValidationService', 'utilService', 'dataSyncService', 'purchaseCourseDataService',
                function ($scope, $state, $location, $stateParams, $window, mcsDialogService, mcsValidationService, utilService, dataSyncService, purchaseCourseDataService) {



                    mcsValidationService.init($scope);

                    var vm = this;
                    vm.customer = $location.$$search;

                    var campusId = $stateParams.campusId;
                    var customerId = $stateParams.customerId;
                    var listType = $scope.orderType = $stateParams.listType || 1; // 1:常规清单  2:买赠清单

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


                    vm.customValidate = function (row) {
                        var result = row.item.orderAmount % 0.5 == 0;
                        //console.log(result)
                        row.errorRowMessage = '只支持正数，且大于等于0.5，以0.5为最小单位!';
                        row.validValue = result;
                        $scope.$apply('discountData');
                        return result;
                    };

                    //实际单价
                    vm.actualPrice = function (row) {
                        if (listType == 2) { return row.productPrice; }
                        if (row.tunlandAllowed == 0) {
                            return row.productPrice;
                        } else if (row.specialAllowed == 0 && row.tunlandAllowed == 1) {
                            return row.discount() * row.productPrice;
                        } else if (row.specialAllowed == 1 && row.tunlandAllowed == 1) {
                            if (row.item.specialRate) {
                                return row.item.specialRate * row.productPrice;
                            } else {
                                return row.discount() * row.productPrice;
                            }
                        }
                    };


                    vm.hasZeroDiscount = function () {
                        var zeros = $(vm.data.rows).map(function (i, v) { if (v.item.specialRate == "0") { return v.item.specialRate; } }).toArray();
                        return !(zeros.length > 0);
                    };
                    vm.totalOriginalMoney = function () {
                        var total = 0;
                        $(vm.data.rows).each(function (i, v) {
                            total += v.item.orderAmount * v.productPrice;
                        });
                        return total;
                    }
                    vm.totalMoney = function () {
                        var total = 0;
                        $(vm.data.rows).each(function (i, v) {
                            var totalPrice = v.item.orderAmount * vm.actualPrice(v);
                            total += totalPrice;
                        });
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
                    };


                    vm.showPursueReason = function () {
                        return $(vm.data.rows).map(function (i, v) { if (v.item.specialRate == 0) return v.cartID; }).length > 0;
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
                        if (!utilService.selectMultiRows(vm)) { return; }
                        var cartIDs = $(vm.data.rowsSelected).map(function (i, v) { return v.cartID }).toArray();
                        purchaseCourseDataService.deleteShoppingCart(cartIDs, function (state) { mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected); });
                    };
                    vm.submit = function () {

                        if (vm.data.rows.length < 1) {
                            mcsDialogService.error({ title: '错误', message: '请选购产品后，进行提交！' });
                            return;
                        } else {
                            var invalids = $(vm.data.rows).map(function (i, v) { if (v.isvalid()) { return v.productID; } });
                            if (invalids.length > 0) {
                                mcsDialogService.error({ title: '错误', message: '提交数据有误，请进行更正！' });
                                return;
                            }
                        }

                        if (!mcsValidationService.run($scope)) { return; }

                        if (vm.account.data.rowsSelected.length < 1) {
                            mcsDialogService.error({ title: '错误', message: '没有选择帐户，不可以进行提交！' });
                        }

                        //if (!utilService.selectMultiRows(vm)) { return; }

                        var isContainsNaN = false;
                        $(vm.data.rows).each(function (i, v) { if (v.selected && isNaN(v.item.orderAmount)) { isContainsNaN = true; } });
                        if (isContainsNaN) {
                            mcsDialogService.error({ title: '错误', message: '提交数据有误，订购的数量没有填写。' });
                            return;
                        }

                        var selectedRow = vm.account.data.rowsSelected[0];

                        purchaseCourseDataService.getServiceCharge({ customerId: customerId, campusId: campusId }, function (entity) {

                            //console.log(entity)
                            var messageServiceMoney = '';
                            var totalMoney = vm.totalMoney();

                            var categoryTypes = $(vm.data.rows).map(function (i, v) { if (v.categoryType == 1 || v.categoryType == 2) { return v.categoryType }; }).toArray();
                            if (categoryTypes.length > 0) {

                                var serviceMoney = 0;
                                if (!entity.deductList[3].value) {
                                    if (entity.deductList[3].sc) {
                                        serviceMoney += entity.deductList[3].sc.expenseValue;
                                    } else {
                                        var isdeduct1 = !entity.deductList[1].value && $.inArray(1, categoryTypes);
                                        var isdeduct2 = !entity.deductList[2].value && $.inArray(2, categoryTypes);

                                        if ((isdeduct1 && !entity.deductList[1].sc) || (isdeduct2 && !entity.deductList[2].sc)) {
                                            mcsDialogService.error({ title: '错误', message: '没有创建综合服务费，请先创建综合服务费。' });
                                            return;
                                        }

                                        if (isdeduct1) {
                                            serviceMoney += entity.deductList[1].sc.expenseValue;
                                        }
                                        if (isdeduct2) {
                                            serviceMoney += entity.deductList[2].sc.expenseValue;
                                        }
                                    }
                                }

                                if (categoryTypes.length > 0 && serviceMoney > 0) {
                                    totalMoney += serviceMoney;
                                    messageServiceMoney = '+¥' + serviceMoney + '(综合服务费)';
                                }
                            }


                            var message = '¥' + vm.totalMoney() + '(产品费用)' + messageServiceMoney + '=¥' + (totalMoney) + '(总金额)，您确认要提交审批吗？';
                            mcsDialogService.confirm({ title: '确认', message: message }).result.then(function () {

                                if (selectedRow.accountMoney < vm.totalMoney()) {
                                    mcsDialogService.error({ title: '错误', message: '该学员可用金额不足！' });
                                    return;
                                }

                                var items = $(vm.data.rows).map(function (i, v) { return v.item; }).toArray();
                                var param = {
                                    listType: listType,
                                    customerId: customerId,
                                    accountId: selectedRow.accountID,
                                    item: items,
                                    customerCampusID: campusId,
                                    chargeApplyId: vm.chargeApplyId,
                                    specialType: vm.specialType,
                                    specialMemo: vm.specialMemo,

                                };

                                //console.log(param)
                                //return;

                                purchaseCourseDataService.submitShoppingCart(param, function (entity) {
                                    //vm.goBuy();
                                    $state.go('ppts.purchase');
                                });

                            });


                        });

                    };


                    function setHeaders(result) {

                        var present = result.present;

                        if (listType == 1) {
                            //常规清单

                            //1对1类产品只支持正数，且大于等于0.5，以0.5为最小单位。其他-实物、其他-虚拟只支持正整数。
                            var template = '<span ng-show="row.categoryType==41 || row.categoryType==42"><mcs-input css="mcs-input-small" model="row.item.orderAmount" datatype="int" min="1"/></span>'
                                         + '<span ng-show="row.categoryType==1" ng-class=\'{"has-error":!( row.validValue == undefined ? true: row.validValue)}\' ><mcs-input css="mcs-input-small" model="row.item.orderAmount" min="0.5" validate="vm.customValidate(row)"/><div class=\'help-inline\' ng-if="!row.validValue">{{row.errorRowMessage}}</div></span>';


                            template = '<span ng-if="row.canInput==1">'
                                     + template
                                     + '</span>'
                                     + '<span ng-if="row.canInput==0">'
                                     + '<span ng-if="row.categoryType==2">{{ row.item.orderAmount }}</span>'
                                     + '<span ng-if="row.categoryType!=2">1</span>'
                                     + '</span>';




                            ////实际单价
                            //vm.actualPrice = function (row) {
                            //    if (row.tunlandAllowed == 0 || !row.item.specialRate) { return row.productPrice; }
                            //    if (row.tunlandAllowed == 1) {
                            //        if (row.item.specialRate == 1) { return row.discount() * row.productPrice; }
                            //        if (row.item.specialRate < 1) { return row.item.specialRate * row.productPrice; }
                            //    }
                            //};

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
                            //是否展示 优惠金额 输入
                            vm.isShowPresetMoney = function () {
                                var classgroupArray = new Array();
                                var youxueArray = new Array();
                                $(vm.data.rows).map(function (i, v) {
                                    if (v.categoryType == 2) { classgroupArray.push(v); }
                                    else if (v.categoryType == 3) { youxueArray.push(v); }
                                });
                                return classgroupArray.length >= 2 || youxueArray.length > 0;
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
                                template: template,

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
                                        + '<mcs-input css="mcs-input-small"  model="row.item.specialRate" ng-keyup="vm.reCalcPresetMoney(row)" datatype="float" between="0,0.9999"/>'
                                        + '</span>'
                                        + '</span>'
                                        + '<span ng-if="row.specialAllowed==0">--</span>'
                            }, {
                                name: "优惠金额",
                                template: '<span ng-if="vm.isShowPresetMoney()"><mcs-input css="mcs-input-small" model="row.presetMoney" ng-keyup="vm.reCalcSpecialRate(row)" min="0"/></span>'
                                        + '<span ng-if="!vm.isShowPresetMoney()">--</span>',
                                //template: '<span ng-if="row.categoryType==2 || row.categoryType ==3 ">'
                                //        + '<span ng-show="row.lessonCount>1 || row.categoryType ==3"><mcs-input css="mcs-input-small" model="row.presetMoney" ng-keyup="vm.reCalcSpecialRate(row)" min="0"/></span>'
                                //        + '</span>'
                                //        + '<span ng-if="row.categoryType!=2 && row.categoryType !=3 ">--</span>',
                            }, {
                                name: "订购金额",
                                template: '{{ vm.calcOrderPrice(row) | currency }}',
                            }]);

                        } else if (listType == 2) {
                            //买赠清单

                            vm.reCalcPreset = function (row) {
                                if (present && present.items.length > 0) {
                                    for (var index in present.items) {
                                        if (row.item.orderAmount >= present.items[index].presentStandard) {
                                            row.item.presentAmount = present.items[index].presentValue;
                                        }
                                    }
                                }
                            };

                            var initrow = (function () {
                                $(result.cart).each(function (i, v) { vm.reCalcPreset(v); });
                            })();

                            //验证买赠 订购数量
                            vm.validateOrder = function (row) {

                                var min = present.items[present.items.length - 1].presentStandard;
                                var max = present.items[0].presentStandard;

                                if (row.item.orderAmount < min || row.item.orderAmount > max) {
                                    row.errorRowMessage_order = '不符合买赠规则，请重新填写或者进行常规订购!';
                                    row.validValue_order = false;
                                    $scope.$apply('discountData');
                                    return false;
                                }

                                return vm.customValidate(row);
                            };

                            //验证买赠 赠送数量
                            vm.validatePreset = function (row) {
                                var result = parseInt(row.item.presentAmount) == row.item.presentAmount;
                                if (!result) {
                                    row.errorRowMessage_preset = '输入有误!';
                                } else {
                                    result = !(row.item.presentAmount > row.item.orderAmount / 2);
                                    row.errorRowMessage_preset = '最高不大于订购数量的50%!';
                                }
                                row.validValue_preset = result;
                                $scope.$apply('discountData');
                                return result;
                            };

                            //1对1类产品只支持正数，且大于等于0.5，以0.5为最小单位。其他-实物、其他-虚拟只支持正整数。
                            var template = '<span ng-show="row.categoryType==41 || row.categoryType==42">'
                                         + '<mcs-input css="mcs-input-small" model="row.item.orderAmount" ng-keyup="vm.reCalcPreset(row);" validate="vm.validateOrder(row)" datatype="int" min="1"/>'
                                         + '</span>'
                                         + '<span ng-show="row.categoryType==1" ng-class=\'{"has-error":!( row.validValue_order == undefined ? true: row.validValue_order)}\' ><mcs-input css="mcs-input-small" model="row.item.orderAmount" ng-keyup="vm.reCalcPreset(row);" min="0.5" validate="vm.validateOrder(row)"/><div class=\'help-inline\' ng-if="!row.validValue_order">{{row.errorRowMessage_order}}</div></span>';

                            //订购金额
                            vm.calcOrderPrice = function (row) {
                                row.orderPrice = row.item.orderAmount * row.productPrice;
                                return row.orderPrice;
                            };

                            vm.totalAmount = function (row) { return parseInt(row.item.orderAmount) + parseInt(row.item.presentAmount); };

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
                                template: template
                            }, {
                                name: "赠送数量",
                                template: '<span ng-class=\'{"has-error":!( row.validValue_preset == undefined ? true: row.validValue_preset)}\'><mcs-input css="mcs-input-small" model="row.item.presentAmount" validate="vm.validatePreset(row)" datatype="int" min="0" /><div class=\'help-inline\' ng-if="!row.validValue_preset">{{row.errorRowMessage_preset}}</div></span>'

                            }, {
                                name: "总订购数量",
                                template: '{{ vm.totalAmount(row) }}',
                            }, {
                                name: "订购金额",
                                template: '{{ vm.calcOrderPrice(row) | currency}}',
                            }]);

                        } else if (listType == 3) {
                            //插班清单

                            //1对1类产品只支持正数，且大于等于0.5，以0.5为最小单位。其他-实物、其他-虚拟只支持正整数。
                            var template = '<span ng-show="row.categoryType==41 || row.categoryType==42"><mcs-input css="mcs-input-small" model="row.item.orderAmount"  datatype="int" min="1"/></span>'
                                         + '<span ng-show="row.categoryType==1" ng-class=\'{"has-error":!( row.validValue == undefined ? true: row.validValue)}\' ><mcs-input css="mcs-input-small" model="row.item.orderAmount"  min="0.5" validate="vm.customValidate(row)"/><div class=\'help-inline\' ng-if="!row.validValue">{{row.errorRowMessage}}</div></span>';


                            ////实际单价
                            //vm.actualPrice = function (row) {
                            //    if (row.tunlandAllowed == 0) { return row.productPrice; }
                            //    if (row.tunlandAllowed == 1) {
                            //        if (row.item.specialRate == 1) { return row.discount() * row.productPrice; }
                            //        if (row.item.specialRate < 1) { return row.item.specialRate * row.productPrice; }
                            //    }
                            //};

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
                                template: template,
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

                        };

                        vm.data.headers = headers;
                    }

                    var init = (function () {


                        purchaseCourseDataService.getShoppingCart({ customerId: customerId, listType: listType, campusId: campusId }, function (result) {

                            console.log(result)


                            vm.account.data.rows = result.account;
                            if (result.account.length > 0) {
                                vm.account.data.rowsSelected[0] = $(result.account).map(function (i, v) {
                                    if (v.selected) {
                                        var selectedModel = {};
                                        for (var index in vm.account.data.keyFields) {
                                            var field = vm.account.data.keyFields[index];
                                            selectedModel[field] = v[field];
                                        }
                                        return selectedModel;
                                    }
                                })[0];
                            }

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

                                v.product['isvalid'] = function () { return this.validValue_preset == false || this.validValue_order == false; };

                                return v.product;
                            });

                            setHeaders(result);

                            vm.data.rows = rows.toArray();


                            $scope.$watch('vm.account.data.rowsSelected[0].accountID', function (value) {
                                if (result && mcs.util.isArray(result.chargePays)) {
                                    vm.chargePayments = [];
                                    if (value) {
                                        var items = result.chargePays.filter(function (item) {
                                            return item.accountID == value;
                                        });
                                        items.forEach(function (item) {
                                            vm.chargePayments.push({
                                                key: item.accountID,
                                                value: mcs.util.format('{0} 付款日期：{1} 缴费金额：{2}元', item.applyNo, mcs.date.format(item.payTime), item.paidMoney)
                                            });
                                        });
                                    }
                                }
                            });

                            $scope.$broadcast('dictionaryReady');
                        });

                    })();
                }]);
        });