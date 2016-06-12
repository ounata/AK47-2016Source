define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('followAddController', ['$scope', '$state', '$stateParams', 'followDataService', 'customerService', 'followDataViewService', 'followInstutionItem', 'mcsDialogService', 'mcsValidationService',
            function ($scope, $state, $stateParams, followDataService, customerService, followDataViewService, followInstutionItem, mcsDialogService, mcsValidationService) {
                mcsValidationService.init($scope);
                var vm = this, followItem = {
                    subject: '',
                    institude: '',
                    startDate: '',
                    endDate: '',
                    followID: '',
                    ItemID: ''
                };
                vm.followItems = [];

                // 页面初始化加载
                (function () {
                    customerService.handle('addFollow', function (result) {
                        followDataViewService.initCreateFollowInfo(vm, result, function () {
                            vm.follow.planVerifyTime = '';
                            vm.follow.nextFollowTime = '';
                            vm.follow.planSignDate = '';
                            vm.followItems.push(mcs.util.clone(followItem));
                            $scope.$broadcast('dictionaryReady');
                        });
                    });
                })();

                // 保存数据
                vm.save = function () {
                    if (mcsValidationService.run($scope)) {
                        vm.follow.talkMainResult = vm.selection[0];
                        vm.follow.talkSubResult = vm.selection[1];
                        vm.follow.isStudyThere = (vm.follow.isStudyThere == "1");
                        if (vm.follow.subjects != undefined) {
                            vm.follow.intensionSubjects = vm.follow.subjects.join(',');
                            followDataService.createFollow({
                                follow: vm.follow,
                                followItems: vm.followItems
                            }, function () {
                                $state.go('ppts.customer');
                            });
                        }
                    }
                };
                vm.cancel = function () {
                    customerService.handle('addFollow-cancel');
                };

                vm.addRow = function () {
                    vm.followItems.push(mcs.util.clone(followItem));
                };

                vm.removeRow = function () {
                    if (vm.followItems.length != 1)
                        vm.followItems.shift();
                    else {
                        vm.error();
                    }
                }

                vm.error = function () {
                    mcsDialogService.error({
                        title: '提示',
                        message: '必须最少添一项机构辅导!'
                    }
                    );
                }

                vm.instutionItems = [followInstutionItem.template];

            }]);
        });