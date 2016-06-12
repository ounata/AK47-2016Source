define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService,
        ppts.config.dataServiceConfig.unsubscribeCourseDataService], function (helper) {

            helper.registerController('orderListUnsubscribeController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'purchaseCourseDataService','unsubscribeCourseDataService',
                function ($scope, $state, $stateParams, dataSyncService, purchaseCourseDataService, unsubscribeCourseDataService) {
                    var vm = this;
                    vm.item = { order: { submitTime:new Date()}, item: {} };
                    var id = vm.item.orderItemID = $stateParams.id;
                    
                    vm.applicant = function () { return ppts.user.name };
                    

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



                        unsubscribeCourseDataService.unsubscribe(vm.item, function (entity) {
                            $state.go('ppts.purchase');
                        });

                    };



                    purchaseCourseDataService.getOrderItem(id, function (result) {

                        console.log(result)

                        var entity = result.entity;


                        //买赠
                        if (entity.presentID != "") {
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
                                template: '{{ row.confirmedAmount > row.orderAmount ? row.presetAmount -(row.confirmedAmount - row.orderAmount ) :row.presetAmount  }}'
                                        + '({{ row.confirmedAmount > row.orderAmount ? row.assignedAmount-(row.orderAmount - row.confirmedAmount) : 0 }})'
                            }, {
                                name: "申请退掉数量",
                                template: '{{ row.debookedAmount  }}'
                            }]);
                        } else {

                            //1对1 班组
                            if (entity.categoryType == 1 || entity.categoryType == 2) {
                                vm.data.headers = vm.data.headers.concat([{
                                    field: "confirmedAmount",
                                    name: "已使用数量",
                                }, {
                                    //field: "assignedAmount",
                                    name: "剩余数量（排课数量）",
                                    template: '{{row.orderAmount - row.confirmedAmount}}({{row.assignedAmount}})'
                                }, {
                                    name: "申请退掉数量",
                                    //template: '<input ng-model="vm.item.item.debookAmount" positive/>'
                                    template: '<mcs-input model="vm.item.item.debookAmount" datatype="int"/>'
                                }, {
                                    name: "已使用及保留数量对应的金额",
                                    template: '{{ (row.realAmount - row.confirmedAmount)*row.realPrice  | currency}}'
                                }, {
                                    name: "退订金额",
                                    template: '{{ (vm.item.item.debookAmount||0)*row.realPrice  | currency}}'
                                }]);
                            } else {
                                //其他 、游学
                                vm.data.headers = vm.data.headers.concat([ {
                                    name: "退订金额",
                                    template: '{{ (vm.item.item.debookAmount||0)*row.realPrice  | currency}}'
                                }]);
                            }
                        }


                        vm.entity = entity;
                        vm.data.rows = [entity];
                    });

                }]);

        });