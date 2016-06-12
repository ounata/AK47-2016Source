(
    function() {
        angular.module('mcs.ng.filesUpload', ['ngFileUpload'])

        .directive('filesUpload', ['Upload', '$timeout', '$http', function(Upload, $timeout, $http) {



            return {
                restrict: 'A',
                templateUrl: '../src/tpl/upload.tpl.html',
                scope: {
                    filesAmount: '=?',
                    pattern: '@?',
                    model: '=?',
                    url: '@',
                    downloadUrl: '@',
                    moduleName: '@',
                    resourceId: '@',
                    readonly: '=?'

                },
                link: function($scope, iElm, iAttrs, controller) {

                    $scope.filesUpload = [];

                    $scope.editFile = function(file) {
                        file.method = "edit";


                    };

                    $scope.delecteFile = function(file) {
                        file.method = "delete";

                    };

                    $scope.downloadFile = function(file) {

                        mcs.util.postMockForm($scope.downloadUrl, file);


                    };

                    $scope.uploadFiles = function(files) {


                        angular.forEach(files, function(file) {

                            file.upload = Upload.upload({
                                url: $scope.url,
                                data: {
                                    materialUploadModel: JSON.stringify({
                                        materialClass: $scope.moduleName,
                                        resourceID: $scope.resourceId,
                                        originalName: file.name,
                                        title: file.title,
                                        method: file.method


                                    }),
                                    file: file
                                }
                            });

                            file.upload.then(function(response) {
                                $timeout(function() {
                                    mcs.util.removeByObject(files, file);
                                    response.data[0].method = 'edit';

                                    $scope.model.push(response.data[0]);
                                });
                            }, function(response) {
                                if (response.status > 0) {
                                    $scope.errorMsg = response.status + ': ' + response.data;


                                }
                            }, function(evt) {
                                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                            });
                        });



                    };



                    $scope.fileSelect = function(files) {
                        files.forEach(function(file, index) {
                            file.method = 'add';

                        });
                    };


                }
            };
        }]);


    }
)();
