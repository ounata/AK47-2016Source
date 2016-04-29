(function() {
    'use strict';
    angular.module('app.component').controller('MCSAutoCompleteController', [
        '$scope', '$http',

        function($scope, $http) {
            var vm = this;
            $scope.vm = vm;

            vm.teachers = [{
                "teacherId": "1",
                "name": "tom"
            }];

            vm.tags = [{
                teacherId: '1',
                name: 'tom'
            }, {
                teacherId: '2',
                name: 'jack'
            }, {
                teacherId: '3',
                name: 'jerry'
            }];

            vm.queryTeacherList = function(query) {
                var result = [];

                vm.tags.forEach(function(item) {
                    if (item.name.indexOf(query) > -1) {
                        result.push(item);
                    }

                });

                return result;
            };

        }
    ]);
})();
