(function () {
    'use strict';

    angular.module('app.widget').directive('pptsDatetimepicker', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                model: '=',
                placeholder: '@',
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
                $scope.placeholder = $scope.placeholder || '输入时间';
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.find('input[type=text]').attr('required', 'required');

                    mcs.util.appendMessage($elem);
                }

                $this.datetimepicker({
                    format: ppts.config.datetimePickerFormat,
                    language: ppts.config.datePickerLang,
                    autoclose: true,
                }).on("hide", function () {
                    var _this = this;
                    $scope.$apply(function () {
                        $scope[$this.attr('ng-model')] = _this.value;
                    });
                    validationService.validate($elem.find('input[required]'), $scope);
                }).next().on('click', function () {
                    $(this).prev().focus();
                });
            }
        }
    }]);

})();