(function () {
    'use strict';

    angular.module('app.widget').directive('pptsDatarange', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                width: '@',
                min: '=',
                minText: '@',
                max: '=',
                maxText: '@',
                datatype: '@',
                unit: '@'
            },
            templateUrl: 'app/widget/ppts-datarange/datarange.tpl.html',
            controller: function ($scope) {
                $scope.customStyle = {};

                if ($scope.width) {
                    $scope.customStyle['width'] = $scope.width;
                }
            },
            link: function ($scope, $elem) {
                $scope.minText = $scope.minText || '起始金额';
                $scope.maxText = $scope.maxText || '截止金额';
                $scope.unit = $scope.unit || '元';
                var dataType = ($scope.datatype || 'number').toLowerCase();
                var inputs = $elem.find('input[type=text]');
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    inputs.attr('required', 'required');

                    mcs.util.appendMessage($elem);
                }

                if (dataType == 'int') {
                    inputs.bind('keyup afterpaste', function () {
                        var $this = $(this);
                        mcs.util.limit($this);
                    });
                } else if (dataType == 'number') {
                    inputs.bind('keyup afterpaste', function () {
                        var $this = $(this);
                        mcs.util.number($this);
                    });
                }

                $scope.$watch('min', function (data) {
                    if (data == undefined) return;
                    var max = parseFloat($scope.max);
                    if (data > max) {
                        $scope.min = max;
                    }
                    validationService.validate(inputs, $scope);
                });
                $scope.$watch('max', function (data) {
                    if (data == undefined) return;
                    var min = parseFloat($scope.min);
                    if (data < min) {
                        $scope.max = min;
                    }
                    validationService.validate(inputs, $scope);
                });
            }
        }
    }]);

})();