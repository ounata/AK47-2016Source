define([ppts.config.modules.schedule],
    function (schedule) {
        schedule.registerController('lessonEditController', [
            'data',
            'dataSyncService',
            '$uibModalInstance',
            'classgroupDataService',
            function (data, dataSyncService, $uibModalInstance, classgroupDataService) {
                var vm = this;
                vm.data = data;
                vm.isEdit = '0';

                //dataSyncService.injectDictData({
                //    c_codE_ABBR_EditCourseType: [{ key: '0', value: '仅修改此次课' }, { key: '1', value: '从当前课次至最后课次都修改' }]
                //});

                dataSyncService.injectPageDict(['ifElse']);
                dataSyncService.injectPageDict(['week']);

                //关闭窗口
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                //保存
                vm.save = function () {
                    if (vm.isEdit != 1) {
                        vm.data.dayOfWeeks = [];
                    }
                    classgroupDataService.editClassLessones(vm.data, function () {
                        $uibModalInstance.close();
                    }, function () {
                    });
                }
            }]);
    });