define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.studentDataService],
    function (customer) {
        customer.registerController('studentTransferController', [
            '$scope',
            '$uibModalInstance',
            'mcsDialogService',
            'studentDataService',
            'studentDataViewService',
            'data',
            function ($scope, $uibModalInstance, mcsDialogService, studentDataService, studentDataViewService, data) {
                var vm = this;
                vm.customerID = data.students[0].customerID;

                studentDataViewService.getTransferStudentInfo(vm, data);

                // 页面初始化加载或重新搜索时查询
                vm.init = function () {
                    studentDataService.getStudentTransferApplyByCustomerID(vm.customerID, function (result) {
                        vm.apply = result.apply
                        vm.assert = result.assert;
                        vm.customer = result.customer;
                        vm.errorMessage = result.assert.message;

                    });
                };
                vm.init();

                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                vm.save = function () {

                    if (!vm.apply.toCampusID) {
                        mcsDialogService.error({ title: '警告', message: '请选择转学的校区' });
                        return;
                    }
                    if (vm.apply.campusID == vm.apply.toCampusID) {
                        mcsDialogService.error({ title: '警告', message: '转至校区不能是当前校区' });
                        return;
                    }

                    mcsDialogService.confirm({ title: '确认', message: '是否确认提交审批？' })
                       .result.then(function () {
                           studentDataService.saveStudentTransferApply(vm.apply, function () {
                               $uibModalInstance.close();
                           });
                       });
                }
            }]);
    });