(function () {
    angular.module('app.component').controller('MCSCheckboxGroupController', [
        '$scope', function ($scope) {
            var vm = this;
            vm.criteria = {
                selectedGrades: ['G1', 'G3', 'G4']
            };

            vm.dictionaries = {
                grades: [
                    { key: 'G1', value: '一年级' },
                    { key: 'G2', value: '二年级' },
                    { key: 'G3', value: '三年级' },
                    { key: 'G4', value: '四年级' },
                    { key: 'G5', value: '五年级' },
                    { key: 'G6', value: '六年级' }
                ],
                years: [
                    { key: 'Y1', value: '三年制' },
                    { key: 'Y2', value: '五年制' },
                    { key: 'Y3', value: '六年制' },
                    { key: 'Y4', value: '九年制' }
                ]
            };

            vm.selectAll = function (type, selectedType) {
                vm.criteria[selectedType] = mcs.util.selectAll(vm.dictionaries[type]);
            };

            vm.unSelectAll = function (selectedType) {
                vm.criteria[selectedType] = mcs.util.unSelectAll();
            };

            vm.inverseSelect = function (type, selectedType) {
                vm.criteria[selectedType] = mcs.util.inverseSelect(vm.dictionaries[type], vm.criteria[selectedType]);
            };

            $scope.$watchCollection('vm.criteria.selectedYears', function () {
                if (!vm.criteria.selectedYears.length) return;
                alert('我选中了' + vm.criteria.selectedYears + '，但是我还想干点别的，来这里吧。');
            });
        }
    ]);
})();