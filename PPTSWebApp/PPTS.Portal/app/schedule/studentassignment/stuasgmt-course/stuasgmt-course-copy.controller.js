define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService], function (schedule) {
            schedule.registerController('stuAsgmtCourseCopyController', [
                "$scope", '$uibModalInstance', 'studentassignmentDataService', 'mcsDialogService', 'dataSyncService', '$stateParams',
                function ($scope, $uibModalInstance, studentassignmentDataService, mcsDialogService, dataSyncService, $stateParams) {
                    var vm = this;
                    vm.aCM = {
                        customerID: '',
                        srcDateStart: '',
                        srcDateEnd: '',
                        destDateStart: '',
                        destDateEnd: ''
                    };
                    vm.watchLogOff = null;
                    vm.aCM.customerID = $stateParams.id;

                    dataSyncService.injectDictData({
                        c_codE_ABBR_copyCourseType: [{ key: 0, value: '复制单日课表' }, { key: 1, value: '复制多日课表' }]
                    });
                    vm.copyType = 0;

                    vm.save = function () {
                        if (!vm.validate())
                            return;
                        var tACM = {};
                        tACM.customerID = vm.aCM.customerID;
                        if (vm.copyType == 0) {
                            tACM.srcDateStart = new Date(vm.aCM.srcDateStart);
                            tACM.srcDateEnd =  new Date(vm.aCM.srcDateStart);
                            tACM.destDateStart =  new Date(vm.aCM.srcDateEnd);
                            tACM.destDateEnd =  new Date(vm.aCM.srcDateEnd);
                        }
                        else {
                            tACM.srcDateStart =  new Date(vm.aCM.srcDateStart);
                            tACM.srcDateEnd =  new Date(vm.aCM.srcDateEnd);
                            tACM.destDateStart =  new Date(vm.aCM.destDateStart);
                            tACM.destDateEnd = new Date(vm.aCM.destDateEnd);
                        }
                        studentassignmentDataService.copyAssign(tACM, function (data) {
                            if (data.msg != 'ok') {
                                vm.showMsg(data.msg);
                            }
                            if (vm.watchLogOff != null) {
                                vm.watchLogOff();
                            }
                            $uibModalInstance.close('ok');
                        },
                        function (error) {

                        });
                    };

                    vm.validate = function () {
                        /*复制单日课表*/
                        var currentDate = new Date();
                        var maxDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), 0, 0, 0);
                        maxDate.setDate(maxDate.getDate() + 30);
                        var temp;
                        var msg = "";
                        var flag = true;
                        if (vm.copyType == 0) {
                            if (!vm.aCM.srcDateStart) {
                                msg += '请设置来源日期<br>';
                                flag = false;
                            }
                            if (!vm.aCM.srcDateEnd) {
                                msg += '请设置目标日期<br>';
                                flag = false;
                            }
                            temp = new Date(vm.aCM.srcDateEnd);
                            var srcDateEndSL = new Date(temp.getFullYear(), temp.getMonth(), temp.getDate(), 0, 0, 0);
                            if (srcDateEndSL >= maxDate) {
                                msg += '目标日期不能超过：' + maxDate.getFullYear() + "-" + vm.getDoubleStr(maxDate.getMonth() + 1) + "-" + vm.getDoubleStr(maxDate.getDate()) + '<br>';
                                flag = false;
                            }
                        }
                        else {
                            if (!vm.aCM.srcDateStart) {
                                msg += '请设置来源日期的开始日期<br>';
                                flag = false;
                            }
                            if (!vm.aCM.srcDateEnd) {
                                msg += '请设置来源日期的结束日期<br>';
                                flag = false;
                            }
                            if (!vm.aCM.destDateStart) {
                                msg += '请设置目标日期的开始日期<br>';
                                flag = false;
                            }
                            temp = new Date(vm.aCM.srcDateStart);
                            var srcDateStart = new Date(temp.getFullYear(), temp.getMonth(), temp.getDate(), 0, 0, 0);
                            temp = new Date(vm.aCM.srcDateEnd);
                            var srcDateEnd = new Date(temp.getFullYear(), temp.getMonth(), temp.getDate(), 0, 0, 0);
                            temp = new Date(vm.aCM.destDateStart);
                            var destDateStart = new Date(temp.getFullYear(), temp.getMonth(), temp.getDate(), 0, 0, 0);

                            if (srcDateEnd <= srcDateStart) {
                                msg += '来源日期的结束日期必须大于来源日期的开始日期<br>';
                                flag = false;
                            }
                            if (destDateStart <= srcDateEnd) {
                                msg += '目标日期的开始日期必须大于来源日期的结束日期<br>';
                                flag = false;
                            }
                            var destDateEnd = new Date(vm.aCM.destDateEnd);
                            if (destDateEnd >= maxDate) {
                                msg += '目标日期的结束日期不能超过：' + maxDate.getFullYear() + "-" + vm.getDoubleStr(maxDate.getMonth() + 1) + "-" + vm.getDoubleStr(maxDate.getDate()) + '<br>';
                                flag = false;
                            }
                        }
                        if (flag == false)
                            vm.showMsg(msg);
                        return flag;
                    };

                    vm.watchLogOff = $scope.$watchCollection('vm.aCM', function (newValue, oldValue) {
                        if (vm.copyType == 0)
                            return;
                        if (newValue.srcDateStart == '' || newValue.srcDateStart == null
                            || newValue.srcDateEnd == '' || newValue.srcDateEnd == null
                            || newValue.destDateStart == '' || newValue.destDateStart == null) {
                            return;
                        }
                        var srcDateStart = new Date(newValue.srcDateStart);
                        var srcDateEnd = new Date(newValue.srcDateEnd);
                        var destDateStart = new Date(newValue.destDateStart);

                        if (srcDateEnd <= srcDateStart
                           || destDateStart <= srcDateEnd) {
                            return;
                        }
                        var millTime = srcDateEnd.getTime() - srcDateStart.getTime();
                        var temp = new Date(destDateStart.getFullYear(), destDateStart.getMonth(), destDateStart.getDate())
                        temp.setTime(temp.getTime() + millTime);
                        vm.aCM.destDateEnd = temp.getFullYear() + "-" + vm.getDoubleStr((temp.getMonth() + 1)) + "-" + vm.getDoubleStr(temp.getDate());
                    });

                    /*格式化数字*/
                    vm.getDoubleStr = function (curValue) {
                        if (parseInt(curValue) < 10)
                            return '0' + curValue;
                        return curValue;
                    };

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                    vm.cancel = function () {
                        if (vm.watchLogOff != null) {
                            vm.watchLogOff();
                        }
                        $uibModalInstance.dismiss('canceled');
                    };
                }]);
        });