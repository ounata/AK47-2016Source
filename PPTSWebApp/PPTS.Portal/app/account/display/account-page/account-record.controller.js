define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountDisplayDataService],
        function (account) {
            account.registerController('accountRecordController', [
                '$scope', '$state', '$location', 'mcsDialogService', 'dataSyncService', '$stateParams', 'accountDisplayDataService', 'accountRecordTable',
                function ($scope, $state, $location, mcsDialogService, dataSyncService, $stateParams, accountDataService, accountRecordTable) {
                    var vm = this;
                    vm.transparent = true;
                    vm.page = $location.$$search.prev;

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, accountRecordTable, accountDataService.queryPagedAccountRecordList);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, accountDataService.queryAccountRecordList, function () {
                            dataSyncService.injectDynamicDict('incomeExpend,frozeRelease');
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();
                    
                    vm.debookOrderItem = function (row) {
                        mcsDialogService.create('app/order/unsubscribe/view/debookOrderItem-view.html', {
                            controller: 'debookOrderItem-viewController',
                            params: { row: row },
                            settings: {
                                size: 'lg'
                            }
                        });
                    }

                    vm.assetConsumeView = function (row) {
                        mcsDialogService.create('app/order/purchase/view/assetConsumeView.html', {
                            controller: 'assetConsumeViewController',
                            params: { row: row },
                            settings: {
                                size: 'lg'
                            }
                        });
                    }

                }]);
        });

