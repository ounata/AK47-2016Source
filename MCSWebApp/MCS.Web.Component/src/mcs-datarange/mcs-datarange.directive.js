(function () {
    'use strict';

    mcs.ng.directive('mcsDatarange', ['mcsValidationService', function (validationService) {
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
                disabled: '@',
                unit: '@'
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/datarange.tpl.html',
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
                $scope.disabled = mcs.util.bool($scope.disabled || false);
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

                inputs.each(function () {
                    var $this = $(this);
                    $this.next().on('click', function () {
                        $scope.$apply(function () {
                            $scope[$this.attr('ng-model')] = null;
                        });
                    });
                });

                $scope.$watch('min', function (data) {
                    if (data == undefined || data == '') return;
                    var max = parseFloat($scope.max);
                    if (data > max) {
                        $scope.min = max;
                    }
                    validationService.validate(inputs, $scope);
                });
                $scope.$watch('max', function (data) {
                    if (data == undefined || data == '') return;
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