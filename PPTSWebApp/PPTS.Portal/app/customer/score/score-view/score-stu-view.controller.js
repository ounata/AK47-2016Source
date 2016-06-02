define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.scoreDataService],
    function (customer) {
        customer.registerController('studentScoreViewController', [
            '$scope',
            '$state',
            '$stateParams',
            'dataSyncService',
            'scoresListDataHeader',
            'scoresViewDataHeader',
            'scoreDataService',
            'scoresDataViewService',
            function ($scope, $state, $stateParams, dataSyncService, scoresListDataHeader, scoresViewDataHeader, scoreDataService, scoresDataViewService) {
                var vm = this;

                // 配置数据表头 
                scoresDataViewService.configScoresListHeaders(vm, scoresListDataHeader);

                // 初始化
                (function () {
                    dataSyncService.initCriteria(vm);
                    vm.criteria.customerID = $stateParams.id;
                    scoreDataService.getScoresForStudent(vm.criteria, function (result) {
                        vm.data.rows = result.queryResult.pagedData;
                        dataSyncService.injectDictData({
                            c_codE_ABBR_Score_Satisficing: [{ key: '1', value: '对成绩满意' }, { key: '0', value: '对成绩不满意' }]
                        });
                        dataSyncService.injectPageDict(['ifElse']);
                        dataSyncService.updateTotalCount(vm, result.queryResult);
                    });
                })();

                // 录入
                vm.add = function () {
                    $state.go('ppts.score-add', { id: $stateParams.id, prev: 'ppts.student-view.score' });
                };
                
                // 编辑
                vm.edit = function () {
                    $state.go('ppts.score-edit', { id: vm.data.rowsSelected[0].scoreID, prev: 'ppts.student-view.score' });
                };

                // 查看
                vm.view = function () {
                    $state.go('ppts.score-view', { id: vm.data.rowsSelected[0].scoreID, prev: 'ppts.student-view.score' });
                };
                
                // 导出
                vm.export = function () {
                    dataSyncService.initCriteria(vm);
                    mcs.util.postMockForm('http://localhost/PPTSWebApp/PPTS.WebAPI.Customers/api/customerscores/exportallScores', vm.criteria);
                };
            }]);
    });