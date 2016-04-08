define([ppts.config.modules.dashboard], function (dashboard) {
    dashboard.registerController('dashboardController', ['$scope', function ($scope) {
        var vm = this;
        vm.version = 'PPTS V2.0';
    }]);
});