(function() {
    angular.module('app.widget')
        .controller('PPTSRadiobuttonGroupController', ['$scope', '$http', function ($scope, $http) {
            var vm = this;

            vm.dictionaries = {
                'c_codE_ABBR_CUSTOMER_STAGE': [
                    { key: 'S1', value: '小学', parentKey: '0' },
                    { key: 'S2', value: '初中', parentKey: '0' },
                    { key: 'S3', value: '高中', parentKey: '0' }
                ],
                'c_codE_ABBR_CUSTOMER_GRADE': [
                    { key: 'G1', value: '一年级', parentKey: 'S1,S2,S3' },
                    { key: 'G2', value: '二年级', parentKey: 'S1,S2,S3' },
                    { key: 'G3', value: '三年级', parentKey: 'S1,S2,S3' },
                    { key: 'G4', value: '四年级', parentKey: 'S1' },
                    { key: 'G5', value: '五年级', parentKey: 'S1' },
                    { key: 'G6', value: '六年级', parentKey: 'S1' }
                ]
            };

            mcs.util.merge(vm.dictionaries);

            $http.get('app/widget/sample.json').then(function (result) {
                mcs.util.merge(result.data.dictionaries);
                $scope.$broadcast('dictionaryReady');
            });
        }]);
})();