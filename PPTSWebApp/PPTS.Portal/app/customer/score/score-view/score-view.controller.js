define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.scoreDataService],
    function (customer) {
        customer.registerController('scoreViewController', [
            '$scope',
            '$state',
            '$stateParams',
            'dataSyncService',
            'scoresViewDataHeader',
            'scoreDataService',
            'scoresDataViewService',
            function ($scope, $state, $stateParams, dataSyncService, scoresViewDataHeader, scoreDataService, scoresDataViewService) {
                var vm = this;

                // 成绩详情表头
                scoresDataViewService.configScoresEditHeaders(vm, scoresViewDataHeader);

                (function () {
                    scoreDataService.getScoresInfo($stateParams.id, function (result) {
                        vm.customer = result.customer;
                        vm.teachers = result.teachers;
                        vm.score = result.score;
                        vm.scoreItems = result.scoreItems;
                        result.scoreItems.forEach(function (item) { item.classPeoples = result.score.classPeoples; });
                        vm.data.rows = result.scoreItems;
                        dataSyncService.injectDynamicDict('ifElse,scoreSatisficing');
                        scoresDataViewService.fillGradeParentKey();
                        $scope.$broadcast('dictionaryReady');
                    });
                })();

                // 编辑
                vm.edit = function () {
                    $state.go('ppts.score-edit', { id: $stateParams.id, prev: 'ppts.score' });
                };
            }]);
    });