define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (helper) {

            helper.registerController('orderPrintController', [
                '$scope', '$state', '$stateParams', '$window', 'printService', 'dataSyncService', 'purchaseCourseDataService',
                function ($scope, $state, $stateParams, $window,printService, dataSyncService, purchaseCourseDataService) {
                    var vm = this;
                    var itemId = $stateParams.id;


                    vm.data = {
                        headers: [{
                            field: "productCode",
                            name: "产品编码",
                            //template: '<a ui-sref="ppts.productView({id:row.productID})">{{row.productCode}}</a>',
                            //sortable: true
                        }, {
                            field: "productName",
                            name: "产品名称",
                            //template: '<a ui-sref="ppts.productView({id:row.productID})">{{row.productName}}</a>'
                        }, {
                            field: "grade",
                            name: "年级",
                            template: '<span>{{row.grade | grade}}</span>'
                        },{
                            field: "subject",
                            name: "科目",
                            template: '<span>{{row.subject | subject}}</span>'
                        }, {
                            field: "courseLevel",
                            name: "课程级别",
                            template: '<span>{{row.courseLevel | courseLevel}}</span>'
                        },{
                            field: "periodDuration",
                            name: "时长（分钟）",
                            template: '<span>{{row.periodDuration | period }}</span>'
                        }, {
                            name: "产品单价（元）",
                            template: '<span>{{row.productPrice}}</span>',
                        }, {
                            name: "实际单价",
                            template: '{{ row.realPrice | currency }}'

                        }, {
                            field: "orderAmount",
                            name: "订购数量",
                        }, {
                            name: "产品原价",
                            template: '{{ row.orderAmount * row.productPrice | currency }}'

                        }, {
                            name: "客户折扣",
                            template: '{{ row.discountRate }}',
                        }, {
                            name: "订购金额",
                            template: '<span>{{ row.realPrice * row.realAmount  }}</span>'
                        }],
                        pager: {
                            pageable:false
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }

                    vm.cancel = function () { $window.history.back(); };
                    vm.print = function () { printService.print(); };



                    var init = (function () {

                        purchaseCourseDataService.getOrderItem(itemId, function (result) {
                            console.log(result);

                            vm.entity = result.entity;

                            vm.data.rows = [result.entity];

                        });

                    })();

                }]);
        });