define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService,
        ppts.config.dataServiceConfig.unsubscribeCourseDataService], function (helper) {

            helper.registerController('orderListUnsubscribeController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'mcsValidationService', 'purchaseCourseDataService', 'unsubscribeCourseDataService',
                function ($scope, $state, $stateParams, dataSyncService, mcsValidationService, purchaseCourseDataService, unsubscribeCourseDataService) {

                    mcsValidationService.init($scope);

                    var vm = this;
                    vm.item = { order: { submitTime: new Date() }, item: {} };
                    var id = vm.item.orderItemID = $stateParams.id;



                    vm.data = {
                        //selection: 'radio',
                        //rowsSelected: [],
                        //keyFields: ['orderID'],
                        headers: [{
                            field: "productCode",
                            name: "产品编号",
                        }, {
                            field: "productName",
                            name: "产品名称",
                        }, {
                            //field: "categoryType",
                            name: "产品类型",
                            template: '<span>{{row.categoryType | categoryType}}</span>'
                        }, {
                            name: "实际单价",
                            template: '<span>{{row.realPrice | currency}}</span>',
                        }, {
                            name: "产品单价",
                            template: '<span>{{row.orderPrice | currency}}</span>',
                        }, {
                            name: "订购金额",
                            template: '<span>{{row.realPrice * row.realAmount | currency}}</span>'
                        }, {
                            name: "订购数量",
                            template: '<span>{{ row.orderAmount }}</span>'
                        }, ],
                        pager: {
                            pageabled: false
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }


                    vm.submit = function () {

                        if (!mcsValidationService.run($scope)) { return; }

                        unsubscribeCourseDataService.unsubscribe(vm.item, function (entity) {
                            $state.go('ppts.purchase');
                        });

                    };
                    vm.applicant = function () { return ppts.user.name };
                    

                    purchaseCourseDataService.getOrderItem(id, function (result) {

                        console.log(result)

                        var entity = result.entity;

                        //买赠
                        if (entity.orderType == "2") {

                            vm.debookTotalMoney = function () {
                                return (entity.realAmount - entity.confirmedAmount) * entity.realPrice;
                            };

                            //剩余赠送数量
                            vm.remainPresentCount = function (row) {
                                return row.confirmedAmount > row.orderAmount ? row.presentAmount - (row.confirmedAmount - row.orderAmount) : row.presentAmount;
                            };

                            //退订金额
                            vm.debookMoney = function (row) {
                                vm.item.item.debookAmount = row.realAmount - row.confirmedAmount;
                                return vm.item.item.debookAmount * row.realPrice;
                            };


                            vm.data.headers = vm.data.headers.concat([{
                                field: "confirmedAmount",
                                name: "已使用订购数量",
                            }, {
                                name: "已使用赠送数量",
                                template: '{{ row.confirmedAmount > row.orderAmount ? row.confirmedAmount - row.orderAmount:0  }}'
                            }, {
                                name: "剩余订购数量(排课数量)",
                                template: '{{ row.confirmedAmount > row.orderAmount? 0:row.orderAmount-row.confirmedAmount  }}'
                                        + '({{ row.confirmedAmount > row.orderAmount ? 0: row.assignedAmount > ( row.orderAmount - row.confirmedAmount )? (row.orderAmount - row.confirmedAmount):row.assignedAmount  }})'
                            }, {
                                name: "剩余赠送数量(排课数量)",
                                template: '{{ vm.remainPresentCount(row)  }}'
                                        + '({{ row.confirmedAmount > row.orderAmount ? row.assignedAmount-(row.orderAmount - row.confirmedAmount) : 0 }})'
                            }, {
                                name: "申请退掉数量(退赠送数量)",
                                template: '{{ row.realAmount - row.confirmedAmount  }}({{ vm.remainPresentCount(row) }})'
                            }, {
                                name: "已使用保留数量对应的金额",
                                template: '{{ row.realAmount * row.realPrice - row.confirmedMoney - row.realPrice * row.debookedAmount |currency }}'
                            }, {
                                name: "退订金额",
                                template: '{{ vm.debookMoney(row) |currency }}'
                            }]);

                        } else {

                            vm.debookTotalMoney = function () {
                                return vm.item.item.debookAmount * entity.realPrice;
                            };

                            vm.customValidate = function (row) {
                                var result = row.amount  >= vm.item.item.debookAmount && vm.item.item.debookAmount > 0;
                                row.errorRowMessage = '输入有误!';
                                row.validValue = result;
                                $scope.$apply('discountData');
                                return result;
                            };

                            //1对1 班组
                            if (entity.categoryType == 1 || entity.categoryType == 2) {
                                vm.data.headers = vm.data.headers.concat([{
                                    field: "confirmedAmount",
                                    name: "已使用数量",
                                }, {
                                    name: "剩余数量（排课数量）",
                                    template: '{{ row.amount }}({{row.assignedAmount}})'
                                }, {
                                    name: "申请退掉数量",
                                    template: '<span ng-class=\'{"has-error":!( row.validValue == undefined ? true: row.validValue)}\'><mcs-input model="vm.item.item.debookAmount" datatype="int"  validate="vm.customValidate(row)" required /><div class=\'help-inline\' ng-if="!row.validValue">{{row.errorRowMessage}}</div></span>'
                                }, {
                                    name: "已使用及保留数量对应的金额",
                                    template: '{{ row.amount * row.realPrice  | currency}}'
                                }, {
                                    name: "退订金额",
                                    template: '{{ (vm.item.item.debookAmount||0)*row.realPrice  | currency}}'
                                }]);
                            } else {

                                //退订金额
                                vm.debookMoney = function (row) {
                                    vm.item.item.debookAmount = row.amount ;
                                    return vm.item.item.debookAmount * row.realPrice;
                                };

                                vm.debookTotalMoney = function () {
                                    return row.amount * entity.realPrice;
                                };

                                //其他 、游学
                                vm.data.headers = vm.data.headers.concat([{
                                    name: "退订金额",
                                    template: '{{ vm.debookMoney(row)  | currency}}'
                                }]);
                            }
                        }


                        vm.entity = entity;
                        vm.data.rows = [entity];
                    });

                }]);

        });