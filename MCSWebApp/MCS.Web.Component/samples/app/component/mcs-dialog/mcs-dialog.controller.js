'use strict';
(
    function() {
        angular.module('app.component')
            .controller('MCSDialogController', ['$scope', 'dialogs', function($scope, dialogs) {
                var vm = {};
                $scope.vm = vm;



                vm.waiting = function() {

                    dialogs.wait('waiting', 'data is loading, please wait!');
                }

                vm.confirm = function() {
                    dialogs.confirm('confirm', 'are you sure?');
                }

                vm.error = function() {
                    dialogs.error('Error', 'error occurs!');
                }



            }]);


    }
)();
