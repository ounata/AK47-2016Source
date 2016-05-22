define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentCourseDataService], function (schedule) {
            schedule.registerController("stuCourseConfirmController", [
                "$scope", "data", '$uibModalInstance', 'studentCourseDataService', 'mcsDialogService',
                function ($scope, data, $uibModalInstance, studentCourseDataService, mcsDialogService) {

                    var vm = this;

                    vm.weekText = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");

                    vm.data = {
                        headers: [
                            {
                                field: "teacherName",
                                name: "教师姓名",
                            },
                            {
                                field: "customerName",
                                name: "学生姓名",
                            },
                            {
                                field: "subjectName",
                                name: "上课科目",
                            },
                            {
                                field: "startTime",
                                name: "上课日期",
                                template: '<span>{{ vm.getCourseDate(row.startTime) }}</span>'
                            },
                            {
                                field: "courseHour",
                                name: "上课时段",
                                template: '<span>{{  vm.getCourseHour(row.startTime,row.endTime) }}</span>'
                            },
                            {
                                field: "amount",
                                name: "课时",
                            }
                        ],
                        rows: data.para
                    }


                    vm.getCourseHour = function (sDate, eDate) {
                        var startDate = new Date(sDate);
                        var endDate = new Date(eDate);

                        return vm.getDoubleStr(startDate.getHours()) + ':' + vm.getDoubleStr(startDate.getMinutes()) + ' - ' +
                           vm.getDoubleStr(endDate.getHours()) + ':' + vm.getDoubleStr(endDate.getMinutes());
                    };

                    vm.getCourseDate = function (sDate) {
                        var startDate = new Date(sDate);
                        return startDate.getFullYear() + '-' + vm.getDoubleStr((startDate.getMonth() + 1)) + '-' + vm.getDoubleStr(startDate.getDate())
                         + '(' + vm.weekText[startDate.getDay()] + ')';
                    };

                    vm.getDoubleStr = function (curValue) {
                        if (parseInt(curValue) < 10)
                            return '0' + curValue;
                        return curValue;
                    };


                    vm.save = function () {
                        studentCourseDataService.confirmAssign(data.para, function (result) {
                            $uibModalInstance.close('ok');
                        });

                    };

                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('canceled');
                    };

                }]);
        });