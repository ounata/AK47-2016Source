define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.classgroupDataService
    ],
    function (schedule) {
        schedule.registerController('classListController', [
            '$scope',
            '$state',
            '$q',
            '$stateParams',
            'classgroupDataService',
            'dataSyncService',
            'mcsDialogService',
            'classSearchItems',
            'classListHeader',
            'utilService',
            function ($scope, $state, $q, $stateParams, classgroupDataService, dataSyncService, mcsDialogService, classSearchItems, classListHeader, util) {
                var vm = this;

                vm.criteria = vm.criteria || {};
                vm.criteria.productCode = $stateParams.productCode;

                // 配置数据表头 
                dataSyncService.configDataHeader(vm, classListHeader, classgroupDataService.getPageClasses);

                // 页面初始化加载或重新搜索时查询
                vm.search = function () {
                    vm.searchItems = classSearchItems;                    
                    dataSyncService.initDataList(vm, classgroupDataService.getAllClasses, function () {
                        $scope.$broadcast('dictionaryReady');
                    });
                };

                vm.search();

                //删除班级
                vm.deleteClass = function () {
                    if (!util.selectOneRow(vm)) return;
                    if (util.showMessage(vm, vm.data.rowsSelected[0].classStatus != 0, '请选择一个状态为新建的班级！')) return;

                    classgroupDataService.deleteClass(
                             vm.data.rowsSelected[0].classID, function () {
                                 //刷新页面
                                 vm.search();
                             }, function () {

                             });
                }

                //查看学生
                vm.searchCustomers = function () {
                    if(!util.selectOneRow(vm)) return;
                    mcsDialogService.create('app/schedule/classgroup/customer-list/customer-list.html', {
                        controller: 'customerListController',
                        params: { classID: vm.data.rowsSelected[0].classID },
                        settings: {
                            size: 'lg'
                        }
                    }).result.then(function (data) {

                    }, function () {

                    });
                }
            }]);
    });