ppts.ng.controller('appController', ['$rootScope', '$scope', 'userService', 'storage', function ($rootScope, $scope, user, storage) {
    var vm = this;
    user.initJob(vm);

    vm.toggle = function () {
        vm.hideSidebar = !vm.hideSidebar;
    };

    vm.switch = function (job) {
        user.switchJob(vm, job);
    };

    vm.logoff = function () {
        storage.remove('vm.currentUser');
        storage.remove('ppts.user.currentJobId');
    };
}]);