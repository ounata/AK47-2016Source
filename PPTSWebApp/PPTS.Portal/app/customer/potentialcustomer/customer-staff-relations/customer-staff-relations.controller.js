define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerStaffRelationController', [
            '$scope',
            '$uibModalInstance',
            'customerDataViewService',
            'data',
            'customerDataService',
            function ($scope, $uibModalInstance, customerDataViewService, data, customerDataService) {
                var vm = this;
                vm.jobType = data.relationType;
                vm.customers = data.customers;

                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                switch (data.type) {
                    case 'viewHistory':
                        // 页面初始化加载
                        customerDataViewService.configStaffRelationHeaders(vm);
                        customerDataViewService.getStaffRelationInfo(vm, data);
                        break;
                    case 'assignStaff':
                        customerDataViewService.getAssignStaffRelationInfo(vm, data);

                        vm.assign = function () {
                            var customerStaffRelations = [];
                            for (var index in vm.customers) {
                                var cr = { 
                                    customerID: vm.customers[index].customerID,
                                    StaffID: vm.staff.userID,
                                    StaffName: vm.staff.userName,
                                    StaffJobID: vm.staff.jobID,
                                    StaffJobName: vm.staff.jobName,
                                    StaffJobOrgID: '暂未提供',
                                    StaffJobOrgName: '暂未提供',
                                    RelationType: vm.jobType
                                };
                                customerStaffRelations.push(cr);
                            }
                            customerDataService.createCustomerStaffRelations({ customerStaffRelations: customerStaffRelations,messageType:vm.selectTypes }, function () {
                                $uibModalInstance.close();
                            });
                        };
                        break;
                }
            }]);
    });