(function() {
    angular.module('app.component')

    .controller('MCSUploadController', ['$scope', 'Upload', '$http', function($scope, Upload, $http) {
        var vm = this;

        vm.upload = function() {
            if ($scope.form.files.$valid && vm.files) {
                vm.uploadFiles(vm.files);
            }
        };

        vm.sample = [{
            "status": 1,
            "id": "9f5a798d-8b35-a06f-4c7e-01bba4185b30",
            // "department": {
            //     "id": "6155-Org",
            //     "name": "总技术部",
            //     "fullPath": "机构人员\\总公司\\总技术部",
            //     "description": "",
            //     "displayName": "总技术部",
            //     "objectType": 1,
            //     "codeName": ""
            // },
            "resourceID": "1f6245a5-7916-a729-4fbe-88274958a95e",
            "sortID": 0,
            "materialClass": "新增会议上传",
            "title": "员工试用期工作总结.doc",
            "pageQuantity": 0,
            "relativeFilePath": "9f5a798d-8b35-a06f-4c7e-01bba4185b30.doc",
            "originalName": "员工试用期工作总结.doc",
            "creator": {
                "id": "39086",
                "name": "张晓燕",
                "fullPath": "机构人员\\总公司\\总技术部\\张晓燕",
                "description": "",
                "displayName": "张晓燕",
                "objectType": 2,
                "codeName": "zhangxiaoyan_2"
            },
            "lastUploadTag": "e81b8a9c-a2f0-b56a-4dfd-b815fbbd2beb",
            "createDateTime": "0001-01-01T00:00:00.000Z",
            "modifyTime": "0001-01-01T00:00:00.000Z",
            "wfProcessID": null,
            "wfActivityID": null,
            "wfActivityName": null,
            "parentID": null,
            "versionType": 0,
            "extraData": null,
            "method": "edit",
            "$$hashKey": "object:557"
        }];



        vm.userInfo = {
            userName: 'tom',
            userAge: 18,
            moduleName: 'liucy',
            resourceId: 'abc123',
            files: []
        };



        vm.submit = function() {
            $http.post('http://localhost/MCSWebApp/MCS.Web.API/api/sample/UploadMaterialWriteDB', JSON.stringify(vm.sample)).then(function(result) {
                alert(result);
            });
        };



    }]);
})();
