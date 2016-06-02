(function () {
    'use strict';

    angular.module('app.widget').directive('pptsDaterangepicker', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                width: '@',
                css: '@',
                size: '@',
                startDate: '=',
                endDate: '=',
                startText: '@',
                endText: '@',
                zIndex: '@'
            },
            templateUrl: 'app/widget/ppts-daterangepicker/daterangepicker.tpl.html',
            controller: function ($scope) {
                $scope.dateStyle = {};

                if ($scope.zIndex) {
                    $scope.dateStyle['z-index'] = $scope.zIndex;
                }
                if ($scope.width) {
                    $scope.dateStyle['width'] = $scope.width;
                }
            },
            link: function ($scope, $elem) {
                var $this = $elem.find('.input-daterange');

                $scope.startText = $scope.startText || '开始时间';
                $scope.endText = $scope.endText || '结束时间';
                $scope.size = $scope.size || 'lg';
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.find('input[type=text]').attr('required', 'required');

                    mcs.util.appendMessage($elem);
                }

                $this.datepicker({
                    autoclose: true,
                    todayHighlight: true,
                    format: ppts.config.datePickerFormat,
                    language: ppts.config.datePickerLang
                }).on('hide', function () {
                    validationService.validate($elem.find('input[required]'), $scope);
                }).find('.date-picker').next().on('click', function () {
                    $(this).prev().focus();
                });
            }
        }
    }]);

})();