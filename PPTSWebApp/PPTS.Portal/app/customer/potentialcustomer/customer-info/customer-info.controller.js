define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerInfoController', [
            '$scope',
            '$stateParams',
            'customerService',
            'dataSyncService',
            'customerDataViewService',
            'customerParentService',
            'customerRelationType',
            'mcsDialogService',
            function ($scope, $stateParams, customerService, dataSyncService, customerDataViewService, customerParentService, relationType, mcsDialogService) {
                var vm = this;

                // 页面初始化加载
                customerService.handle('init', function (result) {
                    vm.customer = result.customer;
                    vm.parent = result.parent;
                    vm.isCustomer = result.isCustomer || false;
                    customerDataViewService.getRelationInfo(vm, result.customerStaffRelations);

                    dataSyncService.injectDynamicDict('ifElse');
                    $scope.$broadcast('dictionaryReady');
                });

                // 获取学员与员工关系
                vm.relationType = relationType;

                // 查看咨询师/学管师/坐席/市场专员历史
                vm.viewHistory = function (relationType) {
                    customerDataViewService.viewStaffRelation($stateParams.id, relationType);
                };

                // 归属教师
                vm.viewHistoryTeachers = function (customerID) {
                    customerDataViewService.viewHistoryTeachers(customerID);
                };

                // 添加孩子家长
                vm.parentAdd = function () {
                    customerParentService.popupParentAdd(vm, 'confirm');
                }

                // 编辑
                vm.edit = function () {
                    customerService.handle('edit');
                }

                //解冻
                vm.thaw = function () {
                    mcsDialogService.create('app/customer/student/student-thaw/student-thaw.html', {
                        controller: 'studentThawController',
                        params: {
                            //customerID: $stateParams.id,
                            //customerCode: vm.customer.customerCode,
                            //customerName: vm.customer.customerName
                            customer: vm.customer
                        },
                        settings: {
                            size: 'lg'//md
                        }
                    }).result.then(function () {
                        // vm.init();
                    });
                }
            }]);
    });