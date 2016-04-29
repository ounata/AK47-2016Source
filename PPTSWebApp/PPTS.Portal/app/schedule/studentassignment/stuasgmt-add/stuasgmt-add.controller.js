define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (schedule) {
            schedule.registerController('stuAsgmtAddController', [
                '$scope', '$uibModalInstance', '$state', '$stateParams', '$filter', 'dataSyncService', 'studentassignmentDataService',
            function ($scope, $uibModalInstance, $state, $stateParams, $filter, dataSyncService, studentassignmentDataService) {
                    var vm = this;
                    //存储时间
                    vm.beginDate = ''; vm.beginHour = ''; vm.beginMinute = ''; vm.endHour = ''; vm.endMinute = '';
                    vm.showOrderSelect = true; vm.showSubjectSelect = true;
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.customerID = $stateParams.cID;
                        vm.CID = $stateParams.cID;

                        vm.result = vm.result || {};
                        //vm.selectAssignCondition = vm.selectAssignCondition || {};
                        //vm.selectAsset = vm.selectAsset || {};

                        studentassignmentDataService.getAssignCondition(vm.criteria, function (result) {
                            vm.result = result;
                            vm.aeClone = mcs.util.clone(vm.result.assignExtension);
                            dataSyncService.injectDictData(mcs.util.mapping(vm.result.assignCondition, { key: 'conditionID', value: 'conditionName' }, 'AssignCondition'));
                            dataSyncService.injectDictData(mcs.util.mapping(vm.aeClone, { key: 'assetID', value: 'assetName' }, 'Asset'));
                            dataSyncService.injectPageDict(['ifElse']);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };

                    vm.init();

                    vm.filedName = ['assetID', 'assetCode', 'grade', 'gradeName', 'courseLevel', 'courseLevelName', 'subject', 'subjectName'
                        , 'lessonDuration', 'lessonDurationValue', 'assetName', 'customerID', 'price', 'customerCode', 'customerName', 'consultantID', 'consultantJobID'
                    , 'consultantName', 'educatorID', 'educatorJobID', 'educatorName', 'productID', 'productCode', 'productName', 'productCampusID', 'productCampusName'];
                    vm.filedNameAC = ['conditionID', 'conditionName', 'subject', 'subjectName', 'teacherID', 'teacherName', 'teacherJobID'];
                    ////选择排课条件
                    vm.selectAssignConditionClick = function (item) {
                        //不等-1，选择了一个已经存在的排课条件
                        if (item.key != '-1') {
                            vm.showOrderSelect = false; vm.showSubjectSelect = false;
                            var ac = vm.getAssignCondition(item.key);
                            var ae = vm.getAssignExtension(ac.assetID);
                            for (var fn in vm.filedName) {
                                vm.result.assign[vm.filedName[fn]] = ae[vm.filedName[fn]];
                            }
                            for (var fn in vm.filedNameAC) {
                                vm.result.assign[vm.filedNameAC[fn]] = ac[vm.filedNameAC[fn]];
                            }
                        }
                        else { //新建
                            vm.showOrderSelect = true; vm.showSubjectSelect = true;
                            vm.resetAssignExtension();
                            vm.result.assign.assetID = '-1';
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
                        var ae = vm.getAssignExtension(item.key);
                        for (var fn in vm.filedName) {
                            vm.result.assign[vm.filedName[fn]] = ae[vm.filedName[fn]];
                        };
                        //如果科目不为空，则科目控件设置为不能操作状态，并且根据科目和学员ID获取对应的教师列表，教师可能不止一个
                        if (ae.subject != '') {
                            vm.selectSubjectClick({ key: vm.result.assign.subject, value: vm.result.assign.subjectName });
                            vm.showSubjectSelect = false;
                        };
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
                        for (var fn in vm.filedNameAC) {
                            vm.result.assign[vm.filedNameAC[fn]] = "";
                        };
                    };

                    vm.selectSubjectClick = function (item) {
                        //科目为空时，当选择科目时，根据选择的科目  当前学员ID   年级 过滤获取对应的教师列表
                        var teacherQueryModel = new Array();
                        subject = item.key;

                        grade = vm.result.assign.grade;
                        for (var i in vm.result.teacher) {
                            if (vm.result.teacher[i].subject == subject && vm.result.teacher[i].grade == grade) {
                                teacherQueryModel.push(vm.result.teacher[i]);
                            };
                        };
                        dataSyncService.injectDictData(mcs.util.mapping(teacherQueryModel, { key: 'teacherID', value: 'teacherName' }, 'Teacher'));
                        $scope.$broadcast('dictionaryReady');

                    };

                    vm.selectTeacherClick = function (item) {
                        var teacherID = item.key;
                        for (var i in vm.result.teacher) {
                            if (vm.result.teacher[i].teacherID == teacherID) {
                                vm.result.assign.teacherID = vm.result.teacher[i].teacherID;
                                vm.result.assign.teacherName = vm.result.teacher[i].teacherName;
                                vm.result.assign.teacherJobID = vm.result.teacher[i].teacherJobID;
                                break;
                            };
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
                        if (vm.beginHour == '' || vm.beginMinute == '' || vm.endHour == '' || vm.endMinute == '')
                            return;
                        if ((vm.beginHour > vm.endHour) || (vm.beginHour == vm.endHour && vm.beginMinute > vm.endMinute)) {
                            alert("上课开始时间不能大于结束时间，请重新设置");
                            return;
                        }
                        vm.result.assign.durationValue = (parseInt(vm.endHour, 10) - parseInt(vm.beginHour, 10)) * 60 + (parseInt(vm.endMinute, 10) - parseInt(vm.beginMinute, 10));
                        //实际上课时长                        
                        if (vm.result.assign.lessonDurationValue != '' && vm.result.assign.lessonDurationValue != 0) {
                            vm.result.assign.Amount = (vm.result.assign.durationValue / vm.result.assign.lessonDurationValue).toFixed(1);
                        }
                    };

                    vm.save = function () {                       
                        if (!vm.beginDate)
                        {
                            alert('请设置上课日期');
                            return false;
                        }
                        if (vm.beginHour == '' || vm.beginMinute == '' || vm.endHour == '' || vm.endMinute == '')
                        {
                            alert('请设置上课时间');
                            return false;
                        }
                        $uibModalInstance.close('ok');
                        var bdateobj = vm.beginDate.getFullYear() + '-' + (vm.beginDate.getMonth() + 1) + '-' + vm.beginDate.getDate() + ' ' + vm.beginHour + ':' + vm.beginMinute + ':00';
                        var edateobj = vm.beginDate.getFullYear() + '-' + (vm.beginDate.getMonth() + 1) + '-' + vm.beginDate.getDate() + ' ' + vm.endHour + ':' + vm.endMinute + ':00';
                        var bdate = new Date(bdateobj);
                        var edate = new Date(edateobj);
                        if (bdate >= edate) {
                            alert("上课结束时间不能小于开始时间，请重新设置");
                            return false;
                        };
                        vm.result.assign.startTime = bdate;
                        vm.result.assign.endTime = edate;
                        studentassignmentDataService.createAssignCondition(vm.result.assign, function (result) {
                            $state.go('ppts.schedule');
                               $uibModalInstance.close(true);
                        });
                    };

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('canceled');
                    };

                    vm.popup1 = { opened: false };
                    vm.open1 = function () { vm.popup1.opened = true; };

                }])
        });