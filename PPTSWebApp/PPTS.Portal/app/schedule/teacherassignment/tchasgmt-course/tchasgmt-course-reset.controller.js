define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.teacherAssignmentDataService], function (schedule) {
            schedule.registerController("tchAsgmtCourseResetController", [
               "$scope", "data", '$uibModalInstance', 'teacherAssignmentDataService', 'mcsDialogService',
                function ($scope, data, $uibModalInstance, teacherAssignmentDataService, mcsDialogService) {
                    var vm = this;
                    vm.resetModel = data.para;
                    vm.result = vm.result || {};
                    vm.init = function () {
                        teacherAssignmentDataService.initResetAssign(function (retValue) {
                            vm.result = retValue;
                        });
                    };
                    vm.init();

                    vm.save = function () {
                        if (!vm.validate())
                            return;
                        teacherAssignmentDataService.resetAssign(vm.resetModel, function (result) {
                            $uibModalInstance.close('ok');
                        });

                    };

                    vm.validate = function () {
                        var flag = true;
                        for (var i in vm.resetModel) {
                            var mm = vm.resetModel[i];
                            if (mm.reDate == '' || mm.reDate == null) {
                                flag = false;
                                vm.showMsg('有调整日期未设置');
                                break;
                            };
                            var resetStartDateTime = new Date(mm.reDate);
                            if (vm.result.allowDateTime <= resetStartDateTime) {
                                flag = false;
                                vm.showMsg('从当日算起，调整日期不能超过：' + vm.result.allowDateTime.getFullYear()
                                    + "-" + vm.getDoubleStr(vm.result.allowDateTime.getMonth() + 1)
                                    + "-" + vm.getDoubleStr(vm.result.allowDateTime.getDate()));
                                break;
                            };
                        };
                        return flag;
                    };

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
                        $uibModalInstance.dismiss('canceled');
                    };

                }]);
        });