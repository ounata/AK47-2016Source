(function() {
    'use strict';

    mcs.ng.directive('mcsDatepicker', ['mcsValidationService', function(validationService) {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                model: '=',
                placeholder: '@',
                zIndex: '@',
                startDate: '=?',
                endDate: '=?',
                disabled: '=?' //禁用(默认false,true) 
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/datepicker.tpl.html',
            controller: function($scope) {
                if ($scope.zIndex) {
                    $scope.dateStyle = {
                        'z-index': $scope.zIndex
                    };
                }
            },
            link: function($scope, $elem) {
                var $this = $elem.find('.date-picker');
                $scope.placeholder = $scope.placeholder || '输入日期';
                $scope.disabled = mcs.util.bool($scope.disabled || false);
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.find('input[type=text]').attr('required', 'required');

                    mcs.util.appendMessage($elem);
                }

                $this.datetimepicker({
                        autoclose: true,
                        todayBtn: true,
                        todayHighlight: true,
                        startDate: $scope.startDate,
                        endDate: $scope.endDate,
                        minView: 'month', //选择日期后，不会再跳转去选择时分秒 
                        maxView: 'decade',
                        format: ppts.config.datePickerFormat,
                        language: ppts.config.datePickerLang
                    }) //show datepicker when clicking on the icon
                    .on("hide", function() {
                        var _this = $(this);
                        $scope.$apply(function() {
                            $scope.model = _this.val() == '' ? null : moment(_this.val()).utc()._d;
                        });
                        validationService.validate($elem.find('input[required]'), $scope);
                    }).next().on('click', function() {
                        $scope.$apply(function() {
                            $scope[$this.attr('ng-model')] = null;
                        });
                    }).next().on('click', function() {
                        $this.focus();
                    });
            }
        }
    }]);
})();
