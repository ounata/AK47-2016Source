(function() {
    angular.module('app.component')

    .controller('MCSUploadController', ['$scope', 'Upload', '$http', function($scope, Upload, $http) {
        var vm = this;

        vm.submit = function() {
            if ($scope.form.files.$valid && vm.files) {
                vm.uploadFiles(vm.files);
            }
        };

        vm.userInfo = {
            userName: 'tom',
            userAge: 18,
            files: [{
                originalName: 'azure.PNG',
                title: 'hello',
                method: 'edit',
                moduleName: 'liucy',
                resourceId: 'abc123'
            }]
        };



        vm.submit = function() {
            $http.post('http://localhost/MCSWebApp/MCS.Web.API/api/sample/UploadMaterial', vm.userInfo).then(function(result) {
                alert(result);
            });
        };



    }]);
})();
