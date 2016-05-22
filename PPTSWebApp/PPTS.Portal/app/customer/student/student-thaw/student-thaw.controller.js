define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.studentDataService],
        function (customer) {
            customer.registerController('studentThawController', [
                '$scope', '$stateParams', '$uibModalInstance', 'Upload', 'studentDataViewService', 'mcsDialogService',
                function ($scope, $stateParams, $uibModalInstance, Upload, studentDataViewService, mcsDialogService) {
                    var vm = this;

                    // 页面初始化加载
                    (function () {
                        $scope.$broadcast('dictionaryReady');
                        //studentDataViewService.initCreateStopAlertInfo(vm, $stateParams, function () {
                        //    $scope.$broadcast('dictionaryReady');
                        //});
                    })();

                    vm.cancel = function () {
                        $uibModalInstance.dismiss('Canceled');
                    };

                    vm.save = function () {
                        vm.criteria.customerID = $stateParams.id;
                        studentDataViewService.createStopAlertInfo(vm, function () {
                            $uibModalInstance.close();
                        });
                    };

                    //vm.submit = function () {
                    //    if ($scope.form.files.$valid && vm.files) {
                    //        vm.uploadFiles(vm.files);
                    //    }
                    //};

                    // for multiple files:
                    vm.uploadFiles = function (files) {
                        if (files && files.length) {
                            for (var i = 0; i < files.length; i++) {
                                Upload.upload({
                                    url: 'upload',
                                    data: {
                                        file: files[i]
                                    }
                                })

                                .then(function (resp) {
                                    vm.msg = 'Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data;
                                }, function (resp) {
                                    vm.msg = 'Error status: ' + resp.status;
                                }, function (evt) {
                                    vm.files.progress = parseInt(100.0 * evt.loaded / evt.total);
                                })
                                .catch(function () {
                                });
                            }
                        }
                    }
                }]);
        });