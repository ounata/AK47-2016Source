(function () {
    'use strict';
 
    angular.module('app.widget').directive('pptsDatepicker', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                model: '=',
                placeholder: '@',
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
                $scope.placeholder = $scope.placeholder || '输入日期';
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
                }) //show datepicker when clicking on the icon
                .on("hide", function () {
                    validationService.validate($elem.find('input[required]'), $scope);
                }).next().on('click', function () {
                    $(this).prev().focus();
                });
            }
        }
    }]);

})();