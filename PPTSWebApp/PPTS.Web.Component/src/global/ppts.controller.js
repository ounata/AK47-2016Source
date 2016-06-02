ppts.ng.controller('appController', ['$rootScope', '$scope', 'userService', 'utilService', 'storage', function ($rootScope, $scope, user, util, storage) {
    var vm = this;
    user.initJob(vm);

    vm.switch = function (job) {
        user.switchJob(vm, job);
    };

    vm.logoff = function () {
        storage.remove('vm.currentUser');
        storage.remove('ppts.user.currentJobId');
    };
}]);

ppts.ng.controller('treeController', ['$uibModalInstance', 'dataSyncService', 'data', function ($uibModalInstance, dataSyncService, data) {
    var vm = this;

    vm.title = data.title || '选择数据';
    vm.distinctLevel = data.distinctLevel;

    vm.treeSetting = dataSyncService.loadTreeSetting(data);
    // 加载树数据
    vm.loadData = function (callback) {
        dataSyncService.loadTreeData(data, callback);
    };

    vm.close = function () {
        $uibModalInstance.dismiss('Canceled');
    };

    vm.select = function () {
        $uibModalInstance.close(vm.treeSetting);
    };

}]);