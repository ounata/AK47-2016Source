(
    function() {

        'use strict';

        angular.module('app.component')
            .controller('MCSDialogController', function($scope, mcsDialogService) {
                var vm = {};
                $scope.vm = vm;

                vm.name = 'tom';



                vm.wait = function() {

                    mcsDialogService.wait({
                            title: 'waiting',
                            message: 'data is loading, please wait!'
                        }

                    );
                }



                vm.confirm = function() {
                    mcsDialogService.confirm({
                            title: '请确认',
                            message: 'are you sure to do that?'
                        }


                    );
                }

                vm.info = function() {
                    mcsDialogService.info({
                            title: '提示',
                            message: 'info occurs!',
                            backdrop: false
                        }


                    );
                }

                vm.error = function() {
                    mcsDialogService.error({
                            title: '警告',
                            message: 'error occurs!',
                            settings: {
                                backdrop: 'static'
                            }
                        }


                    );
                }

                vm.create = function() {
                    var dlg = mcsDialogService.create('app/component/mcs-dialog/dialogCustom.html', {
                        controller: 'customDialogCtrl',
                        controllerAs: 'vm'
                    });
                    dlg.result.then(function(userName) {
                        vm.name = userName;
                        alert('you have entered: ' + userName);
                    }, function() {
                        alert('hi');

                    });
                }



            })

        .controller('customDialogCtrl', function($scope, $uibModalInstance) {
            var vm = this;
            $scope.vm = vm;
            vm.user = {
                name: ''
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
