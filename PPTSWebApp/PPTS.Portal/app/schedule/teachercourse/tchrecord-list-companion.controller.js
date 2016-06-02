/*教师补录   陪读课时*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherCourseDataService,
        ppts.config.dataServiceConfig.teacherAssignmentDataService],
        function (schedule) {
            schedule.registerController('tchcrscompanionController', [
            '$scope', 'teacherCourseDataService', '$uibModalInstance', 'mcsDialogService', 'teacherAssignmentDataService', 'dataSyncService',
            function ($scope, teacherCourseDataService, $uibModalInstance, mcsDialogService, teacherAssignmentDataService, dataSyncService) {
                var vm = this;
                vm.teachers = '';

                /*存储时间*/
                vm.beginDate = ''; vm.beginHour = ''; vm.beginMinute = ''; vm.endHour = ''; vm.endMinute = '', vm.amount = '';
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
                vm.calcDurationValueBHClick = function (item) {
                    vm.beginHour = item.value;
                    calcDurationValue();
                };
                /*计算结束时间*/
                vm.calcDurationValueBMClick = function (item) {
                    vm.beginMinute = item.value;
                    calcDurationValue();
                };
                /*计算结束时间*/
                vm.calcDurationValueCAClick = function (item) {
                    vm.amount = item.value;
                    calcDurationValue();
                };
                /*计算结束时间*/
                calcDurationValue = function () {
                    if (vm.beginHour == '' || vm.beginMinute == '' || vm.beginHour == null || vm.beginMinute == null)
                        return;

                    if (vm.amount == '' || vm.amount == 0 || vm.amount == null)
                        return;

                    vm.durationValue = 60;

                    ///计算结束时间
                    vm.endHour = '', vm.endMinute = '';

                    //实际上课时长  分钟   
                    var courseMinute = vm.durationValue * vm.amount;
                    var curDate = new Date();
                    var curDateHour = new Date(curDate.getFullYear(), curDate.getMonth(), curDate.getDate(), vm.beginHour, vm.beginMinute, 0);

                    curDate.setTime(curDateHour.getTime() + courseMinute * 60 * 1000);

                    vm.endHour = vm.getDoubleStr(curDate.getHours());
                    vm.endMinute = vm.getDoubleStr(curDate.getMinutes());
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
                                jobID: item.jobID,
                                tchName: item.teacherName
                            });
                        });
                        return vm.tchList;
                    }
                     , function (error) {
                     });
                };

                vm.getDoubleStr = function (curValue) {
                    if (parseInt(curValue) < 10)
                        return '0' + curValue;
                    return curValue;
                };
                /*保存陪读记录*/
                vm.save = function () {
                    if (vm.teachers.length == 0)
                    {
                        vm.showMsg('请设置上课教师');
                        return false;
                    }

                    vm.accompanion.teacherID = vm.teachers[0].teacherId;
                    vm.accompanion.teacherName = vm.teachers[0].tchName;
                    vm.accompanion.teacherJobID = vm.teachers[0].jobID;

                    if (!vm.beginDate) {
                        vm.showMsg('请设置上课日期');
                        return false;
                    }

                    if (vm.beginHour == '' || vm.beginMinute == '' || vm.beginHour == null || vm.beginMinute == null ) {
                        vm.showMsg('请设置上课时间');
                        return false;
                    }

                    if (vm.amount == '' || vm.amount == 0 || vm.amount == null) {
                        vm.showMsg('请设置课时数');
                        return false;
                    }

                    var bdate = new Date(vm.beginDate.getFullYear(), vm.beginDate.getMonth(), vm.beginDate.getDate(), vm.beginHour, vm.beginMinute, 0);
                    var edate = new Date(vm.beginDate.getFullYear(), vm.beginDate.getMonth(), vm.beginDate.getDate(), vm.endHour, vm.endMinute, 0);
                    if (bdate >= edate) {
                        vm.showMsg("上课结束时间不能小于开始时间，请重新设置");
                        return false;
                    };

                    vm.accompanion.amount = vm.amount;
                    vm.accompanion.startTime = bdate;
                    vm.accompanion.endTime = edate;

                    teacherCourseDataService.addAccompanion(vm.accompanion, function (success) {
                        if (success.msg != 'ok') {
                            vm.showMsg(success.msg);
                            return;
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
                    $uibModalInstance.dismiss('canceled');
                };

                vm.showMsg = function (msg) {
                    mcsDialogService.error({ title: '提示信息', message: msg });
                };

            }]);
        });