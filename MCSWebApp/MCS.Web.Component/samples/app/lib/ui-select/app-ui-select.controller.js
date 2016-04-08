(function () {
    angular.module('app.lib').controller('AppUISelectController', [
        '$scope', function ($scope) {
            var vm = this;

            vm.disabled = undefined;

            vm.enable = function () {
                vm.disabled = false;
            };

            vm.disable = function () {
                vm.disabled = true;
            };

            vm.clear = function () {
                vm.customer.entranceGrade = '';
            };

            vm.customer = {
                entranceGrade: ''
            };

            vm.dictionaries = {
                grades: [
                    { key: 'G1', value: '一年级' },
                    { key: 'G2', value: '二年级' },
                    { key: 'G3', value: '三年级' },
                    { key: 'G4', value: '四年级' },
                    { key: 'G5', value: '五年级' },
                    { key: 'G6', value: '六年级' }
                ]
            };
        }
    ]);
})();