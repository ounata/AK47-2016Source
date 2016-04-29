define([ppts.config.modules.schedule],
    function (schedule) {
        schedule.registerController('dayOfWeekController', [            
            'data',
            '$uibModalInstance',
            function ( data, $uibModalInstance) {
                var vm = this;
                vm.data = data;

                //关闭窗口
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                    vm.data.form.dayOfWeeks.$setDirty();
                };

                //保存按周上课时间
                vm.save = function () {
                    $uibModalInstance.close(vm.dayOfWeeks.selecteds);
                }

                //按周上课时间Model
                vm.dayOfWeeks = {
                    keyValues: [
                                { key: '1', value: '星期一' },
                                { key: '2', value: '星期二' },
                                { key: '3', value: '星期三' },
                                { key: '4', value: '星期四' },
                                { key: '5', value: '星期五' },
                                { key: '6', value: '星期六' },
                                { key: '0', value: '星期日' }
                    ],
                    selecteds: vm.data.dayOfWeeks,
                    click: function (value) {
                        if (vm.dayOfWeeks.selecteds.indexOf(value) == -1)
                            vm.dayOfWeeks.selecteds.push(value);
                        else
                            vm.dayOfWeeks.selecteds.splice(vm.dayOfWeeks.selecteds.indexOf(value), 1)

                        vm.dayOfWeeks.selecteds.sort();
                    }
                };
            }]);
    });