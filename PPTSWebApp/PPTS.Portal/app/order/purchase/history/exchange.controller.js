define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService,
        ppts.config.dataServiceConfig.productDataService], function (helper) {

            helper.registerController('orderExchangeController', [
                '$scope', '$state', '$stateParams','$window', 'dataSyncService', 'purchaseCourseDataService','productDataService',
                function ($scope, $state, $stateParams,$window, dataSyncService, purchaseCourseDataService, productDataService) {

                    var vm = this;
                    var itemId = $stateParams.itemid;
                    var type = $stateParams.type;

                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['productID'],
                        headers: [{
                            field: "productCode",
                            name: "产品编码",
                        }, {
                            field: "productName",
                            name: "产品名称",
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
                            field: "periodDuration",
                            name: "时长（分钟）",
                            template: '<span>{{row.periodDuration | period }}</span>'
                        }, {
                            field: "lessonCount",
                            name: "总课时",
                            template: '<span>{{ row.categoryType==2 ? row.lessonCount : "--" }}</span>'
                        }, {
                            field: "productUnit",
                            name: "产品颗粒度",
                            template: '<span>{{ row.productUnit | unit }}</span>'
                        }, {
                            field: "productPrice",
                            name: "产品单价（元）",
                            template: '<span>{{row.productPrice}}</span>',
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



                    vm.search = function () {
                        console.log(vm.criteria)

                        productDataService.getAllProducts(vm.criteria, function (result) {
                            console.log(result)

                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };

                    vm.next = function () {
                        var productId = vm.data.rowsSelected[0].productID;
                        $state.go('ppts.purchaseExchangeAmount', { itemid: itemId,type:type, productid: productId });
                    };

                    vm.cancel = function () { $window.history.back();};





                    var init = (function () {

                        dataSyncService.initCriteria(vm);
                        
                        purchaseCourseDataService.getOrderItem(itemId, function (presult) {

                            console.log(presult);

                            vm.customer = { customerName: presult.entity.customerName, customerCode: presult.entity.customerCode };

                            vm.criteria.campusIDs = [presult.entity.campusID];
                            vm.criteria.categoryType = presult.entity.categoryType;
                            //vm.criteria.productStatus = '4';

                            vm.search();
                        });

                    })();

                }]);


        });