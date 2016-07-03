(function() {
    angular.module('app.component')
        .controller('MCSDatepickerController', ['$scope', function($scope) {
            var vm = this;
            vm.meetingTime = new Date('2014/12/14');
        }]);
})();
