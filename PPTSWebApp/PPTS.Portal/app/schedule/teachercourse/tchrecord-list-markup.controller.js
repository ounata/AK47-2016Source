/*教师补录  学科课时*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherCourseDataService,
        ppts.config.dataServiceConfig.teacherAssignmentDataService],
        function (schedule) {
            schedule.registerController('tchcrsMarkupController', [
            '$scope', 'teacherCourseDataService', '$uibModalInstance', 'mcsDialogService', 'teacherAssignmentDataService', 'dataSyncService',
            function ($scope, teacherCourseDataService, $uibModalInstance, mcsDialogService, teacherAssignmentDataService, dataSyncService) {
                var vm = this;
                vm.currCampusID = '';
                vm.currCampusName = '';
                vm.teachers = '';

                /*存储时间*/
                vm.beginDate = ''; vm.beginHour = ''; vm.beginMinute = ''; vm.endHour = ''; vm.endMinute = '';
                vm.assignDuration = 0;
                vm.showStudentSelect = false; vm.showOrderSelect = false; vm.showGradeSelect = false; vm.showSubjectSelect = false;

                /*格式化数字*/
                vm.getDoubleStr = function (curValue) {
                    if (parseInt(curValue) < 10)
                        return '0' + curValue;
                    return curValue;
                };

                /*小时*/
                vm.courseHour = new Array();
                for (i = 6; i <= 23; i += 1) {
                    var t = vm.getDoubleStr(i)
                    vm.courseHour.push({ key: t, value: t })
                };

                /*分钟*/
                vm.courseMinute = new Array();
                for (i = 0; i <= 55; i += 5) {
                    var t = vm.getDoubleStr(i)
                    vm.courseMinute.push({ key: t, value: t })
                };

                /*课时数*/
                vm.courseAmount = new Array()
                for (i = 0.5; i <= 6; i += 0.5) {
                    vm.courseAmount.push({ key: i, value: i })
                };

                vm.init = function () {
                    teacherCourseDataService.getCurrUserCampus(function (retValue) {
                        if (retValue.campusID == undefined) {
                            vm.showMsg("当前操作人没有归属校区，不能补录课时");
                            vm.cancel();
                        }
                        vm.currCampusID = retValue.campusID;
                        vm.currCampusName = retValue.campusName;

                    }, function (error) { });
                };

                vm.init();

                vm.watchLogOff = $scope.$watch('vm.teachers', function (newValue, oldValue) {
                    vm.result = vm.result || {};
                    if (newValue == '' || newValue.length == 0)
                        return;
                    vm.criteria = vm.criteria || {};
                    vm.criteria.teacherID = newValue[0].teacherId;
                    vm.criteria.teacherJobID = newValue[0].jobID;

                    teacherAssignmentDataService.initCreateAssign(vm.criteria, function (result) {
                        vm.result = result;
                        vm.result.assign.copyAllowed = 0;
                        dataSyncService.injectDictData(mcs.util.mapping(vm.result.assignCondition, { key: 'conditionID', value: 'conditionName4Teacher' }, 'AssignCondition'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.result.student.student, { key: 'key', value: 'value' }, 'Student'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.result.student.grade, { key: 'key', value: 'value' }, 'SubGrade'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.courseAmount, { key: 'key', value: 'value' }, 'CourseAmount'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.courseHour, { key: 'key', value: 'value' }, 'Hour'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.courseMinute, { key: 'key', value: 'value' }, 'Minute'));
                        dataSyncService.injectPageDict(['ifElse']);
                        $scope.$broadcast('dictionaryReady');
                    });
                });

                vm.shareFieldName = ['assetID', 'assetCode', 'customerID', 'customerCode', 'customerName', 'productID', 'productCode', 'productName', 'accountID'
                    , 'grade', 'gradeName', 'subject', 'subjectName'];

                vm.initAssignFieldFromAsset = ['assetID', 'assetCode', 'customerID', 'customerCode', 'customerName', 'productID', 'productCode', 'productName', 'accountID'
            , 'grade', 'gradeName', 'subject', 'subjectName', 'courseLevel', 'courseLevelName', 'lessonDuration', 'lessonDurationValue'];

                vm.fieldToAssign = [];

                vm.fieldToAC = ['conditionID', 'conditionName4Customer', 'conditionName4Teacher', 'courseLevel', 'courseLevelName', 'lessonDuration'
                    , 'lessonDurationValue'];

                vm.subjectInfo = ['subject', 'subjectName'];

                /*选择排课条件*/
                vm.selectAssignConditionClick = function (item) {
                    //不等-1，选择了一个已经存在的排课条件
                    if (item.key != '-1') {
                        ///重新设置传回结果对象值
                        vm.resetAssignExtension();
                        //资产选择及科目选择都已经确定，所以选项隐藏
                        vm.showStudentSelect = false;
                        vm.showOrderSelect = false;
                        vm.showGradeSelect = false;
                        vm.showSubjectSelect = false;
                        //获取排课条件对象
                        var ac = vm.getAssignCondition(item.key);
                        //获取资产对象
                        var data = { customerID: ac.customerID, assetID: ac.assetID };
                        teacherAssignmentDataService.getAssetByAssetID(data, function (retValue) {
                            //将资产对象的字段值赋值给排课对象
                            var ae = retValue.result;
                            for (var fn in vm.initAssignFieldFromAsset) {
                                vm.result.assign[vm.initAssignFieldFromAsset[fn]] = ae[vm.initAssignFieldFromAsset[fn]];
                            }
                            vm.result.assign["assetName"] = ae["assetName"];
                            vm.result.assign['assignPrice'] = ae["price"];
                            //将排课条件对象的字段值赋值给排课对象
                            for (var fn in vm.fieldToAC) {
                                vm.result.assign[vm.fieldToAC[fn]] = ac[vm.fieldToAC[fn]];
                            }
                            for (var fn in vm.shareFieldName) {
                                vm.result.assign[vm.shareFieldName[fn]] = ac[vm.shareFieldName[fn]];
                            };
                        }, function (error) {
                        });
                    }
                    else {
                        //新建
                        ///教师对象的学员下拉框要显示出来，资产清空
                        vm.showStudentSelect = true; vm.showOrderSelect = false;
                        vm.showGradeSelect = false; vm.showSubjectSelect = false;
                        //重新初始化排课对象
                        vm.resetAssignExtension();
                    }
                };

                vm.resetAssignExtension = function () {
                    for (var fn in vm.shareFieldName) {
                        vm.result.assign[vm.shareFieldName[fn]] = "";
                    };
                    for (var fn in vm.fieldToAC) {
                        vm.result.assign[vm.fieldToAC[fn]] = "";
                    };
                    //对应资产表中的Price
                    vm.result.assign['assignPrice'] = "";
                    vm.result.assign["assetName"] = "";
                    vm.result.assign.conditionID = '-1';
                };

                /*获取指定排课条件对象*/
                vm.getAssignCondition = function (conditionID) {
                    var result = null;
                    for (var i in vm.result.assignCondition) {
                        if (vm.result.assignCondition[i].conditionID == conditionID) {
                            result = vm.result.assignCondition[i];
                            break;
                        };
                    };
                    return result;
                };

                /*获取指定的资产对象*/
                vm.getAssignExtension = function (assetID) {
                    var result = null;
                    for (var i in vm.result.assignExtension) {
                        if (vm.result.assignExtension[i].assetID == assetID) {
                            result = vm.result.assignExtension[i];
                            break;
                        };
                    };
                    return result;
                };

                /*选择学员，加载学员的资产*/
                var currStudent = null;
                vm.selectStudentClick = function (item) {
                    //重新初始化排课对象
                    vm.resetAssignExtension();
                    var data = {};
                    vm.showOrderSelect = true; vm.showGradeSelect = false; vm.showSubjectSelect = false;
                    var studentCollection = vm.result.student;
                    for (var i in studentCollection.student) {
                        currStudent = studentCollection.student[i];
                        if (currStudent.key == item.key) {
                            //将学员信息赋值给排课对象
                            vm.result.assign.customerID = currStudent["key"];
                            vm.result.assign.customerCode = currStudent["field01"];
                            vm.result.assign.customerName = currStudent["value"];
                            data.customerID = currStudent["key"];
                            break;
                        }
                    };
                    teacherAssignmentDataService.getAssetByCustomerID(data, function (retValue) {
                        vm.result.assignExtension = retValue.result;
                        dataSyncService.injectDictData(mcs.util.mapping(vm.result.assignExtension, { key: 'assetID', value: 'assetName' }, 'Asset'));
                        $scope.$broadcast('dictionaryReady');
                    }, function (error) {
                    });
                };

                /*选择资产，新建排课条件及排课*/
                var aeSubject = '';
                vm.selectAssetClick = function (item) {
                    //重新初始化排课对象
                    vm.resetAssignExtension();
                    //根据选择的资产ID，从资产集合中选择资产实体
                    var ae = vm.getAssignExtension(item.key);
                    //把资产中的字段值赋值给排课实体属性
                    for (var fn in vm.initAssignFieldFromAsset) {
                        vm.result.assign[vm.initAssignFieldFromAsset[fn]] = ae[vm.initAssignFieldFromAsset[fn]];
                    }
                    //将资产对象的字段值赋值给排课对象,名称不一样了
                    vm.result.assign['assignPrice'] = ae['price'];

                    aeSubject = vm.result.assign.subject;
                    if (ae.grade != '' && ae.grade != '0' && ae.subject != '' && ae.subject != '0') {
                        vm.showGradeSelect = false; vm.showSubjectSelect = false;
                    }
                    else if (ae.grade != '' && ae.grade != '0') {
                        vm.showStudentSelect = true;
                    }
                    else {
                        vm.showGradeSelect = true;
                    }
                };

                var curGrade = "";
                /*选择年级事件*/
                vm.selectGradeClick = function (item) {
                    curGrade = item.key;
                    ///设置排课对象年级的值
                    vm.result.assign.grade = item.key;
                    vm.result.assign.gradeName = item.value;
                    //根据选择的年级加载科目，从教师信息中加载对应的科目信息
                    for (var index in vm.result.student.gradeSubjectRela) {
                        var cSubject = vm.result.student.gradeSubjectRela[index];
                        if (item.key == index) {
                            dataSyncService.injectDictData(mcs.util.mapping(cSubject, { key: 'key', value: 'value' }, 'SubSubject'));
                            $scope.$broadcast('dictionaryReady');
                            break;
                        };
                    };
                    //如果资产中科目为空，科目需要选择
                    if (aeSubject == '' || aeSubject == '0') {
                        vm.showSubjectSelect = true;
                        //年级被选择改变，清空科目信息
                        for (var fn in vm.subjectInfo) {
                            vm.result.assign.subject = ''; vm.result.assign.subjectName = '';
                        };
                    }
                };

                /*选择科目*/
                vm.selectSubjectClick = function (item) {
                    vm.result.assign['subject'] = item.key;
                    vm.result.assign['subjectName'] = item.value;
                };

                vm.calcDurationValueBHClick = function (item) {
                    vm.beginHour = item.value;
                    calcDurationValue();
                };

                vm.calcDurationValueBMClick = function (item) {
                    vm.beginMinute = item.value;
                    calcDurationValue();
                };

                vm.calcDurationValueCAClick = function (item) {
                    //vm.result.assign.realAmount = item.value;
                    vm.result.assign.amount = item.value;
                    calcDurationValue();
                };

                calcDurationValue = function () {
                    if (vm.beginHour == '' || vm.beginMinute == '' || vm.beginHour == null || vm.beginMinute == null)
                        return;

                    if (vm.result.assign.amount == '' || vm.result.assign.amount == 0 || vm.result.assign.amount == null)
                        return;

                    if (vm.result.assign.lessonDurationValue == '' || vm.result.assign.lessonDurationValue == 0 || vm.result.assign.lessonDurationValue == null)
                        return;

                    vm.result.assign.durationValue = vm.result.assign.lessonDurationValue;

                    ///计算结束时间
                    vm.endHour = '', vm.endMinute = '';

                    //实际上课时长  分钟   
                    var courseMinute = vm.result.assign.durationValue * vm.result.assign.amount;
                    var curDate = new Date();
                    var curDateHour = new Date(curDate.getFullYear(), curDate.getMonth(), curDate.getDate(), vm.beginHour, vm.beginMinute, 0);

                    curDate.setTime(curDateHour.getTime() + courseMinute * 60 * 1000);

                    vm.endHour = vm.getDoubleStr(curDate.getHours());
                    vm.endMinute = vm.getDoubleStr(curDate.getMinutes());
                };

                vm.save = function () {
                    if (!vm.beginDate) {
                        vm.showMsg('请设置上课日期');
                        return false;
                    }
                    if (vm.beginHour == '' || vm.beginMinute == '' || vm.endHour == '' || vm.endMinute == '' ||
                    vm.beginHour == null || vm.beginMinute == null || vm.endHour == null || vm.endMinute == null) {
                        vm.showMsg('请设置上课时间');
                        return false;
                    }
                    var bdate = new Date(vm.beginDate.getFullYear(), vm.beginDate.getMonth(), vm.beginDate.getDate(), vm.beginHour, vm.beginMinute, 0);
                    var edate = new Date(vm.beginDate.getFullYear(), vm.beginDate.getMonth(), vm.beginDate.getDate(), vm.endHour, vm.endMinute, 0);
                    if (bdate >= edate) {
                        vm.showMsg("上课结束时间不能小于开始时间，请重新设置");
                        return false;
                    };

                    if (vm.result.assign.teacherID == '' || vm.result.assign.teacherID == null) {
                        vm.showMsg("请选择上课教师，如没有待选项，请联系管理员！");
                        return false;
                    };
                    if (vm.result.assign.grade == '' || vm.result.assign.grade == null) {
                        vm.showMsg("请选择上课年级，如没有待选项，请联系管理员！");
                        return false;
                    };
                    if (vm.result.assign.subject == '' || vm.result.assign.subject == null) {
                        vm.showMsg("请选择上课科目，如没有待选项，请联系管理员！");
                        return false;
                    };

                    vm.result.assign.startTime = bdate;
                    vm.result.assign.endTime = edate;

                    if (vm.result.assign.copyAllowed == 0)
                        vm.result.assign.copyAllowed = false;
                    else
                        vm.result.assign.copyAllowed = true;

                    teacherCourseDataService.markupAssign(vm.result.assign, function (success) {
                        if (success != null) {
                            if (vm.watchLogOff != null) {
                                vm.watchLogOff();
                            }
                            vm.result.assign.assignID = success.assignID;
                            $uibModalInstance.close(vm.result.assign);
                        }
                    }, function (error) {
                        vm.showMsg(error.data.description);
                    });
                };

                vm.tchList = [];
                /*自动选择框事件*/
                vm.queryTeacherList = function (query) {
                    var criteria = {};
                    criteria.teacherName = query;
                    return teacherCourseDataService.getTeacher(criteria, function (retValue) {
                        vm.tchList = [];
                        retValue.data.result.forEach(function (item, index) {
                            vm.tchList.push({
                                teacherId: item.teacherID,
                                name: item.jobOrgName + '-' + item.teacherName + '(' + item.teacherOACode + ')',
                                jobID: item.jobID
                            });
                        });

                        return vm.tchList;
                    }
                     , function (error) {

                     });
                };

                vm.cancel = function () {
                    if (vm.watchLogOff != null) {
                        vm.watchLogOff();
                    }
                    $uibModalInstance.dismiss('canceled');
                };

                vm.showMsg = function (msg) {
                    mcsDialogService.error({ title: '提示信息', message: msg });
                };

            }]);
        });