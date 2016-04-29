define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService], function (schedule) {
            schedule.registerController('stuAsgmtCourseCopyController', ["$scope",'$uibModalInstance', 'studentassignmentDataService', 'mcsDialogService', 'dataSyncService',
                function ($scope,$uibModalInstance,studentassignmentDataService, mcsDialogService, dataSyncService) {
                    var vm = this;

                    dataSyncService.injectDictData({
                        c_codE_ABBR_copyCourseType: [{ key: 0, value: '复制单日课表' }, { key: 1, value: '复制多日课表' }]
                    });

                    vm.copyType = 1;


                    vm.save = function () {
                      alert("save");
                    };

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('canceled');
                    };
            }]);
        });