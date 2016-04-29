ppts.ng.controller('appController', ['$rootScope', '$scope', 'userService', 'utilService', function ($rootScope, $scope, user, util) {
    var vm = this;

    vm.currentUser = vm.currentUser || user.initJob();
    vm.switch = function (job) {
        user.switchJob(vm.currentUser, job);
    };

    util.fixSidebar(vm);
}]);

ppts.ng.controller('treeController', ['$uibModalInstance', 'dataSyncService', 'data', function ($uibModalInstance, dataSyncService, data) {
    var vm = this;

    vm.title = data.title || '选择数据';

    vm.treeSetting = dataSyncService.loadTreeSetting(vm, data);
    // 加载树数据
    vm.loadData = function (callback) {
        dataSyncService.loadTreeData(vm, data, callback);
    };

    vm.close = function () {
        $uibModalInstance.dismiss('Canceled');
    };

    vm.select = function () {
        $uibModalInstance.close(vm.treeSetting);        
    };

}]);