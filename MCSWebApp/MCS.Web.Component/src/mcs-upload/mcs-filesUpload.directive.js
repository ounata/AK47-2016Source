(
    function() {
        angular.module('mcs.ng.filesUpload', ['ngFileUpload'])

        .directive('filesUpload', ['Upload', '$timeout', '$http', function(Upload, $timeout, $http) {



            return {
                restrict: 'A',
                templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/upload.tpl.html',
                scope: {
                    filesAmount: '=?',
                    pattern: '@?',
                    model: '=?',
                    url: '@',
                    errorMessage: '@',
                    downloadUrl: '@',
                    moduleName: '@',
                    resourceId: '@',
                    readonly: '=?'

                },
                link: function($scope, iElm, iAttrs, formCtrl) {

                    $scope.filesUpload = [];

                    $scope.statusEnum = {
                        inserted: 1,
                        updated: 2,
                        deleted: 3
                    };

                    $scope.fileStatusFilter = function(e) {
                        return e.status != $scope.statusEnum.deleted;
                    };



                    $scope.delecteFile = function(file) {
                        file.status = $scope.statusEnum.deleted;

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
                                        status: file.status


                                    }),
                                    file: file
                                }
                            });

                            file.upload.then(function(response) {
                                $timeout(function() {
                                    mcs.util.removeByObject(files, file);
                                    response.data[0].status = $scope.statusEnum.inserted;

                                    $scope.model.push(response.data[0]);
                                });
                            }, function(response) {

                                $scope.errorMsg = '上传失败';


                            }, function(evt) {
                                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                            });
                        });



                    };



                    $scope.fileSelect = function(files) {


                        files.forEach(function(file, index) {
                            file.status = $scope.statusEnum.inserted;
                            file.title = file.name;

                        });

                        $scope.uploadFiles(files);
                    };


                }
            };
        }]);


    }
)();
