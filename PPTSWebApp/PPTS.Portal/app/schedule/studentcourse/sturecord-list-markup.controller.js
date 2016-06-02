define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentCourseDataService],
        function (schedule) {
            schedule.registerController('stucrsMarkupController', [
            '$scope', 'studentassignmentDataService', 'studentCourseDataService', '$uibModalInstance', 'mcsDialogService', 'dataSyncService', '$stateParams',
            function ($scope, studentassignmentDataService, studentCourseDataService, $uibModalInstance, mcsDialogService, dataSyncService, $stateParams) {
                var vm = this;
                //存储时间
                vm.beginDate = ''; vm.beginHour = ''; vm.beginMinute = ''; vm.endHour = ''; vm.endMinute = '';
                vm.assignDuration = 0;
                vm.showOrderSelect = false; vm.showSubjectSelect = false; vm.showGradeSelect = false; vm.showTchSelect = false;

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
                    vm.courseAmount.push({key:i,value:i})
                };
                
                // 页面初始化加载或重新搜索时查询
                vm.init = function () {
                    vm.criteria = vm.criteria || {};
                    vm.CID = $stateParams.id;
                    vm.criteria.customerID = vm.CID;               
                    vm.result = vm.result || {};

                    studentassignmentDataService.getAssignCondition(vm.criteria, function (result) {
                        vm.result = result;
                        vm.result.assign.copyAllowed = 0;
                        dataSyncService.injectDictData(mcs.util.mapping(vm.result.assignCondition, { key: 'conditionID', value: 'conditionName4Customer' }, 'AssignCondition'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.result.assignExtension, { key: 'assetID', value: 'assetName' }, 'Asset'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.courseAmount, { key: 'key', value: 'value' }, 'CourseAmount'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.courseHour, { key: 'key', value: 'value' }, 'Hour'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.courseMinute, { key: 'key', value: 'value' }, 'Minute'));
                        dataSyncService.injectPageDict(['ifElse']);
                        $scope.$broadcast('dictionaryReady');
                    });
                };

                vm.init();

                vm.shareFieldName = ['assetID', 'assetCode', 'customerID', 'customerCode', 'customerName', 'productID', 'productCode', 'productName', 'accountID'
                  , 'grade', 'gradeName', 'subject', 'subjectName'];

                /* 'RoomID','RoomCode','RoomName','TeacherID','TeacherName','TeacherJobID','TeacherJobOrgID','TeacherJobOrgName','IsFullTimeTeacher'*/

                vm.initAssignFieldFromAsset = ['assetID', 'assetCode', 'customerID', 'customerCode', 'customerName', 'productID', 'productCode', 'productName', 'accountID'
                    , 'grade', 'gradeName', 'subject', 'subjectName', 'courseLevel', 'courseLevelName', 'lessonDuration', 'lessonDurationValue'];

                vm.fieldToAssign = [];

                vm.fieldToAC = ['conditionID', 'conditionName4Customer', 'conditionName4Teacher', 'courseLevel', 'courseLevelName', 'lessonDuration'
                    , 'lessonDurationValue'];

              /*  vm.filedName = ['assetID', 'assetCode', 'grade', 'gradeName', 'courseLevel', 'courseLevelName', 'subject', 'subjectName', 'lessonDuration'
                    , 'lessonDurationValue', 'assetName', 'customerID', 'customerCode', 'customerName', 'consultantID', 'consultantJobID', 'consultantName', 'educatorID'
                    , 'educatorJobID', 'educatorName', 'productID', 'productCode', 'productName', 'accountID'];//'campusID', 'campusName'

                vm.filedNameAC = ['conditionID', 'conditionName4Customer', 'ConditionName4Teacher', 'subject', 'subjectName', 'grade', 'gradeName', 'teacherID'
                    , 'teacherName', 'teacherJobID', 'teacherJobOrgID', 'teacherJobOrgName', 'isFullTimeTeacher', 'accountID', 'customerID', 'customerCode'
                    , 'customerName'];*/

                vm.teacherInfo = ['teacherID', 'teacherName', 'teacherJobID', 'teacherJobOrgID', 'teacherJobOrgName', 'isFullTimeTeacher'];
                vm.subjectInfo = ['subject', 'subjectName'];

                /*选择排课条件*/
                vm.selectAssignConditionClick = function (item) {
                    //不等-1，选择了一个已经存在的排课条件
                    if (item.key != '-1') {
                        //选项隐藏
                        vm.showOrderSelect = false; vm.showSubjectSelect = false; vm.showGradeSelect = false; vm.showTchSelect = false;
                        //重新初始化排课对象
                        vm.resetAssignExtension();
                        vm.result.assign.assetID = '-1';
                        //获取排课条件对象
                        var ac = vm.getAssignCondition(item.key);
                        //获取资产对象
                        var ae = vm.getAssignExtension(ac.assetID);
                        vm.result.assign["assetName"] = ae["assetName"];
                        //将资产对象的字段值付给排课对象 名称不一样了
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
                        ///课程时间有可能发生改变，所以要重新计算
                        calcDurationValue();
                    }
                    else {
                        //新建 重新初始化排课对象
                        vm.showOrderSelect = true; vm.showSubjectSelect = false; vm.showGradeSelect = false; vm.showTchSelect = false;
                        vm.resetAssignExtension();
                        vm.result.assign.conditionID = '-1';
                    }
                };

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
                            vm.selectGradeClick({ key: vm.result.assign.grade,value:vm.result.assign.gradeName });
                        }
                    }
                    ///课程时间有可能发生改变，所以要重新计算
                    calcDurationValue();
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

                /*计算排课时间*/
                vm.calcDurationValueBHClick = function (item) {
                    vm.beginHour = item.value;
                    calcDurationValue();
                };
                /*计算排课时间*/
                vm.calcDurationValueBMClick = function (item) {
                    vm.beginMinute = item.value;
                    calcDurationValue();
                };
                /*计算排课时间*/
                vm.calcDurationValueCAClick = function (item) {
                    //vm.result.assign.realAmount = item.value;
                    vm.result.assign.amount = item.value;
                    calcDurationValue();
                };
                /*计算排课时间*/
                calcDurationValue = function () {
                    if (vm.beginHour == '' || vm.beginMinute == '' ||  vm.beginHour == null || vm.beginMinute == null )
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
                     vm.endMinute = vm.getDoubleStr( curDate.getMinutes());
                };

                /*保存补录的课时*/
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
                    if (vm.result.assign.grade == '' || vm.result.assign.grade == null) {
                        vm.showMsg("请选择上课年级，如没有待选项，请联系管理员！");
                        return false;
                    };
                    if (vm.result.assign.subject == '' || vm.result.assign.subject == null) {
                        vm.showMsg("请选择上课科目，如没有待选项，请联系管理员！");
                        return false;
                    };
                    if (vm.result.assign.teacherID == '' || vm.result.assign.teacherID == null) {
                        vm.showMsg("请选择上课教师，如没有待选项，请联系管理员！");
                        return false;
                    };
                    vm.result.assign.startTime = bdate;
                    vm.result.assign.endTime = edate;
                    if (vm.result.assign.copyAllowed == 0)
                        vm.result.assign.copyAllowed = false;
                    else
                        vm.result.assign.copyAllowed = true;

                    studentCourseDataService.markupAssign(vm.result.assign, function (retValue) {
                        if (retValue != null && retValue.msg != "ok") {
                            vm.showMsg(retValue.msg);
                            return;
                        }
                        $uibModalInstance.close("ok");
                    }, function (error) {
                        vm.showMsg(error.data.description);
                    });
                };

                vm.cancel = function () {
                    $uibModalInstance.dismiss('canceled');
                };

                vm.showMsg = function (msg) {
                    mcsDialogService.error({ title: '提示信息', message: msg });
                };

            }]);
        });