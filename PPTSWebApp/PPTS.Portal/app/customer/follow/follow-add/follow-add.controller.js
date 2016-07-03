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

                var talkData = [{ key: 'A1', value: '有意向上门时间的客户，一周内上门' },
                    { key: 'A2', value: '有意向上门时间的客户，两周内上门' },
                    { key: 'A3', value: '有意向上门时间的客户，一个月内上门' },
                    { key: 'A4', value: '有意向上门时间的客户，一个月以上上门' },
                    { key: 'B1', value: '小一到高三年级以内的有效客户，需要一对一' },
                    { key: 'B2', value: '小一到高三年级以内的有效客户，需要班组/上门及其他' },
                    { key: 'B3', value: '小一到高三年级以内的有效客户，需要适中可以联系' },
                    { key: 'B4', value: '小一到高三年级以内的有效客户，需要弱（再联系）' },
                    { key: 'C1', value: '小一到高三年级以外的潜在客户，非目标年级（小一以下，高三以上年级）' },
                    { key: 'C2', value: '小一到高三年级以外的潜在客户，非目标地域（客户城市无校区）' },
                    { key: 'D', value: '无效客户' }];

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
                        if (vm.follow.isStudyThere)
                            vm.follow.isStudyThere = (vm.follow.isStudyThere == "1" ? 1 : 0);
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
                        vm.followItems.pop();
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

                $scope.$watch('vm.customerLevelValue', function (value) {
                    var talkResultArray = talkData.filter(function (item) {
                        return item.key == value;
                    });
                    vm.talkResult = talkResultArray != undefined && talkResultArray.length > 0 ? talkResultArray[0].value : "";
                });

            }]);
        });