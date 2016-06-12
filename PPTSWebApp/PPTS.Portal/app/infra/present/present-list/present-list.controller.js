define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.presentDataService],
        function (customer) {
            customer.registerController('presentListController', ['$scope', '$stateParams', 'utilService', 'presentDataService', 'presentDataViewService', 'presentListDataHeader', 'mcsDialogService',
                function ($scope, $stateParams, util, presentDataService, presentDataViewService, presentListDataHeader, mcsDialogService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    presentDataViewService.configPresentListHeaders(vm, presentListDataHeader);

                    vm.init = function () {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.customerID = $stateParams.id;
                        presentDataViewService.initPresentList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

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
                        mcs.util.postMockForm('http://localhost/PPTSWebApp/PPTS.WebAPI.Products/api/present/exportAllPresents', vm.criteria);
                    };

                    vm.search = function () {
                        if (vm.criteria.startDate && vm.criteria.endDate && vm.criteria.endDate < vm.criteria.startDate) {
                            var dlg = mcsDialogService.error({
                                title: '提示',
                                message: '开始时间不能大于结束时间'
                            });
                            
                        }
                        presentDataViewService.initPresentList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                }]);
        });