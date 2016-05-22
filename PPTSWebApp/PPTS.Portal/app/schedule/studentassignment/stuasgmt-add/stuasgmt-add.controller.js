define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (schedule) {
            schedule.registerController('stuAsgmtAddController', [
                '$scope', '$uibModalInstance', '$state', '$stateParams', '$filter', 'dataSyncService', 'studentassignmentDataService', 'mcsDialogService',
            function ($scope, $uibModalInstance, $state, $stateParams, $filter, dataSyncService, studentassignmentDataService, mcsDialogService) {
                var vm = this;
                //存储时间
                vm.beginDate = ''; vm.beginHour = ''; vm.beginMinute = ''; vm.endHour = ''; vm.endMinute = '';
                vm.assignDuration = 0;
                vm.showOrderSelect = false; vm.showSubjectSelect = false; vm.showGradeSelect = false; vm.showTchSelect = false;
                // 页面初始化加载或重新搜索时查询
                vm.init = function () {
                    vm.criteria = vm.criteria || {};
                    vm.criteria.customerID = $stateParams.cID;
                    vm.CID = $stateParams.cID;

                    vm.result = vm.result || {};

                    studentassignmentDataService.getAssignCondition(vm.criteria, function (result) {
                        vm.result = result;
                        vm.aeClone = mcs.util.clone(vm.result.assignExtension);
                        dataSyncService.injectDictData(mcs.util.mapping(vm.result.assignCondition, { key: 'conditionID', value: 'conditionName4Customer' }, 'AssignCondition'));
                        dataSyncService.injectDictData(mcs.util.mapping(vm.aeClone, { key: 'assetID', value: 'assetName' }, 'Asset'));
                        dataSyncService.injectPageDict(['ifElse']);
                        $scope.$broadcast('dictionaryReady');
                    });
                };

                vm.init();

                vm.filedName = ['assetID', 'assetCode', 'grade', 'gradeName', 'courseLevel', 'courseLevelName', 'subject', 'subjectName'
                    , 'lessonDuration', 'lessonDurationValue', 'assetName', 'customerID', 'customerCode', 'customerName', 'consultantID', 'consultantJobID'
                , 'consultantName', 'educatorID', 'educatorJobID', 'educatorName', 'productID', 'productCode', 'productName', 'campusID', 'campusName', 'orderNo'];
                vm.filedNameAC = ['conditionID', 'conditionName4Customer', 'ConditionName4Teacher', 'subject', 'subjectName', 'grade', 'gradeName',
                    'teacherID', 'teacherName', 'teacherJobID'];
                vm.teacherInfo = ['teacherID', 'teacherName', 'teacherJobID'];
                vm.subjectInfo = ['subject', 'subjectName'];
                ////选择排课条件
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
                        for (var fn in vm.filedName) {
                            vm.result.assign[vm.filedName[fn]] = ae[vm.filedName[fn]];//将资产对象的字段值付给排课对象
                        }
                        //将资产对象的字段值付给排课对象 名称不一样了
                        vm.result.assign["assignPrice"] = ae["price"];
                        vm.result.assign["campusID"] = ae["customerCampusID"];
                        vm.result.assign["campusName"] = ae["customerCampusName"];
                        //将排课条件对象的字段值付给排课对象
                        for (var fn in vm.filedNameAC) {
                            vm.result.assign[vm.filedNameAC[fn]] = ac[vm.filedNameAC[fn]];
                        }
                    }
                    else {
                        //新建
                        //重新初始化排课对象
                        vm.showOrderSelect = true; vm.showSubjectSelect = false; vm.showGradeSelect = false; vm.showTchSelect = false;
                        vm.resetAssignExtension();
                        vm.result.assign.conditionID = '-1';
                        vm.aeClone = mcs.util.clone(vm.result.assignExtension);
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

                ///选择订单编号，新建排课条件及排课
                vm.selectAssetClick = function (item) {
                    //获取资产对象
                    var ae = vm.getAssignExtension(item.key);
                    ///将资产对象赋值给排课对象
                    for (var fn in vm.filedName) {
                        vm.result.assign[vm.filedName[fn]] = ae[vm.filedName[fn]];
                    };
                    //将资产对象的字段值付给排课对象 名称不一样了
                    vm.result.assign["assignPrice"] = ae["price"];
                    vm.result.assign["campusID"] = ae["customerCampusID"];
                    vm.result.assign["campusName"] = ae["customerCampusName"];
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
                            vm.selectGradeClick(new { key: vm.result.assign.grade });
                        }
                    }
                };

                var curGrade = "";
                ///选择年级事件
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
                    for (var fn in vm.filedName) {
                        vm.result.assign[vm.filedName[fn]] = "";
                    };
                    vm.result.assign["assignPrice"] = "";
                    for (var fn in vm.filedNameAC) {
                        vm.result.assign[vm.filedNameAC[fn]] = "";
                    };
                };

                vm.calcDurationValueBHClick = function (item) {
                    vm.beginHour = item.value;
                    calcDurationValue();
                };
                vm.calcDurationValueBMClick = function (item) {
                    vm.beginMinute = item.value;
                    calcDurationValue();
                };
                vm.calcDurationValueEHClick = function (item) {
                    vm.endHour = item.value;
                    calcDurationValue();
                };
                vm.calcDurationValueEMClick = function (item) {
                    vm.endMinute = item.value;
                    calcDurationValue();
                };
                calcDurationValue = function () {
                    if (vm.beginHour == '' || vm.beginMinute == '' || vm.endHour == '' || vm.endMinute == ''||
                        vm.beginHour == null || vm.beginMinute == null || vm.endHour == null || vm.endMinute == null)
                        return;
                    if ((vm.beginHour > vm.endHour) || (vm.beginHour == vm.endHour && vm.beginMinute > vm.endMinute)) {
                        vm.showMsg("上课开始时间不能大于结束时间，请重新设置");
                        return;
                    }
                    if (vm.result.assign.lessonDurationValue == '' || vm.result.assign.lessonDurationValue == 0)
                        return;
                    vm.result.assign.durationValue = vm.result.assign.lessonDurationValue;
                    //实际上课时长     
                    vm.assignDuration = (parseInt(vm.endHour, 10) - parseInt(vm.beginHour, 10)) * 60 + (parseInt(vm.endMinute, 10) - parseInt(vm.beginMinute, 10));
         
                    if (vm.result.assign.lessonDurationValue != '' && vm.result.assign.lessonDurationValue != 0) {

                        vm.result.assign.realAmount = (vm.assignDuration / 60).toFixed(2);

                        var realValue = vm.assignDuration / vm.result.assign.lessonDurationValue;

                        var minValue = Math.floor(realValue);
                        var midValue = minValue + 0.5;
                      
                        if (realValue >= midValue)
                            vm.result.assign.amount = midValue;
                        else
                            vm.result.assign.amount = minValue;
                    }
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

                    studentassignmentDataService.createAssignCondition(vm.result.assign, function (success) {
                        if (success != null) {
                            vm.result.assign.assignID = success.assignID;
                            $uibModalInstance.close(vm.result.assign);
                        }
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

             

            }])
        });