(
    function() {

        'use strict';

        angular.module('app.component')
            .controller('MCSDialogController', function($scope, mcsDialogService) {
                var vm = {};
                $scope.vm = vm;



                vm.wait = function() {

                    mcsDialogService.wait({
                            title: 'waiting',
                            message: 'data is loading, please wait!'
                        }

                    );
                }



                vm.confirm = function() {
                    mcsDialogService.confirm({
                            title: 'confirm',
                            message: 'are you sure to do that?'
                        }


                    );
                }

                vm.error = function() {
                    mcsDialogService.error({
                            title: 'Error',
                            message: 'error occurs!'
                        }


                    );
                }

                vm.create = function() {
                    var dlg = mcsDialogService.create('app/component/mcs-dialog/dialogCustom.html', {
                        controller: 'customDialogCtrl',
                        controllerAs: 'vm'
                    });
                    dlg.result.then(function(userName) {
                        alert('you have entered: ' + userName);
                    }, function() {

                    });
                }



            })

        .controller('customDialogCtrl', function($scope, $uibModalInstance) {
            var vm = {};
            $scope.vm = vm;
            vm.user = {
                name: 'tom'
            };


            vm.cancel = function() {
                $uibModalInstance.dismiss('Canceled');
            };

            vm.save = function() {
                $uibModalInstance.close(vm.user.name);
            };

            vm.hitEnter = function(evt) {
                if (angular.equals(evt.keyCode, 13) && !(angular.equals(vm.user.name, null) || angular.equals(vm.user.name, '')))
                    vm.save();
            };



        });


    }
)();