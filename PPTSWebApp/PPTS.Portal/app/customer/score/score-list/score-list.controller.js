define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.scoreDataService],
        function (customer) {
            customer.registerController('scoreListController', [
                '$state',
                '$scope',
                '$stateParams',
                'utilService',
                'dataSyncService',
                'mcsDialogService',
                'scoreDataService',
                'scoresDataViewService',
                'scoresListDataHeader',
                'scoresAdvanceSearchItems',
                'exportExcelService',
                function ($state, $scope, $stateParams, util, dataSyncService, mcsDialogService, scoreDataService, scoresDataViewService, scoresListDataHeader, searchItems, exportExcelService) {
                    var vm = this;

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, scoresListDataHeader, scoreDataService.getPagedScores);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, scoreDataService.getAllScores, function (result) {
                            vm.searchItems = searchItems;
                            dataSyncService.injectDynamicDict('ifElse,scoreSatisficing');
                            scoresDataViewService.initWatchExps($scope, vm, [
                                  { watchExp: 'vm.criteria.scoreType', selectedValue: 16, watch: 'otherScoreTypeName' }
                            ]);

                            vm.isLastDayOfMonth = result.isLastDayOfMonth;
                            scoresDataViewService.fillGradeParentKey();
                            // $scope.$broadcast('dictionaryReady');
                        })
                    };

                    vm.search();

                    // 录入成绩
                    vm.add = function () {
                        if (util.selectOneRow(vm)) {
                            var transfer = function () {
                                $state.go('ppts.score-add', { id: vm.data.rowsSelected[0].customerID, prev: 'ppts.score' });
                            };
                            if (vm.isLastDayOfMonth) {
                                mcsDialogService.confirm({
                                    title: '确认',
                                    message: '因绩效统计需要，跨月不允许编辑，请准确录入'
                                }).result.then(function () {
                                    transfer();
                                });
                            } else {
                                transfer();
                            }
                        }
                    }

                    // 查看成绩
                    vm.view = function () {
                        if (util.selectOneRow(vm)) {
                            $state.go('ppts.score-view', { id: vm.data.rowsSelected[0].scoreID, prev: 'ppts.score' });
                        }
                    };

                    // 编辑
                    vm.edit = function () {
                        if (util.selectOneRow(vm)) {
                            $state.go('ppts.score-edit', { id: vm.data.rowsSelected[0].scoreID, prev: 'ppts.score' });
                        }
                    }

                    // 批量添加成绩
                    vm.batchAdd = function () {
                        if (vm.data.rowsSelected.length >= 1) {
                            vm.errorMessage = "单个学员成绩录入，请点击“录入成绩”按钮";
                            return;
                        }
                        else {
                            $state.go('ppts.score-batch-add');
                        }
                    };

                    // 导出
                    vm.export = function () {
                        vm.maxExportCount = 3000;
                        if (vm.criteria.pageParams.totalCount < vm.maxExportCount) {
                            var dlg = mcsDialogService.confirm({
                                title: '提示',
                                message: '您将导出共' + vm.criteria.pageParams.totalCount + '条记录，请确认是否要导出？'
                            });
                            dlg.result.then(function () {
                                exportExcelService.export(ppts.config.customerApiBaseUrl + 'api/customerscores/exportallScores', vm.criteria);
                            });
                        } else {
                            mcsDialogService.info({
                                title: '提示',
                                message: '内容超过' + vm.maxExportCount + '条以上，无法正常导出，请缩小范围后再尝试!'
                            });
                        }
                    };

                }]);
        });