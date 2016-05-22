(function () {
    'use strict';

    angular.module('app.widget').directive('pptsDaterangepicker', function () {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                size: '@',
                startDate: '=',
                endDate: '=',
                zIndex: '@'
            },
            templateUrl: 'app/widget/ppts-daterangepicker/daterangepicker.tpl.html',
            controller: function ($scope) {
                if ($scope.zIndex) {
                    $scope.dateStyle = { 'z-index': $scope.zIndex };
                }
            },
            link: function ($scope, $elem) {
                var $this = $elem.find('.input-daterange');

                $scope.size = $scope.size || 'lg';

                $this.datepicker({
                    autoclose: true,
                    todayHighlight: true,
                    format: ppts.config.datePickerFormat,
                    language: ppts.config.datePickerLang
                }).find('.date-picker').next().on('click', function () {
                    $(this).prev().focus();
                });
            }
        }
    });

})();