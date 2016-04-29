(function() {
    angular.module('app.component')

    .controller('MCSValidationController', ['$scope', 'mcsDialogService', function($scope, mcsDialogService) {
        var vm = this;
        vm.validation = {
            name: '请输入用户名',
            email: 'email输入格式不正确'
        };

        vm.submitForm = function(formCtrl) {
            if (!formCtrl.$valid) {
                var errorMessage = '<span>有以下验证错误：</span><br>';
                Object.getOwnPropertyNames(vm.validation).forEach(function(key) {
                    if (formCtrl[key].$invalid) {
                        errorMessage += vm.validation[key] + '<br>';
                    }
                })

                mcsDialogService.error({ title: '验证提醒', message: errorMessage });
            }

        }

    }])
})();
