/*
mcs.ng.directive('pptsTimepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            step: '@',
            model: '=',
            zIndex: '@'
        },
        templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/timepicker.tpl.html',
        controller: function ($scope) {
            if ($scope.zIndex) {
                $scope.dateStyle = { 'z-index': $scope.zIndex };
            }
        },
        link: function ($scope, $elem) {
            var $this = $elem.find('.time-picker');

            $this.timepicker({
                minuteStep: $scope.step || 30,
                showSeconds: true,
                showMeridian: false
            }).next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
});
*/