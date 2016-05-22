define([ppts.config.modules.custcenter,
        ppts.config.dataServiceConfig.custserviceDataService],
        function (custcenter) {
            custcenter.registerController('custserviceNextProcessController', ['$scope', '$state', '$stateParams',
                'custserviceDataService', 'custserviceDataViewService', 'custserviceAdvanceSearchItems', 'dataSyncService',
                function ($scope, $state, $stateParams, custserviceDataService, custserviceDataViewService, searchItems, dataSyncService) {
                    var vm = this;

                    vm.id = $stateParams.id;

                    custserviceDataService.getCustomerServiceInfo(vm.id, function (result) {
                        vm.customerService = result.customerService;

                        vm.currJobType = '0';
                        //总客服专员未升级
                        if (result.currJobName == '总客服专员' && result.customerService.isUpgradeHandle == 1) {
                            vm.currJobType = '1';
                        }
                        //总客服专员已升级
                        if (result.currJobName == '总客服专员' && result.customerService.isUpgradeHandle == 2) {
                            vm.currJobType = '2';
                        }
                        //总客服经理已升级
                        if (result.currJobName == '总客服经理') {
                            vm.currJobType = '3';
                        }
                        //分客服专员未升级
                        if (result.currJobName == '分客服专员') {
                            vm.currJobType = '4';
                        }

                        vm.currJobType = '3';

                        dataSyncService.injectDictData({
                            c_codE_ABBR_Headquarters_Service: [{ key: '1', value: '分客服专员' }, { key: '2', value: '分客服经理' }, { key: '3', value: '总客服经理' }],
                            c_codE_ABBR_Update_Service: [{ key: '3', value: '总客服经理' }],
                            c_codE_ABBR_Update_Headquarters_Service: [{ key: '1', value: '分客服专员' }, { key: '2', value: '分客服经理' }, { key: '4', value: '分总' }, { key: '5', value: '校区总监' }, { key: '6', value: '总客服总监' }, { key: '7', value: '助理副总裁' }],
                            c_codE_ABBR_Branch_Service: [{ key: '5', value: '校区总监' }, { key: '2', value: '分客服经理' }, { key: '8', value: '分教管经理' }, { key: '9', value: '分咨询经理' }, { key: '10', value: '校教育咨询师' }, { key: '11', value: '校学习管理师' }, { key: '12', value: '校咨询主任' }, { key: '13', value: '校学管主任' }]

                        });

                        dataSyncService.injectPageDict(['messageType']);

                        $scope.$broadcast('dictionaryReady');
                    });

                    // 转下一个处理人
                    vm.nextProcess = function () {

                        //if (vm.currJobType == 1) {
                        //    vm.customerService.handlerJobType = vm.headquarters;
                        //}
                        //else if (vm.currJobType == 2) {
                        //    vm.customerService.handlerJobType = vm.update;
                        //}
                        //else if (vm.currJobType == 3) {
                        //    vm.customerService.handlerJobType = vm.updateHeadquarters;
                        //}
                        //else if (vm.currJobType == 4) {
                        //    vm.customerService.handlerJobType = vm.branch;
                        //}

                        vm.customerService.handlerName = '下一个处理人123';

                        custserviceDataService.updateCustomerService({
                            customerService: vm.customerService
                        }, function () {
                            $state.go('ppts.custservice');
                        });
                    };

                }]);
        });