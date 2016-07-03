define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
            product.registerController('orderClassGroupController', [
                '$scope', '$state','$location', '$stateParams', 'dataSyncService', 'utilService', 'purchaseCourseDataService',
                function ($scope, $state, $location, $stateParams, dataSyncService, utilService, purchaseCourseDataService) {

                    var vm = this;
                    vm.customer = $location.$$search;

                    var productId = $stateParams.productId;
                    var customerId = $stateParams.customerId;
                    var productCampusId = $stateParams.productCampusId ;


                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['classID'],
                        headers: [{
                            field: "className",
                            name: "班级名称",
                        }, {
                            name: "未上课次",
                            template: '{{row.lessonCount - row.finishedLessons}}'
                        }, {
                            name: "可订购数量",
                            template: '{{ row.lessonCount - row.invalidLessons }}'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: ppts.config.pageSizeItem,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                purchaseCourseDataService.getPagedClasses(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }


                    vm.search = function () {

                        purchaseCourseDataService.getPagedClasses(vm.criteria, function (result) {


                            vm.data.rows = result.pagedData; 
                            dataSyncService.updateTotalCount(vm, result);

                            $scope.$broadcast('dictionaryReady');

                        });

                    };
                    vm.goBack = function () {
                        var param =$.extend($stateParams, vm.customer);
                        $state.go('ppts.purchaseClassGroupList', param);
                    };
                    vm.buy = function () {

                        if (!utilService.selectMultiRows(vm)) { return; }
                        var data = $(vm.data.rowsSelected).map(function (i, v) { return { productID: productId, customerID: customerId, classId: v.classID, orderType: 3, productCampusID: productCampusId }; }).toArray();
                        purchaseCourseDataService.addShoppingCart(data, function (entity) { console.log(entity); });
                    };

                    var init = (function () {

                        dataSyncService.initCriteria(vm);
                        vm.search();
                    })();




                }]);
        });

