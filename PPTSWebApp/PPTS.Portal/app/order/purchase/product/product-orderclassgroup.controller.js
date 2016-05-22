define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService,
        ppts.config.dataServiceConfig.productDataService],
        function (product) {
            product.registerController('orderClassGroupController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'purchaseCourseDataService',
                function ($scope, $state, $stateParams, dataSyncService, purchaseCourseDataService) {
                    var vm = this;
                    var productId = $stateParams.productId;
                    var customerId = $stateParams.customerId;
                    var productCampusId = $stateParams.productCampusId || 8;

                    //查询条件
                    vm.criteria = { productID: productId, className: '', };

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
                            pageSize: 10,
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


                    dataSyncService.initCriteria(vm);
                    vm.search = function () {

                        purchaseCourseDataService.getPagedClasses(vm.criteria, function (result) {

                            console.log(result)

                            vm.data.rows = result.pagedData;
                            dataSyncService.updateTotalCount(vm, result.pagedData);
                            $scope.$broadcast('dictionaryReady');



                        });

                    };


                    vm.goBack = function () { $state.go('ppts.purchaseClassGroupList', { customerId: customerId }); };

                    vm.buy = function () {
                        var data = $(vm.data.rowsSelected).map(function (i, v) { return { productID: productId, customerID: customerId, classId: v.classID, orderType: 3, productCampusID: productCampusId }; }).toArray();
                        purchaseCourseDataService.addShoppingCart(data, function (entity) { console.log(entity); });
                    };

                    vm.init = function () {
                        vm.search();
                    };
                    vm.init();




                }]);
        });

