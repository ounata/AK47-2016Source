(function() {
    angular.module('app.widget')
        .controller('PPTSSelectController', ['$scope', '$http', function ($scope, $http) {
            var vm = this;

            vm.dictionaries = {
                'c_codE_ABBR_CUSTOMER_GRADE': [
                    { key: 'G1', value: '一年级' },
                    { key: 'G2', value: '二年级' },
                    { key: 'G3', value: '三年级' },
                    { key: 'G4', value: '四年级' },
                    { key: 'G5', value: '五年级' },
                    { key: 'G6', value: '六年级' }
                ]
            };

            mcs.util.merge(vm.dictionaries);

            $http.get('app/widget/sample.json').then(function (result) {
                mcs.util.merge(result.data.dictionaries);
                $scope.$broadcast('dictionaryReady');
            });

            vm.select = function (item, model) {
                alert('我选中了item：' + item.value + ',对应key:' + model);
            };
        }]);
})();