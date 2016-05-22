(function() {
    angular.module('app.component').controller('MCSUICopyController', [
        '$scope',
        function($scope) {
            var vm = this;

            vm.students = [{
                name: 'tom',
                age: 12
            }, {
                name: 'jack',
                age: 88
            }];

            vm.students = [{
                parentName: 'tom'
            }];

            vm.add = function() {
                vm.students.push({
                    parentName: 'zhangsan',
                    age: 11
                })
            };

            vm.remove = function() {
                vm.students.shift();
            }


        }
    ]);
})();
