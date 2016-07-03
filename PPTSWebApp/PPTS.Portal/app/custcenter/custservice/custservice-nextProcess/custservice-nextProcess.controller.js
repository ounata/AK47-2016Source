define([ppts.config.modules.custcenter,
        ppts.config.dataServiceConfig.custserviceDataService],
        function (custcenter) {
            custcenter.registerController('custserviceNextProcessController', ['$scope', '$state', '$stateParams',
                'custserviceDataService', 'custserviceDataViewService', 'custserviceAdvanceSearchItems', 'dataSyncService',
                function ($scope, $state, $stateParams, custserviceDataService, custserviceDataViewService, searchItems, dataSyncService) {
                    var vm = this;

                    vm.id = $stateParams.id;
                    vm.processID = $stateParams.processID;
                    vm.activityID = $stateParams.activityID;
                    vm.resourceID = $stateParams.resourceID;

                    vm.currJobType = '0';

                    custserviceDataService.getCustomerServiceInfo(vm.id, function (result) {
                        vm.customerService = result.customerService;
                        vm.pCustomer = result.pCustomer;
                        //总客服专员未升级
                        if (result.currJobName == '总客服专员' && result.customerService.isUpgradeHandle == 1) {
                            vm.currJobType = '1';
                        }
                        //总客服专员已升级
                        else if (result.currJobName == '总客服专员' && result.customerService.isUpgradeHandle == 2) {
                            vm.currJobType = '2';
                        }
                        //总客服经理已升级
                        else if (result.currJobName == '总客服经理') {
                            vm.currJobType = '1';
                        }
                        //分客服专员未升级
                        else if (result.currJobName == '分客服专员') {
                            vm.currJobType = '3';
                        }
                        //校区级别的岗位
                        else if (result.currJobName == '分客服专员') {
                            vm.currJobType = '4';
                        }

                        //注：此处模拟岗位流转，上线后会去掉
                        vm.currJobType = '4';

                        dataSyncService.injectDynamicDict([{key: '1', value: '分客服专员'}], {category: 'headquarters' });
                        dataSyncService.injectDynamicDict([{ key: '2', value: '总客服经理' }], { category: 'update' });
                        dataSyncService.injectDynamicDict([{ key: '3', value: '校区总监' }, { key: '4', value: '校教育咨询师' }, { key: '5', value: '校学习管理师' }, { key: '6', value: '校咨询主任' }, { key: '7', value: '校学管主任' }, { key: '8', value: '校教学主任' }], { category: 'updateHeadquarters'});
                        dataSyncService.injectDynamicDict([{ key: '3', value: '校区总监' }, { key: '4', value: '校教育咨询师' }, { key: '6', value: '校咨询主任' }, { key: '7', value: '校学管主任' }, { key: '8', value: '校教学主任' }], { category: 'branch' });
                        dataSyncService.injectDynamicDict('messageType');

                        $scope.$broadcast('dictionaryReady');
                    });

                    // 转下一个处理人
                    vm.nextProcess = function () {

                        custserviceDataService.accessProcess({
                            customerService: vm.customerService,
                            pCustomer: vm.pCustomer,
                            processID: vm.processID,
                            activityID: vm.activityID,
                            resourceID: vm.resourceID
                        }, function () {
                            $state.go('ppts.custservice');
                        });
                    };

                }]);
        });