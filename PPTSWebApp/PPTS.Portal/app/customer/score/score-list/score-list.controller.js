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
                function ($state, $scope, $stateParams, util, dataSyncService, mcsDialogService, scoreDataService, scoresDataViewService, scoresListDataHeader, searchItems) {
                    var vm = this;

                    // 配置数据表头 
                    scoresDataViewService.configScoresListHeaders(vm, scoresListDataHeader);

                    // 搜索
                    vm.search = function () {
                        scoresDataViewService.initCustomerScoresList(vm, function () {
                            vm.searchItems = searchItems;
                            scoresDataViewService.initWatchExps($scope, vm, [
                                  { watchExp: 'vm.criteria.scoreType', selectedValue: 16, watch: 'otherScoreTypeName' }
                            ]);
                            scoresDataViewService.fillGradeParentKey();
                            $scope.$broadcast('dictionaryReady');
                        });
                    }
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
                        $state.go('ppts.score-batch-add');
                    };

                    // 导出
                    vm.export = function () {
                        mcs.util.postMockForm(ppts.config.customerApiBaseUrl + 'api/customerscores/exportallScores', vm.criteria);
                    };

                }]);
        });