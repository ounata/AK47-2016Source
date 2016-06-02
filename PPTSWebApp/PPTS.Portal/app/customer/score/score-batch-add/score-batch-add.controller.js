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
                        initHeaders();
                        $scope.$broadcast('dictionaryReady');
                    })
                })();

                var initHeaders = function () {
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
                            template: '<span ng-if="row.canEdit"><ppts-select category="scoreTeacher_{{row.customerID}}" filter="{{row.subject}}" prop="subject" model="row.teacherID" value="row.teacherName" ng-show="row.subject != \'60\'" callback=" row.teacherOrgID=\'\';row.teacherOrgName=\'\'; " async="false" style="width:150px;"/></span>'
                                    + '<span ng-if="!row.canEdit">{{row.teacherName}}</span>'
                                    + '<small><span ng-show="row.subject == \'60\'">总得分={{vm.totalRealScore(row.customerID)}}</span><br /><span ng-show="row.subject == \'60\'">总卷面分={{vm.totalPaperScore(row.customerID)}}</span></small>'
                        }, {
                            field: "jobOrgShortName",
                            name: "学科组",
                            template: '<span ng-if="row.canEdit"><ppts-select category="scoreTeacherOrgName_{{row.customerID}}" filter="{{row.teacherID}}" prop="teacherID" show-all="false" model="row.teacherOrgID" value="row.teacherOrgName" ng-show="row.subject != \'60\'" async="false" style="width:150px;"/></span>'
                                    + '<span ng-if="!row.canEdit">{{row.teacherOrgName}}</span>'
                        }, {
                            field: 'realScore',
                            name: '得分',
                            // template: '<input type="text" ng-model="row.realScore" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-search" style="width:150px"/>'
                            template: '<span ng-if="row.canEdit"><input type="text" ng-model="row.realScore" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-small"/></span>'
                                    + '<span ng-if="!row.canEdit">'
                                        + '<span ng-if="row.subject ==\'60\'">'
                                            + '<span ng-if="row.canEidt_totalScore"><input type="text" ng-model="row.realScore" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-small"/></span>'
                                            + '<span ng-if="!row.canEidt_totalScore">{{row.realScore}}</span>'
                                        + '</span>'
                                        + '<span ng-if="row.subject !=\'60\'">{{row.realScore}}</span>'
                                    + '</span>'
                        }, {
                            field: 'paperScore',
                            name: '卷面分',
                            // template: '<input type="text" ng-model="row.paperScore" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-search" style="width:150px"/>'
                            template: '<span ng-if="row.canEdit"><input type="text" ng-model="row.paperScore" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-small"/></span>'
                                    + '<span ng-if="!row.canEdit">'
                                        + '<span ng-if="row.subject ==\'60\'">'
                                            + '<span ng-if="row.canEidt_totalScore"><input type="text" ng-model="row.paperScore" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-small"/></span>'
                                            + '<span ng-if="!row.canEidt_totalScore">{{row.paperScore}}</span>'
                                        + '</span>'
                                        + '<span ng-if="row.subject !=\'60\'">{{row.paperScore}}</span>'
                                    + '</span>'
                        }, {
                            field: 'classRank',
                            name: '班级名次',
                            // template: '<input type="text" ng-model="row.classRank" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-search" style="width:150px"/>'
                            template: '<span ng-if="row.canEdit"><input type="text" ng-model="row.classRank" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-small"/></span>'
                                    + '<span ng-if="!row.canEdit">{{row.classRank}}</span>'
                        }, {
                            field: 'gradeRank',
                            name: '年级名次',
                            // template: '<input type="text" ng-model="row.gradeRank" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-search" style="width:150px"/>'
                            template: '<span ng-if="row.canEdit"><input type="text" ng-model="row.gradeRank" onafterpaste="mcs.util.limit(this)" onkeyup="mcs.util.limit(this)" class="mcs-input-small"/></span>'
                                    + '<span ng-if="!row.canEdit">{{row.gradeRank}}</span>'
                        }, {
                            field: 'scoreChangeType',
                            name: '成绩升降',
                            // template: '<ppts-select category="scoreChangeType" model="row.scoreChangeType" async="false" style="width:150px;"/>'
                            template: '<span ng-if="row.canEdit"><ppts-select category="scoreChangeType" model="row.scoreChangeType" async="false" custom-style="width:150px;"/></span>'
                                    + '<span ng-if="!row.canEdit">{{row.scoreChangeType | scoreChangeType}}</span>'
                        }, {
                            field: 'satisficing',
                            name: '家长满意度',
                            // template: '<ppts-select category="scoreSatisficing" model="row.satisficing" async="false" style="width:150px;"/>'
                            template: '<span ng-if="row.canEdit"><ppts-select category="scoreSatisficing" model="row.satisficing" async="false" custom-style="width:200px;"/></span>'
                                    + '<span ng-if="!row.canEdit">{{row.satisficing | scoreSatisficing}}</span>'
                        }, {
                            field: 'isStudyHere',
                            name: '是否在学大辅导',
                            // template: '<ppts-select category="ifElse" model="row.isStudyHere" async="false" style="width:150px;"/>'
                            template: '<span ng-if="row.canEdit"><ppts-select category="ifElse" model="row.isStudyHere" async="false" style="width:150px;"/></span>'
                                    + '<span ng-if="!row.canEdit">{{row.isStudyHere | ifElse}}</span>'
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
                    if (vm.criteria.scoreType == 6 || vm.criteria.scoreType == 10 || vm.criteria.scoreType == 14) {
                        // 录取院校类别：小升初、中考、高考
                        vm.data.headers.push({
                            field: 'admissionType',
                            name: '录取院校类别',
                            template: '<ppts-select ng-if="row.subject==1" category="admissionType" model="row.scores.admissionType" filter="{{vm.criteria.scoreType}}" prop="parentKey" async="false" style="width:150px;"/>'
                        });
                        // 录取院校
                        vm.data.headers.push({
                            field: 'admissionSchool',
                            name: '院校名称',
                            template: '<mcs-input ng-if="row.subject==1" model="row.scores.admissionSchool" class="mcs-input-search" style="width:150px" />'
                        });
                    }
                    // 高考
                    if (vm.criteria.scoreType == 14) {
                        vm.data.headers.push({
                            field: 'admissionSchool',
                            name: '985/211院校',
                            template: '<ppts-select ng-if="row.subject==1" category="ifElse" model="row.scores.isKeyCollege" async="false" style="width:150px;"/>'
                        });
                        vm.data.headers.push({
                            field: 'studentType',
                            name: '学员类别',
                            template: '<ppts-select ng-if="row.subject==1" category="examCustomerType" model="row.scores.studentType" filter="{{vm.score.scoreType}}" prop="parentKey" async="false" caption="学员类别" css="mcs-padding-0" />'
                        });
                    }
                    // 月考
                    if (vm.criteria.scoreType == 15) {
                        vm.data.headers.push({
                            field: 'admissionSchool',
                            name: '考试月份',
                            template: '<ppts-select ng-if="row.subject==1" category="examMonth" model="row.scores.examineMonth" caption="考试月份" />'
                        });
                    }
                };

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
                                row.isStudyHere = mcs.util.bool(row.isStudyHere);
                                obj.scoreItems.push(row);
                            }
                        }
                        if (obj.scoreItems.length) {
                            obj.scores = rows[0].scores || {};
                            obj.scores.scoreID = obj.scores.scoreID;
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

                vm.search = function () {
                    dataSyncService.initCriteria(vm);
                    scoreDataService.getScoreForBatchAdd(vm.criteria, function (result) {
                        vm.isTeacher = result.isTeacher;
                        vm.handleTeacherCategory(result.queryResult.pagedData);
                        vm.handleTeacherOrgNameCategory(result.queryResult.pagedData);

                        initHeaders();
                        vm.data.pager.totalCount = result.queryResult.totalCount;
                        vm.data.pager.pageSize = result.queryResult.pageSize;
                        vm.data.pager.pageIndex = result.queryResult.pageIndex;

                        //dataSyncService.injectDictData(mcs.util.mapping(vm.teachers, { key: 'teacherID', value: 'teacherName', props: 'subject' }, 'scoreTeacher'));
                        //dataSyncService.injectDictData(mcs.util.mapping(vm.teachers, { key: 'teacherJobOrgID', value: 'teacherJobOrgName', props: 'teacherID' }, 'scoreTeacherOrgName'));
                        dataSyncService.injectDictData({
                            c_codE_ABBR_Score_Satisficing: [{ key: '1', value: '对成绩满意' }, { key: '0', value: '对成绩不满意' }]
                        });
                        dataSyncService.injectPageDict(['ifElse']);
                        $scope.$broadcast('dictionaryReady');

                        scoresDataViewService.fillGradeParentKey();

                        vm.showRowItems(vm, vm.criteria.studyStage, result.queryResult.pagedData);
                        vm.fillItemsData(result.queryResult.pagedData);
                        vm.checkCanEdit();
                    })
                };

                vm.handleTeacherCategory = function (data) {
                    for (var i in data) {
                        var teachers = data[i].teachers;
                        var result_teacher = [];
                        for (var j in teachers) {
                            var flag = false;
                            for (var k in result_teacher) {
                                if (teachers[j].teacherID == result_teacher[k].teacherID) {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                                result_teacher.push(teachers[j]);
                            var categoryName = 'c_codE_ABBR_scoreTeacher_' + data[i].customerID;
                            var categoryValue = mcs.util.mapping(result_teacher, { key: 'teacherID', value: 'teacherName', props: 'subject' }, 'scoreTeacher_' + data[i].customerID);
                            ppts.config.dictMappingConfig['scoreTeacher_' + data[i].customerID] = categoryName;
                            dataSyncService.injectDictData(categoryValue);
                        }
                    }
                };

                vm.handleTeacherOrgNameCategory = function (data) {
                    for (var i in data) {
                        var teachers = data[i].teachers;
                        var categoryName = 'c_codE_ABBR_scoreTeacherOrgName_' + data[i].customerID;
                        var categoryValue = mcs.util.mapping(teachers, { key: 'teacherJobOrgID', value: 'teacherJobOrgName', props: 'teacherID' }, 'scoreTeacherOrgName_' + data[i].customerID);
                        ppts.config.dictMappingConfig['scoreTeacherOrgName_' + data[i].customerID] = categoryName;
                        dataSyncService.injectDictData(categoryValue);
                    }
                };

                vm.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    scoreDataService.getScoreForBatchAdd(vm.criteria, function (result) {
                        vm.isTeacher = result.isTeacher;

                        vm.data.pager.totalCount = result.queryResult.totalCount;
                        vm.data.pager.pageSize = result.queryResult.pageSize;
                        vm.data.pager.pageIndex = result.queryResult.pageIndex;

                        vm.showRowItems(vm, vm.criteria.studyStage, result.queryResult.pagedData);
                        vm.fillItemsData(result.queryResult.pagedData);
                        vm.checkCanEdit();
                    });
                }

                vm.checkCanEdit = function () {
                    if (!vm.dataList || !vm.dataList.length) return;
                    for (var i in vm.dataList) {
                        var canEidt_totalScore = false;
                        for (var j in vm.dataList[i].rows) {
                            vm.dataList[i].rows[j].canEdit = true;
                            if (vm.dataList[i].rows[j].createTime) {
                                if (vm.dataList[i].rows[j].createTime.getMonth() < new Date().getMonth() /*item.createTime > vm.closingAccountDate*/) {
                                    vm.dataList[i].rows[j].canEdit = false;
                                }
                            }
                            if (vm.dataList[i].rows[j].canEdit)
                                canEidt_totalScore = true;
                        }
                        if (canEidt_totalScore) {
                            for (var j in vm.dataList[i].rows) {
                                if (vm.dataList[i].rows[j] && vm.dataList[i].rows[j].subject == '60') {
                                    vm.dataList[i].rows[j].canEidt_totalScore = true;
                                }
                            }
                        }
                    }
                };

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
                    // var dataList = vm.dataList;
                    for (var i in vm.dataList) {
                        for (var j in vm.dataList[i].rows) {
                            for (var k in pagedData) {
                                if (pagedData[k].customerID == vm.dataList[i].customerID) {
                                    var scoreItems = pagedData[k].scoreItems || [];
                                    for (var n in scoreItems) {
                                        if (vm.dataList[i].rows[j].subject == scoreItems[n].subject) {
                                            vm.dataList[i].rows[j] = scoreItems[n];
                                        }
                                    }
                                    vm.dataList[i].rows[j].scores = pagedData[k].scores;
                                    break;
                                }
                            }
                            vm.dataList[i].rows[j].customerName = vm.dataList[i].customerName;
                            vm.dataList[i].rows[j].customerID = vm.dataList[i].customerID;
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

            }]);
    });