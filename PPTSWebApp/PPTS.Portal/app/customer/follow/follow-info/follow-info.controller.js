define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.followDataService],
        function (customer) {
            customer.registerController('followInfoController', ['$scope', '$stateParams', 'followDataViewService', 'followInstutionItem', 'customerLevelFilter',
                function ($scope, $stateParams, followDataViewService, followInstutionItem, customerLevelFilter) {
                    var vm = this;

                    // 页面初始化加载
                    (function () {

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

                        followDataViewService.initFollowInfo(vm, $stateParams.followId, function () {
                            
                            var customerLevelValue = customerLevelFilter(vm.follow.customerLevel);
                            
                            var talkResultArray = talkData.filter(function (item) {
                                return item.key == customerLevelValue;
                            });
                            vm.talkResult = talkResultArray != undefined && talkResultArray.length > 0 ? talkResultArray[0].value : "";

                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    vm.instutionItems = [followInstutionItem.template];

                }]);
        });