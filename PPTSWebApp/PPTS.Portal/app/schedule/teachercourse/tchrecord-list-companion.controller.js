/*教师补录   陪读课时*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherCourseDataService,
        ppts.config.dataServiceConfig.teacherAssignmentDataService],
        function (schedule) {
            schedule.registerController('tchcrscompanionController', [
            '$scope', 'teacherCourseDataService', '$uibModalInstance', 'mcsDialogService', 'teacherAssignmentDataService', 'dataSyncService',
            function ($scope, teacherCourseDataService, $uibModalInstance, mcsDialogService, teacherAssignmentDataService, dataSyncService) {
                var vm = this;
                vm.apiBaseUrl = ppts.config.orderApiBaseUrl;
                vm.teachers = '';
                vm.tchList = [];
                vm.filter = function (retValue) {
                    vm.tchList = [];
                    retValue.result.forEach(function (item, index) {
                        vm.tchList.push({
                            teacherId: item.teacherID,
                            name: item.jobOrgName + '-' + item.teacherName + '(' + item.teacherOACode + ')',
                            jobID: item.jobID,
                            tchName: item.teacherName,
                            iSFullTimeTeacher:item.isFullTime,
                            teacherJobOrgName:item.jobOrgName,
                            teacherJobOrgID:item.jobOrgID
                        });
                    });
                    return vm.tchList;
                };

                /*存储时间*/
                vm.beginDate = ''; vm.amount = ''; vm.endDate = '';
                vm.assignDuration = 0;

                /*初始化课时数下拉框数据源*/
                vm.courseAmount = new Array()
                for (i = 0.5; i <= 6; i += 0.5) {
                    vm.courseAmount.push({ key: i, value: i })
                };

                /*初始化界面*/
                vm.init = function () {
                    teacherCourseDataService.initAccompanion(function (retValue) {
                        if (retValue.campusID == undefined) {
                            vm.showMsg("当前操作人没有归属校区，不能补录课时");
                            vm.cancel();
                        }
                        vm.currCampusID = retValue.campusID;
                        vm.currCampusName = retValue.campusName;
                        vm.accompanion = retValue.accompanion;
                        dataSyncService.injectDictData(mcs.util.mapping(vm.courseAmount, { key: 'key', value: 'value' }, 'CourseAmount'));
                        $scope.$broadcast('dictionaryReady');

                    }, function (error) { });
                };
                vm.init();


                /*计算结束时间*/
                vm.calcDurationValueCAClick = function (item) {
                    vm.amount = item.value;
                    calcDurationValue();
                };
                /*计算结束时间*/
                calcDurationValue = function () {
                    if (vm.beginDate == '' || vm.beginDate == null) {
                        return;
                    }
                    if (vm.amount == '' || vm.amount == 0 || vm.amount == null)
                        return;
                    vm.durationValue = 60;
                    //实际上课时长  分钟   
                    var courseMinute = vm.durationValue * vm.amount;
                    vm.endDate = new Date(vm.beginDate);

                    vm.endDate.setTime(vm.endDate.getTime() + courseMinute * 60 * 1000);
                };

                vm.watchbeginDate = $scope.$watch('vm.beginDate', function (newValue, oldValue) {
                    calcDurationValue();
                });

                vm.getDoubleStr = function (curValue) {
                    if (parseInt(curValue) < 10)
                        return '0' + curValue;
                    return curValue;
                };
                /*保存陪读记录*/
                vm.save = function () {
                    var flag = true;
                    var msg = "";
                    if (vm.teachers.length == 0) {
                        msg += "请通过智能提示功能，选择上课教师(在“上课教师”框中输入教师姓名的关键字)！<br>";
                        flag = false;
                    }
                    if (vm.beginDate == '' || vm.beginDate == null) {
                        msg += "请设置上课日期！<br>";
                        flag = false;
                    }
                    if (vm.amount == '' || vm.amount == 0 || vm.amount == null) {
                        msg += "请设置课时数！<br>";
                        flag = false;
                    }
                    if (flag == false) {
                        vm.showMsg(msg);
                        return false;
                    }
                    vm.accompanion.teacherID = vm.teachers[0].teacherId;
                    vm.accompanion.teacherName = vm.teachers[0].tchName;
                    vm.accompanion.teacherJobID = vm.teachers[0].jobID;
                    vm.accompanion.iSFullTimeTeacher = vm.teachers[0].iSFullTimeTeacher;
                    vm.accompanion.teacherJobOrgName = vm.teachers[0].teacherJobOrgName;
                    vm.accompanion.teacherJobOrgID = vm.teachers[0].teacherJobOrgID;
                  
                    vm.accompanion.amount = vm.amount;
                    vm.accompanion.startTime = new Date(vm.beginDate);
                    vm.accompanion.endTime = vm.endDate;

                    teacherCourseDataService.addAccompanion(vm.accompanion, function (data) {
                        if (data.msg != 'ok') {
                            vm.showMsg(data.msg);
                            return;
                        }
                        if (vm.watchLogOff != null) {
                            vm.watchLogOff();
                        }
                        if (vm.watchbeginDate != null) {
                            vm.watchbeginDate();
                        }
                        $uibModalInstance.close();
                    }, function (error) {
                        vm.showMsg(error.data.description);
                    });
                };

                /*取消*/
                vm.cancel = function () {
                    if (vm.watchLogOff != null) {
                        vm.watchLogOff();
                    }
                    if (vm.watchbeginDate != null) {
                        vm.watchbeginDate();
                    }
                    $uibModalInstance.dismiss('canceled');
                };

                vm.showMsg = function (msg) {
                    mcsDialogService.error({ title: '提示信息', message: msg });
                };

            }]);
        });