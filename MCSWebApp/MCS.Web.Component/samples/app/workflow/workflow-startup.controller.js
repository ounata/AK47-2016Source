(function() {
    angular.module('app.workflow')

    .controller('WorkflowStartupController', ['$scope', 'mcsWorkflowService', function ($scope, mcsWorkflowService) {
        var vm = this;

        vm.startupParames = {
            userLogonName: 'zhangxiaoyan_2',
            processKey: 'Order_XD_Consultant_V01',
            processParameters: {
                'DiscountRate': 80 ,
                'IsYouxue': '游学'
            }
        };

        vm.submit = function () {
            mcsWorkflowService.startup(vm.startupParames, function (result) {
                alert("完成");
            });
        };
    }])
})();