(
    function() {

        'use strict';

        angular.module('app.component')
            .controller('MCSDialogController', ['$scope', 'mcsDialogService', function($scope, mcsDialogService) {
                var vm = {};
                $scope.vm = vm;


                vm.wait = function() {

                    mcsDialogService.wait(
                        'waiting',
                        'data is loading, please wait!'
                    );
                }



                vm.confirm = function() {
                    mcsDialogService.confirm(
                        'confirm',
                        '<div>are you sure?<br/>please do more action!</div>'
                    );
                }

                vm.error = function() {
                    mcsDialogService.error(
                        'Error',
                        'error occurs!'
                    );
                }



            }]);


    }
)();
