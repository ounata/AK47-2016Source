(function () {
    'use strict';
    mcs.ng.directive('parseInputValue', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function ($scope, $elem, $attrs, $ctrl) {
                $ctrl.$parsers.push(function (data) {
                    if ($attrs.datatype) {
                        var dataType = $attrs.datatype.toLowerCase();
                        switch (dataType) {
                            case 'int':
                                return parseInt(data);
                            case 'number':
                                return parseFloat(data);
                            default:
                                return data;
                        }
                    } else {
                        return data;
                    }
                });
            }
        };
    });

    mcs.ng.directive('mcsInput', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                id: '@',
                type: '@',
                placeholder: '@',
                readonly: '@',
                css: '@',
                customStyle: '@',
                datatype: '@', //int, number, string
                model: '='
            },
            template: '<input placeholder="{{placeholder}}" class="mcs-default-size-input mcs-margin-right-20 {{css}}" popover-trigger="mouseenter" ng-model="model" style="{{customStyle}}" parse-input-value/>',
            link: function ($scope, $elem, $attrs) {
                var dataType = ($scope.datatype || 'string').toLowerCase();
                if ($scope.id) {
                    $elem.attr('id', $scope.id);
                }
                if (dataType == 'int') {
                    $elem.bind('keyup afterpaste', function () {
                        mcs.util.limit($elem);
                    });
                } else if (dataType == 'number') {
                    $elem.bind('keyup afterpaste', function () {
                        mcs.util.number($elem);
                    });
                }
                $elem.attr('type', $scope.type || 'text');
                var readonly = mcs.util.bool($scope.readonly);
                if (readonly) {
                    //$elem.attr('readonly', 'readonly');
                    $elem.attr('disabled', 'disabled');
                }
                $scope.$watch('$parent.maxlength', function () {
                    if ($scope.$parent.maxlength) {
                        $elem.attr('maxlength', $scope.$parent.maxlength);
                    }
                });
                // 执行验证
                $scope.$watch('$parent.required', function () {
                    if ($scope.$parent.required) {
                        $elem.attr('required', 'required');
                    }
                    var events = $elem.data('events');
                    if (!events || !events['blur']) {
                        $elem.blur(function () {
                            validationService.validate($elem, $scope.$parent);
                        });
                    }
                });
            }
        };
    }]);
})();