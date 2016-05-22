(function () {
    'use strict';

    angular.module('app.widget').directive('pptsDatetimepicker', function () {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                model: '=',
                zIndex: '@'
            },
            templateUrl: 'app/widget/ppts-datetimepicker/datetimepicker.tpl.html',
            controller: function ($scope) {
                if ($scope.zIndex) {
                    $scope.dateStyle = { 'z-index': $scope.zIndex };
                }
            },
            link: function ($scope, $elem) {
                var $this = $elem.find('.date-timepicker');

                $this.datetimepicker({
                    format: ppts.config.datetimePickerFormat,
                    showSeconds: true,
                    language: ppts.config.datePickerLang
                }).next().on('click', function () {
                    $(this).prev().focus();
                });
            }
        }
    });

})();