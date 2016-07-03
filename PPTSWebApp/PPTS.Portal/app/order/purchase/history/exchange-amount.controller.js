define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService], function (helper) {

            helper.registerController('orderExchangeAmountController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'purchaseCourseDataService',
                function ($scope, $state, $stateParams, dataSyncService, purchaseCourseDataService) {

                    var vm = this;
                    var itemId = $stateParams.itemid;
                    var type = $scope.type = $stateParams.type;
                    var productId = $stateParams.productid;

                    vm.data = {
                        headers: [{
                            field: "assetCode",
                            name: "订单编号",
                        }, {
                            field: "productName",
                            name: "资产名称",
                        }, {
                            name: "剩余需兑换数量（已排课）",
                            template: '<span>{{ row.realAmount - row.confirmedAmount - row.debookedAmount }}({{row.assignedAmount}})</span>'
                        }, {
                            field: "realPrice",
                            name: "购买单价",
                        }],
                        pager: {
                            pageable: false
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }



                    vm.goBack = function () {
                        $state.go('ppts.purchaseExchange-' + type, $stateParams);
                    };
                    vm.save = function () {

                        purchaseCourseDataService.exchangeOrder({ itemId: itemId, productId: productId, type: type }, function (entity) { $state.go('ppts.purchase'); });

                    };



                    var init = (function () {

                        if (type == "one") {
                            vm.data.headers = vm.data.headers.concat([{
                                field: "discountRate",
                                name: "原购买单价享受折扣",
                            }, {
                                name: "新产品原单价",
                                template: '{{ row.showProductPrice() | currency }}'
                            }, {
                                name: "新产品折扣后单价",
                                template: '{{ row.showProductDiscountPrice(row) | currency }}'
                            }, {
                                name: "兑换新产品数量",
                                template: '{{ row.showExchangeCount(row)  }}'
                            }, {
                                name: "兑换资产日期",
                                template: '{{ row.exchangeDate | date:"yyyy-MM-dd" }}'
                            }]);
                        } else if (type == "two") {
                            vm.data.headers = vm.data.headers.concat([{
                                name: "新产品单价",
                                template: '<span>{{ vm.product.productPrice }}</span>'
                            }, {
                                name: "新产品兑换后单价",
                                template: '<span>{{ row.orderPrice }}</span>'
                            }, {
                                name: "兑换新产品数量",
                                template: '<span>{{ row.amount }}</span>'
                            }, {
                                name: "资产兑换日期",
                                template: '<span>{{ row.exchangeDate | date:"yyyy-MM-dd"}}</span>'
                            }]);
                        }


                        purchaseCourseDataService.getExchangeInfo({ itemId: itemId, productId: productId }, function (result) {

                            console.log(result);

                            vm.customer = { customerName: result.orderItem.customerName, customerCode: result.orderItem.customerCode };
                            vm.product = result.product;

                            if (type == "one") {

                                result.asset['showProductPrice'] = function () { return vm.product.productPrice; };
                                result.asset['showProductDiscountPrice'] = function (row) { return vm.product.productPrice * row.discountRate; };
                                result.asset['showExchangeCount'] = function (row) { return (row.realAmount - row.confirmedAmount - row.debookedAmount) * row.realPrice / row.showProductDiscountPrice(row); };
                            }

                            result.asset['exchangeDate'] = new Date();

                            vm.data.rows = [result.asset];
                            $scope.$broadcast('dictionaryReady');
                        });

                    })();



                }]);

        });