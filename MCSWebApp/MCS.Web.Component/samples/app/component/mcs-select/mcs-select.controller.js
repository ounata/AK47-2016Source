(function () {
    angular.module('app.component').controller('MCSSelectController', [
        '$scope', function ($scope) {
            var vm = this;

            vm.createCustomer = function () {
                alert('我要加一个潜客，随你咋想！');
            };
        }
    ]);
})();