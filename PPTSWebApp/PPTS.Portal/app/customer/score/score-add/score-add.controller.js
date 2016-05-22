define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.scoreDataService],
    function (customer) {
        customer.registerController('scoreAddController', [
            '$scope',
            '$state',
            '$stateParams',
            'dataSyncService',
            'scoresEditDataHeader',
            'scoreDataService',
            'scoresDataViewService',
            function ($scope, $state, $stateParams, dataSyncService, scoresEditDataHeader, scoreDataService, scoresDataViewService) {
                var vm = this;

                // 录入成绩表头
                scoresDataViewService.configScoresEditHeaders(vm, scoresEditDataHeader);

                // 初始化成绩信息
                (function () {
                    scoreDataService.getScoreForAdd($stateParams.id, function (result) {
                        vm.isTeacher = result.isTeacher;
                        vm.customer = result.customer;
                        vm.score = result.score;
                        vm.teachers = result.teachers;
                        dataSyncService.injectDictData(mcs.util.mapping(vm.teachers, { key: 'teacherID', value: 'teacherName', props: 'subjectMemo' }, 'Teacher'));
                        dataSyncService.injectDictData({
                            c_codE_ABBR_Score_Satisficing: [{ key: '1', value: '对成绩满意' }, { key: '0', value: '对成绩不满意' }]
                        });
                        dataSyncService.injectPageDict(['ifElse']);
                        scoresDataViewService.fillGradeParentKey();
                        $scope.$broadcast('dictionaryReady');
                    })
                })();

                // 保存
                vm.save = function () {
                    var rows = vm.data.rows;
                    if (!rows || !rows.length) return;
                    // 只统计有输入得分项
                    var scoreItems = [];
                    for (var index in rows) {
                        var row = rows[index];
                        if (row.realScore) {
                            vm.teachers.forEach(function (t) {
                                if (t.teacherID == row.teacherID) {
                                    row.teacherName = t.teacherName;
                                }
                            });
                            row.isStudyHere = mcs.util.bool(row.isStudyHere);
                            scoreItems.push(row);
                        }
                    }
                    if (scoreItems.length == vm.data.rows.length) {
                        vm.score.isAllAdded = 1;
                    }
                    var data = {
                        customer: vm.customer,
                        score: vm.score,
                        scoreItems: scoreItems
                    };
                    scoreDataService.addScores(data, function (result) {
                        $state.go('ppts.score-view', { id: vm.score.scoreID, prev: 'ppts.score' });
                    });
                };

                // 考试年级
                vm.selectStage = function (item) {
                    vm.score.studyStage = item.parentKey;
                    scoresDataViewService.showRowItems(vm, vm.score.studyStage);
                };

                // 总分-得分
                vm.totalRealScore = function () {
                    var total = 0;
                    vm.data.rows.forEach(function (row) {
                        if (row.realScore && parseInt(row.realScore) && row.subject != '60')
                            total += parseInt(row.realScore || 0);
                    })
                    return total;
                };

                // 总分-卷面分
                vm.totalPaperScore = function () {
                    var total = 0;
                    vm.data.rows.forEach(function (row) {
                        if (row.paperScore && parseInt(row.paperScore) && row.subject != '60')
                            total += parseInt(row.paperScore || 0);
                    })
                    return total;
                };
            }]);
    });