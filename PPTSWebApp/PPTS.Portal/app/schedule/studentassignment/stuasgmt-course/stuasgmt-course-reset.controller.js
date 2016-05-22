define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService], function (schedule) {
            schedule.registerController("stuAsgmtCourseResetController", [
                "$scope", "data", '$uibModalInstance', 'studentassignmentDataService', 'mcsDialogService',
                function ($scope, data, $uibModalInstance, studentassignmentDataService, mcsDialogService) {
                    var vm = this;
                    vm.resetModel = data.para;
                    vm.result = vm.result || {};
                    vm.init = function () {
                        studentassignmentDataService.resetAssignInit(function (retValue) {
                            vm.result = retValue;
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    vm.save = function () {
                        if (!vm.validate())
                            return;
                        studentassignmentDataService.resetAssign(vm.resetModel, function (result) {
                            $uibModalInstance.close('ok');
                        });

                    };

                    vm.validate = function () {
                        var flag = true;
                        for (var i in vm.resetModel) {
                            var mm = vm.resetModel[i];
                            if (mm.reDate == '' || mm.reHour == '' || mm.reMinute == '') {
                                flag = false;
                                vm.showMsg('有调整日期未设置');
                                break;
                            };
                            var resetStartDateTime = new Date(mm.reDate.getFullYear(), mm.reDate.getMonth(), mm.reDate.getDate(), mm.reHour, mm.reMinute, 0);
                            if (vm.result.allowDateTime <= resetStartDateTime) {
                                flag = false;
                                vm.showMsg('从当日算起，调整日期不能超过10日！');
                                break;
                            };
                        };
                        return flag;
                    };

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('canceled');
                    };

                }]);
        });