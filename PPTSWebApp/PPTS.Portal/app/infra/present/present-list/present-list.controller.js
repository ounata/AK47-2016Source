define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.presentDataService],
        function (customer) {
            customer.registerController('presentListController', ['$scope', '$stateParams', 'dataSyncService', 'utilService', 'presentDataService', 'presentListDataHeader', 'mcsDialogService',
                function ($scope, $stateParams, dataSyncService, util, presentDataService, presentListDataHeader, mcsDialogService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    dataSyncService.configDataHeader(vm, presentListDataHeader, presentDataService.getPagedPresents);

                    vm.search = function () {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.customerID = $stateParams.id;
                        dataSyncService.injectDynamicDict('dateRange,ifElse');
                        dataSyncService.initDataList(vm, presentDataService.getAllPresents, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();

                    vm.delete = function () {
                        if (util.selectOneRow(vm)) {
                            vm.confirm = function () {
                                var dlg = mcsDialogService.confirm({
                                    title: '提示',
                                    message: '你确定要删除这条数据吗?'
                                });
                                dlg.result.then(function () {
                                    vm.criteria = vm.criteria || {};
                                    vm.criteria.presentID = vm.data.rowsSelected[0].presentID;
                                    presentDataService.getDeletePresent(vm.criteria, function () {
                                        vm.init();
                                    });
                                })
                            };
                            vm.confirm();
                        }
                    };

                    vm.disable = function () {
                        if (util.selectOneRow(vm)) {
                            vm.confirm = function () {
                                var dlg = mcsDialogService.confirm({
                                    title: '提示',
                                    message: '你确定要停用这条数据吗?'
                                });
                                dlg.result.then(function () {
                                    vm.criteria = vm.criteria || {};
                                    vm.criteria.presentID = vm.data.rowsSelected[0].presentID;
                                    presentDataService.getDisablePresent(vm.criteria, function () {
                                        vm.init();
                                    });
                                })
                            };
                            vm.confirm();
                        }
                    };

                    vm.export = function () {
                        mcs.util.postMockForm(ppts.config.productApiBaseUrl + 'api/present/exportAllPresents', vm.criteria);
                    };
                }]);
        });