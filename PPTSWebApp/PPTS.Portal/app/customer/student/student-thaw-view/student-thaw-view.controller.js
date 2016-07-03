define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.studentDataService],
        function (customer) {
            customer.registerController('studentThawViewController', [
                '$scope', '$stateParams', 'dataSyncService', 'Upload', 'studentDataViewService', 'studentDataService', 'mcsWorkflowService', 'mcsDialogService', 'thawData',
                function ($scope, $stateParams, dataSyncService, Upload, studentDataViewService, studentDataService, mcsWorkflowService, mcsDialogService, thawData) {
                    var vm = this;

                    // 页面初始化加载
                    (function () {
                        vm.searchParams = {
                            processID: $stateParams.processID,
                            activityID: $stateParams.activityID,
                            resourceID: $stateParams.resourceID
                        };
                        thawData.rows = [];
                        studentDataService.ShowStudentThaw(vm.searchParams, function (result) {
                            vm.thaw = result.thaw;
                            for (var index in vm.thaw.files) {
                                vm.downLoadObject = vm.thaw.files[index];
                                thawData.rows.push({
                                    fileName: vm.thaw.files[index].title,
                                    headerCss: 'datatable-header',
                                    downLoad: function () { vm.downloadFile(vm.downLoadObject) }
                                });
                            }

                            vm.attachmentData = thawData;
                            vm.clientProcess = result.clientProcess;
                            dataSyncService.injectDynamicDict('thawReasonType');
                            $scope.$broadcast('dictionaryReady');
                        });

                    })();

                    vm.downloadFile = function (file) {
                        mcs.util.postMockForm(ppts.config.customerApiBaseUrl + 'api/students/DownloadMaterial', file);
                    };
                }]);
        });