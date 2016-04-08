(function () {
    angular.module('app.component').controller('MCSRadiobuttonGroupController', [
        '$scope',
        function ($scope) {
            var vm = this;

            vm.parent = {
                gender: 'G1'
            };

            vm.customer = {
                gender: 'G2'
            };

            vm.dictionaries = {
                genders: [{
                    key: 'G1',
                    value: '男'
                }, {
                    key: 'G2',
                    value: '女'
                }, {
                    key: 'G3',
                    value: '未知'
                }]
            };
            // 一般情况下选完之后不会做其他事，适用于联动响应的情况
            $scope.$watch('vm.parent.gender', function () {
                alert('我选中了' + vm.parent.gender + '，但是我还想干点别的，来这里吧。');
            });
            // 由于$watch有些性能问题，当我们执行不再使用时请将其释放掉 
            // watch();
        }
    ]);
})();