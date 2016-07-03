define(['angular', ppts.config.modules.customer], function (ng, customer) {

    customer.registerFactory('scoreDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customerscores/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.getAllScores = function (criteria, success, error) {
            resource.post({ operation: 'getAllScores' }, criteria, success, error);
        }

        resource.getPagedScores = function (criteria, success, error) {
            resource.post({ operation: 'GetPagedScores' }, criteria, success, error);
        }

        resource.getScoreForAdd = function (customerId, success, error) {
            resource.query({ operation: 'addScores', id: customerId }, success, error);
        }

        resource.addScores = function (model, success, error) {
            resource.save({ operation: 'addScores' }, model, success, error);
        }

        resource.getScoresInfo = function (scoreId, success, error) {
            resource.query({ operation: 'getScoresInfo', id: scoreId }, success, error);
        }

        resource.getScoreForEdit = function (scoreId, success, error) {
            resource.query({ operation: 'editScores', id: scoreId }, success, error);
        }

        resource.editScores = function (model, success, error) {
            resource.save({ operation: 'editScores' }, model, success, error);
        }

        resource.initForBatchAdd = function (success, error) {
            resource.query({ operation: 'getScoreForBatchAdd' }, success, error);
        }

        resource.getScoreForBatchAdd = function (model, success, error) {
            resource.post({ operation: 'getScoreForBatchAdd' }, model, success, error);
        }

        resource.getPagedScoreForBatchAdd = function (model, success, error) {
            resource.post({ operation: 'getPagedScoreForBatchAdd' }, model, success, error);
        }

        resource.getScoresForStudent = function (criteria, success, error) {
            resource.post({ operation: 'getScoresForStudent' }, criteria, success, error);
        }

        resource.addBatchScores = function (model, success, error) {
            resource.save({ operation: 'addBatchScores' }, model, success, error);
        }

        resource.getPagedScoresForStudent = function (criteria, success, error) {
            resource.post({ operation: 'getPagedScoresForStudent' }, criteria, success, error);
        }

        resource.exportAllScores = function (criteria, success, error) {
            resource.post({ operation: 'exportAllScores' }, criteria, success, error);
        }

        return resource;
    }]);

    customer.registerValue('scoresAdvanceSearchItems', [
        { name: '学年度：', template: '<ppts-radiobutton-group category="studyYear" model="vm.criteria.studyYear" show-all="true" async="false" />' },
        { name: '是否在学大辅导：', template: '<ppts-radiobutton-group category="ifElse" model="vm.criteria.isStudyHere" show-all="true" async="false"/>' },
        { name: '家长满意度：', template: '<ppts-radiobutton-group category="scoreSatisficing" model="vm.criteria.satisficing" show-all="true" async="false"/>' },
        { name: '考试学段：', template: '<ppts-radiobutton-group category="studyStage" model="vm.criteria.studyStage" show-all="true" async="false" />' },
        { name: '考试年级：', template: '<ppts-radiobutton-group category="grade" model="vm.criteria.scoreGrade" parent="vm.criteria.studyStage" show-all="true" async="false" />' },
        { name: '考试科目：', template: '<ppts-radiobutton-group category="examSubject" model="vm.criteria.subject" parent="vm.criteria.studyStage" show-all="true" async="false" />' },
        { name: '考试类型：', template: '<ppts-radiobutton-group category="scoreType" model="vm.criteria.scoreType" parent="vm.criteria.studyStage" show-all="true" async="false" />&nbsp;<span ng-show="vm.criteria.scoreType == 16"><input type="text" ng-model="vm.criteria.otherScoreTypeName" class="mcs-input-small" /></span>' },
        { name: '录取院校类型：', template: '<ppts-radiobutton-group category="admissionType" model="vm.criteria.admissionType" parent="vm.criteria.scoreType" show-all="true" async="false" />', hide: "!(vm.criteria.scoreType==6 || vm.criteria.scoreType==10 || vm.criteria.scoreType==14)" },
        { name: '学员类别：', template: '<ppts-radiobutton-group category="examCustomerType" model="vm.criteria.studentType" parent="vm.criteria.scoreType" show-all="true" async="false" />', hide: "vm.criteria.scoreType != 14" },
        { name: '成绩范围：', template: '<mcs-datarange min="vm.criteria.minPaperScore" max="vm.criteria.maxPaperScore" min-text="最低成绩" max-text="最高成绩" unit="分" css="col-xs-4 col-sm-4" />' }
    ]);

    customer.registerValue('scoresListDataHeader', {
        selection: 'radio',
        rowsSelected: [],
        keyFields: ['itemID', 'scoreID', 'customerID'],
        headers: [{
            field: "campusName",
            name: "校区"
        }, {
            field: "studyYear",
            name: "学年度",
            template: '<span>{{ row.studyYear | studyYear }}</span>'
        }, {
            field: "customerName",
            name: "学生姓名",
            template: '<a ui-sref="ppts.student-view.score({id:row.customerID,prev:\'ppts.score\'})">{{row.customerName}}</a>'
        }, {
            field: "scoreGrade",
            name: "考试年级",
            template: '<span>{{ row.scoreGrade | grade | normalize }}</span>'
        }, {
            field: "studyTerm",
            name: "学期",
            template: '<span>{{ row.studyTerm | studyTerm | normalize }}</span>'
        }, {
            field: "scoreType",
            name: "考试类型",
            template: '<span><a ui-sref="ppts.score-view({id: row.scoreID, prev: \'ppts.score\'})">{{ row.scoreType | scoreType | normalize }}</a><span>'

        }, {
            field: "subject",
            name: "考试科目",
            template: '<span>{{ row.subject | examSubject | normalize }}</span>'
        }, {
            field: "realScore",
            name: "得分",
            template: '<span>{{ row.realScore | normalize }}</span>'
        }, {
            field: "paperScore",
            name: "卷面分",
            template: '<span>{{ row.paperScore | normalize }}</span>'
        }, {
            field: "classPeoples",
            name: "班级人数",
            template: '<span>{{ row.classPeoples | normalize }}</span>'
        }, {
            field: "classRank",
            name: "班级名次",
            template: '<span>{{ row.classRank | normalize }}</span>'
        }, {
            field: "gradeRank",
            name: "年级名次",
            template: '<span>{{ row.gradeRank | normalize }}</span>'
        }, {
            field: "teacherName",
            name: "任课教师"
        }, {
            field: "isStudyHere",
            name: "是否在学大辅导",
            template: '<span>{{ row.isStudyHere | ifElse }}</span>'
        }, {
            field: "satisficing",
            name: "家长满意度",
            template: '<span>{{ row.satisficing | scoreSatisficing | normalize }}</span>'
        }, {
            field: "customerStatus",
            name: "学员状态"
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{
            dataField: 'CustomerScores.modifyTime', sortDirection: 1
        }]
    });

    customer.registerValue('scoresEditDataHeader', {
        headers: [{
            field: "subject",
            name: "科目",
            template: '<span>{{ row.subject | examSubject }}</span>',
        }, {
            field: "teacherName",
            name: "任课教师",
            template: '<span ng-if="row.canEdit"><mcs-select category="scoreTeacher" filter="{{row.subject}}" prop="subject" model="row.teacherID" value="row.teacherName" ng-show="row.subject != \'60\'" callback=" row.teacherOrgID=\'\';row.teacherOrgName=\'\'; " async="false" custom-style="width:150px;"/></span>'
                    + '<span ng-if="!row.canEdit">{{row.teacherName}}</span>'
                    + '<small><span ng-show="row.subject == \'60\'">总得分={{vm.totalRealScore()}}</span><br /><span ng-show="row.subject == \'60\'">总卷面分={{vm.totalPaperScore()}}</span></small>'
        },{
            field: "jobOrgShortName",
            name: "学科组",
            template: '<span ng-if="row.canEdit"><mcs-select category="scoreTeacherOrgName" filter="{{row.teacherID}}" prop="teacherID" is-dynamic="true" model="row.teacherOrgID" value="row.teacherOrgName" ng-show="row.subject != \'60\'" async="false" style="width:150px;"/></span>'
                    + '<span ng-if="!row.canEdit">{{row.teacherOrgName}}</span>'
        }, {
            field: 'realScore',
            name: '得分',
            template: '<span ng-if="row.canEdit"><mcs-input model="row.realScore" datatype="number" class="mcs-input-small" /></span>'
                    + '<span ng-if="!row.canEdit">'
                        + '<span ng-if="row.subject ==\'60\'">'
                            + '<span ng-if="row.canEidt_totalScore"><mcs-input model="row.realScore" datatype="number" class="mcs-input-small"/></span>'
                            + '<span ng-if="!row.canEidt_totalScore">{{row.realScore}}</span>'
                        + '</span>'
                        + '<span ng-if="row.subject !=\'60\'">{{row.realScore}}</span>'
                    + '</span>'
        }, {
            field: 'paperScore',
            name: '卷面分',
            template: '<span ng-if="row.canEdit"><mcs-input model="row.paperScore" datatype="number" class="mcs-input-small"/></span>'
                    + '<span ng-if="!row.canEdit">'
                        + '<span ng-if="row.subject ==\'60\'">'
                            + '<span ng-if="row.canEidt_totalScore"><mcs-input model="row.paperScore" datatype="number" class="mcs-input-small"/></span>'
                            + '<span ng-if="!row.canEidt_totalScore">{{row.paperScore}}</span>'
                        + '</span>'
                        + '<span ng-if="row.subject !=\'60\'">{{row.paperScore}}</span>'
                    + '</span>'
        }, {
            field: 'classRank',
            name: '班级名次',
            template: '<span ng-if="row.canEdit"><mcs-input model="row.classRank" datatype="int" class="mcs-input-small"/></span>'
                    + '<span ng-if="!row.canEdit">{{row.classRank}}</span>'
        }, {
            field: 'gradeRank',
            name: '年级名次',
            template: '<span ng-if="row.canEdit"><mcs-input model="row.gradeRank" datatype="int" class="mcs-input-small"/></span>'
                    + '<span ng-if="!row.canEdit">{{row.gradeRank}}</span>'
        }, {
            field: 'scoreChangeType',
            name: '成绩升降',
            template: '<span ng-if="row.canEdit"><mcs-select category="scoreChangeType" model="row.scoreChangeType" async="false" custom-style="width:150px;"/></span>'
                    + '<span ng-if="!row.canEdit">{{row.scoreChangeType | scoreChangeType}}</span>'
        }, {
            field: 'satisficing',
            name: '家长满意度',
            template: '<span ng-if="row.canEdit"><mcs-select category="scoreSatisficing" model="row.satisficing" async="false" custom-style="width:200px;"/></span>'
                    + '<span ng-if="!row.canEdit">{{row.satisficing | scoreSatisficing}}</span>'
        }, {
            field: 'isStudyHere',
            name: '是否在学大辅导',
            template: '<span ng-if="row.canEdit">'
                        + '<span ng-if="row.subject !=\'60\'"><mcs-select category="ifElse" model="row.isStudyHere" async="false" custom-style="width:150px;"/></span>'
                        + '<span ng-if="row.subject ==\'60\'"><mcs-input model="vm.score.classPeoples | normalize" placeholder="班级人数" datatype="int" /></span>'
                    + '</span>'
                    + '<span ng-if="!row.canEdit">'
                        + '<span ng-if="row.subject !=\'60\'">{{row.isStudyHere | ifElse}}</span>'
                        + '<span ng-if="row.subject ==\'60\'">班级人数：{{ vm.score.classPeoples }}</span>'
                    + '</span>'
        }],
        rows: [],
        pager: {
            pagable: false
        }
    });

    customer.registerValue('scoresViewDataHeader', {
        headers: [{
            field: "subject",
            name: "科目",
            template: '<span>{{ row.subject | examSubject }}</span>',
        }, {
            field: "teacherName",
            name: "任课教师"
        }, {
            field: "teacherOrgName",
            name: "学科组"
        }, {
            field: 'realScore',
            name: '得分'
        }, {
            field: 'paperScore',
            name: '卷面分'
        }, {
            field: "classPeoples",
            name: "班级总人数"
        }, {
            field: 'classRank',
            name: '班级名次'
        }, {
            field: 'gradeRank',
            name: '年级名次'
        }, {
            field: 'scoreChangeType',
            name: '成绩升降',
            template: '<span>{{ row.scoreChangeType | scoreChangeType }}</span>'
        }, {
            field: 'satisficing',
            name: '家长满意度',
            template: '<span>{{ row.satisficing | scoreSatisficing }}</span>'
        }, {
            field: 'isStudyHere',
            name: '是否在学大辅导',
            template: '<span>{{ row.isStudyHere | ifElse }}</span>'
        }],
        rows: [],
        pager: {
            pagable: false
        }
    });

    customer.registerFactory('scoresDataViewService', ['$state', 'scoreDataService', 'dataSyncService', 'mcsDialogService', 'scoresListDataHeader',
        function ($state, scoreDataService, dataSyncService, mcsDialogService) {
            var service = this;

            // 录入成绩表头
            service.configScoresEditHeaders = function (vm, header) {
                vm.data = header;
            };

            service.initWatchExps = function ($scope, vm, watchExps) {
                if (!watchExps && !watchExps.length) return;
                for (var index in watchExps) {
                    (function () {
                        var exp = watchExps[index];
                        $scope.$watch(exp.watchExp, function (value) {
                            if (value != exp.selectedValue) {
                                if (vm.criteria[exp.watch]) {
                                    vm.criteria[exp.watch] = '';
                                } else {
                                    vm[exp.watch] = '';
                                }
                            }
                        });
                    })();
                }
            };

            service.handleTeachers = function () {
                var teachers = ppts.dict[ppts.config.dictMappingConfig.scoreTeacher];
                var result = [];
                for (var i in teachers) {
                    var flag = false;
                    for (var j in result) {
                        if (teachers[i].key == result[j].key) {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                        result.push(teachers[i]);
                }
                ppts.dict[ppts.config.dictMappingConfig.scoreTeacher] = result;
            };

            // 考试年级添加parentKey
            service.fillGradeParentKey = function () {
                var grades = ppts.dict[ppts.config.dictMappingConfig.grade];
                var result = [];
                for (var index in grades) {
                    var grade = grades[index];
                    switch (grade.value) {
                        case '小学一年级':
                        case '小学二年级':
                        case '小学三年级':
                        case '小学四年级':
                        case '小学五年级':
                        case '小学六年级':
                            grade.parentKey = '1';
                            result.push(grade);
                            break;
                        case '初中一年级':
                        case '初中二年级':
                        case '初中三年级':
                        case '初中四年级':
                            grade.parentKey = '2';
                            result.push(grade);
                            break;
                        case '高中一年级':
                        case '高中二年级':
                        case '高中三年级':
                        case '高三毕业':
                            grade.parentKey = '3';
                            result.push(grade);
                            break;
                    }
                }
                ppts.dict[ppts.config.dictMappingConfig.grade] = result;
            }

            service.showRowItems = function (vm, studyStage) {
                vm.data.rows = [];
                var examSubject = ppts.dict[ppts.config.dictMappingConfig.examSubject];
                if (!examSubject) return;
                var subjects = [];
                for (var index in examSubject) {
                    if (mcs.util.containsElement(examSubject[index].parentKey, studyStage)) {
                        var subject = examSubject[index];
                        if (vm.isTeacher) {
                            if (vm.teachers[0].subject == subject.key) {
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
                addRow(vm);
            };

            var addRow = function (vm) {
                for (var index in vm.subjects) {
                    vm.data.rows.push({
                        subject: vm.subjects[index].key,
                        sortNo: vm.subjects[index].sortNo,
                        canEdit: true
                    });
                }
            };

            service.checkCanEdit = function (vm) {
                if (!vm.scoreItems) return;
                var item = [];
                for (var index in vm.scoreItems) {
                    item = vm.scoreItems[index];
                    item.canEdit = true;
                    if (item.createTime.getMonth() < new Date().getMonth() /*item.createTime > vm.closingAccountDate*/ ) {
                        item.canEdit = false;
                    }
                }
                var canEidt_totalScore = false;
                vm.scoreItems.forEach(function (item) {
                    if (item.canEdit)
                        canEidt_totalScore = true;
                });
                if (!canEidt_totalScore) {
                    if (vm.scoreItems < vm.subjects) {
                        canEidt_totalScore = true;
                    }
                }
                vm.canEidt_totalScore = canEidt_totalScore;
            };

            return service;
        }]);
});