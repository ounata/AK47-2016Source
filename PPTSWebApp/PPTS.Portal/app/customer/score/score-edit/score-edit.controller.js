﻿define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.scoreDataService],
    function (customer) {
        customer.registerController('scoreEditController', [
            '$scope',
            '$state',
            '$stateParams',
            'dataSyncService',
            'scoresEditDataHeader',
            'scoreDataService',
            'scoresDataViewService',
            'mcsValidationService',
            function ($scope, $state, $stateParams, dataSyncService, scoresEditDataHeader, scoreDataService, scoresDataViewService, mcsValidationService) {
                var vm = this;

                // 录入成绩
                scoresDataViewService.configScoresEditHeaders(vm, scoresEditDataHeader);

                // 初始化成绩信息
                (function () {
                    scoreDataService.getScoreForEdit($stateParams.id, function (result) {
                        vm.isTeacher = result.isTeacher;
                        vm.closingAccountDate = result.closingAccountDate;
                        vm.customer = result.customer;
                        vm.teachers = result.teachers;
                        vm.score = result.score;
                        vm.scoreItems = result.scoreItems;

                        dataSyncService.injectDynamicDict(vm.teachers, { key: 'teacherID', value: 'teacherName', props: 'subject', category: 'scoreTeacher' });
                        dataSyncService.injectDynamicDict(vm.teachers, { key: 'teacherJobOrgID', value: 'teacherJobOrgName', props: 'teacherID', category: 'scoreTeacherOrgName' });
                        dataSyncService.injectDynamicDict('ifElse,scoreSatisficing');

                        scoresDataViewService.fillGradeParentKey();
                        $scope.$broadcast('dictionaryReady');
                        scoresDataViewService.showRowItems(vm, vm.score.studyStage);
                        scoresDataViewService.handleTeachers();
                        scoresDataViewService.checkCanEdit(vm);
                        vm.fillItemsData();
                    });
                })();

                // 填充成绩信息
                vm.fillItemsData = function () {
                    var rows = vm.data.rows;
                    for (var i in vm.data.rows) {
                        for (var j in vm.scoreItems) {
                            if (vm.data.rows[i].subject == vm.scoreItems[j].subject) {
                                vm.data.rows[i] = vm.scoreItems[j];
                            }
                        }
                    }
                };

                // 编辑保存
                vm.save = function () {
                    if (mcsValidationService.run($scope)) {
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
                        if (scoreItems.length == rows.length) {
                            vm.score.isAllAdded = 1;
                        }
                        var data = {
                            customer: vm.customer,
                            score: vm.score,
                            scoreItems: scoreItems
                        };
                        scoreDataService.editScores(data, function (result) {
                            $state.go('ppts.score-view', { id: $stateParams.id, prev: 'ppts.score' });
                        });
                    }
                };

                vm.cancel = function () {
                    $state.go('ppts.score-view', { id: $stateParams.id, prev: $stateParams.prev });
                };

                // 总分-得分
                vm.totalRealScore = function () {
                    var total = 0;
                    vm.data.rows.forEach(function (row) {
                        if (row.realScore && parseFloat(row.realScore) && row.subject != '60')
                            total += parseFloat(row.realScore || 0);
                    })
                    return total;
                };

                // 总分-卷面分
                vm.totalPaperScore = function () {
                    var total = 0;
                    vm.data.rows.forEach(function (row) {
                        if (row.paperScore && parseFloat(row.paperScore) && row.subject != '60')
                            total += parseFloat(row.paperScore || 0);
                    })
                    return total;
                };

            }]);
    });