define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherAssignmentDataService], function (schedule) {
            schedule.registerController("tchAsgmtCourseCopyController", [
              "$scope", '$uibModalInstance', 'teacherAssignmentDataService', 'mcsDialogService', 'dataSyncService', '$stateParams',
                function ($scope, $uibModalInstance, teacherAssignmentDataService, mcsDialogService, dataSyncService, $stateParams) {

                    var vm = this, t = new Date();
                    var initDate = new Date(t.getFullYear(), t.getMonth(), t.getDate(), 0, 0, 0, 0);
                    vm.aCM = {
                        teacherID: '',
                        teacherJobID:'',
                        srcDateStart: initDate,
                        srcDateEnd: initDate,
                        destDateStart: initDate,
                        destDateEnd: initDate
                    };
                    vm.watchLogOff = null;
                    vm.aCM.teacherID = $stateParams.cID;
                    vm.aCM.teacherJobID = $stateParams.tji;

                    dataSyncService.injectDictData({
                        c_codE_ABBR_copyCourseType: [{ key: 0, value: '复制单日课表' }, { key: 1, value: '复制多日课表' }]
                    });
                    vm.copyType = 0;

                    vm.save = function () {
                        if (!vm.validate())
                            return;
                        var tACM = {};
                        tACM.teacherID = vm.aCM.teacherID;
                        tACM.teacherJobID = vm.aCM.teacherJobID;
                        if (vm.copyType == 0) {
                            tACM.srcDateStart = vm.aCM.srcDateStart;
                            tACM.srcDateEnd = vm.aCM.srcDateStart;
                            tACM.destDateStart = vm.aCM.srcDateEnd;
                            tACM.destDateEnd = vm.aCM.srcDateEnd;
                        }
                        else {
                            tACM.srcDateStart = vm.aCM.srcDateStart;
                            tACM.srcDateEnd = vm.aCM.srcDateEnd;
                            tACM.destDateStart = vm.aCM.destDateStart;
                            tACM.destDateEnd = vm.aCM.destDateEnd;
                        }
                        teacherAssignmentDataService.copyAssign(tACM, function (data) {
                            $uibModalInstance.close('ok');
                            if (vm.watchLogOff != null) {
                                vm.watchLogOff();
                            }
                        },
                        function (error) {

                        });
                    };

                    vm.validate = function () {
                        /*复制单日课表*/
                        if (vm.copyType == 0) {
                            if (!vm.aCM.srcDateStart) {
                                vm.showMsg('请设置来源日期');
                                return false;
                            }
                            if (!vm.aCM.srcDateEnd) {
                                vm.showMsg('请设置目标日期');
                                return false;
                            }
                            if (vm.aCM.srcDateEnd <= vm.aCM.srcDateStart) {
                                vm.showMsg('目标日期必须大于来源日期');
                                return false;
                            }
                            var tempdate = new Date();
                            tempdate.setDate(tempdate.getDate() + 30);
                            if (vm.aCM.srcDateEnd >= tempdate) {
                                vm.showMsg('目标日期不能设置为超过一个月的日期');
                                return false;
                            }
                        }
                        else {
                            if (!vm.aCM.srcDateStart) {
                                vm.showMsg('请设置来源日期的开始日期');
                                return false;
                            }
                            if (!vm.aCM.srcDateEnd) {
                                vm.showMsg('请设置来源日期的结束日期');
                                return false;
                            }
                            if (!vm.aCM.destDateStart) {
                                vm.showMsg('请设置目标日期的开始日期');
                                return false;
                            }
                            if (vm.aCM.srcDateEnd <= vm.aCM.srcDateStart) {
                                vm.showMsg('来源日期的开始日期必须大于来源日期的结束日期');
                                return false;
                            }
                            if (vm.aCM.destDateStart <= vm.aCM.srcDateEnd) {
                                vm.showMsg('目标日期的开始日期必须大于来源日期的结束日期');
                                return false;
                            }
                        }
                        return true;
                    };

                    vm.watchLogOff = $scope.$watchCollection('vm.aCM', function (newValue, oldValue) {
                        if (vm.copyType == 0)
                            return;
                        if (!newValue.srcDateStart || !newValue.srcDateEnd || !newValue.destDateStart || newValue.srcDateEnd <= newValue.srcDateStart || newValue.destDateStart <= newValue.srcDateEnd) {
                            return;
                        }
                        var millTime = newValue.srcDateEnd.getTime() - newValue.srcDateStart.getTime();
                        var temp = new Date(newValue.destDateStart.getFullYear(), newValue.destDateStart.getMonth(), newValue.destDateStart.getDate())
                        temp.setTime(temp.getTime() + millTime);
                        vm.aCM.destDateEnd = temp;
                    });


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