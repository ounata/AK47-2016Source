define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (schedule) {
            schedule.registerController('asgmtConditionEditController', [
                '$scope', '$state', 'dataSyncService', 'studentassignmentDataService', '$stateParams', 'mcsDialogService',
                function ($scope, $state, dataSyncService, studentassignmentDataService, $stateParams, mcsDialogService) {
                    var vm = this;
                    vm.accID = $stateParams.accid;
                    vm.customerID = $stateParams.cid;

                    vm.showSubjectSelect = false; vm.showTchSelect = false;

                    vm.filedNameAC = ['assetID', 'assetCode', 'subject', 'subjectName', 'grade', 'gradeName', 'productID', 'productCode', 'productName',
                        'courseLevel', 'courseLevelName','lessonDuration', 'lessonDurationValue'];

                    vm.teacherInfo = ['teacherID', 'teacherName', 'teacherJobID', 'teacherJobOrgID', 'teacherJobOrgName'];

                    //'conditionID', customerID'  'conditionName4Customer', 'ConditionName4Teacher'

                    /*页面初始化加载或重新搜索时查询*/
                    vm.init = function () {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.customerID = vm.customerID;
                        vm.criteria.accID = vm.accID;

                        vm.result = vm.result || {};
                        studentassignmentDataService.initEditACC(vm.criteria, function (result) {
                            vm.result = result;
                            dataSyncService.injectDictData(mcs.util.mapping(vm.result.avc, { key: 'assetID', value: 'assetName' }, 'Asset'));
                            $scope.$broadcast('dictionaryReady');

                            //获取资产对象
                            var ae = vm.getAssignExtension(vm.result.acc.assetID);
                            //如果资产的科目为空
                            if (ae.subject == '' || ae.subject == null) {
                                vm.showSubjectSelect = true;
                                for (var index in vm.result.teacher.gradeSubjectRela) {
                                    var cSubject = vm.result.teacher.gradeSubjectRela[index];
                                    if (vm.result.acc.grade == index) {
                                        dataSyncService.injectDictData(mcs.util.mapping(cSubject, { key: 'key', value: 'value' }, 'SubSubject'));
                                        $scope.$broadcast('dictionaryReady');
                                        break;
                                    };
                                };
                            }
                            else {
                                ////设置待选教师
                                vm.showTchSelect = true;
                                for (var index in vm.result.teacher.tch) {
                                    var gs = vm.result.teacher.tch[index];
                                    if (vm.result.acc.grade == gs.grade && vm.result.acc.subject == gs.subject) {
                                        dataSyncService.injectDictData(mcs.util.mapping(gs.teachers, { key: 'key', value: 'value' }, 'Teacher'));
                                        $scope.$broadcast('dictionaryReady');
                                        break;
                                    }
                                };
                            }
                         
                        });
                    };

                    vm.init();

                    vm.teacherInfo = ['teacherID', 'teacherName', 'teacherJobID'];

                    /*选择订单编号，新建排课条件及排课*/
                    vm.selectAssetClick = function (item) {
                        //获取资产对象
                        var ae = vm.getAssignExtension(item.key);
                        //如果资产的科目为空
                        if (ae.subject == '' || ae.subject == null) {
                            vm.showSubjectSelect = true;
                            vm.showTchSelect = false;
                            for (var index in vm.result.teacher.gradeSubjectRela) {
                                var cSubject = vm.result.teacher.gradeSubjectRela[index];
                                if (vm.result.acc.grade == index) {
                                    dataSyncService.injectDictData(mcs.util.mapping(cSubject, { key: 'key', value: 'value' }, 'SubSubject'));
                                    $scope.$broadcast('dictionaryReady');
                                    break;
                                };
                            };
                        }
                        else {
                            ////设置待选教师
                            vm.showTchSelect = true;
                            for (var index in vm.result.teacher.tch) {
                                var gs = vm.result.teacher.tch[index];
                                if (vm.result.acc.grade == gs.grade && vm.result.acc.subject == gs.subject) {
                                    dataSyncService.injectDictData(mcs.util.mapping(gs.teachers, { key: 'key', value: 'value' }, 'Teacher'));
                                    $scope.$broadcast('dictionaryReady');
                                    break;
                                }
                            };
                        }

                        ///将资产对象赋值给排课条件对象
                        for (var fn in vm.filedNameAC) {
                            vm.result.acc[vm.filedNameAC[fn]] = ae[vm.filedNameAC[fn]];
                        };

                        for (var fn in vm.teacherInfo) {
                            vm.result.acc[vm.teacherInfo[fn]] = '';
                        };

                    };

                    /*选择科目事件*/
                    vm.selectSubjectClick = function (item) {
                        ///设置排课对象科目的值
                        vm.result.acc.subject = item.key;
                        vm.result.acc.subjectName = item.value;
                        ////设置待选教师
                        vm.showTchSelect = true;
                        for (var index in vm.result.teacher.tch) {
                            var gs = vm.result.teacher.tch[index];
                            if (vm.result.acc.grade == gs.grade && item.key == gs.subject) {
                                dataSyncService.injectDictData(mcs.util.mapping(gs.teachers, { key: 'key', value: 'value' }, 'Teacher'));
                                $scope.$broadcast('dictionaryReady');
                                break;
                            }
                        };
                        //科目发生改变，清空排课对象教师信息
                        for (var fn in vm.teacherInfo) {
                            vm.result.acc[vm.teacherInfo[fn]] = '';
                        };
                    };

                    /*选择教师事件*/
                    vm.selectTeacherClick = function (item) {
                        var teacherID = item.key;
                        for (var index in vm.result.teacher.tch) {
                            var gs = vm.result.teacher.tch[index];
                            for (var index2 in gs.teachers) {
                                if (gs.teachers[index2].key == item.key) {
                                    vm.result.acc.teacherID = gs.teachers[index2].key;
                                    vm.result.acc.teacherName = gs.teachers[index2].value;
                                    vm.result.acc.teacherJobID = gs.teachers[index2].field01;
                                    vm.result.acc.teacherJobOrgID = gs.teachers[index2].teacherJobOrgID;
                                    vm.result.acc.teacherJobOrgName = gs.teachers[index2].teacherJobOrgName;
                                    break;
                                }
                            }
                        }
                    };

                    /*获取资产对象*/
                    vm.getAssignExtension = function (assetID) {
                        var result = null;
                        for (var i in vm.result.avc) {
                            if (vm.result.avc[i].assetID == assetID) {
                                result = vm.result.avc[i];
                                break;
                            };
                        };
                        return result;
                    };

                    vm.cancel = function () {
                        $state.go('ppts.student-view.stuAsgmtConditionEdit', { id: vm.customerID });
                    };

                    /*保存排课条件*/
                    vm.save = function () {
                        if (vm.result.acc.teacherID == '')
                        {
                            vm.showMsg("请设置教师");
                            return false;
                        }
                        studentassignmentDataService.SaveAssignConditon(vm.result.acc, function (success) {
                            if (success.msg != 'ok') {
                                vm.showMsg(success.msg);
                                return;
                            }
                            $uibModalInstance.close();
                        }, function (error) {
                            vm.showMsg(error.data.description);
                        });
                    };

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                }]);
        });