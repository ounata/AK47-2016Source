define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (schedule) {
            schedule.registerController('stuAsgmtAddController', [
                '$scope', '$uibModalInstance', '$state', '$stateParams', '$filter', 'dataSyncService', 'studentassignmentDataService', 'mcsDialogService',
            function ($scope, $uibModalInstance, $state, $stateParams, $filter, dataSyncService, studentassignmentDataService, mcsDialogService) {
                var vm = this;
                /*实际上课时长*/
                vm.assignDuration = 0;
                vm.showOrderSelect = false; vm.showSubjectSelect = false; vm.showGradeSelect = false; vm.showTchSelect = false;
                vm.dateClass = { beginDate: '', endDate: '' };

                /*页面初始化加载*/
                vm.init = function () {
                    vm.criteria = vm.criteria || {};
                    vm.criteria.customerID = $stateParams.id;
                    vm.CID = $stateParams.id;
                    vm.result = vm.result || {};
                    studentassignmentDataService.getAssignCondition(vm.criteria, function (result) {
                        vm.result = result;
                        dataSyncService.injectDictData(mcs.util.mapping(vm.result.assignCondition, { key: 'conditionID', value: 'conditionName4Customer' }, 'AssignCondition'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.result.assignExtension, { key: 'assetID', value: 'assetName' }, 'Asset'));
                        dataSyncService.injectDynamicDict('ifElse');
                        $scope.$broadcast('dictionaryReady');

                        vm.selectAssignConditionClick({ key: "" });
                    });
                };
                vm.init();
                vm.shareFieldName = ['assetID', 'assetCode', 'customerID', 'customerCode', 'customerName', 'productID', 'productCode', 'productName', 'accountID'
                    , 'grade', 'gradeName', 'subject', 'subjectName', 'categoryType', 'categoryTypeName'];

                /* 'RoomID','RoomCode','RoomName','TeacherID','TeacherName','TeacherJobID','TeacherJobOrgID','TeacherJobOrgName','IsFullTimeTeacher'*/

                vm.initAssignFieldFromAsset = ['assetID', 'assetCode', 'customerID', 'customerCode', 'customerName', 'productID', 'productCode', 'productName', 'accountID'
                    , 'grade', 'gradeName', 'subject', 'subjectName', 'courseLevel', 'courseLevelName', 'lessonDuration', 'lessonDurationValue', 'categoryType', 'categoryTypeName'];

                vm.fieldToAssign = [];

                vm.fieldToAC = ['conditionID', 'conditionName4Customer', 'conditionName4Teacher', 'courseLevel', 'courseLevelName', 'lessonDuration'
                    , 'lessonDurationValue', 'categoryType', 'categoryTypeName'];

                vm.teacherInfo = ['teacherID', 'teacherName', 'teacherJobID', 'teacherJobOrgID', 'teacherJobOrgName', 'isFullTimeTeacher'];
                vm.subjectInfo = ['subject', 'subjectName'];

                /*选择排课条件*/
                vm.selectAssignConditionClick = function (item) {
                    //不等于空，选择了一个已经存在的排课条件
                    if (item.key != "") {
                        //选项隐藏
                        vm.showOrderSelect = false; vm.showSubjectSelect = false; vm.showGradeSelect = false; vm.showTchSelect = false;
                        //重新初始化排课对象
                        vm.resetAssignExtension();
                        vm.result.assign.assetID = "";
                        //获取排课条件对象
                        var ac = vm.getAssignCondition(item.key);
                        //获取资产对象
                        var ae = vm.getAssignExtension(ac.assetID);
                        vm.result.assign["assetName"] = ae["assetName"];
                        //名称不一样的字段，单独赋值；初始化排课对象，将资产对象的字段值赋值给排课对象
                        vm.result.assign["assignPrice"] = ae["price"];

                        //将排课条件对象的字段值赋值给排课对象
                        for (var fn in vm.shareFieldName) {
                            vm.result.assign[vm.shareFieldName[fn]] = ac[vm.shareFieldName[fn]];
                        }
                        for (var fn in vm.fieldToAC) {
                            vm.result.assign[vm.fieldToAC[fn]] = ac[vm.fieldToAC[fn]];
                        }
                        for (var fn in vm.teacherInfo) {
                            vm.result.assign[vm.teacherInfo[fn]] = ac[vm.teacherInfo[fn]];
                        }
                    }
                    else {
                        //新建,重新初始化排课对象
                        vm.showOrderSelect = true; vm.showSubjectSelect = false; vm.showGradeSelect = false; vm.showTchSelect = false;
                        vm.resetAssignExtension();
                        vm.result.assign.conditionID = "";
                    }
                };

                /*选择订单编号，新建排课条件及排课*/
                vm.selectAssetClick = function (item) {
                    //获取资产对象
                    var ae = vm.getAssignExtension(item.key);
                    ///将资产对象赋值给排课对象
                    for (var fn in vm.initAssignFieldFromAsset) {
                        vm.result.assign[vm.initAssignFieldFromAsset[fn]] = ae[vm.initAssignFieldFromAsset[fn]];
                    };
                    //将资产对象的字段值付给排课对象 名称不一样了
                    vm.result.assign["assignPrice"] = ae["price"];
                    ///年级 科目都一定的情况下，直接加载教师
                    if (ae.grade != '' && ae.grade != '0' && ae.subject != '' && ae.subject != '0') {
                        vm.showTchSelect = true;
                        for (var index in vm.result.teacher.tch) {
                            var gs = vm.result.teacher.tch[index];
                            if (gs.grade == ae.grade && gs.subject == ae.subject) {
                                dataSyncService.injectDictData(mcs.util.mapping(gs.teachers, { key: 'key', value: 'value' }, 'Teacher'));
                                $scope.$broadcast('dictionaryReady');
                                break;
                            }
                        };
                    }
                    else {
                        //如果年级为空，选择年级数据
                        if (ae.grade == '' || ae.grade == '0') {
                            vm.showGradeSelect = true;
                            ///初始化年级列表
                            dataSyncService.injectDictData(mcs.util.mapping(vm.result.teacher.grade, { key: 'key', value: 'value' }, 'SubGrade'));
                            $scope.$broadcast('dictionaryReady');
                        }
                        else { //说明年级已经确定，设置科目
                            vm.selectGradeClick({ key: vm.result.assign.grade, value: vm.result.assign.gradeName });
                        }
                    }
                };

                /*选择年级事件*/
                var curGrade = "";
                vm.selectGradeClick = function (item) {
                    curGrade = item.key;
                    ///设置排课对象年级的值
                    vm.result.assign.grade = item.key;
                    vm.result.assign.gradeName = item.value;
                    ///设置待选科目
                    vm.showSubjectSelect = true;
                    vm.result.assign.subject = "";
                    //根据选择的年级加载科目，从教师信息中加载对应的科目信息
                    for (var index in vm.result.teacher.gradeSubjectRela) {
                        var cSubject = vm.result.teacher.gradeSubjectRela[index];
                        if (item.key == index) {
                            dataSyncService.injectDictData(mcs.util.mapping(cSubject, { key: 'key', value: 'value' }, 'SubSubject'));
                            $scope.$broadcast('dictionaryReady');
                            break;
                        };
                    };
                    //年级被选择改变，教师下拉框被隐藏，清空排课对象中教师的相关信息 ,清空科目信息
                    for (var fn in vm.subjectInfo) {
                        vm.result.assign[vm.subjectInfo[fn]] = '';
                    };
                    vm.showTchSelect = false;
                    for (var fn in vm.teacherInfo) {
                        vm.result.assign[vm.teacherInfo[fn]] = '';
                    };
                };

                /*选择科目事件*/
                vm.selectSubjectClick = function (item) {
                    ///设置排课对象科目的值
                    vm.result.assign.subject = item.key;
                    vm.result.assign.subjectName = item.value;
                    ////设置待选教师
                    vm.showTchSelect = true;
                    for (var index in vm.result.teacher.tch) {
                        var gs = vm.result.teacher.tch[index];
                        if (curGrade == gs.grade && item.key == gs.subject) {
                            dataSyncService.injectDictData(mcs.util.mapping(gs.teachers, { key: 'key', value: 'value' }, 'Teacher'));
                            $scope.$broadcast('dictionaryReady');
                            break;
                        }
                    };
                    //科目发生改变，清空排课对象教师信息
                    for (var fn in vm.teacherInfo) {
                        vm.result.assign[vm.teacherInfo[fn]] = '';
                    };
                };

                /*选择教师事件*/
                vm.selectTeacherClick = function (item) {
                    var teacherID = item.key;
                    for (var index in vm.result.teacher.tch) {
                        var gs = vm.result.teacher.tch[index];
                        for (var index2 in gs.teachers) {
                            if (gs.teachers[index2].key == item.key) {
                                vm.result.assign.teacherID = gs.teachers[index2].key;
                                vm.result.assign.teacherName = gs.teachers[index2].value;
                                vm.result.assign.teacherJobID = gs.teachers[index2].field01;
                                vm.result.assign.teacherJobOrgID = gs.teachers[index2].teacherJobOrgID;
                                vm.result.assign.teacherJobOrgName = gs.teachers[index2].teacherJobOrgName;
                                vm.result.assign.isFullTimeTeacher = gs.teachers[index2].isFullTimeTeacher;
                                break;
                            }
                        }
                    }
                };

                vm.watchLogOff = $scope.$watchCollection('vm.dateClass', function (newValue, oldValue) {
                    if (vm.dateClass.beginDate == '' || vm.dateClass.beginDate == null || vm.dateClass.endDate == '' || vm.dateClass.endDate == null)
                        return;

                    //实际上课时长     
                    vm.assignDuration = ( new Date(vm.dateClass.endDate).getTime() -  new Date(vm.dateClass.beginDate).getTime()) / 60000;

                    if (vm.result.assign.lessonDurationValue != '' && vm.result.assign.lessonDurationValue != 0) {

                        var realValue = vm.assignDuration / vm.result.assign.lessonDurationValue;

                        var minValue = Math.floor(realValue);
                        var midValue = minValue + 0.5;

                        if (realValue >= midValue)
                            vm.result.assign.amount = midValue;
                        else
                            vm.result.assign.amount = minValue;
                    }
                });

                /*保存排课信息*/
                vm.save = function () {
                    var flag = true;
                    var msg = "";
                    if ((vm.result.assign.conditionID == "" || vm.result.assign.conditionID == null) && vm.result.assign.assetID == "") {
                        msg += '请选择订单编号<br>';
                        flag = false;
                    }
                    if (vm.result.assign.grade == '' || vm.result.assign.grade == null || vm.result.assign.grade == '0') {
                        msg += "请选择上课年级，如没有待选项，请联系管理员！<br>";
                        flag = false;
                    };
                    if (vm.result.assign.subject == '' || vm.result.assign.subject == null || vm.result.assign.subject == '0') {
                        msg += "请选择上课科目，如没有待选项，请联系管理员！<br>";
                        flag = false;
                    };
                    if (vm.result.assign.teacherID == '' || vm.result.assign.teacherID == null || vm.result.assign.teacherID == '0') {
                        msg += "请选择上课教师，如没有待选项，请联系管理员！<br>";
                        flag = false;
                    };

                    if (vm.dateClass.beginDate == '' || vm.dateClass.beginDate == null || vm.dateClass.endDate == '' || vm.dateClass.endDate == null) {
                        msg += "请设置上课时间<br>";
                        flag = false;
                    }
                    if (flag == false) {
                        vm.showMsg(msg);
                        return false;
                    }
                    vm.result.assign.startTime = new Date(vm.dateClass.beginDate);
                    vm.result.assign.endTime = new Date(vm.dateClass.endDate);
                    if (vm.result.assign.startTime.getDate() != vm.result.assign.endTime.getDate()) {
                        vm.showMsg("上课开始时间和结束时间必须为同一天");
                        return false;
                    }
                    //var curDate = new Date();
                    //var minDate = new Date(curDate.getFullYear(), curDate.getMonth(), curDate.getDate(), 0, 0, 0);
                    //var maxDate = new Date(curDate.getFullYear(), curDate.getMonth(), curDate.getDate(), 0, 0, 0);
                    //maxDate.setDate(maxDate.getDate() + 29);
                    //flag = true;
                    //msg = "";
                    //if (vm.result.assign.startTime < minDate) {
                    //    msg += "上课时间不能小于当前日期";
                    //    flag = false;
                    //}
                    //if (vm.result.assign.endTime >= maxDate) {
                    //    msg += "上课时间不能大于等于:" + maxDate.getFullYear() + "-" + (maxDate.getMonth() + 1) + "-" + maxDate.getDate();
                    //    flag = false;
                    //}
                    //if (flag == false) {
                    //    vm.showMsg(msg);
                    //    return false;
                    //}
                    studentassignmentDataService.createAssignCondition(vm.result.assign, function (success) {
                        if (success != null) {
                            vm.result.assign.assignID = success.assignID;

                            if (vm.watchLogOff != null) {
                                vm.watchLogOff();
                            }

                            $uibModalInstance.close(vm.result.assign);
                        }
                    }, function (error) {
                        vm.showMsg(error.data.description);
                    });
                };

                vm.cancel = function () {

                    if (vm.watchLogOff != null) {
                        vm.watchLogOff();
                    }

                    $uibModalInstance.dismiss('canceled');
                };

                /*获取资产对象*/
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

                /*获取排课条件对象*/
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

                /*重新设置排课对象*/
                vm.resetAssignExtension = function () {
                    for (var fn in vm.shareFieldName) {
                        vm.result.assign[vm.shareFieldName[fn]] = "";
                    };
                    for (var fn in vm.fieldToAC) {
                        vm.result.assign[vm.fieldToAC[fn]] = "";
                    };
                    for (var fn in vm.teacherInfo) {
                        vm.result.assign[vm.teacherInfo[fn]] = "";
                    };
                    vm.result.assign["assignPrice"] = "";
                };

                vm.showMsg = function (msg) {
                    mcsDialogService.error({ title: '提示信息', message: msg });
                };

            }])
        });