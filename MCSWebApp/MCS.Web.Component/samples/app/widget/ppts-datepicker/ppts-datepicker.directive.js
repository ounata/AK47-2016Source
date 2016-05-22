(function () {
    'use strict';
 
    angular.module('app.widget').directive('pptsDatepicker', function () {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                model: '=',
                zIndex: '@'
            },
            templateUrl: 'app/widget/ppts-datepicker/datepicker.tpl.html',
            controller: function ($scope) {
                if ($scope.zIndex) {
                    $scope.dateStyle = { 'z-index': $scope.zIndex };
                }
            },
            link: function ($scope, $elem) {
                var $this = $elem.find('.date-picker');

                $this.datepicker({
                    autoclose: true,
                    todayHighlight: true,
                    format: ppts.config.datePickerFormat,
                    language: ppts.config.datePickerLang
                }) //show datepicker when clicking on the icon
                .next().on('click', function () {
                    $(this).prev().focus();
                });
            }
        }
    });

})();