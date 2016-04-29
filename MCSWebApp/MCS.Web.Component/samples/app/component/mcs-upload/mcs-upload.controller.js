(function () {
    angular.module('app.component')

    .controller('MCSUploadController', ['$scope', 'Upload', function ($scope, Upload) {
        var vm = this;

        vm.submit = function () {
            if ($scope.form.files.$valid && vm.files) {
                vm.uploadFiles(vm.files);
            }
        };


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
})();