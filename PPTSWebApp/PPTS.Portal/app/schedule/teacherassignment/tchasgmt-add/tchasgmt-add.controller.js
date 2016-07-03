define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherAssignmentDataService], function (schedule) {
            schedule.registerController("tchAsgmtAddController", [
               '$scope', '$uibModalInstance', '$state', '$stateParams', '$filter', 'dataSyncService', 'teacherAssignmentDataService', 'mcsDialogService',
                function ($scope, $uibModalInstance, $state, $stateParams, $filter, dataSyncService, teacherAssignmentDataService, mcsDialogService) {
                    var vm = this;
                    /*存储时间*/
                    vm.assignDuration = 0;
                    vm.dateClass = { beginDate: '', endDate: '' };

                    vm.showStudentSelect = false; vm.showOrderSelect = false; vm.showGradeSelect = false; vm.showSubjectSelect = false;

                    vm.CID = $stateParams.cID;
                    vm.tji = $stateParams.tji;

                    /*页面初始化加载或重新搜索时查询*/
                    vm.init = function () {
                        vm.criteria = vm.criteria || {};
                        vm.criteria.teacherID = $stateParams.cID;
                        vm.criteria.TeacherJobID = $stateParams.tji;
                        vm.result = vm.result || {};
                        teacherAssignmentDataService.initCreateAssign(vm.criteria, function (result) {
                            if (result.msg != 'ok') {
                                vm.showMsg(result.msg);
                                return;
                            }
                            vm.result = result;
                            dataSyncService.injectDictData(mcs.util.mapping(vm.result.assignCondition, { key: 'conditionID', value: 'conditionName4Teacher' }, 'AssignCondition'));
                            dataSyncService.injectDictData(mcs.util.mapping(vm.result.student.student, { key: 'key', value: 'value' }, 'Student'));
                            dataSyncService.injectDictData(mcs.util.mapping(vm.result.student.grade, { key: 'key', value: 'value' }, 'SubGrade'));
                            dataSyncService.injectDynamicDict('ifElse');
                            $scope.$broadcast('dictionaryReady');

                            vm.selectAssignConditionClick({key:""});
                        });
                    };

                    vm.init();

                    vm.shareFieldName = ['assetID', 'assetCode', 'customerID', 'customerCode', 'customerName', 'productID', 'productCode', 'productName', 'accountID'
                   , 'grade', 'gradeName', 'subject', 'subjectName', 'categoryType', 'categoryTypeName'];

                    vm.initAssignFieldFromAsset = ['assetID', 'assetCode', 'customerID', 'customerCode', 'customerName', 'productID', 'productCode', 'productName', 'accountID'
                , 'grade', 'gradeName', 'subject', 'subjectName', 'courseLevel', 'courseLevelName', 'lessonDuration', 'lessonDurationValue', 'categoryType', 'categoryTypeName'];

                    vm.fieldToAssign = [];

                    vm.fieldToAC = ['conditionID', 'conditionName4Customer', 'conditionName4Teacher', 'courseLevel', 'courseLevelName', 'lessonDuration'
                        , 'lessonDurationValue', 'categoryType', 'categoryTypeName'];

                    /*选择排课条件*/
                    vm.selectAssignConditionClick = function (item) {
                        //不等空，选择了一个已经存在的排课条件
                        if (item.key != "") {
                            ///重新设置传回结果对象值
                            vm.resetAssignExtension();
                            //资产选择及科目选择都已经确定，所以选项隐藏
                            vm.showStudentSelect = false; vm.showOrderSelect = false; vm.showGradeSelect = false; vm.showSubjectSelect = false;
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
                                }
                            }, function (error) {
                            });
                        }
                        else {
                            //新建 教师对象的学员下拉框要显示出来，资产清空
                            vm.showStudentSelect = true;
                            vm.showOrderSelect = false;
                            vm.showGradeSelect = false;
                            vm.showSubjectSelect = false;
                            //重新初始化排课对象
                            vm.resetAssignExtension();
                        }
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

                    /*选择年级事件*/
                    var curGrade = "";
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
                           vm.result.assign.subject = '';
                          vm.result.assign.subjectName = '';
                        }
                    };

                    /*选择科目*/
                    vm.selectSubjectClick = function (item) {
                        vm.result.assign['subject'] = item.key;
                        vm.result.assign['subjectName'] = item.value;
                    };


                    vm.watchLogOff = $scope.$watchCollection('vm.dateClass', function (newValue, oldValue) {
                        if (vm.dateClass.beginDate == '' || vm.dateClass.beginDate == null || vm.dateClass.endDate == '' || vm.dateClass.endDate == null)
                            return;
                        //实际上课时长     
                        vm.assignDuration = (new Date(vm.dateClass.endDate).getTime() - new Date(vm.dateClass.beginDate).getTime()) / 60000;

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


                    /*保存排课*/
                    vm.save = function () {
                        var flag = true;
                        var msg = "";
                        if (vm.result.assign.customerID == '' || vm.result.assign.customerID == null || vm.result.assign.customerID == '0') {
                            msg += "请选择学员姓名，如没有待选项，请联系管理员！<br>";
                            flag = false;
                        };
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

                        teacherAssignmentDataService.createAssign(vm.result.assign, function (success) {
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

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
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
                        vm.result.assign.conditionID = "";
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


                }]);
        });