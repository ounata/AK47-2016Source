/*按学员排课列表*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (schedule) {
            schedule.registerController('stuAsgmtListController', [
                '$scope', '$state', 'dataSyncService', 'utilService', 'studentassignmentDataService', 'studentUnAssignmentDataHeader',
                function ($scope, $state, dataSyncService, utilService, studentassignmentDataService, studentUnAssignmentDataHeader) {
                    var vm = this;

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, studentUnAssignmentDataHeader, studentassignmentDataService.getPagedStuUnAsgmt, function () {
                        $scope.$broadcast('dictionaryReady');
                    });

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, studentassignmentDataService.getAllStuUnAsgmt, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.search();
                    /*跳转排课*/
                    vm.stuAssignClick = function () {
                        if (utilService.selectOneRow(vm)) {
                            $state.go('ppts.stuasgmt-course', { id: vm.data.rowsSelected[0].customerID, prev: 'ppts.schedule' });
                        }                        
                    };
                }]);
        });