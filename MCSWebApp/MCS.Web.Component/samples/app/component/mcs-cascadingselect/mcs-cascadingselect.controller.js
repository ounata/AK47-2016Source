'use strict';
(
    function() {
        angular.module('app.component')

        .controller('MCSCascadingSelectController', ['$scope', '$http', function($scope, $http) {
            var vm = {};
            $scope.vm = vm;
            vm.data = {};

            $http.get('../src/tpl/sample.json').success(function(result) {
                vm.data = result;
            });


            vm.selection = {
                province: '',
                city: '',
                district: ''
            }
        }])

    }
)();