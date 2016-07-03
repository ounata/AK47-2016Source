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
                // scoresDataViewService.configScoresListHeaders(vm, scoresListDataHeader);
                dataSyncService.configDataHeader(vm, scoresListDataHeader, scoreDataService.getPagedScores);

                vm.search = function () {
                    dataSyncService.initCriteria(vm);
                    vm.criteria.customerID = $stateParams.id;
                    dataSyncService.initDataList(vm, scoreDataService.getScoresForStudent, function (result) {
                        dataSyncService.injectDynamicDict('ifElse,scoreSatisficing');
                    })
                };
                vm.search();

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