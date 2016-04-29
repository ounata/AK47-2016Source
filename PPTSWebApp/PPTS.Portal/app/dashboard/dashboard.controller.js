define([ppts.config.modules.dashboard], function (dashboard) {
    dashboard.registerController('dashboardController', function () {
        var vm = this;
        vm.version = 'PPTS V2.0';
    });
});