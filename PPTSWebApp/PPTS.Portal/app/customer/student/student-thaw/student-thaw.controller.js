define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.studentDataService],
        function (customer) {
            customer.registerController('studentThawController', [
                '$scope', '$stateParams', 'dataSyncService', '$uibModalInstance', 'Upload', 'studentDataViewService', 'studentDataService', 'mcsDialogService', 'data', 'mcsValidationService',
                function ($scope, $stateParams, dataSyncService, $uibModalInstance, Upload, studentDataViewService, studentDataService, mcsDialogService, customerData, mcsValidationService) {
                    var vm = this;
                    mcsValidationService.init($scope);
                    // 页面初始化加载
                    (function () {
                        vm.criteria = {};
                        vm.criteria.files = [];
                        vm.criteria.customerID = $stateParams.id;
                        vm.criteria.customerCode = customerData.customer.customerCode;
                        vm.criteria.customerName = customerData.customer.customerName;
                        vm.criteria.campusName = customerData.customer.campusName;
                        dataSyncService.injectDynamicDict('thawReasonType');
                        $scope.$broadcast('dictionaryReady');
                    })();

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('Canceled');
                    };

                    vm.upload = function () {
                        if ($scope.form.files.$valid && vm.criteria.files) {
                            vm.uploadFiles(vm.criteria.files);
                        }
                    };

                    vm.save = function () {
                        if (mcsValidationService.run($scope)) {
                            studentDataService.studentThawSave(vm.criteria, function () {
                                $uibModalInstance.close();
                            });
                        }
                    };
                    vm.url = ppts.config.customerApiBaseUrl;
                }]);
        });