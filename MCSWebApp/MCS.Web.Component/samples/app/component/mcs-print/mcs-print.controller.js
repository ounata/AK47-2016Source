(function() {
    angular.module('app.component').controller('MCSPrintController', [
        '$scope', 'printService',
        function($scope, printService) {
            var vm = this;

            vm.print = function() {
                printService.print();
            }

        }
    ])
})();
