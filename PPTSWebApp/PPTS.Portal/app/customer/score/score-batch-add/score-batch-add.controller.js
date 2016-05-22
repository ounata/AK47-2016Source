define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.scoreDataService],
    function (customer) {
        customer.registerController('scoreBatchAddController', [
            '$scope',
            '$state',
            '$stateParams',
            'dataSyncService',
            'scoresEditDataHeader',
            'scoreDataService',
            'scoresDataViewService',
            function ($scope, $state, $stateParams, dataSyncService, scoresEditDataHeader, scoreDataService, scoresDataViewService) {
                var vm = this;

                (function () {
                    scoreDataService.initForBatchAdd(function (result) {
                        scoresDataViewService.fillGradeParentKey();
                        $scope.$broadcast('dictionaryReady');
                    })
                })();

                vm.save = function () {
                    var data = [];
                    var dataList = vm.dataList;
                    for (var i in dataList) {
                        var obj = {};
                        obj.scores = {};
                        obj.scoreItems = [];
                        var rows = dataList[i].rows;
                        for (var j in rows) {
                            var row = rows[j];
                            if (row.realScore) {
                                vm.teachers.forEach(function (t) {
                                    if (t.teacherID == row.teacherID) {
                                        row.teacherName = t.teacherName;
                                    }
                                });
                                row.isStudyHere = mcs.util.bool(row.isStudyHere);
                                obj.scoreItems.push(row);
                            }
                        }
                        if (obj.scoreItems.length) {
                            obj.scores.scoreID = row.scoreID;
                            obj.scores.customerID = row.customerID;
                            obj.scores.studyYear = vm.criteria.studyYear;
                            obj.scores.studyTerm = vm.criteria.studyTerm;
                            obj.scores.studyStage = vm.criteria.studyStage;
                            obj.scores.scoreGrade = vm.criteria.scoreGrade;
                            obj.scores.scoreType = vm.criteria.scoreType;
                            if (obj.scoreItems.length == rows.length) {
                                obj.scores.isAllAdded = 1;
                            } 
                            data.push(obj);
                        }
                    }
                    scoreDataService.addBatchScores(data, function (result) {
                        $state.go('ppts.score');
                    });
                };

                vm.data = {
                    headers: [{
                        field: "customerName",
                        name: "学员姓名",
                        template: '<span ng-if="row.subject==1">{{ row.customerName }}</span>'
                    }, {
                        field: "subject",
                        name: "科目",
                        template: '<span>{{ row.subject | examSubject }}</span>',
                    }, {
                        field: "teacherName",
                        name: "任课教师",
                        template: '<ppts-select category="teacher" filter="{{row.subject}}" prop="subjectMemo" model="row.teacherID" ng-show="row.subject != \'60\'" async="false" style="width:150px;"/><small><span ng-show="row.subject == \'60\'">总得分={{vm.totalRealScore(row.customerID)}}</span><br /><span ng-show="row.subject == \'60\'">总卷面分={{vm.totalPaperScore(row.customerID)}}</span></small>'
                    }, {
                        field: 'realScore',
                        name: '得分',
                        template: '<input type="text" ng-model="row.realScore" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-search" style="width:150px"/>'
                    }, {
                        field: 'paperScore',
                        name: '卷面分',
                        template: '<input type="text" ng-model="row.paperScore" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-search" style="width:150px"/>'
                    }, {
                        field: 'classRank',
                        name: '班级名次',
                        template: '<input type="text" ng-model="row.classRank" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-search" style="width:150px"/>'
                    }, {
                        field: 'gradeRank',
                        name: '年级名次',
                        template: '<input type="text" ng-model="row.gradeRank" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-search" style="width:150px"/>'
                    }, {
                        field: 'scoreChangeType',
                        name: '成绩升降',
                        template: '<ppts-select category="scoreChangeType" model="row.scoreChangeType" async="false" style="width:150px;"/>'
                    }, {
                        field: 'satisficing',
                        name: '家长满意度',
                        template: '<ppts-select category="scoreSatisficing" model="row.satisficing" async="false" style="width:150px;"/>'
                    }, {
                        field: 'isStudyHere',
                        name: '是否在学大辅导',
                        template: '<ppts-select category="ifElse" model="row.isStudyHere" async="false" style="width:150px;"/>'
                    }],
                    rows: [],
                    pager: {
                        pageIndex: 1,
                        pageSize: 5,
                        totalCount: -1
                    },
                    orderBy: [{
                        dataField: 'customers.CreateTime', sortDirection: 1
                    }]
                };

                vm.search = function () {
                    dataSyncService.initCriteria(vm);
                    scoreDataService.getScoreForBatchAdd(vm.criteria, function (result) {
                        vm.isTeacher = result.isTeacher;
                        vm.teachers = result.teachers;

                        vm.data.pager.totalCount = result.queryResult.totalCount;
                        vm.data.pager.pageIndex = result.queryResult.pageSize;
                        vm.data.pager.pageIndex = result.queryResult.pageIndex;

                        dataSyncService.injectDictData(mcs.util.mapping(vm.teachers, { key: 'teacherID', value: 'teacherName', props: 'subjectMemo' }, 'Teacher'));
                        dataSyncService.injectDictData({
                            c_codE_ABBR_Score_Satisficing: [{ key: '1', value: '对成绩满意' }, { key: '0', value: '对成绩不满意' }]
                        });
                        dataSyncService.injectPageDict(['ifElse']);
                        $scope.$broadcast('dictionaryReady');

                        vm.showRowItems(vm, vm.criteria.studyStage, result.queryResult.pagedData);
                        vm.fillItemsData(result.queryResult.pagedData);
                        // scoresDataViewService.checkCanEdit(vm);
                    })
                };

                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    scoreDataService.getScoreForBatchAdd(vm.criteria, function (result) {
                        vm.isTeacher = result.isTeacher;
                        vm.teachers = result.teachers;

                        vm.data.pager.totalCount = result.queryResult.totalCount;
                        vm.data.pager.pageIndex = result.queryResult.pageSize;
                        vm.data.pager.pageIndex = result.queryResult.pageIndex;

                        vm.showRowItems(vm, vm.criteria.studyStage, result.queryResult.pagedData);
                        vm.fillItemsData(result.queryResult.pagedData);
                        // scoresDataViewService.checkCanEdit(vm);
                    });
                }

                vm.showRowItems = function (vm, studyStage, pagedData) {
                    var examSubject = ppts.dict[ppts.config.dictMappingConfig.examSubject];
                    if (!examSubject) return;
                    var subjects = [];
                    for (var index in examSubject) {
                        if (mcs.util.containsElement(examSubject[index].parentKey, studyStage)) {
                            var subject = examSubject[index];
                            if (vm.isTeacher) {
                                if (mcs.util.containsElement(vm.teachers[0].subjectMemo, subject.key)) {
                                    subject.sortNo = index;
                                    subjects.push(subject);
                                    break;
                                }
                            }
                            else {
                                subject.sortNo = index;
                                subjects.push(subject);
                            }
                        }
                    }
                    vm.subjects = subjects;
                    vm.addRow(vm, pagedData);
                };

                vm.addRow = function (vm, pagedData) {
                    vm.dataList = [];
                    for (var i in pagedData) {
                        var data = {};
                        data.rows = [];
                        data.customerID = pagedData[i].customerID;
                        data.customerName = pagedData[i].customerName;
                        data.headers = vm.data.headers;
                        for (var j in vm.subjects) {
                            data.rows.push({
                                subject: vm.subjects[j].key,
                                sortNo: vm.subjects[j].sortNo,
                                canEdit: true
                            });
                        }
                        vm.dataList.push(data);
                    }
                };

                vm.fillItemsData = function (pagedData) {
                    var dataList = vm.dataList;
                    for (var i in dataList) {
                        for (var j in dataList[i].rows) {
                            for (var k in pagedData) {
                                if (pagedData[k].customerID == dataList[i].customerID) {
                                    var scoreItems = pagedData[k].scoreItems || [];
                                    for (var n in scoreItems) {
                                        if (dataList[i].rows[j].subject == scoreItems[n].subject) {
                                            dataList[i].rows[j] = scoreItems[n];
                                        }
                                    }
                                }
                            }
                            dataList[i].rows[j].customerName = dataList[i].customerName;
                            dataList[i].rows[j].customerID = dataList[i].customerID;
                        }
                    }
                };

                // 总分-得分
                vm.totalRealScore = function (customerID) {
                    var total = 0;
                    vm.dataList.forEach(function (data) {
                        if (data.customerID == customerID) {
                            data.rows.forEach(function (row) {
                                if (row.realScore && parseInt(row.realScore) && row.subject != '60')
                                    total += parseInt(row.realScore || 0);
                            })
                        }
                    });
                    return total;
                };

                // 总分-卷面分
                vm.totalPaperScore = function (customerID) {
                    var total = 0;
                    vm.dataList.forEach(function (data) {
                        if (data.customerID == customerID) {
                            data.rows.forEach(function (row) {
                                if (row.paperScore && parseInt(row.paperScore) && row.subject != '60')
                                    total += parseInt(row.paperScore || 0);
                            })
                        }
                    });
                    return total;
                };

                var initRowsData = function (vm, result) {
                    if (!result) return;
                    vm.dataList = [];
                    for (var index in result) {
                        var data = {};
                        //data.scores = result[index].scores || {};
                        //data.scores.studyYear = vm.criteria.studyYear;
                        //data.scores.studyTerm = vm.criteria.studyTerm;
                        //data.scores.studyStage = vm.criteria.studyStage;
                        //data.scores.scoreGrade = vm.criteria.scoreGrade;
                        //data.scores.scoreType = vm.criteria.scoreType;
                        //data.scoreItems = result[index].scoreItems || {};
                        //data.customerID = result[index].customerID;
                        data.customerName = result[index].customerName;
                        data.headers = vm.data.headers;
                        data.rows = result[index].scoreItems;
                        vm.dataList.push(data);
                    }
                };
            }]);
    });