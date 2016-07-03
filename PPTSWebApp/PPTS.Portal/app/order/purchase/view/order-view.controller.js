define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (helper) {
            helper.registerController('orderViewController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'purchaseCourseDataService',
                function ($scope, $state, $stateParams, dataSyncService, purchaseCourseDataService) {

                    //define
                    var vm = this;
                    var orderId = $stateParams.orderId;
                    var headers = [{
                        field: "productCode",
                        name: "产品编码",
                    }, {
                        field: "productName",
                        name: "产品名称",
                    }, {
                        //field: "productName",
                        name: "班级名称",
                        template: '<span ng-if="row.orderType==3"> {{ row.joinedClassName }}</span>'
                                + '<span ng-if="row.orderType!=3">--</span>'
                    }, {
                        field: "catalogName",
                        name: "产品类型"
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
                        name: "时长（分钟）",
                        template: '<span>{{row.lessonDurationValue }}</span>'
                    }, {
                        field: "lessonCount",
                        name: "总课次",
                        template: '<span>{{ row.categoryType==2 ? row.lessonCount : "--" }}</span>'
                    }, {
                        //field: "Amount",
                        name: "未上课次",
                        template: '<span>{{ row.amount }}</span>'
                    }, {
                        field: "productUnit",
                        name: "产品颗粒度",
                        template: '<span>{{ row.productUnit | unit }}</span>'
                    }, {
                        name: "产品单价（元）",
                        template: '<span>{{row.orderPrice | currency }}</span>',
                    }, {
                        name: "实际单价",
                        template: '{{ row.realPrice | currency }}'
                    }, {
                        name: "订购数量",
                        template: '{{ row.orderAmount }}',
                    }, {
                        name: "赠送数量",
                        template: '{{ row.presentAmount }}',
                    }, {
                        name: "总订购数量",
                        template: '{{ row.realAmount }}',
                    }, {
                        name: "客户折扣",
                        template: '{{ row.discountRate >0 ?row.discountRate:"--" }}',
                    }, {
                        name: "特殊折扣",
                        template: '{{ row.specialRate >0 ?row.specialRate:"--" }}',
                    }, {
                        name: "订购金额",
                        template: '{{ row.realPrice*row.realAmount | currency}}',
                    }, ];



                    //init
                    vm.data = {
                        headers: headers,
                        pager: {
                            pageable: false
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }],
                    }


                    //function




                    //load
                    var init=(function () {
                        
                        purchaseCourseDataService.getOrder({ orderId: orderId }, function (result) {

                            //console.log(result);

                            vm.account = result.account;
                            vm.order = result.order;

                            if (result.chargePayment) {
                                vm.chargePayment = mcs.util.format('{0} 付款日期：{1} 缴费金额：{2}元', result.chargePayment.applyNo, mcs.date.format(result.chargePayment.payTime), result.chargePayment.paidMoney);
                            }
                            //console.log(vm)

                            vm.data.rows = result.items;
                            $scope.$broadcast('dictionaryReady');
                        });

                    })();

                }]);

        });

