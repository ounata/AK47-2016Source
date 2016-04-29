define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.studentDataService],
        function (customer) {
            customer.registerController('studentListController', [
                '$scope',
                '$state',
                'utilService',
                'dataSyncService',
                'studentDataViewService',
                'studentAdvanceSearchItems',
                function ($scope, $state, util, dataSyncService, studentDataViewService, searchItems) {
                    var vm = this;

                    // 配置数据表头
                    studentDataViewService.configStudentListHeaders(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        studentDataViewService.initStudentList(vm, function () {
                            vm.searchItems = searchItems;
                        });
                        studentDataViewService.initDateRange($scope, vm, [
                                { watchExp: 'vm.selectedAttendRange', selectedValue: 'selectedAttendRange', start: 'attendStartTime', end: 'attendEndTime' },
                                { watchExp: 'vm.selectedRange', selectedValue: 'selectedRange', start: 'statusStartTime', end: 'statusEndTime' }
                        ]);
                    };
                    vm.search();

                    // 选择大区/公司/校区
                    vm.select = function () {
                        dataSyncService.popupTree(vm, {
                            title: '选择大区/分公司/校区'
                        });
                    };

                    // 新增跟进记录
                    vm.addFollow = function () {
                        if (util.selectOneRow(vm)) {
                            $state.go('ppts.follow-add', {
                                customerId: vm.data.rowsSelected[0].customerID,
                                prev: 'ppts.student'
                            });
                        }
                    };
                }]);
        });